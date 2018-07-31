#ifndef __TIMER1_H
#define __TIMER1_H

#include "sys.h"
#include "motor.h"

extern uint16_t count_time;

void TIM1_Config_Init(u16 arr,u16 psc);        	// Timer1初始化设置
void TIM1Nvic_Config_Init(void);     			// Timer1中断优先级设置

#endif



