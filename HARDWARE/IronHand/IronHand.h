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

//				机械手（伺服马达）控制信号使用GPIOB口
//				GPIOB8	--> IRONHAND(TIM4 CH3)

#ifndef __IRONHAND_H
#define __IRONHAND_H

#include "sys.h"

#define HAND_OPEN 	PCout(6)
#define HAND_CLOSE 	PCout(7)

void OpenHand(void);
void CloseHand(void);
void HoldHand(void);

#endif



