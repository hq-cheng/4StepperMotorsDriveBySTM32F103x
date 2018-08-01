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

// 自己测试/找bug的时候，设置为1
#define USART1_DEBUG 		0						// 串口测试，此时USART1_DEBUG=1，KEY_DEBUG=0
#define	KEY_DEBUG			0						// 按键模拟串口测试，此时USART1_DEBUG=0，KEY_DEBUG=1

void Main_Init(void);
void Step(void);

// 定时器相关变量
uint16_t count_time = 0;							// 定时器中断每间隔1ms触发，达到指定count值清零，电机变换转动方向

// 键盘相关变量
uint16_t Key_Flag = 0;

// 串口接收缓冲数组
#if KEY_DEBUG
	unsigned char buf[64] = {9,12,4,7,10,4,11,6};
#else
	volatile unsigned char buf[30];
#endif


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
	
#if KEY_DEBUG
#else
	USART1_Master_Init(9600);
#endif

#if USART1_DEBUG
	for(k=0;k<10;k++)
	{
		USART_SendData(USART1,buf[k]);
		// 等待发送缓存器清空（数据已经开始发送）
		while(USART_GetFlagStatus(USART1, USART_FLAG_TXE) == RESET);
	}

#endif

	while(1)
	{												// 主循环程序

#if KEY_DEBUG
		Key_Flag = Key8_scan();						// 检测按键
		switch(Key_Flag) {
			case 0:
				// do nothing
				break;
			case KEY_CC:
				Step();
				break;
		}
#else
		Step();
#endif
		
		if(motor1.isDestn && motor2.isDestn)
		{											// 到达了目的地
			TIM_Cmd(TIM1,DISABLE);
			delay_s(2);								// 执行盖章动作
			Start_Motor_withS(MOTOX,1);
			Start_Motor_withS(MOTOY,1);
			motor1.nextMove = 1;
			motor2.nextMove = 1;
			motor1.isDestn = 0;
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
	GPIO_IronHand_Init();
	
	// 初始化三个电机结构体变量的参数
	Init_Motor_Struct(1);
	Init_Motor_Struct(2);
	Init_Motor_Struct(3);
	
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
	static int i=0;
	
#if USART1_DEBUG
	USART_SendData(USART1,i);
	while(USART_GetFlagStatus(USART1, USART_FLAG_TXE) == RESET);
#endif
	
	if(motor1.isResetPoint && motor2.isResetPoint)
	{
		TIM_Cmd(TIM1,DISABLE);
		motor1.totalTime = 0;	// 当一次单步操作完成，步进电机复位到原点后，
		motor2.totalTime = 0;	// 将上次移动所需totalTime清零，防止电机瞎几把乱动
		if(buf[i]!=0 && buf[i+1]!=0)
		{
			// 确定电机移动坐标
			motor1.destn = buf[i];
			motor2.destn = buf[i+1];
			// 确定所需时间
			motor1.totalTime = buf[i];
			motor2.totalTime = buf[i+1];
#if KEY_DEBUG
#else			
			// 串口缓冲区当前指令清0
			buf[i] = 0;
			buf[i+1] = 0;
#endif		
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

































	

