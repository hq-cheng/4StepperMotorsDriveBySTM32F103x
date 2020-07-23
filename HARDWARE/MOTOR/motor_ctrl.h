/*
 * @ Author: luolichq
 * @ Description: 滑轨机器人步进电机结构体MOTOR类型初始化
 * @ More:
 * 		motor_camera	PULSE-		ENA-	DIR-		
 *						PI5			PD3		PD7
 *		motor_rail		PULSE-		ENA-	DIR-
 *						PI6			PD11	PF11
 */
#ifndef __MOTOR_CTRL_H
#define __MOTOR_CTRL_H

#include "stm32f4xx_hal.h"
#include "motor_ioinit.h"
#include "motor.h"

extern volatile MOTOR motor_rail;
extern volatile MOTOR motor_camera;

// 单个步进电机控制的基本函数声明
void MOTOR_SetSpeed(uint32_t motorSpeed);
void MOTOR_SetENA(MOTOR motor, FunctionalState motorEna);
void MOTOR_SetDIR(MOTOR motor, MotorDirection motorDir); 
void MOTOR_SetTIMENA(MOTOR motor, MotorTIMStatus motorTIMState);
#endif


