/*
 *
 *  Copyright 2018，程豪琪，哈尔滨工业大学（深圳）
 *  All Rights Reserved.
 */
/*
 * *******************************郑重声明****************************************
 
 * 该程序为本人所写，付出了大量的精力，现将其公开出来供大家参考学习；
 * 任何个人和组织不得未经授权将此程序转载，或用于商业行为！
 * 
 * 由于本人水平有限，程序难免出现错误，可以通过下面的联系方式联系本人
 * 谢谢你的指正！
 *
 * 邮箱：18s153717@stu.hit.edu.cn
 * github：https://github.com/clearcumt
 * 博客：https://www.cnblogs.com/loveclear/
 * 
 * ******************************************************************************
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
#if USART1_DEBUG
	int k;
#endif
	Main_Init();
	TIM_Cmd(TIM1,DISABLE);
	USART1_Master_Init(9600);
	delay_s(10);

#if USART1_DEBUG
	for(k=0;k<4;k++)
	{
		USART_SendData(USART1,rec_buffer.sbuf[k]);
		// 等待发送缓存器清空（数据已经开始发送）
		while(USART_GetFlagStatus(USART1, USART_FLAG_TXE) == RESET);
	}
	for(k=0;k<4;k++)
	{
		USART_SendData(USART1,rec_buffer.isNeedChange[k]);
		// 等待发送缓存器清空（数据已经开始发送）
		while(USART_GetFlagStatus(USART1, USART_FLAG_TXE) == RESET);
	}
	for(k=0;k<8;k++)
	{
		USART_SendData(USART1,rec_buffer.buf[k]);
		// 等待发送缓存器清空（数据已经开始发送）
		while(USART_GetFlagStatus(USART1, USART_FLAG_TXE) == RESET);
	}	

#endif

	while(1)
	{												// 主循环程序
		
		Step();										// 如果电机在复位点，且串口缓冲区不为0的时候，开始一步操作
		if(motor1.isDestn && motor2.isDestn)
		{											// 到达了目的地
			TIM_Cmd(TIM1,DISABLE);
			Annex_Seal_By(MOTOZ,1);					// 执行盖章动作,按下印章
			delay_s(1);
			Annex_Seal_By(MOTOZ,0);					// 执行盖章动作,抬起印章
			Start_Motor_withS(MOTOX,1);
			Start_Motor_withS(MOTOY,1);
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
	
	// 初始化三个电机结构体变量的参数
	Init_Motor_Struct(1);
	Init_Motor_Struct(2);
	Init_Motor_Struct(3);
	
	Init_USART1_Buffer();							// 初始化SUART1接收缓冲区结构体变量的参数
	
	// 电机A4988驱动器使能,
	Set_Motor_EN(MOTOX,motor1.enable);
	Set_Motor_EN(MOTOY,motor2.enable);
	Set_Motor_EN(MOTOZ,motor3.enable);
	
	// 电机方向设置为反转(0)
	Set_Motor_Dir(MOTOX,motor1.direction);
	Set_Motor_Dir(MOTOY,motor2.direction);
	Set_Motor_Dir(MOTOZ,motor3.direction);
	
	// 各个脉冲通道初始化后，默认关闭pwm脉冲输出使能
	TIM2_PWM_Config_Init(100,956);					// MotorX
	TIM3_PWM_Config_Init(100,956);					// MotorY	
	TIM4_PWM_Config_Init(100,956);					// MotorZ
	
	// 定时器1初始化
	delay_ms(1000);	
	TIM1_Config_Init(1000,72);						// 1ms
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
	int j;
	
	if(motor1.isResetPoint && motor2.isResetPoint)
	{

#if USART1_DEBUG
		for(j=0;j<4;j++)
		{
			USART_SendData(USART1,rec_buffer.sbuf[j]);
			// 等待发送缓存器清空（数据已经开始发送）
			while(USART_GetFlagStatus(USART1, USART_FLAG_TXE) == RESET);
		}
		for(j=0;j<4;j++)
		{
			USART_SendData(USART1,rec_buffer.isNeedChange[j]);
			// 等待发送缓存器清空（数据已经开始发送）
			while(USART_GetFlagStatus(USART1, USART_FLAG_TXE) == RESET);
		}
#endif
		
		TIM_Cmd(TIM1,DISABLE);
		motor1.totalTime = 0;	// 当一次单步操作完成，步进电机复位到原点后，
		motor2.totalTime = 0;	// 将上次移动所需totalTime清零，防止电机瞎几把乱动
		
		if(rec_buffer.sbuf[k] != 0)
		{
			// 需要更换印章(包括初始第一次更换印章操作，rec_buffer.isNeedChange[k]==11)
			if((rec_buffer.isNeedChange[k]==1) || (rec_buffer.isNeedChange[k]==11))
			{
				Annex_Seal_By(MOTOZ,1);						// Z轴电机下放
				// if(has seal) 打开机械手，放回印章;
				// if(no seal)  执行下面的代码;
				OpenHand();
				delay_s(1);
				CloseHand();
				HoldHand();									// 获取印章成功
				Annex_Seal_By(MOTOZ,0);	
				delay_s(1);
			}
		}
		else
		{
			k = 0;
		}
		
		if(rec_buffer.buf[i]!=0 && rec_buffer.buf[i+1]!=0)
		{
			// 确定电机移动坐标
			motor1.destn = rec_buffer.buf[i];
			motor2.destn = rec_buffer.buf[i+1];
			// 确定所需时间
			motor1.totalTime = rec_buffer.buf[i];
			motor2.totalTime = rec_buffer.buf[i+1];
			// 串口缓冲区当前目标点坐标指令清0
			rec_buffer.buf[i] = 0;
			rec_buffer.buf[i+1] = 0;
			
			// XY轴动了一次，代表着下一次检索 sbuf 和 isNeedChange 的下一个元素
			rec_buffer.sbuf[k] = 0;						// 串口缓冲区当前印章ID指令清0
			rec_buffer.isNeedChange[k] = 0;				// 串口缓冲区当前操作是否需要更换印章指令清0
			k++;
			
			Start_Motor_withS(MOTOX,0);
			Start_Motor_withS(MOTOY,0);
			motor1.nextMove = 0;					// 正向过程
			motor1.nextMove = 0;
			motor1.isResetPoint = 0;
			motor2.isResetPoint = 0;				// 电机移动偏离复位点，更新状态
			count_time = 0;
			TIM_Cmd(TIM1,ENABLE);					// 当接收到并且已经传入给电机对应参数的指令，打开定时器
			i += 2;
		}
		else
		{
			i = 0;
		}
	}
	
}



































	

