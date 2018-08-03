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
 
 
#ifndef __MOTOR_CTRL_H
#define __MOTOR_CTRL_H

#include "sys.h"
#include "delay.h"
#include "GPIO_Motor.h"
#include "IronHand.h"
#include "pwm.h"
#include "motor.h"



void Set_Motor_EN(TIM_TypeDef* MOTOx,FunctionalState NewState);
void Set_Motor_Dir(TIM_TypeDef* MOTOx,unsigned char Dir);
void Start_Motor_withS(TIM_TypeDef* MOTOx, unsigned char Dir);
void Stop_Motor_withS(TIM_TypeDef* MOTOx);
void Change_Motor_Dir(TIM_TypeDef* MOTOx,unsigned char Dir);
uint16_t Get_Motor_RunningState(TIM_TypeDef* MOTOx);
void TIM_OCENSet(TIM_TypeDef* MOTOx, uint16_t OCx_ENR,FunctionalState NewState);
void Set_Motor_Speed(TIM_TypeDef* MOTOx,uint16_t psc);
void Annex_Seal_By(TIM_TypeDef* MOTOx, unsigned char Dir);
#endif



