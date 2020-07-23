/*
 * luoliCHq
 */
 
#include "motor.h"

#undef NULL
#define NULL 0


/*******************************************************************************
* 函 数 名         : Init_Motor_Struct
* 函数功能         : XYZ轴，单个步进电机的结构体变量参数初始化
* 输    入         : motor.id（电机的id号，1->x, 2->y, 3->z） 
* 输    出         : 无
*******************************************************************************/
void Init_Motor_Struct(unsigned char MOTOID)
{
	volatile MOTOR_CONTROL* pmotor = NULL;
	switch(MOTOID)
	{
		case 1:
			pmotor = &motor1;
			pmotor->id = 1;
			pmotor->MOTOx = MOTOX;
			break;
		case 2:
			pmotor = &motor2;
			pmotor->id = 2;
			pmotor->MOTOx = MOTOY;
			break;
		case 3:
			pmotor = &motor3;
			pmotor->id = 3;
			pmotor->MOTOx = MOTOZ;

			break;
	}
	pmotor->direction = REVERSE;
	pmotor->speed = 956;
	pmotor->running = STOPPING;
	pmotor->enable = ENABLE;
	pmotor->destn = 0;
	pmotor->totalTime = 0;
	pmotor->isResetPoint = 1;
	pmotor->isDestn = 0;
	pmotor->nextMove = 0;

}



