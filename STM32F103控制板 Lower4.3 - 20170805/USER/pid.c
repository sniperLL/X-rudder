#include "pid.h"
#include "stm32f10x.h"

struct _pid
{
	float setSpeed;
	float actualSpeed;
	float actualSpeedPwm;
	float err;
	float err_next;
	float kp,ki,kd;
	float err_last;
} pid;

//�ṹ�帳ֵ��������ʶ�ֱ���ö�����������ʽ��PID
float setSpeed = 0.0;
float actualSpeed = 0.0;
float actualSpeedPwm = 0.0;
float err = 0.0;
float err_next = 0.0;
float kp = 10,ki = 3,kd = 2;
float err_last = 0.0;

void PidInit(void)
{
	pid.setSpeed = 45.0;
	pid.actualSpeed = 0.0;
	pid.err = 0.0;
	pid.err_last = 0.0;
	pid.err_next = 0.0;
	//��Ҫ����ʵ�ʵ�������
	pid.kp = 0;
	pid.ki = 0;
	pid.kd = 0;
}
//�˺�����ʱ����
float MotorPid(float speed)
{
	float incrementSpeed = 0.0;
	pid.setSpeed = speed;
	pid.err = pid.setSpeed - pid.actualSpeed;
	incrementSpeed = pid.kp * (pid.err - pid.err_next) + pid.ki * pid.err + pid.kd * (pid.err - 2 * pid.err_next + pid.err_last);
	pid.actualSpeed += incrementSpeed;
	//����ʵ�ʵ��ٶȺ�PWM�Ķ�Ӧ����ת������������ٶȵ�PWMֵ
//	float speedToPwm = 0.0;
	pid.err_last = pid.err_next;
	pid.err_next = pid.err;
//	return speedToPwm;
	return pid.actualSpeed;
}

/*************************************************************
****�������ƣ�MotorPidContorl
****����������speedErr  ����ٶȲ�
****�������ܣ����õ���ٶȲ�ֵ��ȡ��������PWMֵ���Ƶ��ת��
*************************************************************/
float MotorPidContorl(float speedErr)
{
	float incrementSpeedPwm = 0.0;
	pid.err = speedErr;
	incrementSpeedPwm = pid.kp * (pid.err - pid.err_next) + pid.ki * pid.err + pid.kd * (pid.err - 2 * pid.err_next + pid.err_last);
	pid.actualSpeedPwm += incrementSpeedPwm;
	if(pid.actualSpeedPwm > 1000)
		pid.actualSpeedPwm = 1000;
	else if (pid.actualSpeedPwm < 0)
		pid.actualSpeedPwm = 0;
	//����ʵ�ʵ��ٶȺ�PWM�Ķ�Ӧ����ת������������ٶȵ�PWMֵ
//	float speedToPwm = 0.0;
	pid.err_last = pid.err_next;
	pid.err_next = pid.err;
//	return speedToPwm;
	return pid.actualSpeedPwm;
}

float MotorPidCtl(float speedErr)
{
	float incrementSpeedPwm = 0.0;
	err = speedErr;
	incrementSpeedPwm = kp * (err - err_next) + ki * err + kd * (err - 2 * err_next + err_last);
	actualSpeedPwm += incrementSpeedPwm;
	if(actualSpeedPwm > 1000)
		actualSpeedPwm = 1000;
	else if (actualSpeedPwm < 0)
		actualSpeedPwm = 0;
	//����ʵ�ʵ��ٶȺ�PWM�Ķ�Ӧ����ת������������ٶȵ�PWMֵ
//	float speedToPwm = 0.0;
	err_last = err_next;
	err_next = err;
//	return speedToPwm;
	return actualSpeedPwm;
}
