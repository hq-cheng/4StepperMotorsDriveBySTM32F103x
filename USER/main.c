/*
 * luoliCHq
 */


#include "sys.h"
#include "delay.h"
#include "GPIO_Motor.h"
#include "IronHand.h"
#include "key8.h"
#include "timer1.h"
#include "pwm.h"
#include "stm32f10x_gpio.h"
#include "motor_ctrl.h"
#include "motor.h"
#include "math.h"
#include "usart_master.h"

void Main_Init(void);
void Step(void);
void Compute_Time_Of(unsigned char MOTOID,unsigned char H,unsigned char L);


// 定时器相关变量
uint16_t count_time = 0;							// 定时器中断每间隔1ms触发，达到指定count值清零，电机变换转动方向

// 键盘相关变量
uint16_t Key_Flag = 0;

// 串口接收缓冲数组
// unsigned char buf[64] = {9,12,4,7,10,4,11,6};
// volatile unsigned char buf[30];
volatile REC_BUFFER rec_buffer;

// 自己测试/找bug的时候，设置为1
#define USART1_DEBUG 1


// 主函数文件中定义电机的结构体变量
volatile MOTOR_CONTROL motor1;
volatile MOTOR_CONTROL motor2;
volatile MOTOR_CONTROL motor3;

int main(void)
{
	Main_Init();
	TIM_Cmd(TIM1,DISABLE);
	USART1_Master_Init(9600);

	while(1)
	{												// 主循环程序
		
		Step();										// 如果电机在复位点，且串口缓冲区不为0的时候，开始一步操作
		if(motor1.isDestn && motor2.isDestn)
		{											// 到达了目的地
			TIM_Cmd(TIM1,DISABLE);
			Annex_Seal_By(MOTOZ,1);					// 执行盖章动作,按下印章
			delay_s(1);
			Annex_Seal_By(MOTOZ,0);					// 执行盖章动作,抬起印章
			delay_s(1);
			Start_Motor_withS(MOTOX,1);
			Start_Motor_withS(MOTOY,0);
			motor1.nextMove = 1;					// 反向过程
			motor2.nextMove = 1;
			motor1.isDestn = 0;						// 电机移动偏离目标点，更新状态
			motor2.isDestn = 0;
			count_time = 0;
			TIM_Cmd(TIM1,ENABLE);
		}					
	}
	

}

/*******************************************************************************
* 函 数 名         : Main_Init
* 函数功能         : 主程序外设初始化程序
* 输    入         : 无 
* 输    出         : 无
*******************************************************************************/
void Main_Init()
{
	// 外设初始化程序
	delay_init();									// 延时函数初始化
	NVIC_Configuration(); 	 						// 设置NVIC中断分组2:2位抢占优先级，2位响应优先级
	
	GPIO_Motor_Init();								// 电机引脚对应IO口初始化设置
	GPIO_Key_Init();								// 键盘对应IO口初始化设置
	GPIO_IronHand_Init();							// 机械手八二马达对应IO口初始化设置
	
	Init_USART1_Buffer();							// 初始化SUART1接收缓冲区结构体变量的参数
	// 初始化三个电机结构体变量的参数
	Init_Motor_Struct(1); Init_Motor_Struct(2); Init_Motor_Struct(3);
	// 电机A4988驱动器使能,
	Set_Motor_EN(MOTOX,motor1.enable); Set_Motor_EN(MOTOY,motor2.enable); Set_Motor_EN(MOTOZ,motor3.enable);
	// 电机方向设置为反转(0)
	Set_Motor_Dir(MOTOX,motor1.direction); Set_Motor_Dir(MOTOY,motor2.direction); Set_Motor_Dir(MOTOZ,motor3.direction);
	// 各个脉冲通道初始化后，默认关闭pwm脉冲输出使能
	TIM2_PWM_Config_Init(100,956); TIM3_PWM_Config_Init(100,956); TIM4_PWM_Config_Init(100,956);					
	
	delay_ms(1000);
	// 定时器1初始化,1ms	
	TIM1_Config_Init(1000,72);
}


/*******************************************************************************
* 函 数 名         : Step
* 函数功能         : 控制程序单步操作
* 输    入         : 无 
* 输    出         : 无
*******************************************************************************/
void Step()
{

	static int i=0,k=0;
	
#if USART1_DEBUG
	int j;
#endif	
	
	if(motor1.isResetPoint && motor2.isResetPoint)
	{
		TIM_Cmd(TIM1,DISABLE);
		motor1.totalTime = 0;	// 当一次单步操作完成，步进电机复位到原点后，
		motor2.totalTime = 0;	// 将上次移动所需totalTime清零，防止电机瞎几把乱动
		
		if(!rec_buffer.issbufNull)
		{
			// 需要更换印章(包括初始第一次更换印章操作，rec_buffer.isNeedChange[k]==11)
			if((rec_buffer.isNeedChange[k]==1) || (rec_buffer.isNeedChange[k]==11))
			{
				Annex_Seal_By(MOTOZ,1);						// Z轴电机下放
				// if(has seal) 打开机械手，放回印章;
				// if(no seal)  执行下面的代码;
				Get_Seal_By(1,3000,50);						// 二八步进电机顺时针旋转一圈，打开机械手手掌
				delay_s(1);
				Get_Seal_By(0,3000,50);						// 二八步进电机逆时针旋转一圈，收拢机械手手掌
															// 获取印章成功
				Annex_Seal_By(MOTOZ,0);	
				delay_s(1);
			}
		}
		else
		{
			k = 0;
		}

		
		// 如果 buf 数组接收到数据，不为空
		if(!rec_buffer.isbufNull)
		{
			// 确定所需时间,移动坐标
			Compute_Time_Of(motor1.id,rec_buffer.buf[i],rec_buffer.buf[i+1]);
			Compute_Time_Of(motor2.id,rec_buffer.buf[i+2],rec_buffer.buf[i+3]);

			// 串口缓冲区当前目标点坐标指令清0
			rec_buffer.buf[i] = 0; rec_buffer.buf[i+1] = 0; rec_buffer.buf[i+2] = 0; rec_buffer.buf[i+3] = 0;	
			// XY轴动了一次，代表着下一次检索 sbuf 和 isNeedChange 的下一个元素
			rec_buffer.sbuf[k] = 0;						// 串口缓冲区当前印章ID指令清0
			rec_buffer.isNeedChange[k] = 0;				// 串口缓冲区当前操作是否需要更换印章指令清0
			// 检查缓冲区数组是否为空
			rec_buffer.isbufNull = Check_Null_Buffer(rec_buffer.buf_id);
			rec_buffer.issbufNull = Check_Null_Buffer(rec_buffer.sbuf_id);
			k++;
			
#if USART1_DEBUG				
			for(j=0;j<17;j++)
			{
				if(j < 16)
				{
					USART_SendData(USART1,rec_buffer.buf[j]);
					// 等待发送缓存器清空（数据已经开始发送）
					while(USART_GetFlagStatus(USART1, USART_FLAG_TXE) == RESET);					
				}

				if(j == 16)
				{
					USART_SendData(USART1,'\n');
					while(USART_GetFlagStatus(USART1, USART_FLAG_TXE) == RESET);
				}
			}
#endif			
			
			Start_Motor_withS(MOTOX,0);
			Start_Motor_withS(MOTOY,1);
			motor1.nextMove = 0;					// 正向过程
			motor1.nextMove = 0;
			motor1.isResetPoint = 0;
			motor2.isResetPoint = 0;				// 电机移动偏离复位点，更新状态
			count_time = 0;
			i += 4;
			TIM_Cmd(TIM1,ENABLE);					// 当接收到并且已经传入给电机对应参数的指令，打开定时器
		}
		else
		{
			// 如果 buf 数组为空
			i = 0;
		}
	}
	
}

/*******************************************************************************
* 函 数 名         : Compute_Time_Of
* 函数功能         : 确定所需时间,移动坐标
* 输    入         : MOTOID(电机结构体变量参数的id值）
					H（buf 数组中坐标值的高8位）
					L（buf 数组中坐标值的低8位）
* 输    出         : 无
*******************************************************************************/
void Compute_Time_Of(unsigned char MOTOID,unsigned char H,unsigned char L)
{
	int qian,bai,shi,ge;
	qian = H/16;
	bai = H%16;
	shi = L/16;
	ge = L%16;
	switch(MOTOID)
	{
		case 1:
			motor1.totalTime = qian*1000 + bai*100 + shi*10 + ge*1;
			motor1.destn = motor1.totalTime * 50.0 / 60;				// 假定电机转速 500rpm，每转一圈前进 6mm
			break;
		case 2:
			motor2.totalTime = qian*1000 + bai*100 + shi*10 + ge*1;
			motor2.destn = motor2.totalTime * 50.0 / 60;
			break;
	}
}


