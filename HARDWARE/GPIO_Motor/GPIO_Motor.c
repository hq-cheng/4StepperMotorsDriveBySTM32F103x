/*
 *
 *  Copyright 2018，程豪琪，哈尔滨工业大学
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

//*使用的IO：  	电机用GPIOC口

//				(MotorX)A4988--> GPIOC0-GPIO1
//				GPIOC0 	--> ENABLE
//				GPIOA1 	--> STEP(TIM2)
//				GPIOC1	--> DIR
//				
//				(MotorY)A4988--> GPIOC2-GPIO3
//				GPIOC2 	--> ENABLE
//				GPIOA7 	--> STEP(TM3)
//				GPIOC3	--> DIR
//				
//				(MotorZ)A4988--> GPIOC4-GPIO5
//				GPIOC4 	--> ENABLE
//				GPIOB7 	--> STEP(TIM4)
//				GPIOC5	--> DIR

#include "GPIO_Motor.h"
#include "IronHand.h"

// 控制使能和方向的电机驱动器引脚
void GPIO_Motor_Init(void)
{
	GPIO_InitTypeDef GPIO_InitStructure;

	// 对应IO端口时钟使能
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOC,ENABLE);

	// 对于A4988驱动的步进电机，初始化DIR引脚与ENABLE引脚(ENABLE与DIR都在GPIOC口)
	GPIO_InitStructure.GPIO_Pin = MotorX_ENABLE|MotorX_DIR;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;	// 推挽输出
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOC,&GPIO_InitStructure);				
	GPIO_ResetBits(GPIOC,MotorX_ENABLE|MotorX_DIR);
	
	GPIO_InitStructure.GPIO_Pin = MotorY_ENABLE|MotorY_DIR;
	GPIO_Init(GPIOC,&GPIO_InitStructure);
	GPIO_ResetBits(GPIOC,MotorY_ENABLE|MotorY_DIR);
	
	GPIO_InitStructure.GPIO_Pin = MotorZ_ENABLE|MotorZ_DIR;
	GPIO_Init(GPIOC,&GPIO_InitStructure);
	GPIO_ResetBits(GPIOC,MotorZ_ENABLE|MotorZ_DIR);
}

// 输出pwm脉冲信号的电机驱动器引脚
void TIM2_GPIO_PWM_Init()
{
	GPIO_InitTypeDef GPIO_InitStructure;
	
    RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA, ENABLE);   	//使能GPIOA的时钟 
    //RCC_APB2PeriphClockCmd(RCC_APB2Periph_AFIO, ENABLE);    	//复用时钟不需要使能
	
	GPIO_InitStructure.GPIO_Pin = MotorX_STEP; //TIM2二通道PWM波形输出端口PA1
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;//复用推挽输出
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;//50MHz
	GPIO_Init(GPIOA,&GPIO_InitStructure);
}

void TIM3_GPIO_PWM_Init()
{
	GPIO_InitTypeDef GPIO_InitStructure;
	
    RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA, ENABLE);   	//使能GPIOA的时钟 
	
	GPIO_InitStructure.GPIO_Pin = MotorY_STEP; //TIM3二通道PWM波形输出端口PA7
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;//复用推挽输出
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;//50MHz
	GPIO_Init(GPIOA,&GPIO_InitStructure);
}

void TIM4_GPIO_PWM_Init()
{
	GPIO_InitTypeDef GPIO_InitStructure;
	
    RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB, ENABLE);   	//使能GPIOB的时钟 
	
	GPIO_InitStructure.GPIO_Pin = MotorZ_STEP; //TIM4二通道PWM波形输出端口PB7
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;//复用推挽输出
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;//50MHz
	GPIO_Init(GPIOB,&GPIO_InitStructure);
	
}

void GPIO_IronHand_Init() 
{
	GPIO_InitTypeDef GPIO_InitStructure;

	// 对应IO端口时钟使能
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOC,ENABLE);

	// 机械手GPIOC6(HAND_OPEN)，或GPIOC7(HAND_CLOSE)
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_6|GPIO_Pin_7;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;		// 推挽输出
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOC,&GPIO_InitStructure);				
	GPIO_ResetBits(GPIOC, GPIO_Pin_6|GPIO_Pin_7);
}

