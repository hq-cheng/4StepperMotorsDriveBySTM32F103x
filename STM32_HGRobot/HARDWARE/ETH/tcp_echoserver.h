#ifndef __TCP_ECHOSERVER_H__
#define __TCP_ECHOSERVER_H__

/* 包含头文件 ----------------------------------------------------------------*/
#include "stm32f4xx_hal.h"
#include "lwip/debug.h"
#include "lwip/stats.h"
#include "lwip/tcp.h"
#include "main.h"
#include "stdio.h"
#include "string.h"

/* 类型定义 ------------------------------------------------------------------*/
/* TCP服务器连接状态 */
enum tcp_echoserver_states
{
  ES_NONE = 0,
  ES_ACCEPTED,
  ES_RECEIVED,
  ES_CLOSING
};

/* LwIP回调函数使用结构体 */
struct tcp_echoserver_struct
{
  u8_t state;             /* 当前连接状态 */
  u8_t retries;
  struct tcp_pcb *pcb;    /* 指向当前的pcb */
  struct pbuf *p;         /* 指向当前接收或传输的pbuf */
};

/* 宏定义 --------------------------------------------------------------------*/
/* 扩展变量 ------------------------------------------------------------------*/
/* 函数声明 ------------------------------------------------------------------*/
void tcp_echoserver_connect(void);
void tcp_echoserver_close(void);



#endif /* __TCP_ECHOSERVER */

/******************* (C) COPYRIGHT 2015-2020 硬石嵌入式开发团队 *****END OF FILE****/

