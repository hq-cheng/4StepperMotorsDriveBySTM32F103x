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



