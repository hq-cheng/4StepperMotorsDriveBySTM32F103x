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


// 初始化配置pwm输出到A4988步进电机驱动的STEP引脚
// 采用TIMx_CH2产生PWM脉冲信号，所以设置STEP引脚与默认输出端口连
//*使用的IO：  	电机用GPIOC口

//				(MotorX)A4988--> GPIOC0-GPIO1
//						GPIOA1 	--> STEP(TIM2)
//				
//				(MotorY)A4988--> GPIOC2-GPIO3
//						GPIOA7 	--> STEP(TM3)
//				
//				(MotorZ)A4988--> GPIOC4-GPIO5
//						GPIOB7 	--> STEP(TIM4)

#include "pwm.h"
#include "GPIO_Motor.h"

// 默认关闭了输出使能
// 占空比50%
/*****************************************************************************
						PWM   By   TIM2
******************************************************************************/

void TIM2_PWM_Config_Init(uint16_t arr, uint16_t psc)
{
	
	
	TIM_OCInitTypeDef TIM_OCInitStructure;
	TIM_TimeBaseInitTypeDef TIM_TimeBaseStructure;
	
	/* 1. 开启TIM2复用时钟功能，配置MotorX_STEP为复用推挽输出 */
	TIM2_GPIO_PWM_Init();
	
	/* 2. 如果有需要，设置TIMx_CHx的重映射到PXx上---->这里未设置，采用默认输出端口 */
	
	/* 3. 初始化TIMx，设置TIMx的ARR和PSC(参考定时器中断) */
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM2,ENABLE);	//使能TIM2时钟
	
	
	TIM_TimeBaseStructure.TIM_Period = arr-1;//自动重装载值，取值必须在0x0000~0xFFFF之间
	TIM_TimeBaseStructure.TIM_Prescaler = psc-1;//预分频值，+1为分频系数，取值必须在0x0000~0xFFFF之间	
	// 时钟频率=72MHZ/(时钟预分频+1),决定记一次数多少秒
	TIM_TimeBaseStructure.TIM_ClockDivision = 0; 				//时钟分割，TDTS = Tck_tim
	TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;	//向上计数模式	 
	TIM_TimeBaseInit(TIM2, &TIM_TimeBaseStructure);//根据TIM_TimeBaseInitStruct中指定的参数初始化TIMx的时间基数单位
	
	/* 4. 设置TIMx_CHx的PWM模式，使能TIMx的CHx（这里选择通道2）输出 */
	TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM1;             //TIM脉冲宽度调制模式1
	TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Disable; //比较输出使能，先暂时关闭输出使能，防止电机一开启就启动
	TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_High;     //输出极性:TIM输出比较极性高
	//TIM_OCInitStructure.TIM_Pulse =1800;               //设置待装入捕获比较寄存器的脉冲值,取值必须在0x0000~0xFFFF之间，占空比1800/3600
	TIM_OCInitStructure.TIM_Pulse =arr/2;
	TIM_OC2Init(TIM2, &TIM_OCInitStructure);          //根据TIM_OCInitStruct中指定的参数初始化外设TIMx
	
	// 这里设置了TIM_Pulse值，设置待装入捕获比较寄存器的脉冲值
	TIM_OC2PreloadConfig(TIM2, TIM_OCPreload_Enable); //使能TIMx在CCR2上的预装载寄存器
	
	/* 5. 使能TIMx定时器 */
	
	TIM_Cmd(TIM2,ENABLE);
	
}

/*****************************************************************************
						PWM   By   TIM3
******************************************************************************/




void TIM3_PWM_Config_Init(uint16_t arr, uint16_t psc)
{
	
	
	TIM_OCInitTypeDef TIM_OCInitStructure;
	TIM_TimeBaseInitTypeDef TIM_TimeBaseStructure;
	
	/* 1. 开启TIM3复用时钟功能，配置MotorX_STEP为复用推挽输出 */
	TIM3_GPIO_PWM_Init();
	
	/* 2. 如果有需要，设置TIMx_CHx的重映射到PXx上---->这里未设置，采用默认输出端口 */
	
	/* 3. 初始化TIMx，设置TIMx的ARR和PSC(参考定时器中断) */
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM3,ENABLE);	//使能TIM3时钟
	
	TIM_TimeBaseStructure.TIM_Period = arr-1;//自动重装载值，取值必须在0x0000~0xFFFF之间
	TIM_TimeBaseStructure.TIM_Prescaler = psc-1;//预分频值，+1为分频系数，取值必须在0x0000~0xFFFF之间		
	TIM_TimeBaseStructure.TIM_ClockDivision = 0; 				//时钟分割，TDTS = Tck_tim
	TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;	//向上计数模式	 
	TIM_TimeBaseInit(TIM3, &TIM_TimeBaseStructure);//根据TIM_TimeBaseInitStruct中指定的参数初始化TIMx的时间基数单位
	
	/* 4. 设置TIMx_CHx的PWM模式，使能TIMx的CHx（这里选择通道2）输出 */
	TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM1;             //TIM脉冲宽度调制模式1
	TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Disable; //比较输出使能
	TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_High;     //输出极性:TIM输出比较极性高

	TIM_OCInitStructure.TIM_Pulse = arr/2;
	TIM_OC2Init(TIM3, &TIM_OCInitStructure);          //根据TIM_OCInitStruct中指定的参数初始化外设TIMx
	
	// 这里设置了TIM_Pulse值，设置待装入捕获比较寄存器的脉冲值
	TIM_OC2PreloadConfig(TIM3, TIM_OCPreload_Enable); //使能TIMx在CCR2上的预装载寄存器
	
	/* 5. 使能TIMx定时器 */
	
	TIM_Cmd(TIM3,ENABLE);
	
}


/*****************************************************************************
						PWM   By   TIM4
******************************************************************************/

void TIM4_PWM_Config_Init(uint16_t arr, uint16_t psc)
{
	
	
	TIM_OCInitTypeDef TIM_OCInitStructure;
	TIM_TimeBaseInitTypeDef TIM_TimeBaseStructure;
	
	/* 1. 开启TIM4复用时钟功能，配置MotorX_STEP为复用推挽输出 */
	TIM4_GPIO_PWM_Init();
	
	/* 2. 如果有需要，设置TIMx_CHx的重映射到PXx上---->这里未设置，采用默认输出端口 */
	
	/* 3. 初始化TIMx，设置TIMx的ARR和PSC(参考定时器中断) */
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM4,ENABLE);	//使能TIM4时钟
	
	TIM_TimeBaseStructure.TIM_Period = arr-1;//自动重装载值，取值必须在0x0000~0xFFFF之间
	TIM_TimeBaseStructure.TIM_Prescaler = psc-1;//预分频值，+1为分频系数，取值必须在0x0000~0xFFFF之间
	TIM_TimeBaseStructure.TIM_ClockDivision = 0; 				//时钟分割，TDTS = Tck_tim
	TIM_TimeBaseStructure.TIM_CounterMode = TIM_CounterMode_Up;	//向上计数模式	 
	TIM_TimeBaseInit(TIM4, &TIM_TimeBaseStructure);//根据TIM_TimeBaseInitStruct中指定的参数初始化TIMx的时间基数单位
	
	/* 4. 设置TIMx_CHx的PWM模式，使能TIMx的CHx（这里选择通道2）输出 */
	TIM_OCInitStructure.TIM_OCMode = TIM_OCMode_PWM1;             //TIM脉冲宽度调制模式1
	TIM_OCInitStructure.TIM_OutputState = TIM_OutputState_Disable; //比较输出使能
	
	TIM_OCInitStructure.TIM_OCPolarity = TIM_OCPolarity_High;     //输出极性:TIM输出比较极性高
	

	TIM_OCInitStructure.TIM_Pulse = arr/2;
	TIM_OC2Init(TIM4, &TIM_OCInitStructure);          //根据TIM_OCInitStruct中指定的参数初始化外设TIMx
	
	
	// 这里设置了TIM_Pulse值，设置待装入捕获比较寄存器的脉冲值
	TIM_OC2PreloadConfig(TIM4, TIM_OCPreload_Enable); //使能TIMx在CCR2上的预装载寄存器
	
	/* 5. 使能TIMx定时器 */
	
	TIM_Cmd(TIM4,ENABLE);
	
}

