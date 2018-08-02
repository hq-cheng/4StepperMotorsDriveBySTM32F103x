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



