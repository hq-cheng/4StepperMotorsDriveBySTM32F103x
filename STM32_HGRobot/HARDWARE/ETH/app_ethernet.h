#ifndef __APP_ETHERNET_H
#define __APP_ETHERNET_H

/* 包含头文件 ----------------------------------------------------------------*/
#include "lwip/netif.h"
#include "main.h"
   
/* 类型定义 ------------------------------------------------------------------*/
/* 宏定义 --------------------------------------------------------------------*/
#define DHCP_OFF                   (uint8_t) 0
#define DHCP_START                 (uint8_t) 1
#define DHCP_WAIT_ADDRESS          (uint8_t) 2
#define DHCP_ADDRESS_ASSIGNED      (uint8_t) 3
#define DHCP_TIMEOUT               (uint8_t) 4
#define DHCP_LINK_DOWN             (uint8_t) 5
   
/* 扩展变量 ------------------------------------------------------------------*/
/* 函数声明 ------------------------------------------------------------------*/

void User_notification(struct netif *netif);
#ifdef USE_DHCP
void DHCP_Process(struct netif *netif);
void DHCP_Periodic_Handle(struct netif *netif);
#endif  


#endif /* __APP_ETHERNET_H */


/******************* (C) COPYRIGHT 2015-2020 硬石嵌入式开发团队 *****END OF FILE****/

