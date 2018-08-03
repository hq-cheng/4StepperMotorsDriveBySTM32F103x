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

// 缓冲区数组大小
#define BUF_MAXSIZE				64
#define SBUF_MAXSIZE			32
#define	ISNC_MAXSIZE			32

// USART1接收缓冲区结构体声明
typedef __packed struct {
	unsigned char buf_id;
	unsigned char buf[BUF_MAXSIZE];
	int buf_index;
	unsigned char isbufNull;
	unsigned char sbuf_id;
	unsigned char sbuf[SBUF_MAXSIZE];
	int sbuf_index;
	unsigned char issbufNull;
	unsigned char isnc_id;
	unsigned char isNeedChange[ISNC_MAXSIZE];
	int isnc_index;
	unsigned char isisncNull;
}REC_BUFFER;



void GPIO_USART1_Init(void);
void USART1Nvic_Config_Init(void);
void USART1_Master_Init(u32 bound);
void Init_USART1_Buffer(void);
unsigned char Check_Null_Buffer(unsigned char BUF_ID);

// USART1接收缓冲区外向声明
//extern volatile unsigned char buf[30];
extern volatile REC_BUFFER rec_buffer;
#endif

