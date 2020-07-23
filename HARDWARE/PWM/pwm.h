/*
 * luoliCHq
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

#ifndef __PWM_H
#define __PWM_H


#include "sys.h"
#include "GPIO_Motor.h"


#define OC1_ENR	((uint16_t)0)		// 选择比较输出使能通道
#define OC2_ENR	((uint16_t)4)
#define OC3_ENR	((uint16_t)8)
#define OC4_ENR	((uint16_t)12)



void TIM2_PWM_Config_Init(uint16_t arr, uint16_t psc);
void TIM3_PWM_Config_Init(uint16_t arr, uint16_t psc);
void TIM4_PWM_Config_Init(uint16_t arr, uint16_t psc);


#endif


