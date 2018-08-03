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



