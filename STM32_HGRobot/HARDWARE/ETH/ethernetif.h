#ifndef __ETHERNETIF_H__
#define __ETHERNETIF_H__

/* 包含头文件 ----------------------------------------------------------------*/
#include "stm32f4xx_hal.h"
#include "lwip/err.h"
#include "lwip/netif.h"

/* 类型定义 ------------------------------------------------------------------*/
/* 宏定义 --------------------------------------------------------------------*/
#define ETH_RCC_CLK_ENABLE()                __HAL_RCC_ETH_CLK_ENABLE()

#define ETH_GPIO_ClK_ENABLE()              {__HAL_RCC_GPIOA_CLK_ENABLE();__HAL_RCC_GPIOC_CLK_ENABLE();\
                                            __HAL_RCC_GPIOB_CLK_ENABLE();__HAL_RCC_GPIOG_CLK_ENABLE();}

#define GPIO_AFx_ETH                        GPIO_AF11_ETH

/* 扩展变量 ------------------------------------------------------------------*/
/* 函数声明 ------------------------------------------------------------------*/
err_t ethernetif_init(struct netif *netif);
void ethernetif_input(struct netif *netif);
void ethernetif_set_link(struct netif *netif);
void ethernetif_update_config(struct netif *netif);
void ethernetif_notify_conn_changed(struct netif *netif);

#endif


/******************* (C) COPYRIGHT 2015-2020 硬石嵌入式开发团队 *****END OF FILE****/

