#include "stm32f4xx_hal.h"
#include "lwip/dhcp.h"
#include "app_ethernet.h"
/* 私有类型定义 --------------------------------------------------------------*/
/* 私有宏定义 ----------------------------------------------------------------*/
/* 私有变量 ------------------------------------------------------------------*/
#ifdef USE_DHCP
#define MAX_DHCP_TRIES  4
uint32_t DHCPfineTimer = 0;
__IO uint8_t DHCP_state = DHCP_OFF;
__IO uint8_t DHCP_flag=0;
#endif

/* 扩展变量 ------------------------------------------------------------------*/
/* 私有函数原形 --------------------------------------------------------------*/
/* 函数体 --------------------------------------------------------------------*/
/**
  * 函数功能: TCP服务器测试（串口查看相关网络接口配置）
  * 输入参数: 无
  * 返 回 值: 无
  * 说    明: 无
  */
void User_notification(struct netif *netif) 
{
  if (netif_is_up(netif))
 {
#ifdef USE_DHCP
    /* Update DHCP state machine */
    DHCP_state = DHCP_START;
#else
    uint8_t iptxt[20];
    sprintf((char *)iptxt, "%s", ip4addr_ntoa((const ip4_addr_t *)&netif->ip_addr));
    printf ("Static IP address: %s\n", iptxt);

#endif /* USE_DHCP */
 }
 else
  {  
#ifdef USE_DHCP
    /* Update DHCP state machine */
    DHCP_state = DHCP_LINK_DOWN;
#endif  /* USE_DHCP */
   printf ("The network cable is not connected \n");
  } 
}

/**
  * 函数功能: 对链接状态进行通知，用于Netif_Config配置网口时的回调（串口）
  * 输入参数: 无
  * 返 回 值: 无
  * 说    明: 无
  */
void ethernetif_notify_conn_changed(struct netif *netif)
{
#ifndef USE_DHCP
  ip_addr_t ipaddr;
  ip_addr_t netmask;
  ip_addr_t gw;
#endif
  
  if(netif_is_link_up(netif))
  {
    printf ("The network cable is now connected \n");
    
#ifdef USE_DHCP
    /* Update DHCP state machine */
    DHCP_state = DHCP_START;
#else
    IP_ADDR4(&ipaddr, IP_ADDR0, IP_ADDR1, IP_ADDR2, IP_ADDR3);
    IP_ADDR4(&netmask, NETMASK_ADDR0, NETMASK_ADDR1 , NETMASK_ADDR2, NETMASK_ADDR3);
    IP_ADDR4(&gw, GW_ADDR0, GW_ADDR1, GW_ADDR2, GW_ADDR3);  
    
    netif_set_addr(netif, &ipaddr , &netmask, &gw);  
     
    uint8_t iptxt[20];
    sprintf((char *)iptxt, "%s", ip4addr_ntoa((const ip4_addr_t *)&netif->ip_addr));
    printf ("Static IP address: %s\n", iptxt);
#endif /* USE_DHCP */   
    
    /* When the netif is fully configured this function must be called.*/
    netif_set_up(netif);     
  }
  else
  {
#ifdef USE_DHCP
    /* Update DHCP state machine */
    DHCP_state = DHCP_LINK_DOWN;
#endif /* USE_DHCP */
    
    /*  When the netif link is down this function must be called.*/
    netif_set_down(netif);
    
    printf ("The network cable is not connected \n");
    
  }
}

#ifdef USE_DHCP
/**
  * 函数功能: DHCP获取函数（DHCP获取IP地址）
  * 输入参数: 无
  * 返 回 值: 无
  * 说    明: 无
  */
void DHCP_Process(struct netif *netif)
{
  ip_addr_t ipaddr;
  ip_addr_t netmask;
  ip_addr_t gw;
  struct dhcp *dhcp;   
#ifdef USE_LCD 
  uint8_t iptxt[20];
#endif
  
  switch (DHCP_state)
  {
    case DHCP_START:
    {
      ip_addr_set_zero_ip4(&netif->ip_addr);
      ip_addr_set_zero_ip4(&netif->netmask);
      ip_addr_set_zero_ip4(&netif->gw);
      DHCP_state = DHCP_WAIT_ADDRESS;
      dhcp_start(netif);
      printf ("  State: Looking for DHCP server ...\n");
    }
    break;
    
  case DHCP_WAIT_ADDRESS:
    {
      if (dhcp_supplied_address(netif)) 
      {
        DHCP_state = DHCP_ADDRESS_ASSIGNED;
        
        sprintf((char *)iptxt, "%s", ip4addr_ntoa((const ip4_addr_t *)&netif->ip_addr));
        printf ("IP address assigned by a DHCP server: %s\n", iptxt); 
        DHCP_flag=1;
      }
      else
      {
        dhcp = (struct dhcp *)netif_get_client_data(netif, LWIP_NETIF_CLIENT_DATA_INDEX_DHCP);
    
        /* DHCP timeout */
        if (dhcp->tries > MAX_DHCP_TRIES)
        {
          DHCP_state = DHCP_TIMEOUT;
          
          /* Stop DHCP */
          dhcp_stop(netif);
          
          /* Static address used */
          IP_ADDR4(&ipaddr, IP_ADDR0 ,IP_ADDR1 , IP_ADDR2 , IP_ADDR3 );
          IP_ADDR4(&netmask, NETMASK_ADDR0, NETMASK_ADDR1, NETMASK_ADDR2, NETMASK_ADDR3);
          IP_ADDR4(&gw, GW_ADDR0, GW_ADDR1, GW_ADDR2, GW_ADDR3);
          netif_set_addr(netif, &ipaddr, &netmask, &gw);
              
          sprintf((char *)iptxt, "%s", ip4addr_ntoa((const ip4_addr_t *)&netif->ip_addr));
          printf ("DHCP Timeout !! \n");
          printf ("Static IP address: %s\n", iptxt);   
        }
      }
    }
    break;
  case DHCP_LINK_DOWN:
    {
      /* Stop DHCP */
      dhcp_stop(netif);
      DHCP_state = DHCP_OFF; 
    }
    break;
  default: break;
  }
}

/**
  * 函数功能: DHCP轮询
  * 输入参数: 无
  * 返 回 值: 无
  * 说    明: 不断调用DHCP_Process函数DHCP功能获取IP
  */
void DHCP_Periodic_Handle(struct netif *netif)
{  
  /* Fine DHCP periodic process every 500ms */
  if (HAL_GetTick() - DHCPfineTimer >= DHCP_FINE_TIMER_MSECS)
  {
    DHCPfineTimer =  HAL_GetTick();
    /* process DHCP state machine */
    DHCP_Process(netif);    
  }
}
#endif
