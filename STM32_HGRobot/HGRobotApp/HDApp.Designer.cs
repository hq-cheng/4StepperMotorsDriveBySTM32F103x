using NVRCsharpDemo;

namespace HGRobotApp
{
    partial class HDApp
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            
            // 在窗体退出的时候会自动调用 Close()方法（包含Dispose()方法）
            // 重写 Dispose()方法，清理所有正在使用的资源。包括：关闭预览、注销用户、释放SDK资源
            
            // 关闭预览
            if (m_lRealHandle >= 0)
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

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.appversion_label = new System.Windows.Forms.Label();
            this.showserial_label = new System.Windows.Forms.Label();
            this.showlocation_label = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.up_button = new System.Windows.Forms.Button();
            this.staytime_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.staytime_label = new System.Windows.Forms.Label();
            this.premove_button1 = new System.Windows.Forms.Button();
            this.premove_button0 = new System.Windows.Forms.Button();
            this.premove_button4 = new System.Windows.Forms.Button();
            this.premove_button3 = new System.Windows.Forms.Button();
            this.premove_button2 = new System.Windows.Forms.Button();
            this.down_button = new System.Windows.Forms.Button();
            this.right_button = new System.Windows.Forms.Button();
            this.left_button = new System.Windows.Forms.Button();
            this.locationsensor2_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.locationsensor2_label = new System.Windows.Forms.Label();
            this.locationsensor1_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.locationsensor1_label = new System.Windows.Forms.Label();
            this.gdlength_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.gdlength_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPortNum = new System.Windows.Forms.TextBox();
            this.labelprotocol = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnNet = new System.Windows.Forms.Button();
            this.labelIPAddr = new System.Windows.Forms.Label();
            this.labelPortNum = new System.Windows.Forms.Label();
            this.textBoxIPAddr = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.DownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPlaybackTime = new System.Windows.Forms.Button();
            this.btnDownloadTime = new System.Windows.Forms.Button();
            this.panel20 = new System.Windows.Forms.Panel();
            this.listViewFile = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.PlaybackprogressBar = new System.Windows.Forms.ProgressBar();
            this.RealPlayWnd = new System.Windows.Forms.PictureBox();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFrame = new System.Windows.Forms.Button();
            this.btnFast = new System.Windows.Forms.Button();
            this.btnSlow = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnJPEG = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.panel18 = new System.Windows.Forms.Panel();
            this.btnResume = new System.Windows.Forms.Button();
            this.panel19 = new System.Windows.Forms.Panel();
            this.btnReverse = new System.Windows.Forms.Button();
            this.panel16 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDownLeft = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnAuto = new System.Windows.Forms.Button();
            this.btnUpLeft = new System.Windows.Forms.Button();
            this.btnUpRight = new System.Windows.Forms.Button();
            this.btnDownRight = new System.Windows.Forms.Button();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnZoomUp = new System.Windows.Forms.Button();
            this.btnZoomDown = new System.Windows.Forms.Button();
            this.btnFocusUp = new System.Windows.Forms.Button();
            this.btnFocusDown = new System.Windows.Forms.Button();
            this.btnIrisUp = new System.Windows.Forms.Button();
            this.btnIrisDown = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxSpeed = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxPreview = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.checkBoxHiDDNS = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel21 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.btnStopDownload = new System.Windows.Forms.Button();
            this.btnStopPlayback = new System.Windows.Forms.Button();
            this.btnPlaybackName = new System.Windows.Forms.Button();
            this.btnDownloadName = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.panel15 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel17 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelCarSpeed = new System.Windows.Forms.Label();
            this.timerPlayback = new System.Windows.Forms.Timer(this.components);
            this.timerDownload = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.staytime_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationsensor2_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationsensor1_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdlength_numericUpDown)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel9.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RealPlayWnd)).BeginInit();
            this.panel12.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel19.SuspendLayout();
            this.panel16.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel17.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.panel21.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.panel15.SuspendLayout();
            this.tableLayoutPanel17.SuspendLayout();
            this.SuspendLayout();
            // 
            // appversion_label
            // 
            this.appversion_label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.appversion_label.AutoSize = true;
            this.appversion_label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.appversion_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.appversion_label.Location = new System.Drawing.Point(396, 6);
            this.appversion_label.Name = "appversion_label";
            this.appversion_label.Size = new System.Drawing.Size(129, 21);
            this.appversion_label.TabIndex = 1;
            this.appversion_label.Text = "@HD_Beta_v1.0";
            // 
            // showserial_label
            // 
            this.showserial_label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.showserial_label.AutoSize = true;
            this.showserial_label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.showserial_label.ForeColor = System.Drawing.Color.Red;
            this.showserial_label.Location = new System.Drawing.Point(47, 6);
            this.showserial_label.Name = "showserial_label";
            this.showserial_label.Size = new System.Drawing.Size(90, 21);
            this.showserial_label.TabIndex = 0;
            this.showserial_label.Text = "串口已关闭";
            // 
            // showlocation_label
            // 
            this.showlocation_label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.showlocation_label.AutoSize = true;
            this.showlocation_label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.showlocation_label.Location = new System.Drawing.Point(208, 6);
            this.showlocation_label.Name = "showlocation_label";
            this.showlocation_label.Size = new System.Drawing.Size(136, 21);
            this.showlocation_label.TabIndex = 9;
            this.showlocation_label.Text = "小车位置：-2, +2";
            // 
            // up_button
            // 
            this.up_button.BackColor = System.Drawing.Color.Green;
            this.up_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.up_button.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.up_button.Location = new System.Drawing.Point(3, 45);
            this.up_button.Name = "up_button";
            this.up_button.Size = new System.Drawing.Size(132, 36);
            this.up_button.TabIndex = 3;
            this.up_button.Text = "上升";
            this.up_button.UseVisualStyleBackColor = false;
            this.up_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Up_button_MouseDown);
            this.up_button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Up_button_MouseUp);
            // 
            // staytime_numericUpDown
            // 
            this.staytime_numericUpDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.staytime_numericUpDown.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.staytime_numericUpDown.Location = new System.Drawing.Point(157, 179);
            this.staytime_numericUpDown.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.staytime_numericUpDown.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.staytime_numericUpDown.Name = "staytime_numericUpDown";
            this.staytime_numericUpDown.Size = new System.Drawing.Size(99, 29);
            this.staytime_numericUpDown.TabIndex = 12;
            this.staytime_numericUpDown.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // staytime_label
            // 
            this.staytime_label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.staytime_label.AutoSize = true;
            this.staytime_label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.staytime_label.Location = new System.Drawing.Point(32, 183);
            this.staytime_label.Name = "staytime_label";
            this.staytime_label.Size = new System.Drawing.Size(74, 21);
            this.staytime_label.TabIndex = 10;
            this.staytime_label.Text = "停留时间";
            // 
            // premove_button1
            // 
            this.premove_button1.BackColor = System.Drawing.Color.PowderBlue;
            this.premove_button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.premove_button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.premove_button1.Location = new System.Drawing.Point(0, 0);
            this.premove_button1.Name = "premove_button1";
            this.premove_button1.Size = new System.Drawing.Size(270, 37);
            this.premove_button1.TabIndex = 11;
            this.premove_button1.Text = "预置点位1";
            this.premove_button1.UseVisualStyleBackColor = false;
            // 
            // premove_button0
            // 
            this.premove_button0.BackColor = System.Drawing.Color.Red;
            this.premove_button0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.premove_button0.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.premove_button0.Location = new System.Drawing.Point(0, 0);
            this.premove_button0.Name = "premove_button0";
            this.premove_button0.Size = new System.Drawing.Size(270, 40);
            this.premove_button0.TabIndex = 11;
            this.premove_button0.Text = "复位";
            this.premove_button0.UseVisualStyleBackColor = false;
            // 
            // premove_button4
            // 
            this.premove_button4.BackColor = System.Drawing.Color.PowderBlue;
            this.premove_button4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.premove_button4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.premove_button4.Location = new System.Drawing.Point(0, 0);
            this.premove_button4.Name = "premove_button4";
            this.premove_button4.Size = new System.Drawing.Size(270, 37);
            this.premove_button4.TabIndex = 10;
            this.premove_button4.Text = "预置点位4";
            this.premove_button4.UseVisualStyleBackColor = false;
            // 
            // premove_button3
            // 
            this.premove_button3.BackColor = System.Drawing.Color.PowderBlue;
            this.premove_button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.premove_button3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.premove_button3.Location = new System.Drawing.Point(0, 0);
            this.premove_button3.Name = "premove_button3";
            this.premove_button3.Size = new System.Drawing.Size(270, 37);
            this.premove_button3.TabIndex = 9;
            this.premove_button3.Text = "预置点位3";
            this.premove_button3.UseVisualStyleBackColor = false;
            // 
            // premove_button2
            // 
            this.premove_button2.BackColor = System.Drawing.Color.PowderBlue;
            this.premove_button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.premove_button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.premove_button2.Location = new System.Drawing.Point(0, 0);
            this.premove_button2.Name = "premove_button2";
            this.premove_button2.Size = new System.Drawing.Size(270, 37);
            this.premove_button2.TabIndex = 7;
            this.premove_button2.Text = "预置点位2";
            this.premove_button2.UseVisualStyleBackColor = false;
            // 
            // down_button
            // 
            this.down_button.BackColor = System.Drawing.Color.Green;
            this.down_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.down_button.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.down_button.Location = new System.Drawing.Point(141, 45);
            this.down_button.Name = "down_button";
            this.down_button.Size = new System.Drawing.Size(132, 36);
            this.down_button.TabIndex = 6;
            this.down_button.Text = "下降";
            this.down_button.UseVisualStyleBackColor = false;
            this.down_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Down_button_MouseDown);
            this.down_button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Down_button_MouseUp);
            // 
            // right_button
            // 
            this.right_button.BackColor = System.Drawing.Color.Green;
            this.right_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.right_button.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.right_button.Location = new System.Drawing.Point(141, 3);
            this.right_button.Name = "right_button";
            this.right_button.Size = new System.Drawing.Size(132, 36);
            this.right_button.TabIndex = 4;
            this.right_button.Text = "右行";
            this.right_button.UseVisualStyleBackColor = false;
            this.right_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Right_button_MouseDown);
            this.right_button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Right_button_MouseUp);
            // 
            // left_button
            // 
            this.left_button.BackColor = System.Drawing.Color.Green;
            this.left_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.left_button.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.left_button.Location = new System.Drawing.Point(3, 3);
            this.left_button.Name = "left_button";
            this.left_button.Size = new System.Drawing.Size(132, 36);
            this.left_button.TabIndex = 5;
            this.left_button.Text = "左行";
            this.left_button.UseVisualStyleBackColor = false;
            this.left_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Left_button_MouseDown);
            this.left_button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Left_button_MouseUp);
            // 
            // locationsensor2_numericUpDown
            // 
            this.locationsensor2_numericUpDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.locationsensor2_numericUpDown.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.locationsensor2_numericUpDown.Location = new System.Drawing.Point(160, 85);
            this.locationsensor2_numericUpDown.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.locationsensor2_numericUpDown.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.locationsensor2_numericUpDown.Name = "locationsensor2_numericUpDown";
            this.locationsensor2_numericUpDown.Size = new System.Drawing.Size(94, 29);
            this.locationsensor2_numericUpDown.TabIndex = 14;
            this.locationsensor2_numericUpDown.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // locationsensor2_label
            // 
            this.locationsensor2_label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.locationsensor2_label.AutoSize = true;
            this.locationsensor2_label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.locationsensor2_label.Location = new System.Drawing.Point(27, 89);
            this.locationsensor2_label.Name = "locationsensor2_label";
            this.locationsensor2_label.Size = new System.Drawing.Size(83, 21);
            this.locationsensor2_label.TabIndex = 13;
            this.locationsensor2_label.Text = "行程开关2";
            // 
            // locationsensor1_numericUpDown
            // 
            this.locationsensor1_numericUpDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.locationsensor1_numericUpDown.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.locationsensor1_numericUpDown.Location = new System.Drawing.Point(160, 45);
            this.locationsensor1_numericUpDown.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.locationsensor1_numericUpDown.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.locationsensor1_numericUpDown.Name = "locationsensor1_numericUpDown";
            this.locationsensor1_numericUpDown.Size = new System.Drawing.Size(94, 29);
            this.locationsensor1_numericUpDown.TabIndex = 16;
            this.locationsensor1_numericUpDown.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // locationsensor1_label
            // 
            this.locationsensor1_label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.locationsensor1_label.AutoSize = true;
            this.locationsensor1_label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.locationsensor1_label.Location = new System.Drawing.Point(27, 49);
            this.locationsensor1_label.Name = "locationsensor1_label";
            this.locationsensor1_label.Size = new System.Drawing.Size(83, 21);
            this.locationsensor1_label.TabIndex = 15;
            this.locationsensor1_label.Text = "行程开关1";
            // 
            // gdlength_numericUpDown
            // 
            this.gdlength_numericUpDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gdlength_numericUpDown.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gdlength_numericUpDown.Location = new System.Drawing.Point(159, 5);
            this.gdlength_numericUpDown.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.gdlength_numericUpDown.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.gdlength_numericUpDown.Name = "gdlength_numericUpDown";
            this.gdlength_numericUpDown.Size = new System.Drawing.Size(96, 29);
            this.gdlength_numericUpDown.TabIndex = 14;
            this.gdlength_numericUpDown.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // gdlength_label
            // 
            this.gdlength_label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gdlength_label.AutoSize = true;
            this.gdlength_label.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gdlength_label.Location = new System.Drawing.Point(32, 9);
            this.gdlength_label.Name = "gdlength_label";
            this.gdlength_label.Size = new System.Drawing.Size(74, 21);
            this.gdlength_label.TabIndex = 13;
            this.gdlength_label.Text = "轨道长度";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.textBoxPortNum, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.labelprotocol, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel9, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.labelIPAddr, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.labelPortNum, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.textBoxIPAddr, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(276, 182);
            this.tableLayoutPanel4.TabIndex = 54;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(161, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 21);
            this.label4.TabIndex = 25;
            this.label4.Text = "TCP_Client";
            // 
            // textBoxPortNum
            // 
            this.textBoxPortNum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPortNum.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPortNum.Location = new System.Drawing.Point(141, 99);
            this.textBoxPortNum.Name = "textBoxPortNum";
            this.textBoxPortNum.Size = new System.Drawing.Size(132, 26);
            this.textBoxPortNum.TabIndex = 23;
            this.textBoxPortNum.Text = "8000";
            // 
            // labelprotocol
            // 
            this.labelprotocol.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelprotocol.AutoSize = true;
            this.labelprotocol.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelprotocol.Location = new System.Drawing.Point(32, 12);
            this.labelprotocol.Name = "labelprotocol";
            this.labelprotocol.Size = new System.Drawing.Size(74, 21);
            this.labelprotocol.TabIndex = 14;
            this.labelprotocol.Text = "协议类型";
            // 
            // panel9
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.panel9, 2);
            this.panel9.Controls.Add(this.btnNet);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(3, 138);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(270, 41);
            this.panel9.TabIndex = 0;
            // 
            // btnNet
            // 
            this.btnNet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNet.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNet.Location = new System.Drawing.Point(0, 0);
            this.btnNet.Name = "btnNet";
            this.btnNet.Size = new System.Drawing.Size(270, 41);
            this.btnNet.TabIndex = 0;
            this.btnNet.Text = "连接服务器";
            this.btnNet.UseVisualStyleBackColor = true;
            this.btnNet.Click += new System.EventHandler(this.BtnNet_Click);
            // 
            // labelIPAddr
            // 
            this.labelIPAddr.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelIPAddr.AutoSize = true;
            this.labelIPAddr.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelIPAddr.Location = new System.Drawing.Point(16, 57);
            this.labelIPAddr.Name = "labelIPAddr";
            this.labelIPAddr.Size = new System.Drawing.Size(105, 21);
            this.labelIPAddr.TabIndex = 16;
            this.labelIPAddr.Text = "服务器IP地址";
            // 
            // labelPortNum
            // 
            this.labelPortNum.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelPortNum.AutoSize = true;
            this.labelPortNum.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPortNum.Location = new System.Drawing.Point(16, 102);
            this.labelPortNum.Name = "labelPortNum";
            this.labelPortNum.Size = new System.Drawing.Size(106, 21);
            this.labelPortNum.TabIndex = 17;
            this.labelPortNum.Text = "服务器端口号";
            // 
            // textBoxIPAddr
            // 
            this.textBoxIPAddr.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxIPAddr.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxIPAddr.Location = new System.Drawing.Point(141, 54);
            this.textBoxIPAddr.Name = "textBoxIPAddr";
            this.textBoxIPAddr.Size = new System.Drawing.Size(132, 26);
            this.textBoxIPAddr.TabIndex = 24;
            this.textBoxIPAddr.Text = "169.254.0.6";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.locationsensor2_numericUpDown, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.gdlength_label, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.locationsensor1_numericUpDown, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.locationsensor2_label, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.gdlength_numericUpDown, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.locationsensor1_label, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(276, 120);
            this.tableLayoutPanel5.TabIndex = 55;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.down_button, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.right_button, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.left_button, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.up_button, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(4, 320);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(276, 84);
            this.tableLayoutPanel6.TabIndex = 56;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.staytime_numericUpDown, 1, 4);
            this.tableLayoutPanel7.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.staytime_label, 0, 4);
            this.tableLayoutPanel7.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.panel10, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.panel11, 0, 5);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 6;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(276, 261);
            this.tableLayoutPanel7.TabIndex = 57;
            // 
            // panel1
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.premove_button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 37);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.premove_button2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(270, 37);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.panel3, 2);
            this.panel3.Controls.Add(this.premove_button3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 89);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(270, 37);
            this.panel3.TabIndex = 2;
            // 
            // panel10
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.panel10, 2);
            this.panel10.Controls.Add(this.premove_button4);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(3, 132);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(270, 37);
            this.panel10.TabIndex = 13;
            // 
            // panel11
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.panel11, 2);
            this.panel11.Controls.Add(this.premove_button0);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(3, 218);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(270, 40);
            this.panel11.TabIndex = 14;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 3;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel8.Controls.Add(this.appversion_label, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.showserial_label, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.showlocation_label, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(553, 33);
            this.tableLayoutPanel8.TabIndex = 58;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel9.ColumnCount = 3;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.33472F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.79195F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.87333F));
            this.tableLayoutPanel9.Controls.Add(this.DownloadProgressBar, 1, 10);
            this.tableLayoutPanel9.Controls.Add(this.panel6, 1, 2);
            this.tableLayoutPanel9.Controls.Add(this.panel12, 0, 12);
            this.tableLayoutPanel9.Controls.Add(this.panel13, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.panel14, 0, 3);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel6, 0, 5);
            this.tableLayoutPanel9.Controls.Add(this.panel7, 1, 7);
            this.tableLayoutPanel9.Controls.Add(this.panel16, 2, 8);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel11, 2, 11);
            this.tableLayoutPanel9.Controls.Add(this.panel4, 2, 0);
            this.tableLayoutPanel9.Controls.Add(this.panel17, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.panel21, 1, 8);
            this.tableLayoutPanel9.Controls.Add(this.btn_Exit, 2, 12);
            this.tableLayoutPanel9.Controls.Add(this.panel15, 0, 7);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel17, 0, 6);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 13;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.257109F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.340512F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.887559F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.357746F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.708773F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.36008F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.360029F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.357746F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.360074F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.310072F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.12007F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.000104F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.580126F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(1037, 749);
            this.tableLayoutPanel9.TabIndex = 59;
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DownloadProgressBar.Location = new System.Drawing.Point(287, 600);
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(270, 38);
            this.DownloadProgressBar.TabIndex = 69;
            // 
            // panel6
            // 
            this.tableLayoutPanel9.SetColumnSpan(this.panel6, 2);
            this.panel6.Controls.Add(this.tableLayoutPanel1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(287, 127);
            this.panel6.Name = "panel6";
            this.tableLayoutPanel9.SetRowSpan(this.panel6, 5);
            this.panel6.Size = new System.Drawing.Size(746, 310);
            this.panel6.TabIndex = 60;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.18182F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.68687F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.13131F));
            this.tableLayoutPanel1.Controls.Add(this.btnPlaybackTime, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDownloadTime, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel20, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.78F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(746, 310);
            this.tableLayoutPanel1.TabIndex = 51;
            // 
            // btnPlaybackTime
            // 
            this.btnPlaybackTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPlaybackTime.Location = new System.Drawing.Point(3, 3);
            this.btnPlaybackTime.Name = "btnPlaybackTime";
            this.btnPlaybackTime.Size = new System.Drawing.Size(129, 38);
            this.btnPlaybackTime.TabIndex = 56;
            this.btnPlaybackTime.Text = "按时间回放";
            this.btnPlaybackTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPlaybackTime.UseVisualStyleBackColor = true;
            this.btnPlaybackTime.Click += new System.EventHandler(this.BtnPlaybackTime_Click);
            // 
            // btnDownloadTime
            // 
            this.btnDownloadTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownloadTime.Location = new System.Drawing.Point(138, 3);
            this.btnDownloadTime.Name = "btnDownloadTime";
            this.btnDownloadTime.Size = new System.Drawing.Size(133, 38);
            this.btnDownloadTime.TabIndex = 59;
            this.btnDownloadTime.Text = "按时间下载";
            this.btnDownloadTime.UseVisualStyleBackColor = true;
            this.btnDownloadTime.Click += new System.EventHandler(this.BtnDownloadTime_Click);
            // 
            // panel20
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel20, 2);
            this.panel20.Controls.Add(this.listViewFile);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel20.Location = new System.Drawing.Point(3, 47);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(268, 260);
            this.panel20.TabIndex = 60;
            // 
            // listViewFile
            // 
            this.listViewFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFile.FullRowSelect = true;
            this.listViewFile.GridLines = true;
            this.listViewFile.HideSelection = false;
            this.listViewFile.Location = new System.Drawing.Point(0, 0);
            this.listViewFile.Name = "listViewFile";
            this.listViewFile.Size = new System.Drawing.Size(268, 260);
            this.listViewFile.TabIndex = 50;
            this.listViewFile.UseCompatibleStateImageBehavior = false;
            this.listViewFile.View = System.Windows.Forms.View.Details;
            this.listViewFile.SelectedIndexChanged += new System.EventHandler(this.ListViewFile_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "文件";
            this.columnHeader3.Width = 93;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "开始时间";
            this.columnHeader4.Width = 109;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "停止时间";
            this.columnHeader5.Width = 113;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tableLayoutPanel16);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(277, 3);
            this.panel5.Name = "panel5";
            this.tableLayoutPanel1.SetRowSpan(this.panel5, 2);
            this.panel5.Size = new System.Drawing.Size(466, 304);
            this.panel5.TabIndex = 61;
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.ColumnCount = 1;
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.Controls.Add(this.PlaybackprogressBar, 0, 1);
            this.tableLayoutPanel16.Controls.Add(this.RealPlayWnd, 0, 0);
            this.tableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel16.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 2;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel16.Size = new System.Drawing.Size(466, 304);
            this.tableLayoutPanel16.TabIndex = 0;
            // 
            // PlaybackprogressBar
            // 
            this.PlaybackprogressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlaybackprogressBar.Location = new System.Drawing.Point(3, 291);
            this.PlaybackprogressBar.Name = "PlaybackprogressBar";
            this.PlaybackprogressBar.Size = new System.Drawing.Size(460, 10);
            this.PlaybackprogressBar.TabIndex = 45;
            // 
            // RealPlayWnd
            // 
            this.RealPlayWnd.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.RealPlayWnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RealPlayWnd.Location = new System.Drawing.Point(3, 3);
            this.RealPlayWnd.Name = "RealPlayWnd";
            this.RealPlayWnd.Size = new System.Drawing.Size(460, 282);
            this.RealPlayWnd.TabIndex = 44;
            this.RealPlayWnd.TabStop = false;
            // 
            // panel12
            // 
            this.tableLayoutPanel9.SetColumnSpan(this.panel12, 2);
            this.panel12.Controls.Add(this.tableLayoutPanel8);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(4, 712);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(553, 33);
            this.panel12.TabIndex = 61;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.tableLayoutPanel4);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(4, 4);
            this.panel13.Name = "panel13";
            this.tableLayoutPanel9.SetRowSpan(this.panel13, 3);
            this.panel13.Size = new System.Drawing.Size(276, 182);
            this.panel13.TabIndex = 62;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.tableLayoutPanel5);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Location = new System.Drawing.Point(4, 193);
            this.panel14.Name = "panel14";
            this.tableLayoutPanel9.SetRowSpan(this.panel14, 2);
            this.panel14.Size = new System.Drawing.Size(276, 120);
            this.panel14.TabIndex = 63;
            // 
            // panel7
            // 
            this.tableLayoutPanel9.SetColumnSpan(this.panel7, 2);
            this.panel7.Controls.Add(this.tableLayoutPanel2);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(287, 444);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(746, 55);
            this.panel7.TabIndex = 65;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.79038F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.210023F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.210023F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.210023F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.210023F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78985F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78985F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78985F));
            this.tableLayoutPanel2.Controls.Add(this.btnFrame, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFast, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSlow, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label19, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnRecord, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnJPEG, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.label20, 6, 1);
            this.tableLayoutPanel2.Controls.Add(this.label22, 7, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnPreview, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSearch, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnPause, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel18, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel19, 3, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(746, 55);
            this.tableLayoutPanel2.TabIndex = 52;
            // 
            // btnFrame
            // 
            this.btnFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFrame.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFrame.Location = new System.Drawing.Point(324, 3);
            this.btnFrame.Name = "btnFrame";
            this.btnFrame.Size = new System.Drawing.Size(62, 21);
            this.btnFrame.TabIndex = 43;
            this.btnFrame.Text = "|>";
            this.btnFrame.UseVisualStyleBackColor = true;
            this.btnFrame.Click += new System.EventHandler(this.BtnFrame_Click);
            // 
            // btnFast
            // 
            this.btnFast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFast.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFast.Location = new System.Drawing.Point(256, 3);
            this.btnFast.Name = "btnFast";
            this.btnFast.Size = new System.Drawing.Size(62, 21);
            this.btnFast.TabIndex = 42;
            this.btnFast.Text = ">>";
            this.btnFast.UseVisualStyleBackColor = true;
            this.btnFast.Click += new System.EventHandler(this.BtnFast_Click);
            // 
            // btnSlow
            // 
            this.btnSlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSlow.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSlow.Location = new System.Drawing.Point(188, 3);
            this.btnSlow.Name = "btnSlow";
            this.btnSlow.Size = new System.Drawing.Size(62, 21);
            this.btnSlow.TabIndex = 41;
            this.btnSlow.Text = "<<";
            this.btnSlow.UseVisualStyleBackColor = true;
            this.btnSlow.Click += new System.EventHandler(this.BtnSlow_Click);
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(32, 35);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 50;
            this.label13.Text = "搜索文件";
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(415, 35);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 45;
            this.label19.Text = "客户端录像";
            // 
            // btnRecord
            // 
            this.btnRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRecord.Location = new System.Drawing.Point(392, 3);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(111, 21);
            this.btnRecord.TabIndex = 44;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.BtnRecord_Click);
            // 
            // btnJPEG
            // 
            this.btnJPEG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnJPEG.Location = new System.Drawing.Point(509, 3);
            this.btnJPEG.Name = "btnJPEG";
            this.btnJPEG.Size = new System.Drawing.Size(111, 21);
            this.btnJPEG.TabIndex = 42;
            this.btnJPEG.Text = "Capture JPEG";
            this.btnJPEG.UseVisualStyleBackColor = true;
            this.btnJPEG.Click += new System.EventHandler(this.BtnJPEG_Click);
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(538, 35);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 12);
            this.label20.TabIndex = 43;
            this.label20.Text = "JPEG抓图";
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.Color.Green;
            this.label22.Location = new System.Drawing.Point(670, 35);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(29, 12);
            this.label22.TabIndex = 34;
            this.label22.Text = "预览";
            // 
            // btnPreview
            // 
            this.btnPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPreview.ForeColor = System.Drawing.Color.Green;
            this.btnPreview.Location = new System.Drawing.Point(626, 3);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(117, 21);
            this.btnPreview.TabIndex = 33;
            this.btnPreview.Text = "Live View";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.BtnPreview_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Location = new System.Drawing.Point(3, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(111, 21);
            this.btnSearch.TabIndex = 49;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnPause
            // 
            this.btnPause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPause.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPause.Location = new System.Drawing.Point(120, 3);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(62, 21);
            this.btnPause.TabIndex = 51;
            this.btnPause.Text = "||";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.BtnPause_Click);
            // 
            // panel18
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.panel18, 2);
            this.panel18.Controls.Add(this.btnResume);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel18.Location = new System.Drawing.Point(120, 30);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(130, 22);
            this.panel18.TabIndex = 52;
            // 
            // btnResume
            // 
            this.btnResume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnResume.Location = new System.Drawing.Point(0, 0);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(130, 22);
            this.btnResume.TabIndex = 50;
            this.btnResume.Text = "Normal Speed";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.BtnResume_Click);
            // 
            // panel19
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.panel19, 2);
            this.panel19.Controls.Add(this.btnReverse);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel19.Location = new System.Drawing.Point(256, 30);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(130, 22);
            this.panel19.TabIndex = 53;
            // 
            // btnReverse
            // 
            this.btnReverse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReverse.Location = new System.Drawing.Point(0, 0);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(130, 22);
            this.btnReverse.TabIndex = 48;
            this.btnReverse.Text = "Reverse";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.BtnReverse_Click);
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.tableLayoutPanel13);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Location = new System.Drawing.Point(564, 506);
            this.panel16.Name = "panel16";
            this.tableLayoutPanel9.SetRowSpan(this.panel16, 3);
            this.panel16.Size = new System.Drawing.Size(469, 132);
            this.panel16.TabIndex = 68;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 2;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel10, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.tableLayoutPanel12, 1, 0);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(469, 132);
            this.tableLayoutPanel13.TabIndex = 0;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 3;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.Controls.Add(this.btnDownLeft, 0, 2);
            this.tableLayoutPanel10.Controls.Add(this.btnUp, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.btnLeft, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.btnRight, 2, 1);
            this.tableLayoutPanel10.Controls.Add(this.btnDown, 1, 2);
            this.tableLayoutPanel10.Controls.Add(this.btnAuto, 1, 1);
            this.tableLayoutPanel10.Controls.Add(this.btnUpLeft, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.btnUpRight, 2, 0);
            this.tableLayoutPanel10.Controls.Add(this.btnDownRight, 2, 2);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 3;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(275, 126);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // btnDownLeft
            // 
            this.btnDownLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownLeft.Location = new System.Drawing.Point(3, 87);
            this.btnDownLeft.Name = "btnDownLeft";
            this.btnDownLeft.Size = new System.Drawing.Size(85, 36);
            this.btnDownLeft.TabIndex = 22;
            this.btnDownLeft.Text = "Down_Left";
            this.btnDownLeft.UseVisualStyleBackColor = true;
            this.btnDownLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnDownLeft_MouseDown);
            this.btnDownLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnDownLeft_MouseUp);
            // 
            // btnUp
            // 
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUp.Location = new System.Drawing.Point(94, 3);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(85, 36);
            this.btnUp.TabIndex = 14;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnUp_MouseDown);
            this.btnUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnUp_MouseUp);
            // 
            // btnLeft
            // 
            this.btnLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLeft.Location = new System.Drawing.Point(3, 45);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(85, 36);
            this.btnLeft.TabIndex = 15;
            this.btnLeft.Text = "Left";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnLeft_MouseDown);
            this.btnLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnLeft_MouseUp);
            // 
            // btnRight
            // 
            this.btnRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRight.Location = new System.Drawing.Point(185, 45);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(87, 36);
            this.btnRight.TabIndex = 17;
            this.btnRight.Text = "Right";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnRight_MouseDown);
            this.btnRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnRight_MouseUp);
            // 
            // btnDown
            // 
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDown.Location = new System.Drawing.Point(94, 87);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(85, 36);
            this.btnDown.TabIndex = 18;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnDown_MouseDown);
            this.btnDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnDown_MouseUp);
            // 
            // btnAuto
            // 
            this.btnAuto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAuto.Location = new System.Drawing.Point(94, 45);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(85, 36);
            this.btnAuto.TabIndex = 19;
            this.btnAuto.Text = "Auto";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnAuto_MouseDown);
            this.btnAuto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnAuto_MouseUp);
            // 
            // btnUpLeft
            // 
            this.btnUpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpLeft.Location = new System.Drawing.Point(3, 3);
            this.btnUpLeft.Name = "btnUpLeft";
            this.btnUpLeft.Size = new System.Drawing.Size(85, 36);
            this.btnUpLeft.TabIndex = 21;
            this.btnUpLeft.Text = "Up_Left";
            this.btnUpLeft.UseVisualStyleBackColor = true;
            this.btnUpLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnUpLeft_MouseDown);
            this.btnUpLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnUpLeft_MouseUp);
            // 
            // btnUpRight
            // 
            this.btnUpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpRight.Location = new System.Drawing.Point(185, 3);
            this.btnUpRight.Name = "btnUpRight";
            this.btnUpRight.Size = new System.Drawing.Size(87, 36);
            this.btnUpRight.TabIndex = 20;
            this.btnUpRight.Text = "Up_Right";
            this.btnUpRight.UseVisualStyleBackColor = true;
            this.btnUpRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnUpRight_MouseDown);
            this.btnUpRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnUpRight_MouseUp);
            // 
            // btnDownRight
            // 
            this.btnDownRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownRight.Location = new System.Drawing.Point(185, 87);
            this.btnDownRight.Name = "btnDownRight";
            this.btnDownRight.Size = new System.Drawing.Size(87, 36);
            this.btnDownRight.TabIndex = 23;
            this.btnDownRight.Text = "Down_Right";
            this.btnDownRight.UseVisualStyleBackColor = true;
            this.btnDownRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnDownRight_MouseDown);
            this.btnDownRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnDownRight_MouseUp);
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 3;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel12.Controls.Add(this.label16, 1, 2);
            this.tableLayoutPanel12.Controls.Add(this.label15, 1, 1);
            this.tableLayoutPanel12.Controls.Add(this.btnZoomUp, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.btnZoomDown, 2, 0);
            this.tableLayoutPanel12.Controls.Add(this.btnFocusUp, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.btnFocusDown, 2, 1);
            this.tableLayoutPanel12.Controls.Add(this.btnIrisUp, 0, 2);
            this.tableLayoutPanel12.Controls.Add(this.btnIrisDown, 2, 2);
            this.tableLayoutPanel12.Controls.Add(this.label7, 1, 0);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(284, 3);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 3;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(182, 126);
            this.tableLayoutPanel12.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(75, 99);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 8;
            this.label16.Text = "光圈";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(75, 57);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 7;
            this.label15.Text = "变焦";
            // 
            // btnZoomUp
            // 
            this.btnZoomUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnZoomUp.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnZoomUp.Location = new System.Drawing.Point(3, 3);
            this.btnZoomUp.Name = "btnZoomUp";
            this.btnZoomUp.Size = new System.Drawing.Size(54, 36);
            this.btnZoomUp.TabIndex = 0;
            this.btnZoomUp.Text = "+";
            this.btnZoomUp.UseVisualStyleBackColor = true;
            this.btnZoomUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnZoomUp_MouseDown);
            this.btnZoomUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnZoomUp_MouseUp);
            // 
            // btnZoomDown
            // 
            this.btnZoomDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnZoomDown.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnZoomDown.Location = new System.Drawing.Point(123, 3);
            this.btnZoomDown.Name = "btnZoomDown";
            this.btnZoomDown.Size = new System.Drawing.Size(56, 36);
            this.btnZoomDown.TabIndex = 1;
            this.btnZoomDown.Text = "-";
            this.btnZoomDown.UseVisualStyleBackColor = true;
            this.btnZoomDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnZoomDown_MouseDown);
            this.btnZoomDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnZoomDown_MouseUp);
            // 
            // btnFocusUp
            // 
            this.btnFocusUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFocusUp.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnFocusUp.Location = new System.Drawing.Point(3, 45);
            this.btnFocusUp.Name = "btnFocusUp";
            this.btnFocusUp.Size = new System.Drawing.Size(54, 36);
            this.btnFocusUp.TabIndex = 2;
            this.btnFocusUp.Text = "+";
            this.btnFocusUp.UseVisualStyleBackColor = true;
            this.btnFocusUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnFocusUp_MouseDown);
            this.btnFocusUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnFocusUp_MouseUp);
            // 
            // btnFocusDown
            // 
            this.btnFocusDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFocusDown.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnFocusDown.Location = new System.Drawing.Point(123, 45);
            this.btnFocusDown.Name = "btnFocusDown";
            this.btnFocusDown.Size = new System.Drawing.Size(56, 36);
            this.btnFocusDown.TabIndex = 3;
            this.btnFocusDown.Text = "-";
            this.btnFocusDown.UseVisualStyleBackColor = true;
            this.btnFocusDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnFocusDown_MouseDown);
            this.btnFocusDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnFocusDown_MouseUp);
            // 
            // btnIrisUp
            // 
            this.btnIrisUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnIrisUp.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnIrisUp.Location = new System.Drawing.Point(3, 87);
            this.btnIrisUp.Name = "btnIrisUp";
            this.btnIrisUp.Size = new System.Drawing.Size(54, 36);
            this.btnIrisUp.TabIndex = 4;
            this.btnIrisUp.Text = "+";
            this.btnIrisUp.UseVisualStyleBackColor = true;
            this.btnIrisUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnIrisUp_MouseDown);
            this.btnIrisUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnIrisUp_MouseUp);
            // 
            // btnIrisDown
            // 
            this.btnIrisDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnIrisDown.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnIrisDown.Location = new System.Drawing.Point(123, 87);
            this.btnIrisDown.Name = "btnIrisDown";
            this.btnIrisDown.Size = new System.Drawing.Size(56, 36);
            this.btnIrisDown.TabIndex = 5;
            this.btnIrisDown.Text = "-";
            this.btnIrisDown.UseVisualStyleBackColor = true;
            this.btnIrisDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnIrisDown_MouseDown);
            this.btnIrisDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnIrisDown_MouseUp);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(75, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "变倍";
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 3;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.27933F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.86033F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.86033F));
            this.tableLayoutPanel11.Controls.Add(this.comboBoxSpeed, 2, 0);
            this.tableLayoutPanel11.Controls.Add(this.label6, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.checkBoxPreview, 0, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(564, 645);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 2;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(469, 60);
            this.tableLayoutPanel11.TabIndex = 67;
            // 
            // comboBoxSpeed
            // 
            this.comboBoxSpeed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxSpeed.FormattingEnabled = true;
            this.comboBoxSpeed.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.comboBoxSpeed.Location = new System.Drawing.Point(354, 5);
            this.comboBoxSpeed.Name = "comboBoxSpeed";
            this.comboBoxSpeed.Size = new System.Drawing.Size(112, 20);
            this.comboBoxSpeed.TabIndex = 68;
            this.comboBoxSpeed.Text = "4";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(260, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "云台速度：";
            // 
            // checkBoxPreview
            // 
            this.checkBoxPreview.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxPreview.AutoSize = true;
            this.checkBoxPreview.Enabled = false;
            this.checkBoxPreview.Location = new System.Drawing.Point(63, 7);
            this.checkBoxPreview.Name = "checkBoxPreview";
            this.checkBoxPreview.Size = new System.Drawing.Size(108, 16);
            this.checkBoxPreview.TabIndex = 21;
            this.checkBoxPreview.Text = "是否已启动预览";
            this.checkBoxPreview.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tableLayoutPanel3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(564, 4);
            this.panel4.Name = "panel4";
            this.tableLayoutPanel9.SetRowSpan(this.panel4, 2);
            this.panel4.Size = new System.Drawing.Size(469, 116);
            this.panel4.TabIndex = 69;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.textBoxPort, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBoxPassword, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.panel8, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label14, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBoxUserName, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.textBoxIP, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label11, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.label12, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnLogin, 4, 2);
            this.tableLayoutPanel3.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label10, 2, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(469, 116);
            this.tableLayoutPanel3.TabIndex = 53;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPort.Location = new System.Drawing.Point(282, 43);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(87, 21);
            this.textBoxPort.TabIndex = 21;
            this.textBoxPort.Text = "8000";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxPassword.Location = new System.Drawing.Point(282, 84);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(87, 21);
            this.textBoxPassword.TabIndex = 23;
            this.textBoxPassword.Text = "dw123456";
            // 
            // panel8
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.panel8, 5);
            this.panel8.Controls.Add(this.checkBoxHiDDNS);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(463, 28);
            this.panel8.TabIndex = 0;
            // 
            // checkBoxHiDDNS
            // 
            this.checkBoxHiDDNS.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxHiDDNS.AutoSize = true;
            this.checkBoxHiDDNS.Location = new System.Drawing.Point(3, 8);
            this.checkBoxHiDDNS.Name = "checkBoxHiDDNS";
            this.checkBoxHiDDNS.Size = new System.Drawing.Size(108, 16);
            this.checkBoxHiDDNS.TabIndex = 29;
            this.checkBoxHiDDNS.Text = "HiDDNS域名登录";
            this.checkBoxHiDDNS.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 12);
            this.label14.TabIndex = 24;
            this.label14.Text = "设备IP或域名";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxUserName.Location = new System.Drawing.Point(96, 84);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(87, 21);
            this.textBoxUserName.TabIndex = 22;
            this.textBoxUserName.Text = "admin";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxIP.Location = new System.Drawing.Point(96, 43);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(87, 21);
            this.textBoxIP.TabIndex = 20;
            this.textBoxIP.Text = "169.254.0.64";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(206, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 25;
            this.label11.Text = "设备端口";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(406, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 28;
            this.label12.Text = "登录";
            // 
            // btnLogin
            // 
            this.btnLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLogin.Location = new System.Drawing.Point(375, 77);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(91, 36);
            this.btnLogin.TabIndex = 19;
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 26;
            this.label9.Text = "用户名";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(218, 89);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 27;
            this.label10.Text = "密码";
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.tableLayoutPanel14);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel17.Location = new System.Drawing.Point(287, 4);
            this.panel17.Name = "panel17";
            this.tableLayoutPanel9.SetRowSpan(this.panel17, 2);
            this.panel17.Size = new System.Drawing.Size(270, 116);
            this.panel17.TabIndex = 70;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.ColumnCount = 2;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel14.Controls.Add(this.dateTimeEnd, 1, 1);
            this.tableLayoutPanel14.Controls.Add(this.dateTimeStart, 1, 0);
            this.tableLayoutPanel14.Controls.Add(this.label18, 0, 1);
            this.tableLayoutPanel14.Controls.Add(this.label17, 0, 0);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 2;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(270, 116);
            this.tableLayoutPanel14.TabIndex = 0;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dateTimeEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEnd.Location = new System.Drawing.Point(111, 76);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(156, 21);
            this.dateTimeEnd.TabIndex = 44;
            this.dateTimeEnd.Value = new System.DateTime(2018, 6, 25, 0, 0, 0, 0);
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dateTimeStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStart.Location = new System.Drawing.Point(111, 18);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(156, 21);
            this.dateTimeStart.TabIndex = 43;
            this.dateTimeStart.UseWaitCursor = true;
            this.dateTimeStart.Value = new System.DateTime(2018, 6, 25, 0, 0, 0, 0);
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(27, 81);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 47;
            this.label18.Text = "结束时间";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(27, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 45;
            this.label17.Text = "开始时间";
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.tableLayoutPanel15);
            this.panel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel21.Location = new System.Drawing.Point(287, 506);
            this.panel21.Name = "panel21";
            this.tableLayoutPanel9.SetRowSpan(this.panel21, 2);
            this.panel21.Size = new System.Drawing.Size(270, 87);
            this.panel21.TabIndex = 71;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 2;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.Controls.Add(this.btnStopDownload, 1, 1);
            this.tableLayoutPanel15.Controls.Add(this.btnStopPlayback, 0, 1);
            this.tableLayoutPanel15.Controls.Add(this.btnPlaybackName, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.btnDownloadName, 1, 0);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 2;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(270, 87);
            this.tableLayoutPanel15.TabIndex = 0;
            // 
            // btnStopDownload
            // 
            this.btnStopDownload.BackColor = System.Drawing.SystemColors.Control;
            this.btnStopDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStopDownload.Enabled = false;
            this.btnStopDownload.Location = new System.Drawing.Point(138, 46);
            this.btnStopDownload.Name = "btnStopDownload";
            this.btnStopDownload.Size = new System.Drawing.Size(129, 38);
            this.btnStopDownload.TabIndex = 69;
            this.btnStopDownload.Text = "终止下载";
            this.btnStopDownload.UseVisualStyleBackColor = false;
            this.btnStopDownload.Click += new System.EventHandler(this.BtnStopDownload_Click);
            // 
            // btnStopPlayback
            // 
            this.btnStopPlayback.BackColor = System.Drawing.SystemColors.Control;
            this.btnStopPlayback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStopPlayback.Enabled = false;
            this.btnStopPlayback.Location = new System.Drawing.Point(3, 46);
            this.btnStopPlayback.Name = "btnStopPlayback";
            this.btnStopPlayback.Size = new System.Drawing.Size(129, 38);
            this.btnStopPlayback.TabIndex = 69;
            this.btnStopPlayback.Text = "停止回放";
            this.btnStopPlayback.UseVisualStyleBackColor = false;
            this.btnStopPlayback.Click += new System.EventHandler(this.BtnStopPlayback_Click);
            // 
            // btnPlaybackName
            // 
            this.btnPlaybackName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPlaybackName.Location = new System.Drawing.Point(3, 3);
            this.btnPlaybackName.Name = "btnPlaybackName";
            this.btnPlaybackName.Size = new System.Drawing.Size(129, 37);
            this.btnPlaybackName.TabIndex = 52;
            this.btnPlaybackName.Text = "按文件名回放";
            this.btnPlaybackName.UseVisualStyleBackColor = true;
            this.btnPlaybackName.Click += new System.EventHandler(this.BtnPlaybackName_Click);
            // 
            // btnDownloadName
            // 
            this.btnDownloadName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDownloadName.Location = new System.Drawing.Point(138, 3);
            this.btnDownloadName.Name = "btnDownloadName";
            this.btnDownloadName.Size = new System.Drawing.Size(129, 37);
            this.btnDownloadName.TabIndex = 60;
            this.btnDownloadName.Text = "按文件名下载";
            this.btnDownloadName.UseVisualStyleBackColor = true;
            this.btnDownloadName.Click += new System.EventHandler(this.BtnDownloadName_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.Color.Red;
            this.btn_Exit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Exit.Location = new System.Drawing.Point(564, 712);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(469, 33);
            this.btn_Exit.TabIndex = 50;
            this.btn_Exit.Text = "退出 Exit";
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.tableLayoutPanel7);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(4, 444);
            this.panel15.Name = "panel15";
            this.tableLayoutPanel9.SetRowSpan(this.panel15, 5);
            this.panel15.Size = new System.Drawing.Size(276, 261);
            this.panel15.TabIndex = 72;
            // 
            // tableLayoutPanel17
            // 
            this.tableLayoutPanel17.ColumnCount = 2;
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel17.Controls.Add(this.comboBox1, 0, 0);
            this.tableLayoutPanel17.Controls.Add(this.labelCarSpeed, 0, 0);
            this.tableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel17.Location = new System.Drawing.Point(4, 411);
            this.tableLayoutPanel17.Name = "tableLayoutPanel17";
            this.tableLayoutPanel17.RowCount = 1;
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel17.Size = new System.Drawing.Size(276, 26);
            this.tableLayoutPanel17.TabIndex = 73;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.comboBox1.Location = new System.Drawing.Point(141, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(132, 20);
            this.comboBox1.TabIndex = 69;
            this.comboBox1.Text = "4";
            // 
            // labelCarSpeed
            // 
            this.labelCarSpeed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelCarSpeed.AutoSize = true;
            this.labelCarSpeed.Location = new System.Drawing.Point(36, 7);
            this.labelCarSpeed.Name = "labelCarSpeed";
            this.labelCarSpeed.Size = new System.Drawing.Size(65, 12);
            this.labelCarSpeed.TabIndex = 20;
            this.labelCarSpeed.Text = "小车速度：";
            // 
            // timerPlayback
            // 
            this.timerPlayback.Tick += new System.EventHandler(this.TimerPlayback_Tick);
            // 
            // timerDownload
            // 
            this.timerDownload.Tick += new System.EventHandler(this.TimerDownload_Tick);
            // 
            // HDApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 749);
            this.Controls.Add(this.tableLayoutPanel9);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.KeyPreview = true;
            this.Name = "HDApp";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.staytime_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationsensor2_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.locationsensor1_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdlength_numericUpDown)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RealPlayWnd)).EndInit();
            this.panel12.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.panel19.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel17.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            this.panel21.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.tableLayoutPanel17.ResumeLayout(false);
            this.tableLayoutPanel17.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label showserial_label;
        private System.Windows.Forms.Label appversion_label;
        private System.Windows.Forms.Button up_button;
        private System.Windows.Forms.Button down_button;
        private System.Windows.Forms.Button left_button;
        private System.Windows.Forms.Button right_button;
        private System.Windows.Forms.Button premove_button2;
        private System.Windows.Forms.Label staytime_label;
        private System.Windows.Forms.Label showlocation_label;
        private System.Windows.Forms.Button premove_button0;
        private System.Windows.Forms.Button premove_button4;
        private System.Windows.Forms.Button premove_button3;
        private System.Windows.Forms.Button premove_button1;
        private System.Windows.Forms.NumericUpDown staytime_numericUpDown;
        private System.Windows.Forms.NumericUpDown locationsensor2_numericUpDown;
        private System.Windows.Forms.Label locationsensor2_label;
        private System.Windows.Forms.NumericUpDown locationsensor1_numericUpDown;
        private System.Windows.Forms.Label locationsensor1_label;
        private System.Windows.Forms.NumericUpDown gdlength_numericUpDown;
        private System.Windows.Forms.Label gdlength_label;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox RealPlayWnd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnJPEG;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.CheckBox checkBoxHiDDNS;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Button btnDownLeft;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.Button btnUpLeft;
        private System.Windows.Forms.Button btnUpRight;
        private System.Windows.Forms.Button btnDownRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.ComboBox comboBoxSpeed;
        private System.Windows.Forms.CheckBox checkBoxPreview;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnZoomUp;
        private System.Windows.Forms.Button btnZoomDown;
        private System.Windows.Forms.Button btnFocusUp;
        private System.Windows.Forms.Button btnFocusDown;
        private System.Windows.Forms.Button btnIrisUp;
        private System.Windows.Forms.Button btnIrisDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnFrame;
        private System.Windows.Forms.Button btnFast;
        private System.Windows.Forms.Button btnSlow;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Button btnReverse;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.Button btnPlaybackTime;
        private System.Windows.Forms.Button btnDownloadTime;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.ListView listViewFile;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ProgressBar DownloadProgressBar;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.Button btnStopDownload;
        private System.Windows.Forms.Button btnStopPlayback;
        private System.Windows.Forms.Button btnPlaybackName;
        private System.Windows.Forms.Button btnDownloadName;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
        private System.Windows.Forms.ProgressBar PlaybackprogressBar;
        private System.Windows.Forms.Timer timerPlayback;
        private System.Windows.Forms.Timer timerDownload;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPortNum;
        private System.Windows.Forms.Label labelprotocol;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btnNet;
        private System.Windows.Forms.Label labelIPAddr;
        private System.Windows.Forms.Label labelPortNum;
        private System.Windows.Forms.TextBox textBoxIPAddr;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel17;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label labelCarSpeed;
    }
}

