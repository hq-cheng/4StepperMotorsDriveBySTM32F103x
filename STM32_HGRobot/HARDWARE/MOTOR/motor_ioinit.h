/*
 * @ Author: luolichq
 * @ Description: 滑轨机器人步进电机结构体MOTOR类型初始化
 * @ More:
 * 		motor_camera	PULSE-		ENA-	DIR-		
 *						PI5			PD3		PD7
 *		motor_rail		PULSE-		ENA-	DIR-
 *						PI6			PD11	PF11
 */
#ifndef __MOTOR_IOINIT_H
#define __MOTOR_IOINIT_H

#include "stm32f4xx_hal.h"

extern TIM_HandleTypeDef tim8_handler;

void MOTOR_CTRL_GPIO_Init(void);
void MOTOR_PULSE_GPIO_Init(void);
void MOTOR_TIM8PWM_Init(uint32_t arr, uint32_t psc);
                        
#endif


