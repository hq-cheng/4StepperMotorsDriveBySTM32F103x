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

//				机械手（二八步进电机）控制信号使用GPIOC口
//				GPIOC6	--> MotorH_A
//				GPIOC7	--> MotorH_B	
//				GPIOC8	--> MotorH_C
//				GPIOC9	--> MotorH_D

#ifndef __IRONHAND_H
#define __IRONHAND_H

#include "sys.h"

#define MotorH_BitA PCout(6)
#define MotorH_BitB PCout(7)
#define MotorH_BitC PCout(8)
#define MotorH_BitD PCout(9)

void Get_Seal_By(u16 Dir,u16 Speed,u16 Steps);
void Set_Motor_CWPort(int k);
void Set_Motor_CCWPort(int k);

#endif



