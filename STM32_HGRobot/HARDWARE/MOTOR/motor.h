/*
 * @ Author: luolichq
 * @ Description: 滑轨机器人步进电机结构体MOTOR类型初始化
 * @ More:
 * 		motor_camera	PULSE-		ENA-	DIR-		
 *						PI5			PD3		PD7
 *		motor_rail		PULSE-		ENA-	DIR-
 *						PI6			PD11	PF11
 */
#ifndef __MOTOR_H
#define __MOTOR_H

#include "stm32f4xx_hal.h"

// 枚举变量，电机ID号
typedef enum {
	MOTOR_RAIL = 0U,
	MOTOR_CAMERA
} MotorID;

// 枚举变量，电机方向
typedef enum {
	FORWARD = 0U,
	BACKWARD
} MotorDirection;

// 枚举变量，电机运动状态
typedef enum {
  RUNNING = 0U, 
  STOPPING = !RUNNING
} MotorStatus;

typedef enum {
	STOPTIM = 0U,
	STARTTIM = !STOPTIM
} MotorTIMStatus;

/*
 * 单个电机结构体类型 MOTOR 定义
 */
typedef __packed struct
{
	MotorID _mtId;				// 电机ID
	TIM_TypeDef* _mtTimer;		// 电机所用的定时器
	MotorStatus _mtState; 		// 电机运行状态
	MotorDirection _mtDir;		// 电机方向
	FunctionalState _mtEnable;	// 电机使能
	uint32_t _mtSpeed;			// 电机速度，改变psc,进而pwm脉冲频率，最后改变速度
	MotorTIMStatus _mtTIMState; // _mtTIMState = STOPTIM，电机带电保持；STARTTIM，正常运转
}MOTOR;

// 电机结构体变量的外向声明
extern volatile MOTOR motor_rail;
extern volatile MOTOR motor_camera;

// 单个步进电机，MOTOR类型结构体变量参数初始化
void MOTOR_Struct_Init(MotorID motorId);

#endif


