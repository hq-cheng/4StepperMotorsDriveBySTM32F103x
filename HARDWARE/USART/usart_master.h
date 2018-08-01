#ifndef __USART_MASTER_H
#define __USART_MASTER_H

#include "sys.h"

// 如果是多处理器通信的话，值为1
// 普通收发模式，值为0
#define IS_MUTI_COMUNICATION	0

// 串口通信对应引脚宏定义
#define MASTER_CTRL				GPIOA
#define MASTER_TXD 				GPIO_Pin_9
#define MASTER_RXD 				GPIO_Pin_10

// 主机地址值
#define MASTER_ADDR				0x1

void GPIO_USART1_Init(void);
void USART1Nvic_Config_Init(void);
void USART1_Master_Init(u32 bound);

extern volatile unsigned char buf[30];

#endif

