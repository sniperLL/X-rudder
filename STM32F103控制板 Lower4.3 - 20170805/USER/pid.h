#ifndef _PID_H_
#define _PID_H_

void PidInit(void);
float MotorPid(float speed);
float MotorPidContorl(float speedErr);
float MotorPidCtl(float speedErr);

#endif
