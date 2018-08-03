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

#include "IronHand.h"
#include "delay.h"


// 机械手（二八马达）
// 打开机械手
void OpenHand(void)
{
	HAND_OPEN = 1;
	HAND_CLOSE = 0;
	delay_ms(50);	
	HAND_OPEN = 0;
	HAND_CLOSE = 0;
}
// 关闭机械手
void CloseHand(void)
{
	HAND_OPEN = 0;
	HAND_CLOSE = 1;
	delay_ms(50);
	HAND_OPEN = 0;
	HAND_CLOSE = 0;
}
// 保持机械手当前状态
void HoldHand(void)
{
	HAND_OPEN = 0;
	HAND_CLOSE = 0;
}


