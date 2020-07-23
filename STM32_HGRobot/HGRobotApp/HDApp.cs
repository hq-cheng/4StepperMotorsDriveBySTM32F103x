using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using System.Runtime.InteropServices;
using NVRCsharpDemo;
using System.Configuration;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace HGRobotApp
{
    public partial class HDApp : Form
    {
        /* 电机相关的变量 */
        Thread threadClient = null;
        Socket socketClient = null;
        private bool m_bConnNet = false; // 用于判断TCP_Client网络连接按钮是否被按下
        private StringBuilder recMsg_sb = new StringBuilder(); // 为了将byte[]类型转换为string类型

        /* 相机相关的变量 */
        private bool m_bInitSDK = false; // NET_DVR_Init()返回值，TRUE 成功，FALSE 失败。
        private Int32 m_lUserID = -1; // NET_DVR_Login_V30()返回的用户ID值，-1表示失败
        private uint iLastErr = 0; // 错误代码，0 没有错误
        private string iLastErr_str; // 存储错误代码，用于显示
        private bool m_bRecord = false; // 用于判断客户端是否开始路线

        private Int32 m_lRealHandle = -1; // NET_DVR_RealPlay_V40()返回值，-1 表示失败
        private CHCNetSDK.REALDATACALLBACK RealData = null;

        private Int32 m_lFindHandle = -1; // NET_DVR_FindFile_V40()返回值，-1 表示失败
        private Int32 m_lPlayHandle = -1; // NET_DVR_PlayBackByTime_V40()返回值，-1 表示失败
        private bool m_bPause = false; // 暂停/播放按钮标志位 
        private bool m_bReverse = false; // 倒放按钮标志位
        private string sPlayBackFileName = null; // 存储ListView控件中，需要回放和下载的文件名称
        private Int32 m_lDownHandle = -1; // NET_DVR_GetFileByTime_V40()返回值，-1 表示失败

        private DateTime current_time = new DateTime(); // 用于存储图片、视频等数据时的命名

        /* 设备网络 SDK 函数的一些参数 */
        public CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo; // NET_DVR_Login_V30() 引用参数 [输出]
        public CHCNetSDK.NET_DVR_RECORD_V30 lpInBuffer; // NET_DVR_SetDVRConfig() 参数


        /*
         * 主窗体构造函数
         */
        public HDApp()
        {
            InitializeComponent();
            /* 初始化SDK */
            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            if ( m_bInitSDK == false )
            {
                MessageBox.Show("相机依赖程序 CHCNetSDK 初始化错误!");
                return;
            }
            else
            {
                // 保存SDK日志 
                CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\HC_SdkLog\\", true);
            }
            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //

        }

        /*
         * 主窗体加载函数
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            // 相机
            comboBoxSpeed.SelectedIndex = 3;
            if (m_lRealHandle > -1)
            {
                checkBoxPreview.Checked = true;
            }
            else
            {
                checkBoxPreview.Checked = false;
            }

            //初始化时间
            dateTimeStart.Text = DateTime.Now.ToShortDateString();
            dateTimeEnd.Text = DateTime.Now.ToString();
        }

        /****************************************电机******************************************************/

        /*
         * 与服务器建立网络连接
         */
        private void BtnNet_Click(object sender, EventArgs e)
        {
            if ( m_bConnNet == false )
            {   // 之前未按下按钮，现在要连接
                IPAddress address = IPAddress.Parse(textBoxIPAddr.Text.Trim()); // 获取textBoxIPAddr控件中的IP地址
                int portNum = int.Parse(textBoxPortNum.Text.Trim()); // 获取textBoxPortNum控件中的端口号
                IPEndPoint endPoint = new IPEndPoint(address, portNum); // 建立一个网络端点，并且用IP地址和端口号进行初始化
                socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    socketClient.Connect(endPoint); // 由指定服务器IP地址与端口号，尝试连接服务器
                    // 其实可以显示本地网络信息

                }
                catch (SocketException se)
                {
                    MessageBox.Show("与服务器连接失败!" + se.Message);
                    return;
                }

                MessageBox.Show("与服务器连接成功！");

                // 连接成功后建立客户端处理线程并启动它
                threadClient = new Thread(RecMsg); // RecMsg()函数用于处理tcp接收的数据。
                threadClient.IsBackground = true;  // 设置为后台线程
                threadClient.Start(socketClient);  // 启动线程threadClient

                btnNet.BackColor = Color.Red;
                btnNet.Text = "断开服务器";
                m_bConnNet = true;
                textBoxIPAddr.Enabled = false; // 连接状态下不能再输入IP地址和端口号
                textBoxPortNum.Enabled = false;
            } else 
            {   // 之前已按下按钮，现在要断开
                try
                {
                    threadClient.Abort();
                    socketClient.Close();
                } catch (SocketException se)
                {
                    MessageBox.Show("与服务器断开出错!\n" + se.Message);
                    return;
                }
                MessageBox.Show("与服务器成功断开");
                btnNet.BackColor = Color.Green;
                btnNet.Text = "连接服务器";
                m_bConnNet = false;
                textBoxIPAddr.Enabled = true; // 连接状态下不能再输入IP地址和端口号
                textBoxPortNum.Enabled = true;
            }          
        }
        /*
         * 处理tcp接收的数据
         */
        void RecMsg(object sockConnectionparn)
        {
            Socket sockConnection = sockConnectionparn as Socket;
            EndPoint remoteEndPoint = new IPEndPoint(0, 0); ;
            byte[] buffMsgRec = new byte[1024 * 1024 * 10]; // 这个 buffMsgRec 初始大概10MB大的数据量
            int length = -1;
            while(true)
            {
                try
                {
                    length = sockConnection.Receive(buffMsgRec);    // 	从绑定的 Socket 套接字（服务器）接收数据，将数据存入接收缓冲区buffMsgRec。
                } catch (SocketException se)
                {
                    new Thread(() => {
                        this.Invoke(new Action(() =>{
                            MessageBox.Show("接收数据出错！" + se.Message);
                        }));   
                        }).Start();
                    return;
                } catch (ThreadAbortException te)
                {
                    new Thread(() => {
                        this.Invoke(new Action(() => {
                            MessageBox.Show("接收数据出错！" + te.Message);
                        }));
                    }).Start();
                    return;
                } catch (Exception e)
                {
                    new Thread(() => {
                        this.Invoke(new Action(() => {
                            MessageBox.Show("接收数据出错！" + e.Message);
                        }));
                    }).Start();
                    return;
                }

                recMsg_sb.Clear(); // 为了防止出错，先清空StringBuilder类型变量 recMsg_sb
                foreach(byte b in buffMsgRec)
                {
                    recMsg_sb.Append(b.ToString());
                }
                try
                {
                    new Thread(() => {
                        this.Invoke(new Action(() => {
                            // 显示接收到的数据
                            //MessageBox.Show("收到来自服务器的数据" + recMsg_sb.ToString());
                            // 这里报错了，不知道，发送暂时没影响
                        }));
                    }).Start();
                } catch
                {
                    MessageBox.Show("同步UI线程出错");
                }

                Thread.Sleep(10);
            }
        }

        /*
         * 处理tcp发送的数据
         */
        void SendMsg(object sockConnectionparn, string sendData)
        {
            Socket sockConnection = sockConnectionparn as Socket;
            if (!sockConnection.IsBound)
            {
                MessageBox.Show("请先建立连接");
                return;
            }

            try
            {
                // 字符串发送
                byte[] arrData = System.Text.Encoding.Default.GetBytes(sendData);
                sockConnection.Send(arrData);
                

                // 十六进制发送
                //try
                //{
                //    sendData.Replace("0x", "");   //去掉0x
                //    sendData.Replace("0X", "");   //去掉0X

                //    string[] strArray = sendData.Split(new char[] { ',', '，', '\r', '\n', ' ', '\t' });
                //    int decNum = 0;
                //    int i = 0;
                //    byte[] sendBuffer = new byte[strArray.Length];  //发送数据缓冲区

                //    foreach (string str in strArray)
                //    {
                //        try
                //        {
                //            decNum = Convert.ToInt16(str, 16);
                //            sendBuffer[i] = Convert.ToByte(decNum);
                //            i++;
                //        }
                //        catch
                //        {
                //            MessageBox.Show("字节越界，请逐个字节输入！", "Error");                          
                //        }
                //    }
                //    sockConnection.Send(sendBuffer);
                //} catch
                //{
                //    MessageBox.Show("当前为16进制发送模式，请输入16进制数据");
                //}

            } catch
            {
                MessageBox.Show("发送数据出错，请重新发送！");
                return;
            }
        }

        /*
         * 为了使用按键控制方向，重写 ProcessDialogKey 处理系统按键的方式
         */
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
            {
                return false; // 遇到方向键（属于系统按键），则直接返回，系统不处理；
            }
            else
                return base.ProcessDialogKey(keyData);
        }

        /*
         * 窗体内按下方向键，控制滑轨机器人移动
         */
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SendMsg(socketClient, "l");
            }
            else if (e.KeyCode == Keys.Down)
            {
                SendMsg(socketClient, "r");
            }
            else if (e.KeyCode == Keys.Left)
            {
                SendMsg(socketClient, "u");
            }
            else if (e.KeyCode == Keys.Right)
            {
                SendMsg(socketClient, "d");
            }
        }

        /*
         * 松开方向控制按键
         */
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SendMsg(socketClient, "i");
            }
            else if (e.KeyCode == Keys.Down)
            {
                SendMsg(socketClient, "j");
            }
            else if (e.KeyCode == Keys.Left)
            {
                SendMsg(socketClient, "k");
            }
            else if (e.KeyCode == Keys.Right)
            {
                SendMsg(socketClient, "h");
            }
        }

        /*
         * 键盘/鼠标按住按钮，滑轨机器人上行
         */
        private void Up_button_MouseDown(object sender, MouseEventArgs e)
        {   // 按住
            SendMsg(socketClient, "u");
        }

        private void Up_button_MouseUp(object sender, MouseEventArgs e)
        {   // 松开
            SendMsg(socketClient, "k");
        }


        /*
         * 鼠标按住按钮，滑轨机器人下行
         */
        private void Down_button_MouseDown(object sender, MouseEventArgs e)
        {   // 按住
            SendMsg(socketClient, "d");
        }

        private void Down_button_MouseUp(object sender, MouseEventArgs e)
        {   // 松开
            SendMsg(socketClient, "h");
        }

        /*
         * 键盘/鼠标按住按钮，滑轨机器人左行
         */
        private void Left_button_MouseDown(object sender, MouseEventArgs e)
        {   // 按住
            SendMsg(socketClient, "l");
        }

        private void Left_button_MouseUp(object sender, MouseEventArgs e)
        {   // 松开
            SendMsg(socketClient, "i");
        }

        /*
         * 键盘/鼠标按住按钮，滑轨机器人右行
         */
        private void Right_button_MouseDown(object sender, MouseEventArgs e)
        {   // 按住
            SendMsg(socketClient, "r");
        }

        private void Right_button_MouseUp(object sender, MouseEventArgs e)
        {   // 松开
            SendMsg(socketClient, "j");
        }

/****************************************电机******************************************************/


/****************************************相机******************************************************/
        
        /*
         * Login按钮，注册相机设备
         */
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (textBoxIP.Text == "" || textBoxPort.Text == "" ||
                textBoxUserName.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show("请输入IP地址、端口号，用户名和密码");
                return;
            }

            if (m_lUserID < 0)
            {   // 之前未登陆过，现在进行登陆操作
                string DVRIPAddress = textBoxIP.Text; //设备IP地址，或者设备域名
                Int16 DVRPortNumber = Int16.Parse(textBoxPort.Text);
                string DVRUserName = textBoxUserName.Text;
                string DVRPassword = textBoxPassword.Text;

                // 选择 HiDDNS域名登录
                if ( checkBoxHiDDNS.Checked )
                {
                    byte[] HiDDNSName = System.Text.Encoding.Default.GetBytes(textBoxIP.Text); // 设备名称（一组字符）转换为字节序列
                    byte[] GetIPAddress = new byte[16];
                    uint dwPort = 0;
                    // IPServer 或者DDNS 域名解析，获取动态IP 地址和端口号
                    if ( !CHCNetSDK.NET_DVR_GetDVRIPByResolveSvr_EX("www.hik-online.com", (ushort)80, HiDDNSName, 
                                                                    (ushort)HiDDNSName.Length, null, 0, GetIPAddress, ref dwPort) )
                    { // 失败
                        iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                        iLastErr_str = "域名解析失败，错误码为：" + iLastErr;
                        MessageBox.Show(iLastErr_str);
                        return;
                    } else
                    {
                        // 将的设备IP地址与端口号（从解析服务器"www.hik-online.com"获得）用于设备登陆
                        DVRIPAddress = System.Text.Encoding.UTF8.GetString(GetIPAddress).TrimEnd('\0');
                        DVRPortNumber = (Int16)dwPort;
                    }
                }

                // 登陆设备
                m_lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
                if ( m_lUserID < 0 )
                {   // 登陆失败，返回最后操作的错误码 NET_DVR_GetLastError
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    iLastErr_str = "登陆失败，错误码为：" + iLastErr;
                    MessageBox.Show(iLastErr_str);
                    return;
                } else
                {

                    // 登陆成功，这里先对 通道1 的一些录像参数进行设置
                    lpInBuffer.dwSize = (uint)Marshal.SizeOf(lpInBuffer);
                    lpInBuffer.struRecAllDay = new CHCNetSDK.NET_DVR_RECORDDAY[7];
                    lpInBuffer.dwRecord = 1; // 是否录像 0-否 1-是
                    lpInBuffer.struRecAllDay[0].wAllDayRecord = 1; // 是否全天录像 0-否 1-是
                    lpInBuffer.struRecAllDay[0].byRecordType = 0; // 定时录像
                    lpInBuffer.struRecAllDay[1].wAllDayRecord = 1; // 是否全天录像 0-否 1-是
                    lpInBuffer.struRecAllDay[1].byRecordType = 0; // 定时录像
                    lpInBuffer.struRecAllDay[2].wAllDayRecord = 1; // 是否全天录像 0-否 1-是
                    lpInBuffer.struRecAllDay[2].byRecordType = 0; // 定时录像
                    lpInBuffer.struRecAllDay[3].wAllDayRecord = 1; // 是否全天录像 0-否 1-是
                    lpInBuffer.struRecAllDay[3].byRecordType = 0; // 定时录像
                    lpInBuffer.struRecAllDay[4].wAllDayRecord = 1; // 是否全天录像 0-否 1-是
                    lpInBuffer.struRecAllDay[4].byRecordType = 0; // 定时录像
                    lpInBuffer.struRecAllDay[5].wAllDayRecord = 1; // 是否全天录像 0-否 1-是
                    lpInBuffer.struRecAllDay[5].byRecordType = 0; // 定时录像
                    lpInBuffer.struRecAllDay[6].wAllDayRecord = 1; // 是否全天录像 0-否 1-是
                    lpInBuffer.struRecAllDay[6].byRecordType = 0; // 定时录像
                    Int32 nSize = (Int32)Marshal.SizeOf(lpInBuffer);
                    IntPtr lpInBuffer_ptr = Marshal.AllocHGlobal(nSize);
                    Marshal.StructureToPtr(lpInBuffer, lpInBuffer_ptr, false); // 将结构体变量lpInBuffer托管到lpInBuffer_ptr指向的内存

                    if (!CHCNetSDK.NET_DVR_SetDVRConfig(m_lUserID, CHCNetSDK.NET_DVR_SET_RECORDCFG_V30, 
                                                        1, lpInBuffer_ptr, (UInt32)nSize))
                    {
                        iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                        iLastErr_str = "录像参数初始化失败，错误码为：" + iLastErr;
                        MessageBox.Show(iLastErr_str);
                        return;
                    }
                    Marshal.FreeHGlobal(lpInBuffer_ptr); // 释放以前从进程的非托管内存中分配的内存。

                    MessageBox.Show("登陆成功"); 
                    btnLogin.Text = "Logout";
                    // 保存日志
                    CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\HC_SdkLog\\", true);
                }
            } else
            {   // 之前已经登陆过，现在要注销登陆
                if ( m_lRealHandle >= 0 )
                {
                    MessageBox.Show("实时预览中，请先关闭再注销！");
                    return;
                } 

                if ( !CHCNetSDK.NET_DVR_Logout(m_lUserID) )
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError(); 
                    iLastErr_str = "注销失败，错误码为：" + iLastErr;
                    MessageBox.Show(iLastErr_str);
                    return;
                }
                m_lUserID = -1;
                btnLogin.Text = "Login";
            }
            return;
        }

        /*
         * 实时预览
         */
        private void BtnPreview_Click(object sender, EventArgs e)
        {
            if ( m_lUserID < 0 )
            {
                MessageBox.Show("请先登陆相机设备！");
                return;
            }

            // 实时预览与回放共用一个播放窗口，不能冲突
            if ( m_lPlayHandle >= 0 )
            {
                MessageBox.Show("请先关闭回放功能！");
                return;
            }

            if ( m_lRealHandle < 0 )
            {   // 之前处于停止预览状态，现在要打开实时预览
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = RealPlayWnd.Handle; // 获取pictureBox控件 RealPlayWnd的窗口句柄
                lpPreviewInfo.lChannel = 1; // 先给 1，调试时可改，说实话，不知道如何确定通道号
                lpPreviewInfo.dwStreamType = 0; // 码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = 0; // 连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true;  // 0- 非阻塞取流，1- 阻塞取流（C#中为bool类型）
                lpPreviewInfo.dwDisplayBufNum = 1; // 播放库显示缓冲区最大帧数
                lpPreviewInfo.byProtoType = 0;
                lpPreviewInfo.byPreviewMode = 0;
                IntPtr pUser = IntPtr.Zero; // 存储用户数据

                // 开启预览
                m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, RealData, pUser);
                if ( m_lRealHandle < 0 )
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    iLastErr_str = "预览失败，错误码为：" + iLastErr;
                    MessageBox.Show(iLastErr_str);
                    return;
                } else
                {   // 预览成功
                    btnPreview.Text = "Stop View";

                    // 保存日志
                    CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\HC_SdkLog\\", true);
                }
            } else
            {   // 之前处于实时预览，现在要关闭预览状态
                if ( !CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle) )
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    iLastErr_str = "停止预览出错，错误码为：" + iLastErr;
                    MessageBox.Show(iLastErr_str);
                    return;
                }

                m_lRealHandle = -1;
                btnPreview.Text = "Live View";
                RealPlayWnd.Invalidate(); // 刷新一下视频窗口 RealPlayWnd（pictureBox控件）
            }
            return;
        }

        /*
         * 抓图功能，格式JPEG
         */
        private void BtnJPEG_Click(object sender, EventArgs e)
        {
            current_time = System.DateTime.Now;
            string sJpegPicFileName;
            sJpegPicFileName = "HCVideo_CaptureJpeg_" + current_time.ToString("yyyyMMdd_HHmmss") + ".jpg";
            int lChannel = 1; // 通道号，先给1吧，一个相机
            CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
            lpJpegPara.wPicQuality = 0; // 最好，图像质量 Image quality
            lpJpegPara.wPicSize = 0xff; // 抓图分辨率 Picture size: 2 - 4CIF，0xff - Auto(使用当前码流分辨率)

            if ( !CHCNetSDK.NET_DVR_CaptureJPEGPicture(m_lUserID, lChannel, 
                                                        ref lpJpegPara, sJpegPicFileName) )
            {   // 失败
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "捕获JPEG图片失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            } else
            {
                MessageBox.Show("成功保存图片：" + sJpegPicFileName);

                // 保存日志
                CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\HC_SdkLog\\", true);
            }
            return;
        }

        /*
         * 客户端录像，存储文件到本地PC电脑
         */
        private void BtnRecord_Click(object sender, EventArgs e)
        {
            if ( m_lRealHandle < 0 )
            {
                MessageBox.Show("未处于实时预览模式，不能录像！");
                return;
            }

            current_time = System.DateTime.Now;
            string sVideoFileName;
            sVideoFileName = "HCVideo_Record_" + current_time.ToString("yyyyMMdd_HHmmss") + ".mp4";

            if ( m_bRecord == false )
            {   
                // 强制I帧，Make a I frame
                int lChannel = 1; // 通道号
                CHCNetSDK.NET_DVR_MakeKeyFrame(m_lUserID, lChannel);
                if ( !CHCNetSDK.NET_DVR_SaveRealData(m_lRealHandle, sVideoFileName) )
                {   // 失败
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    iLastErr_str = "开启录像失败，错误码为：" + iLastErr;
                    MessageBox.Show(iLastErr_str);
                    return;
                } else
                {
                    btnRecord.Text = "Stop";
                    m_bRecord = true;

                    // 保存日志
                    CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\HC_SdkLog\\", true);
                }
            } else
            {
                // 停止录像
                if ( !CHCNetSDK.NET_DVR_StopSaveRealData(m_lRealHandle) )
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    iLastErr_str = "停止录像失败，错误码为：" + iLastErr;
                    MessageBox.Show(iLastErr_str);
                    return;
                } else
                {
                    MessageBox.Show("保存录像成功 " + sVideoFileName);
                    btnRecord.Text = "Record";
                    m_bRecord = false;
                }
            }

        }
        
        /*
         * 退出App按钮，包括：关闭预览、注销用户、释放SDK资源
         */
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            // 关闭预览
            if ( m_lRealHandle >= 0 )
            {
                CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);
                m_lRealHandle = -1;
            }

            //停止回放 Stop playback
            if (m_lPlayHandle >= 0)
            {
                CHCNetSDK.NET_DVR_StopPlayBack(m_lPlayHandle);
                m_lPlayHandle = -1;
            }

                // 注销用户
                if (m_lUserID >= 0)
            {
                CHCNetSDK.NET_DVR_Logout(m_lUserID);
                m_lUserID = -1;
            }

            // 释放SDK资源
            if (m_bInitSDK == true)
            {
                CHCNetSDK.NET_DVR_Cleanup();
            }
            

            // 退出程序
            Application.Exit(); // Application 提供 static 方法和属性以管理应用程序，例如启动和停止应用程序
        }

        /**********************************云台控制（八个方位 + Auto模式）***********************************/
        private void BtnUp_MouseUp(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_UP, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.TILT_UP, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnUp_MouseDown(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_UP, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.TILT_UP, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnRight_MouseUp(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_RIGHT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.PAN_RIGHT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnRight_MouseDown(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_RIGHT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.PAN_RIGHT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnDown_MouseUp(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_DOWN, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.TILT_DOWN, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnDown_MouseDown(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_DOWN, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.TILT_DOWN, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_LEFT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.PAN_LEFT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_LEFT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.PAN_LEFT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnUpLeft_MouseUp(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.UP_LEFT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.UP_LEFT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnUpLeft_MouseDown(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.UP_LEFT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.UP_LEFT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnUpRight_MouseUp(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.UP_RIGHT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.UP_RIGHT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnUpRight_MouseDown(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.UP_RIGHT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.UP_RIGHT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnDownLeft_MouseUp(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.DOWN_LEFT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.DOWN_LEFT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnDownLeft_MouseDown(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.DOWN_LEFT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.DOWN_LEFT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnDownRight_MouseUp(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.DOWN_RIGHT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.DOWN_RIGHT, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnDownRight_MouseDown(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.DOWN_RIGHT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.DOWN_RIGHT, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnAuto_MouseUp(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_AUTO, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.PAN_AUTO, 1, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        private void BtnAuto_MouseDown(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_AUTO, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed_Other(m_lUserID, lChannel, CHCNetSDK.PAN_AUTO, 0, (uint)comboBoxSpeed.SelectedIndex + 1);
            }
        }

        /**********************************云台控制（八个方位 + Auto模式）***********************************/

        /**********************************云台控制（变倍，变焦，调节光圈）***********************************/
        private void BtnZoomUp_MouseUp(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.ZOOM_IN, 1); 
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControl_Other(m_lUserID, lChannel, CHCNetSDK.ZOOM_IN, 1);
            }
        }

        private void BtnZoomUp_MouseDown(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.ZOOM_IN, 0);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControl_Other(m_lUserID, lChannel, CHCNetSDK.ZOOM_IN, 0);
            }
        }

        private void BtnZoomDown_MouseUp(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.ZOOM_OUT, 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControl_Other(m_lUserID, lChannel, CHCNetSDK.ZOOM_OUT, 1);
            }
        }

        private void BtnZoomDown_MouseDown(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.ZOOM_OUT, 0);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControl_Other(m_lUserID, lChannel, CHCNetSDK.ZOOM_OUT, 0);
            }
        }

        private void BtnFocusUp_MouseUp(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.FOCUS_NEAR, 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControl_Other(m_lUserID, lChannel, CHCNetSDK.FOCUS_NEAR, 1);
            }
        }

        private void BtnFocusUp_MouseDown(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.FOCUS_NEAR, 0);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControl_Other(m_lUserID, lChannel, CHCNetSDK.FOCUS_NEAR, 0);
            }
        }

        private void BtnFocusDown_MouseUp(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.FOCUS_FAR, 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControl_Other(m_lUserID, lChannel, CHCNetSDK.FOCUS_FAR, 1);
            }
        }

        private void BtnFocusDown_MouseDown(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.FOCUS_FAR, 0);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControl_Other(m_lUserID, lChannel, CHCNetSDK.FOCUS_FAR, 0);
            }
        }

        private void BtnIrisUp_MouseUp(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.IRIS_OPEN, 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControl_Other(m_lUserID, lChannel, CHCNetSDK.IRIS_OPEN, 1);
            }
        }

        private void BtnIrisUp_MouseDown(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.IRIS_OPEN, 0);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControl_Other(m_lUserID, lChannel, CHCNetSDK.IRIS_OPEN, 0);
            }
        }

        private void BtnIrisDown_MouseUp(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.IRIS_CLOSE, 1);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControl_Other(m_lUserID, lChannel, CHCNetSDK.IRIS_CLOSE, 1);
            }
        }

        private void BtnIrisDown_MouseDown(object sender, MouseEventArgs e)
        {
            int lChannel = 1; // 通道号
            if (checkBoxPreview.Checked)
            {
                CHCNetSDK.NET_DVR_PTZControl(m_lRealHandle, CHCNetSDK.IRIS_CLOSE, 0);
            }
            else
            {
                CHCNetSDK.NET_DVR_PTZControl_Other(m_lUserID, lChannel, CHCNetSDK.IRIS_CLOSE, 0);
            }
        }
        
        /**********************************云台控制（变倍，变焦，调节光圈）***********************************/
        
        /*
         * 查找录像文件
         */
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            listViewFile.Items.Clear(); // 清空文件列表

            CHCNetSDK.NET_DVR_FILECOND_V40 struFileCond_V40 = new CHCNetSDK.NET_DVR_FILECOND_V40();
            struFileCond_V40.lChannel = 1; // 通道号
            struFileCond_V40.dwIsLocked = 0xff; // 0-未锁定文件，1-锁定文件，0xff表示所有文件（包括锁定和未锁定）
            struFileCond_V40.dwFileType = 0xff; // 0xff-全部，0-定时录像，1-移动侦测，2-报警触发，...
            // 设置录像查找的开始时间
            struFileCond_V40.struStartTime.dwYear = (uint)dateTimeStart.Value.Year;
            struFileCond_V40.struStartTime.dwMonth = (uint)dateTimeStart.Value.Month;
            struFileCond_V40.struStartTime.dwDay = (uint)dateTimeStart.Value.Day;
            struFileCond_V40.struStartTime.dwHour = (uint)dateTimeStart.Value.Hour;
            struFileCond_V40.struStartTime.dwMinute = (uint)dateTimeStart.Value.Minute;
            struFileCond_V40.struStartTime.dwSecond = (uint)dateTimeStart.Value.Second;
            // 设置录像查找的结束时间
            struFileCond_V40.struStopTime.dwYear = (uint)dateTimeEnd.Value.Year;
            struFileCond_V40.struStopTime.dwMonth = (uint)dateTimeEnd.Value.Month;
            struFileCond_V40.struStopTime.dwDay = (uint)dateTimeEnd.Value.Day;
            struFileCond_V40.struStopTime.dwHour = (uint)dateTimeEnd.Value.Hour;
            struFileCond_V40.struStopTime.dwMinute = (uint)dateTimeEnd.Value.Minute;
            struFileCond_V40.struStopTime.dwSecond = (uint)dateTimeEnd.Value.Second;

            // 开始查找录像文件
            m_lFindHandle = CHCNetSDK.NET_DVR_FindFile_V40(m_lUserID, ref struFileCond_V40);
            if ( m_lFindHandle < 0 )
            {   // 失败
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "查找录像文件失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            } else
            {
                CHCNetSDK.NET_DVR_FINDDATA_V30 struFileData = new CHCNetSDK.NET_DVR_FINDDATA_V30();
                while (true)
                {
                    // 逐个获取查找到的文件信息（信息存储到结构体参数struFileData中）
                    int result = CHCNetSDK.NET_DVR_FindNextFile_V30(m_lFindHandle, ref struFileData); // 用于获取一条已查找到的文件信息
                    if (result == CHCNetSDK.NET_DVR_ISFINDING )
                    {
                        continue;
                    } else if (result == CHCNetSDK.NET_DVR_FILE_SUCCESS )
                    {
                        string item1 = struFileData.sFileName;
                        string item2 = Convert.ToString(struFileData.struStartTime.dwYear) + " - " +
                                        Convert.ToString(struFileData.struStartTime.dwMonth) + "-" +
                                        Convert.ToString(struFileData.struStartTime.dwDay) + " " +
                                        Convert.ToString(struFileData.struStartTime.dwHour) + ":" +
                                        Convert.ToString(struFileData.struStartTime.dwMinute) + ":" +
                                        Convert.ToString(struFileData.struStartTime.dwSecond);
                        string item3 = Convert.ToString(struFileData.struStopTime.dwYear) + " - " +
                                        Convert.ToString(struFileData.struStopTime.dwMonth) + "-" +
                                        Convert.ToString(struFileData.struStopTime.dwDay) + " " +
                                        Convert.ToString(struFileData.struStopTime.dwHour) + ":" +
                                        Convert.ToString(struFileData.struStopTime.dwMinute) + ":" +
                                        Convert.ToString(struFileData.struStopTime.dwSecond);

                        listViewFile.Items.Add(new ListViewItem(new string[] { item1, item2, item3}));
                        // break; // 这里 break，只能找到了一条文件信息
                    } else if (result == CHCNetSDK.NET_DVR_FILE_NOFIND ||
                                result == CHCNetSDK.NET_DVR_NOMOREFILE )
                    {
                        MessageBox.Show("未查找到文件，或者查找结束");
                        break; // 未查找到文件或者查找结束，退出
                    } else
                    {
                        iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                        iLastErr_str = "获取文件信息失败，错误码为：" + iLastErr;
                        MessageBox.Show(iLastErr_str);
                        break;
                    }
                }
            }
        }

        /*
         * 按时间回放
         */
        private void BtnPlaybackTime_Click(object sender, EventArgs e)
        {
            // 实时预览与回放共用一个播放窗口，不能冲突
            if ( m_lRealHandle >= 0 )
            {
                MessageBox.Show("先停止实时预览，再使用回放功能。");
                return;
            }

            if (m_lPlayHandle >= 0)
            {   // 如果已经在回放，先停止回放
                if ( !CHCNetSDK.NET_DVR_StopPlayBack( m_lPlayHandle ) )
                {   // 失败
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    iLastErr_str = "停止回放录像失败，错误码为：" + iLastErr;
                    MessageBox.Show(iLastErr_str);
                    return;
                }

                m_lPlayHandle = -1;
                PlaybackprogressBar.Value = 0; // 视频进度条恢复为0
                timerPlayback.Enabled = false;
                // 更新视频播放控制相关按钮
                btnPause.Text = "||"; // 暂停按钮由 ">" 恢复为 "||"
                m_bPause = false;
                btnReverse.Text = "Reverse"; // 倒放按钮恢复
                m_bReverse = false;
            }
            CHCNetSDK.NET_DVR_VOD_PARA struVodPara = new CHCNetSDK.NET_DVR_VOD_PARA();
            struVodPara.dwSize = (uint)Marshal.SizeOf(struVodPara);
            struVodPara.struIDInfo.dwChannel = 1; // 通道号
            struVodPara.hWnd = RealPlayWnd.Handle; // 获取播放窗口控件句柄
            //设置回放的开始时间 Set the starting time to search video files
            struVodPara.struBeginTime.dwYear = (uint)dateTimeStart.Value.Year;
            struVodPara.struBeginTime.dwMonth = (uint)dateTimeStart.Value.Month;
            struVodPara.struBeginTime.dwDay = (uint)dateTimeStart.Value.Day;
            struVodPara.struBeginTime.dwHour = (uint)dateTimeStart.Value.Hour;
            struVodPara.struBeginTime.dwMinute = (uint)dateTimeStart.Value.Minute;
            struVodPara.struBeginTime.dwSecond = (uint)dateTimeStart.Value.Second;

            //设置回放的结束时间 Set the stopping time to search video files
            struVodPara.struEndTime.dwYear = (uint)dateTimeEnd.Value.Year;
            struVodPara.struEndTime.dwMonth = (uint)dateTimeEnd.Value.Month;
            struVodPara.struEndTime.dwDay = (uint)dateTimeEnd.Value.Day;
            struVodPara.struEndTime.dwHour = (uint)dateTimeEnd.Value.Hour;
            struVodPara.struEndTime.dwMinute = (uint)dateTimeEnd.Value.Minute;
            struVodPara.struEndTime.dwSecond = (uint)dateTimeEnd.Value.Second;

            m_lPlayHandle = CHCNetSDK.NET_DVR_PlayBackByTime_V40(m_lUserID, ref struVodPara);
            if ( m_lPlayHandle < 0 )
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "回放录像失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            }

            // 保存日志
            CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\HC_SdkLog\\", true);

            // 如果成功开启回放，进入播放控制模式，开始播放
            uint iOutValue = 0; // 参数的长度 [输出]
            if ( !CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYSTART, 
                                                        IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue) )
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "开启回放录像失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            }
            timerPlayback.Interval = 100; // 回放定时器定时间隔 1s
            timerPlayback.Enabled = true; // 开启回放定时器
            btnStopPlayback.Enabled = true; // 使能停止回放按钮
        }

        /*
         * 点击获取ListView控件中，需要回放或下载的文件名
         */
        private void ListViewFile_SelectedIndexChanged(object sender, EventArgs e)
        {   // listViewFile.SelectedItem 发生改变时触发该事件，一定要作这个大于0的判断
            if (listViewFile.SelectedItems.Count > 0)
            {
                sPlayBackFileName = listViewFile.FocusedItem.SubItems[0].Text; // 获取当前控件中具有焦点项的文件名称
            }
        }

        /*
         * 按文件名回放
         */
        private void BtnPlaybackName_Click(object sender, EventArgs e)
        {
            // 实时预览与回放共用一个播放窗口，不能冲突
            if (m_lRealHandle >= 0)
            {
                MessageBox.Show("先停止实时预览，再使用回放功能。");
                return;
            }

            if (sPlayBackFileName == null)
            {
                MessageBox.Show("请选择一个回放的文件!");
                return;
            }

            if (m_lPlayHandle >= 0)
            {
                //如果已经正在回放，先停止回放
                if (!CHCNetSDK.NET_DVR_StopPlayBack(m_lPlayHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    iLastErr_str = "停止回放录像失败，错误码为：" + iLastErr;
                    MessageBox.Show(iLastErr_str);
                    return;
                }

                m_bReverse = false;
                btnReverse.Text = "Reverse";

                m_bPause = false;
                btnPause.Text = "||";

                m_lPlayHandle = -1;
                PlaybackprogressBar.Value = 0;
                timerPlayback.Enabled = false;
            }

            //按文件名回放
            m_lPlayHandle = CHCNetSDK.NET_DVR_PlayBackByName(m_lUserID, sPlayBackFileName, RealPlayWnd.Handle);
            if (m_lPlayHandle < 0)
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "回放录像失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            }

            // 保存日志
            CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\HC_SdkLog\\", true);

            uint iOutValue = 0;
            if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYSTART,
                                                       IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "开启回放录像失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            }
            timerPlayback.Interval = 100;
            timerPlayback.Enabled = true;
            btnStopPlayback.Enabled = true; // 使能停止回放按钮
        }

        /*
         * 回放定时器 Tick 处理程序，主要用于设置 回放进度条
         */
        private void TimerPlayback_Tick(object sender, EventArgs e)
        {
            PlaybackprogressBar.Maximum = 100;
            PlaybackprogressBar.Minimum = 0;
            
            // 获取回放进度
            uint iOutValue = 0; // 输出参数的长度 
            IntPtr lpOutBuffer = Marshal.AllocHGlobal(4); // 指向输出参数的指针
            CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYGETPOS, 
                                                  IntPtr.Zero, 0, lpOutBuffer, ref iOutValue);
            int iPos = 0;
            iPos = (int)Marshal.PtrToStructure(lpOutBuffer, typeof(int));
            if ( iPos > PlaybackprogressBar.Minimum  && iPos < PlaybackprogressBar.Maximum)
            {
                PlaybackprogressBar.Value = iPos; // 将回放进度值赋给进度条
            }

            if ( iPos == 100 )
            {
                PlaybackprogressBar.Value = iPos; // 回放结束，停止回放
                if ( !CHCNetSDK.NET_DVR_StopPlayBack(m_lPlayHandle) )
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    iLastErr_str = "停止回放录像失败，错误码为：" + iLastErr;
                    MessageBox.Show(iLastErr_str);
                    return;
                }
                m_lPlayHandle = -1;
                timerPlayback.Enabled = false;
            }

            if ( iPos == 200 ) 
            {
                MessageBox.Show("网络异常，回放失败");
                timerPlayback.Enabled = false;
            }
            Marshal.FreeHGlobal(lpOutBuffer);
        }

        /*
         * 停止按时间/文件名回放
         */
        private void BtnStopPlayback_Click(object sender, EventArgs e)
        {
            if (m_lPlayHandle < 0)
            {
                return;
            }

            //停止回放
            if (!CHCNetSDK.NET_DVR_StopPlayBack(m_lPlayHandle))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "停止回放录像失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            }

            PlaybackprogressBar.Value = 0;
            timerPlayback.Enabled = false;

            m_bReverse = false;
            btnReverse.Text = "Reverse";

            m_bPause = false;
            btnPause.Text = "||";

            m_lPlayHandle = -1;
            RealPlayWnd.Invalidate();//刷新窗口    
            btnStopPlayback.Enabled = false;
        }

        /*
         * 按时间下载
         */
        private void BtnDownloadTime_Click(object sender, EventArgs e)
        {
            if (m_lDownHandle >= 0)
            {
                MessageBox.Show("正在下载，请先停止下载!");
                return;
            }

            CHCNetSDK.NET_DVR_PLAYCOND struDownPara = new CHCNetSDK.NET_DVR_PLAYCOND();
            struDownPara.dwChannel = 1; //通道号 

            //设置下载的开始时间 Set the starting time
            struDownPara.struStartTime.dwYear = (uint)dateTimeStart.Value.Year;
            struDownPara.struStartTime.dwMonth = (uint)dateTimeStart.Value.Month;
            struDownPara.struStartTime.dwDay = (uint)dateTimeStart.Value.Day;
            struDownPara.struStartTime.dwHour = (uint)dateTimeStart.Value.Hour;
            struDownPara.struStartTime.dwMinute = (uint)dateTimeStart.Value.Minute;
            struDownPara.struStartTime.dwSecond = (uint)dateTimeStart.Value.Second;

            //设置下载的结束时间 Set the stopping time
            struDownPara.struStopTime.dwYear = (uint)dateTimeEnd.Value.Year;
            struDownPara.struStopTime.dwMonth = (uint)dateTimeEnd.Value.Month;
            struDownPara.struStopTime.dwDay = (uint)dateTimeEnd.Value.Day;
            struDownPara.struStopTime.dwHour = (uint)dateTimeEnd.Value.Hour;
            struDownPara.struStopTime.dwMinute = (uint)dateTimeEnd.Value.Minute;
            struDownPara.struStopTime.dwSecond = (uint)dateTimeEnd.Value.Second;

            string sVideoFileName;  //录像文件保存路径和文件名     
            sVideoFileName = "D:\\Downtest_Channel" + struDownPara.dwChannel + ".mp4";

            //按时间下载
            m_lDownHandle = CHCNetSDK.NET_DVR_GetFileByTime_V40(m_lUserID, sVideoFileName, ref struDownPara);
            if (m_lDownHandle < 0)
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "初始化下载录像文件失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            }

            // 保存日志
            CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\HC_SdkLog\\", true);

            uint iOutValue = 0;
            if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lDownHandle, CHCNetSDK.NET_DVR_PLAYSTART, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "开启下载录像文件失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            }

            timerDownload.Interval = 100;
            timerDownload.Enabled = true;
            btnStopDownload.Enabled = true;
        }

        /*
         * 按文件名下载
         */
        private void BtnDownloadName_Click(object sender, EventArgs e)
        {
            if (m_lDownHandle >= 0)
            {
                MessageBox.Show("正在下载，请先停止下载!");
                return;
            }

            string sVideoFileName;  //录像文件保存路径和文件名
            sVideoFileName = "D:\\Downtest_" + sPlayBackFileName + ".mp4";

            //按文件名下载
            m_lDownHandle = CHCNetSDK.NET_DVR_GetFileByName(m_lUserID, sPlayBackFileName, sVideoFileName);
            if (m_lDownHandle < 0)
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "初始化下载失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            }

            // 保存日志
            CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\HC_SdkLog\\", true);

            uint iOutValue = 0;

            // 设置转封装格式 NET_DVR_SET_TRANS_TYPE
            UInt32 iInValue = 5;
            IntPtr lpInValue = Marshal.AllocHGlobal(4);
            Marshal.StructureToPtr(iInValue, lpInValue, false);

            if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lDownHandle, CHCNetSDK.NET_DVR_SET_TRANS_TYPE, 
                                                       lpInValue, 4, IntPtr.Zero, ref iOutValue))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "转封装格式失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            }

            if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lDownHandle, CHCNetSDK.NET_DVR_PLAYSTART, 
                                                       IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "开启下载失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            }
            timerDownload.Interval = 100;
            timerDownload.Enabled = true;
            btnStopDownload.Enabled = true;
        }

        /*
         * 停止按时间/文件名下载
         */
        private void BtnStopDownload_Click(object sender, EventArgs e)
        {
            if (m_lDownHandle < 0)
            {
                return;
            }

            if (!CHCNetSDK.NET_DVR_StopGetFile(m_lDownHandle))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "停止下载失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            }

            timerDownload.Enabled = false;

            MessageBox.Show("成功终止下载该录像!");
            m_lDownHandle = -1;
            DownloadProgressBar.Value = 0;
            btnStopDownload.Enabled = false;
        }

        /*
         * 下载定时器 Tick 处理程序
         */
        private void TimerDownload_Tick(object sender, EventArgs e)
        {
            DownloadProgressBar.Maximum = 100;
            DownloadProgressBar.Minimum = 0;

            int iPos = 0;

            //获取下载进度
            iPos = CHCNetSDK.NET_DVR_GetDownloadPos(m_lDownHandle);

            if ((iPos > DownloadProgressBar.Minimum) && (iPos < DownloadProgressBar.Maximum))
            {
                DownloadProgressBar.Value = iPos;
            }

            if (iPos == 100)  //下载完成
            {
                DownloadProgressBar.Value = iPos;
                if (!CHCNetSDK.NET_DVR_StopGetFile(m_lDownHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    iLastErr_str = "下载录像文件失败，错误码为：" + iLastErr;
                    MessageBox.Show(iLastErr_str);
                    return;
                }
                m_lDownHandle = -1;
                timerDownload.Enabled = false;
                DownloadProgressBar.Value = 0;
                MessageBox.Show("录像下载成功");
                btnStopDownload.Enabled = false;
            }

            if (iPos == 200)
            {
                MessageBox.Show("网络异常，下载失败!");
                timerDownload.Enabled = false;
            }
        }

        /*******************播放控制（暂停/恢复/慢放/快放/单帧/恢复正常速度/倒放）************/
        private void BtnPause_Click(object sender, EventArgs e)
        {
            if ( m_bPause == false )
            {   // 点击暂停
                uint iOutValue = 0; // 参数的长度 [输出]
                if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYPAUSE,
                                                            IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    iLastErr_str = "暂停回放录像失败，错误码为：" + iLastErr;
                    MessageBox.Show(iLastErr_str);
                    return;
                }
                m_bPause = true; // 标志着当前状态为暂停状态
                btnPause.Text = ">";
            } else
            {   // 点击恢复播放
                uint iOutValue = 0; // 参数的长度 [输出]
                if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYRESTART,
                                                            IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    iLastErr_str = "开启回放录像失败，错误码为：" + iLastErr;
                    MessageBox.Show(iLastErr_str);
                    return;
                }
                m_bPause = false;
                btnPause.Text = "||";
            }
            return;
        }

        private void BtnSlow_Click(object sender, EventArgs e)
        {   // 点击慢放
            uint iOutValue = 0; // 参数的长度 [输出]
            if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYSLOW,
                                                        IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "慢放回放录像失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            }
        }

        private void BtnFast_Click(object sender, EventArgs e)
        {   // 点击快放
            uint iOutValue = 0; // 参数的长度 [输出]
            if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYFAST,
                                                        IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "快放回放录像失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            }
        }

        private void BtnFrame_Click(object sender, EventArgs e)
        {   // 点击单帧播放
            uint iOutValue = 0; // 参数的长度 [输出]
            if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYFRAME,
                                                        IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "单帧播放回放录像失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            }
        }

        private void BtnResume_Click(object sender, EventArgs e)
        {   // 点击恢复正常速度
            uint iOutValue = 0; // 参数的长度 [输出]
            if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAYNORMAL,
                                                        IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                iLastErr_str = "恢复正常播放速度失败，错误码为：" + iLastErr;
                MessageBox.Show(iLastErr_str);
                return;
            }
        }

        private void BtnReverse_Click(object sender, EventArgs e)
        {   
            if ( m_bReverse == false )
            {   // 点击倒放
                uint iOutValue = 0; // 参数的长度 [输出]
                if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAY_REVERSE,
                                                            IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    iLastErr_str = "倒放回放录像失败，错误码为：" + iLastErr;
                    MessageBox.Show(iLastErr_str);
                    return;
                }
                m_bReverse = true; // 标志着处于倒放状态
                btnReverse.Text = "Forward";
            } else
            {   // 点击正放
                uint iOutValue = 0; // 参数的长度 [输出]
                if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, CHCNetSDK.NET_DVR_PLAY_FORWARD,
                                                            IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    iLastErr_str = "正放回放录像失败，错误码为：" + iLastErr;
                    MessageBox.Show(iLastErr_str);
                    return;
                }
                m_bReverse = false;
                btnReverse.Text = "Reverse";
            }
        }




        /*******************播放控制（暂停/恢复/慢放/快放/单帧/恢复正常速度/倒放）************/


        /****************************************相机******************************************************/
    }
}
