#include "tcp_echoserver.h"

#if LWIP_TCP
/* 私有类型定义 --------------------------------------------------------------*/
struct tcp_pcb *tcp_echoserver_pcb;
struct tcp_echoserver_struct *tcp_echoserver_es;

/* 私有宏定义 ----------------------------------------------------------------*/
/* 私有变量 ------------------------------------------------------------------*/
uint8_t ServerIp[4];
uint8_t tcp_server_recvbuf[TCP_SERVER_RX_BUFSIZE];	

/* 扩展变量 ------------------------------------------------------------------*/
/* 私有函数原形 --------------------------------------------------------------*/
static err_t tcp_echoserver_accept(void *arg, struct tcp_pcb *newpcb, err_t err);
static err_t tcp_echoserver_recv(void *arg, struct tcp_pcb *tpcb, struct pbuf *p, err_t err);
static void tcp_echoserver_error(void *arg, err_t err);
static err_t tcp_echoserver_poll(void *arg, struct tcp_pcb *tpcb);
static err_t tcp_echoserver_sent(void *arg, struct tcp_pcb *tpcb, u16_t len);
static void tcp_echoserver_send(struct tcp_pcb *tpcb, struct tcp_echoserver_struct *es);
static void tcp_echoserver_connection_close(struct tcp_pcb *tpcb, struct tcp_echoserver_struct *es);


/**
  * 函数功能: TCP服务器测试
  * 输入参数: 无
  * 返 回 值: 无
  * 说    明: tcp_accept的回调函数里面包含了tcp_arg、tcp_recv、tcp_poll
  */
void tcp_echoserver_connect(void)
{
  /* 创建一个新的pcb */
  tcp_echoserver_pcb = tcp_new();
  
  if (tcp_echoserver_pcb != NULL)
  {
    printf("创建一个新的pcb\n");
    err_t err;
    
    /* 将本地的IP与指定的端口绑定在一起，TCP_SERVER_PORT即为指定的端口 */
    err = tcp_bind(tcp_echoserver_pcb, IP_ADDR_ANY, TCP_SERVER_PORT);
    if (err == ERR_OK)
    {
      printf("绑定pcb成功\n");
      /* tcp pcb进入监听状态 */
      tcp_echoserver_pcb = tcp_listen(tcp_echoserver_pcb);
      
      /* 初始化LwIP的tcp_accept的回调函数 */
      tcp_accept(tcp_echoserver_pcb, tcp_echoserver_accept);
    }
    else 
    {      
      /* 重新为pcb释放内存 */
      memp_free(MEMP_TCP_PCB, tcp_echoserver_pcb);
      printf("绑定pcb失败\n");
    }
  }
  else
  {
    printf("创建新的pcb失败\n");
  }
}

/**
  * 函数功能: 关闭TCP连接
  * 输入参数: 无
  * 返 回 值: 无
  * 说    明: 无
  */
void tcp_echoserver_close(void)
{
	tcp_echoserver_connection_close(tcp_echoserver_pcb,tcp_echoserver_es);
	printf("关闭tcp server\n");
}


/**
  * 函数功能: LwIP的accept回调函数
  * 输入参数: 无
  * 返 回 值: 无
  * 说    明: 无
  */
static err_t tcp_echoserver_accept(void *arg, struct tcp_pcb *newpcb, err_t err)
{
  err_t ret_err;
  struct tcp_echoserver_struct *es;

  LWIP_UNUSED_ARG(arg);
  LWIP_UNUSED_ARG(err);

  /* 设置新创建pcb的优先级 */
  tcp_setprio(newpcb, TCP_PRIO_MIN);

  /* 为维持pcb连接信息分配内存并返回分配结果 */
  es = (struct tcp_echoserver_struct *)mem_malloc(sizeof(struct tcp_echoserver_struct));
  tcp_echoserver_es=es;
  if (es != NULL)
  {
    es->state = ES_ACCEPTED;    //接收连接
    es->pcb = newpcb;
    es->retries = 0;
    es->p = NULL;
    
    /* 通过新分配的es结构体作为新pcb参数 */
    tcp_arg(newpcb, es);
    
    /* 初始化tcp_recv()的回调函数  */ 
    tcp_recv(newpcb, tcp_echoserver_recv);
    
    /* 初始化tcp_err()回调函数  */
    tcp_err(newpcb, tcp_echoserver_error);
    
    /* 初始化tcp_poll回调函数 */
    tcp_poll(newpcb, tcp_echoserver_poll, 1);

		ServerIp[0]=newpcb->remote_ip.addr&0xff; 		//IADDR4
		ServerIp[1]=(newpcb->remote_ip.addr>>8)&0xff;  	//IADDR3
		ServerIp[2]=(newpcb->remote_ip.addr>>16)&0xff; 	//IADDR2
		ServerIp[3]=(newpcb->remote_ip.addr>>24)&0xff; 	//IADDR1 
    printf("连接的电脑端IP为：%d %d %d %d\n",ServerIp[0],ServerIp[1],ServerIp[2],ServerIp[3]);
    ret_err = ERR_OK;
  }
  else
  {
    /*  关闭TCP链接 */
    tcp_echoserver_connection_close(newpcb, es);
    /* 返回内存错误 */
    ret_err = ERR_MEM;
  }
  return ret_err;  
}

/**
  * 函数功能: TCP接收数据回调函数
  * 输入参数: 无
  * 返 回 值: 无
  * 说    明: 无
  */
static err_t tcp_echoserver_recv(void *arg, struct tcp_pcb *tpcb, struct pbuf *p, err_t err)
{
  char *recdata=0;
  struct tcp_echoserver_struct *es;
  err_t ret_err;
  
  LWIP_ASSERT("arg != NULL",arg != NULL);
  
  es = (struct tcp_echoserver_struct *)arg;
  
  /* if we receive an empty tcp frame from client => close connection */
  if (p == NULL)
  {
    /* remote host closed connection */
    es->state = ES_CLOSING;
    if(es->p == NULL)
    {
       /* we're done sending, close connection */
       tcp_echoserver_connection_close(tpcb, es);
    }
    else
    {
      /* we're not done yet */
      /* acknowledge received packet */
      tcp_sent(tpcb, tcp_echoserver_sent);
      
      /* send remaining data*/
      tcp_echoserver_send(tpcb, es);
    }
    ret_err = ERR_OK;
  }   
  /* else : a non empty frame was received from client but for some reason err != ERR_OK */
  else if(err != ERR_OK)
  {
    /* free received pbuf*/
    if (p != NULL)
    {
      es->p = NULL;
      pbuf_free(p);
    }
    ret_err = err;
  }
  else if(es->state == ES_ACCEPTED)
  {
    /* 接收到第一个数据块 p->payload */
    es->state = ES_RECEIVED; // 第一次接收数据成功后，修改 es->state 状态为 ES_RECEIVED
    
    /* store reference to incoming pbuf (chain) */
    es->p = p;
    
    /* initialize LwIP tcp_sent callback function */
    tcp_sent(tpcb, tcp_echoserver_sent);
    
    recdata=(char *)malloc(p->len*sizeof(char));
    if(recdata!=NULL)
    {
	  /* 数据接收成功，且接收数据不为空 */
      memcpy(recdata,p->payload,p->len);
	  // 第一次接收到数据之后的对应操作
    }
    free(recdata);    
    
    /* send back the received data (echo) */
    tcp_echoserver_send(tpcb, es);
    
    ret_err = ERR_OK;
  }
  else if (es->state == ES_RECEIVED)
  {
    /* 从客户端接收到更多的数据，之前的数据已经处理了 */
    if(es->p == NULL)
    {
		/* 因为前一次收到的数据追加到pbuf链表末尾，是空的，需要申请内存空间进行处理 */
		es->p = p;
		recdata=(char *)malloc(p->len*sizeof(char));
		if(recdata!=NULL)
		{
			memcpy(recdata,p->payload,p->len);
			// 每次（第一次之后）接收到数据之后的对应操作
		}
		free(recdata);
    }
    else
    {
      struct pbuf *ptr;

      /* 创建一个新的pbuf连接到之前已经接收并处理的数据（链表）  */
      ptr = es->p;
      pbuf_chain(ptr,p);
    }
    ret_err = ERR_OK;
  }
  else if(es->state == ES_CLOSING)
  {
    /* odd case, remote side closing twice, trash data */
    tcp_recved(tpcb, p->tot_len);
    es->p = NULL;
    pbuf_free(p);
    ret_err = ERR_OK;
  }
  else
  {
    /* unkown es->state, trash data  */
    tcp_recved(tpcb, p->tot_len);
    es->p = NULL;
    pbuf_free(p);
    ret_err = ERR_OK;
  }
  return ret_err;
}

/**
  * 函数功能: TCP错误回调函数
  * 输入参数: 无
  * 返 回 值: 无
  * 说    明: 无
  */
static void tcp_echoserver_error(void *arg, err_t err)
{
  struct tcp_echoserver_struct *es;

  LWIP_UNUSED_ARG(err);

  es = (struct tcp_echoserver_struct *)arg;
  if (es != NULL)
  {
    /*  free es structure */
    mem_free(es);
  }
}


/**
  * 函数功能: TCP_poll回调函数
  * 输入参数: 无
  * 返 回 值: 无
  * 说    明: 无
  */
static err_t tcp_echoserver_poll(void *arg, struct tcp_pcb *tpcb)
{
  err_t ret_err;
  struct tcp_echoserver_struct *es;

  es = (struct tcp_echoserver_struct *)arg;
  if (es != NULL)
  {
    if (es->p != NULL)
    {
      tcp_sent(tpcb, tcp_echoserver_sent);
      /* there is a remaining pbuf (chain) , try to send data */
      tcp_echoserver_send(tpcb, es);
    }
    else
    {
      /* no remaining pbuf (chain)  */
      if(es->state == ES_CLOSING)
      {
        /*  close tcp connection */
        tcp_echoserver_connection_close(tpcb, es);
      }
    }
    ret_err = ERR_OK;
  }
  else
  {
    /* nothing to be done */
    tcp_abort(tpcb);
    ret_err = ERR_ABRT;
  }
  return ret_err;
}

/**
  * 函数功能: TCP发送回调函数
  * 输入参数: 无
  * 返 回 值: 无
  * 说    明: 无
  */
static err_t tcp_echoserver_sent(void *arg, struct tcp_pcb *tpcb, u16_t len)
{
  struct tcp_echoserver_struct *es;

  LWIP_UNUSED_ARG(len);

  es = (struct tcp_echoserver_struct *)arg;
  es->retries = 0;
  
  if(es->p != NULL)
  {
    /* still got pbufs to send */
    tcp_sent(tpcb, tcp_echoserver_sent);
    tcp_echoserver_send(tpcb, es);
  }
  else
  {
    /* if no more data to send and client closed connection*/
    if(es->state == ES_CLOSING)
      tcp_echoserver_connection_close(tpcb, es);
  }
  return ERR_OK;
}

/**
  * 函数功能: TCP发送数据函数
  * 输入参数: 无
  * 返 回 值: 无
  * 说    明: 无
  */
static void tcp_echoserver_send(struct tcp_pcb *tpcb, struct tcp_echoserver_struct *es)
{
  struct pbuf *ptr;
  err_t wr_err = ERR_OK;
 
  while ((wr_err == ERR_OK) &&
         (es->p != NULL) && 
         (es->p->len <= tcp_sndbuf(tpcb)))
  {
    
    /* get pointer on pbuf from es structure */
    ptr = es->p;

    /* enqueue data for transmission */
    wr_err = tcp_write(tpcb, ptr->payload, ptr->len, 1);

    if (wr_err == ERR_OK)
    {
      u16_t plen;
      u8_t freed;
      
      plen = ptr->len;
     
      /* continue with next pbuf in chain (if any) */
      es->p = ptr->next;
      
      if(es->p != NULL)
      {
        /* increment reference count for es->p */
        pbuf_ref(es->p);
      }
      
     /* chop first pbuf from chain */
      do
      {
        /* try hard to free pbuf */
        freed = pbuf_free(ptr);
      }
      while(freed == 0);
     /* we can read more data now */
     tcp_recved(tpcb, plen);
   }
   else if(wr_err == ERR_MEM)
   {
      /* we are low on memory, try later / harder, defer to poll */
     es->p = ptr;
     tcp_output(tpcb);   
   }
   else
   {
     /* other problem ?? */
   }
  }
}

/**
  * 函数功能: 关闭TCP连接函数
  * 输入参数: 无
  * 返 回 值: 无
  * 说    明: 无
  */
static void tcp_echoserver_connection_close(struct tcp_pcb *tpcb, struct tcp_echoserver_struct *es)
{
  
  /* remove all callbacks */
  tcp_arg(tpcb, NULL);
  tcp_sent(tpcb, NULL);
  tcp_recv(tpcb, NULL);
  tcp_err(tpcb, NULL);
  tcp_poll(tpcb, NULL, 0);
  
  /* delete es structure */
  if (es != NULL)
  {
    mem_free(es);
  }  
  
  /* close tcp connection */
  tcp_close(tpcb);

}

#endif /* LWIP_TCP */

