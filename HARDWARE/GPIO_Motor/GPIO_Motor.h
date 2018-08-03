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


#ifndef __GPIO_MOTOR_H
#define __GPIO_MOTOR_H

#include "sys.h"


#define MotorX_ENABLE 	GPIO_Pin_0			// 低电平使能
#define MotorX_STEP 	GPIO_Pin_1
#define MotorX_DIR		GPIO_Pin_1

#define MotorY_ENABLE 	GPIO_Pin_2			// 低电平使能
#define MotorY_STEP 	GPIO_Pin_7
#define MotorY_DIR		GPIO_Pin_3

#define MotorZ_ENABLE 	GPIO_Pin_4			// 低电平使能
#define MotorZ_STEP 	GPIO_Pin_7
#define MotorZ_DIR		GPIO_Pin_5


#define MotorX_DIR_Bit PCout(1)
#define MotorY_DIR_Bit PCout(3)
#define MotorZ_DIR_Bit PCout(5)


void GPIO_Motor_Init(void);	// 电机引脚对应GPIO口初始化
void TIM2_GPIO_PWM_Init(void);
void TIM3_GPIO_PWM_Init(void);
void TIM4_GPIO_PWM_Init(void);
void GPIO_IronHand_Init(void); 

#endif

