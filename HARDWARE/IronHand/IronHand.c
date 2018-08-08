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

#include "IronHand.h"
#include "delay.h"



// 二八步进电机转动，机械手旋转（7.2°/Steps,50Steps/r）
void Get_Seal_By(u16 Dir,u16 Speed,u16 Steps)
{
	int i,k=0;
	for(k=0;k<Steps;k++)
	{
		for(i=0;i<8;i++)
		{
			if(Dir == 1)
			{//顺时针			
				Set_Motor_CWPort(i);
			}
			if(Dir == 0)
			{//逆时针
				Set_Motor_CCWPort(i);
			}
			delay_us(Speed);
		}		
	}

}

void Set_Motor_CWPort(int k)
{
	switch(k)
	{
		case 0:
			MotorH_BitA = 0;MotorH_BitB=1;MotorH_BitC=0;MotorH_BitD=1;
			break;
		case 1:
			MotorH_BitA = 0;MotorH_BitB=0;MotorH_BitC=0;MotorH_BitD=1;
			break;
		case 2:	
			MotorH_BitA = 1;MotorH_BitB=0;MotorH_BitC=0;MotorH_BitD=1;
			break;
		case 3:	
			MotorH_BitA = 1;MotorH_BitB=0;MotorH_BitC=0;MotorH_BitD=0;
			break;
		case 4:	
			MotorH_BitA = 1;MotorH_BitB=0;MotorH_BitC=1;MotorH_BitD=0;
			break;
		case 5:
			MotorH_BitA = 0;MotorH_BitB=0;MotorH_BitC=1;MotorH_BitD=0;
			break;
		case 6:	
			MotorH_BitA = 0;MotorH_BitB=1;MotorH_BitC=1;MotorH_BitD=0;
			break;
		case 7:	
			MotorH_BitA = 0;MotorH_BitB=1;MotorH_BitC=0;MotorH_BitD=0;
			break;
	}
}

void Set_Motor_CCWPort(int k)
{
	switch(k)
	{
		case 0:
			MotorH_BitA = 0;MotorH_BitB=1;MotorH_BitC=0;MotorH_BitD=0;
			break;
		case 1:
			MotorH_BitA = 0;MotorH_BitB=1;MotorH_BitC=1;MotorH_BitD=0;
			break;
		case 2:	
			MotorH_BitA = 0;MotorH_BitB=0;MotorH_BitC=1;MotorH_BitD=0;
			break;
		case 3:	
			MotorH_BitA = 1;MotorH_BitB=0;MotorH_BitC=1;MotorH_BitD=0;
			break;
		case 4:	
			MotorH_BitA = 1;MotorH_BitB=0;MotorH_BitC=0;MotorH_BitD=0;
			break;
		case 5:
			MotorH_BitA = 1;MotorH_BitB=0;MotorH_BitC=0;MotorH_BitD=1;
			break;
		case 6:	
			MotorH_BitA = 0;MotorH_BitB=0;MotorH_BitC=0;MotorH_BitD=1;
			break;
		case 7:	
			MotorH_BitA = 0;MotorH_BitB=1;MotorH_BitC=0;MotorH_BitD=1;
			break;
	}
}

