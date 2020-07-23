/*
 * @ Author: luolichq
 * @ Description: 滑轨机器人步进电机结构体MOTOR类型初始化
 * @ More:
 * 		motor_camera	PULSE-		ENA-	DIR-		
 *						PI5			PD3		PD7
 *		motor_rail		PULSE-		ENA-	DIR-
 *						PI6			PD11	PF11
 */
#include "motor.h"

#undef NULL
#define NULL 0

/*******************************************************************************
* 函 数 名         : Init_Motor_Struct
* 函数功能         : 单个步进电机，MOTOR类型结构体变量参数初始化
* 输    入         : motorId
* 输    出         : 无
*******************************************************************************/
void MOTOR_Struct_Init(MotorID motorId)
{
	volatile MOTOR* pmotor = NULL;
	switch(motorId) 
	{
		case MOTOR_RAIL:
			pmotor = &motor_rail;
			pmotor->_mtId = MOTOR_RAIL;
			pmotor->_mtDir = FORWARD;
			pmotor->_mtTIMState = STARTTIM;
			break;
		case MOTOR_CAMERA:
			pmotor = &motor_camera;
			pmotor->_mtId = MOTOR_CAMERA;
			pmotor->_mtDir = FORWARD;
			pmotor->_mtTIMState = STARTTIM;
			break;
	}
	pmotor->_mtTimer = TIM8;
	pmotor->_mtEnable = DISABLE;
	pmotor->_mtState = STOPPING;
	pmotor->_mtSpeed = 956;
} 

