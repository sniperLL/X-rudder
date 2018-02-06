using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace CsOpenGL
{
    public partial class GlForm : Form
    {
        csglViewer myviewer = new csglViewer();
        csglViewer angleviewer = new csglViewer();
        Filtering myfiltering = new Filtering();
        NumericalIntegration myinergration = new NumericalIntegration();
        //FileStream txtfs = new FileStream("E:\\test.txt", FileMode.Create);
        public static GlForm glForm;
        public const int effectiveDecimal = 2;              //元整数据后小数点后面的位数
        public const float gravity = 9.8f;                  //重力加速度
        public const int allRcvDataNum = 37;                //串口获取数据总字节量  MPU6050值以及深度值
        double[] a = new double[3];                         //附体坐标系加速度
        double[] w = new double[3];                         //角速度
        double kalman_p = 0.0; double kalman_q = 0.0; double kalman_r = 0.0;
        double[] angle = new double[3];                     //角  度
        double kalman_phi = 0.0; double kalman_theta = 0.0;
        double T = 0;                                       //温  度
        double depth = 0.0;                                 //航行器深度
        double[] kalman_depth = new double[2];
        double angle2rad = 3.14159 / 180.0;                 //角度转弧度系数
        List<double> axList = new List<double> { };         //三轴加速度滑动平均数据存储区
        List<double> ayList = new List<double> { };
        List<double> azList = new List<double> { };
        List<double> wxList = new List<double> { };         //三轴角速度滑动平均数据存储区
        List<double> wyList = new List<double> { };
        List<double> wzList = new List<double> { };
        List<double> rollList = new List<double> { };       //三轴角度滑动平均数据存储区
        List<double> pitchList = new List<double> { };
        List<double> yawList = new List<double> { };
        int averageDataNum = 8;                            //用于滑动滤波的数据个数
        int averageZeroDriftNum = 0;                      //用于确定零漂值的数据个数
        List<double> axZeroDriftList = new List<double> { };//三轴加速度零漂估计列表      使用平均值滤波方法确定零漂值
        List<double> ayZeroDriftList = new List<double> { };
        List<double> azZeroDriftList = new List<double> { };
        double axZeroDrift = 0.0; double ayZeroDrift = 0.0; double azZeroDrift = 0.0; bool zeroDriftFlag = true;//三轴加速度零漂
        double[] axInter = new double[2]; double[] ayInter = new double[2]; double[] azInter = new double[2];//用于梯形积分的加速度值
        //double[] vxInter = new double[2]; double[] vyInter = new double[2]; double[] vzInter = new double[2];//用于梯形积分的速度值并且存储速度
        double vx = 0.0; double vy = 0.0; double vz = 0.0;  //存储xyz在固定坐标系下的速度值
        double sx = 0.0; double sy = 0.0; double sz = 0.0;  //存储xyz在固定坐标系下的位移值
        double h = 0.001;                                    //数据之间的时间差0.001秒
        double[] aEarth = new double[3] { 0, 0, gravity };  //大地坐标系加速度
        double[] aReal = new double[3];                     //消除重力加速度影响后动系的加速度值
        double[] aRealEarth = new double[3];                //消除重力加速度影响后定系的加速度值
        public static double roll = 0; public static double pitch = 0; public static double yaw = 0;
        double[] Rotx = new double[9] { 1, 0, 0, 0, Math.Cos(roll), -Math.Sin(roll), 0, Math.Sin(roll), Math.Cos(roll) };
        double[] Roty = new double[9] { Math.Cos(pitch), 0, Math.Sin(pitch), 0, 1, 0, -Math.Sin(pitch), 0, Math.Cos(pitch) };
        double[] Rotz = new double[9] { Math.Cos(yaw), -Math.Sin(yaw), 0, Math.Sin(yaw), Math.Cos(yaw), 0, 0, 0, 1 };

        /// <summary>
        /// 运行得到实时旋转矩阵
        /// </summary>
        void Rotxyz()
        {
            Rotx[4] = Math.Cos(roll); Rotx[5] = -Math.Sin(roll); Rotx[7] = Math.Sin(roll); Rotx[8] = Math.Cos(roll);
            Roty[0] = Math.Cos(pitch); Roty[2] = Math.Sin(pitch); Roty[6] = -Math.Sin(pitch); Roty[8] = Math.Cos(pitch);
            Rotz[0] = Math.Cos(yaw); Rotz[1] = -Math.Sin(yaw); Rotz[3] = Math.Sin(yaw); Rotz[4] = Math.Cos(yaw);
        }

        public GlForm()
        {
            InitializeComponent();
            myviewer.Parent = this;
            myviewer.BringToFront();
            myviewer.Location = new Point(0, 0);
            myviewer.Dock = DockStyle.Fill;
            myviewer.Visible = true;
            this.CsGlPanel.Controls.Add(myviewer);
            myviewer.glDraw();
            glForm = this;
            Control.CheckForIllegalCrossThreadCalls = false;

            angleviewer.Parent = this;
            angleviewer.Parent = this;
            angleviewer.BringToFront();
            angleviewer.Location = new Point(0, 0);
            angleviewer.Dock = DockStyle.Fill;
            angleviewer.Visible = true;
            this.anglePanel.Controls.Add(angleviewer);
            angleviewer.glDraw();

            //float xRudderAngle = (float)30.23;
            //byte[] tempAngle = new byte[4];
            ////int d = 110;
            //tempAngle = BitConverter.GetBytes(xRudderAngle);
            //GlTimer.Start();
            
        }

        private void GlTimer_Tick(object sender, EventArgs e)
        {
            //List<double> data = new List<double> {0.0,0.2,0.3,0.4,0.5,0.8,0.9 };
            //Write(data);
            //myviewer.glDraw();
            //ServoTest();
        }

        private void CsGlPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        /// <summary>
        /// 打开串口设置窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialBtn_Click(object sender, EventArgs e)
        {
            serialPortForm serialForm = new serialPortForm(glForm);
            serialForm.Show();
        }
        /// <summary>
        /// 数据接收中断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] dataSerialRcv = DataRcv(serialPort,allRcvDataNum);
                if (dataSerialRcv == null)
                    return;
                DataRcvProcess(dataSerialRcv);  //获取MPU6050以及压力传感器数据
                if(pid_flag == true && motor_go_flag == true)
                    ServoPidControl();
                else
                    ServoTest();    // 测试用函数   没有PID控制状态下测试舵机
            }
            catch(Exception ex)
            {
                serialPort.Close();
                MessageBox.Show(ex.Message,"Error");
            }
        }
        /// <summary>
        /// 获取指定串口指定字节的数据
        /// </summary>
        /// <returns></returns>
        private byte[] DataRcv(SerialPort serialPort,int allRcvDataNum)
        {
            int allRealRcvDataNum = 0;
            while (allRealRcvDataNum < allRcvDataNum)
            {
                if (serialPort.IsOpen == false)
                {
                    return null;
                }
                allRealRcvDataNum = serialPort.BytesToRead;
            }

            byte[] byteRead = new byte[allRcvDataNum];
            serialPort.Read(byteRead, 0, byteRead.Length);

            if (byteRead[0] == 0x55 && byteRead[1] == 0x51 && byteRead[11] == 0x55 && byteRead[12] == 0x52 && byteRead[23] == 0x53)  //判断是正确的数据才能返回
                return byteRead;
            else
            {
                serialPort.DiscardInBuffer();
                return null;
            }
        }
        /// <summary>
        /// 所有数据处理的函数
        /// </summary>
        /// <param name="serialRcvData"></param>
        private void DataRcvProcess(byte[] serialRcvData)
        {
            //每段数据的处理函数 数组是否需要做切片处理？
            MpuDataProcess(serialRcvData);
            GetDepth(serialRcvData);
        }
        /// <summary>
        /// IMU数据处理
        /// </summary>
        /// <param name="serialRcvData"></param>
        List<double> angle2_avg_burrs = new List<double> {};  //存储5个数据后用于去毛刺滤波
        List<double> angle2_avg = new List<double> { 0, 0 };  //存储滤波后的数据
        private void MpuDataProcess(byte[] serialRcvData)
        {
            if (serialRcvData == null)
                return;
            //a[0] = Math.Round(((short)(serialRcvData[3] << 8 | serialRcvData[2])) / 32768.0 * 16 * gravity, effectiveDecimal);
            //a[1] = Math.Round(((short)(serialRcvData[5] << 8 | serialRcvData[4])) / 32768.0 * 16 * gravity, effectiveDecimal);
            //a[2] = Math.Round(((short)(serialRcvData[7] << 8 | serialRcvData[6])) / 32768.0 * 16 * gravity, effectiveDecimal);
            T = Math.Round(((short)(serialRcvData[9] << 8 | serialRcvData[8])) / 340.0 + 36.25, effectiveDecimal); 

            w[0] = Math.Round(((short)(serialRcvData[14] << 8 | serialRcvData[13])) / 32768.0 * 2000, effectiveDecimal);//(°/s)
            w[1] = Math.Round(((short)(serialRcvData[16] << 8 | serialRcvData[15])) / 32768.0 * 2000, effectiveDecimal);
            w[2] = Math.Round(((short)(serialRcvData[18] << 8 | serialRcvData[17])) / 32768.0 * 2000, effectiveDecimal);

            angle[0] = Math.Round(((short)(serialRcvData[25] << 8 | serialRcvData[24])) / 32768.0 * 180, 0);//(度)
            angle[1] = Math.Round(((short)(serialRcvData[27] << 8 | serialRcvData[26])) / 32768.0 * 180, 0);
            angle[2] = Math.Round(((short)(serialRcvData[29] << 8 | serialRcvData[28])) / 32768.0 * 180, 0);
            //Write(angle[1]);
            //angle2_avg_burrs.Add(angle[2]);
            //if (angle2_avg_burrs.Count == 5)
            //{
            //    //depth = myfiltering.FilteringAlgorithm("removeBurrs", depth_avg_burrs);
            //    angle2_avg[0] = angle2_avg[1];
            //    angle2_avg[1] = myfiltering.FilteringAlgorithm("removeBurrs", angle2_avg_burrs);
            //    //Write(angle2_avg[1]);
            //    angle2_avg_burrs.Clear();
            //}
            //获得航行器姿态数据初始值
            if (init_flag == true && pid_flag == true)
            {
                for (int i = 0; i < angle_init.Length; i++)
                {
                    angle_init[i] = angle[i];
                    w_init[i] = w[i];
                }
                init_flag = false;
            }
            //roll = Math.Round(angle[0] * angle2rad, 0); //（弧度）角度取整数
            //pitch = Math.Round(angle[1] * angle2rad, 0);
            //yaw = Math.Round(angle[2] * angle2rad, 0);
            #region  与加速度和位移有关 以注释
            //所有数据存储做滑动平均
            //IMUDataStoreAndCal();
            //kalman_theta = myfiltering.KalmanFilter(angle[1]);
            //kalman_phi = myfiltering.KalmanFilter(angle[2]);
            //更新实时矩阵
            //Rotxyz();
            //消除重力加速度的影响 得到动系中实际加速度的值
            //double[] aEarthG = new double[3];
            //aEarthG = MatrixProduct31(MatrixReverse(AccCoordinateConversion3(Rotz, Roty, Rotx)), aEarth);
            //for (int i = 0; i < aEarthG.Length; i++)
            //{
            //    aReal[i] = a[i] - aEarthG[i];
            //}
            //消除重力加速度的影响 得到定系中实际加速度的值
            //aRealEarth = AccCoordinateConversion(Rotz, Roty, Rotx, a);
            //for (int i = 0; i < aRealEarth.Length; i++)
            //{
            //    aRealEarth[i] -= aEarth[i];
            //}
            //double kalmandata = myfiltering.KalmanFilter(aRealEarth[0]);            
            //获取固定坐标系的零漂值   获取零漂值为了得到积分
            //ZeroDrift();
            //积分得到速度和位移
            //if (axZeroDriftList.Count == averageZeroDriftNum)        //零漂值确定之后才能进行计算
            //{
            //    Inter2SpeedAndPosition();
            //    //数据显示
            //    DisplayDataToLabel();
            //}
            //sx = sx + (kalmandata - 0.026) * h * h;
            //sxLabel.Text = Convert.ToString(Math.Round(sx, 4));
            #endregion
            //kalman_q = myfiltering.KalmanFilter(w[1]);
            //kalman_r = myfiltering.KalmanFilter(w[2]);
            //Write(kalman_q);
            //数据显示
            DisplayDataToLabel();
        }
        /// <summary>
        /// 求取零漂值的平均值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private double AverageZeroDrift(List<double> data)
        {
            double temp = 0.0;
            temp = myfiltering.FilteringAlgorithm("average", data);

            return temp;
        }
        /// <summary>
        /// 获取固定坐标系下的零漂值
        /// </summary>
        private void ZeroDrift()
        {
            //获取固定坐标系的零漂值
            if (axZeroDriftList.Count < averageZeroDriftNum)        //如果没有大于此数，则没有滤波，数据不可用
            {
                axZeroDriftList.Add(aRealEarth[0]);
                ayZeroDriftList.Add(aRealEarth[1]);
                azZeroDriftList.Add(aRealEarth[2]);
                //if (zeroDriftFlag == true)
                //{
                //    axZeroDrift = aRealEarth[0];
                //    ayZeroDrift = aRealEarth[1];
                //    azZeroDrift = aRealEarth[2];
                //    zeroDriftFlag = false;
                //}
            }
            else
            {
                if (zeroDriftFlag == true)
                {
                    axZeroDrift = AverageZeroDrift(axZeroDriftList);
                    ayZeroDrift = AverageZeroDrift(ayZeroDriftList);
                    azZeroDrift = AverageZeroDrift(azZeroDriftList);
                    zeroDriftFlag = false;
                }
            }
        }
        /// <summary>
        /// 数组为引用类型，作为形式参数传入函数的是它的引用
        /// </summary>
        /// <param name="dataArray"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private double[] UpdateData(double[] dataArray,double data)
        {
            for (int i = 1; i < dataArray.Length; i++)
            {
                dataArray[i - 1] = dataArray[i];
            }
            dataArray[dataArray.Length-1] = data;

            return dataArray;
        }
        /// <summary>
        /// 积分得到定坐标系下的速度数据以及位移数据
        /// </summary>
        private void Inter2SpeedAndPosition()
        {
            int effective = 4;
            UpdateData(axInter, Math.Round(aRealEarth[0] - axZeroDrift, effective));
            UpdateData(ayInter, Math.Round(aRealEarth[1] - ayZeroDrift, effective));
            UpdateData(azInter, Math.Round(aRealEarth[2] - azZeroDrift, effective));

            //UpdateData(vxInter, Math.Round(vxInter[1] + myinergration.TiXingFormula(axInter, h, 0), effective));
            //UpdateData(vyInter, Math.Round(vyInter[1] + myinergration.TiXingFormula(ayInter, h, 0), effective));
            //UpdateData(vzInter, Math.Round(vzInter[1] + myinergration.TiXingFormula(azInter, h, 0), effective));
            vx += axInter[1] * h;
            vy += ayInter[1] * h;
            vz += azInter[1] * h;
            //sx += myinergration.TiXingFormula(vxInter, h, 0);
            //sy += myinergration.TiXingFormula(vyInter, h, 0);
            //sz += myinergration.TiXingFormula(vzInter, h, 0);
            sx += axInter[1] * h * h;
            sy += ayInter[1] * h * h;
            sz += azInter[1] * h * h;
        }
        #region 数据显示
        /// <summary>
        /// 将数据显示在Label上
        /// </summary>
        void DisplayDataToLabel()
        {
            //xAccLabel.Text = Convert.ToString(Math.Round(a[0], effectiveDecimal));
            //yAccLabel.Text = Convert.ToString(Math.Round(a[1], effectiveDecimal));
            //zAccLabel.Text = Convert.ToString(Math.Round(a[2], effectiveDecimal));
            xAngularVLabel.Text = Convert.ToString(Math.Round(w[0], effectiveDecimal));
            yAngularVLabel.Text = Convert.ToString(Math.Round(w[1], effectiveDecimal));
            zAngularVLabel.Text = Convert.ToString(Math.Round(w[2], effectiveDecimal));
            rollAngleLabel.Text = Convert.ToString(Math.Round(angle[0], effectiveDecimal));
            pitchAngleLabel.Text = Convert.ToString(Math.Round(angle[1], effectiveDecimal));
            yawAngleLabel.Text = Convert.ToString(Math.Round(angle[2], effectiveDecimal));
            //yawAngleLabel.Text = Convert.ToString(Math.Round(angle2_avg[1], effectiveDecimal));
            //Write(angle[2]);
            temperatureLabel.Text = Convert.ToString(T);
            //accxEarthLabel.Text = Convert.ToString(Math.Round(aEarth[0], effectiveDecimal));
            //accyEarthLabel.Text = Convert.ToString(Math.Round(aEarth[1], effectiveDecimal));
            //acczEarthLabel.Text = Convert.ToString(Math.Round(aEarth[2], effectiveDecimal));
            //accxRealLabel.Text = Convert.ToString(Math.Round(aReal[0], effectiveDecimal));
            //accyRealLabel.Text = Convert.ToString(Math.Round(aReal[1], effectiveDecimal));
            //acczRealLabel.Text = Convert.ToString(Math.Round(aReal[2], effectiveDecimal));
            //daxEarthLabel.Text = Convert.ToString(Math.Round(aRealEarth[0], effectiveDecimal));
            //dayEarthLabel.Text = Convert.ToString(Math.Round(aRealEarth[1], effectiveDecimal));
            //dazEarthLabel.Text = Convert.ToString(Math.Round(aRealEarth[2], effectiveDecimal));

            //sxLabel.Text = Convert.ToString(Math.Round(sx, effectiveDecimal));
            //syLabel.Text = Convert.ToString(Math.Round(sy, effectiveDecimal));
            //szLabel.Text = Convert.ToString(Math.Round(sz, effectiveDecimal));
            //vxLabel.Text = Convert.ToString(Math.Round(vx, effectiveDecimal));
            //vyLabel.Text = Convert.ToString(Math.Round(vy, effectiveDecimal));
            //vzLabel.Text = Convert.ToString(Math.Round(vz, effectiveDecimal));

            depthLabel.Text = Convert.ToString(Math.Round(depth_avg[1], effectiveDecimal));  
        }
        #endregion
        /// <summary>
        /// MPU6050加速度数据、角速度以及角度数据滑动平均滤波
        /// </summary>
        private void IMUDataStoreAndCal()
        {
            //#region  简单但代码行数太多
            if (axList.Count <= averageDataNum)
            {
                axList.Add(a[0]); ayList.Add(a[1]); azList.Add(a[2]);
                wxList.Add(w[0]); wyList.Add(w[1]); wzList.Add(w[2]);
                rollList.Add(roll); pitchList.Add(pitch); yawList.Add(yaw);

                a[0] = 0; a[1] = 0; a[2] = 9.8;
                w[0] = 0; w[1] = 0; w[2] = 0;
                roll = 0; pitch = 0; yaw = 0;
            }
            else
            {
                axList.RemoveAt(0); ayList.RemoveAt(0); azList.RemoveAt(0);
                wxList.RemoveAt(0); wyList.RemoveAt(0); wzList.RemoveAt(0);
                rollList.RemoveAt(0); pitchList.RemoveAt(0); yawList.RemoveAt(0);

                axList.Add(a[0]); ayList.Add(a[1]); azList.Add(a[2]);
                wxList.Add(w[0]); wyList.Add(w[1]); wzList.Add(w[2]);
                rollList.Add(roll); pitchList.Add(pitch); yawList.Add(yaw);

                a[0] = myfiltering.FilteringAlgorithm("movingAverage", axList);
                a[1] = myfiltering.FilteringAlgorithm("movingAverage", ayList);
                a[2] = myfiltering.FilteringAlgorithm("movingAverage", azList);
                w[0] = myfiltering.FilteringAlgorithm("movingAverage", wxList);
                w[1] = myfiltering.FilteringAlgorithm("movingAverage", wyList);
                w[2] = myfiltering.FilteringAlgorithm("movingAverage", wzList);
                roll = myfiltering.FilteringAlgorithm("movingAverage", rollList);
                pitch = myfiltering.FilteringAlgorithm("movingAverage", pitchList);
                yaw = myfiltering.FilteringAlgorithm("movingAverage", yawList);

                //if (zeroDriftFlag == true)
                //{
                //    axZeroDrift = a[0];
                //    ayZeroDrift = a[1];
                //    azZeroDrift = a[2];
                //    zeroDriftFlag = false;
                //}
            }
            //#endregion
        }
        /// <summary>
        /// 更新零漂值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateZeroDriftBtn_Click(object sender, EventArgs e)
        {
            zeroDriftFlag = true;
        }

        /// <summary>
        /// 窗体关闭时需要处理的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(serialPort.IsOpen == true)
                serialPort.Close();
        }
        #region  矩阵计算
        /// <summary>
        /// M1*M2  3*3的方阵相乘
        /// </summary>
        /// <param name="M1"></param>
        /// <param name="M2"></param>
        /// <returns></returns>
        double[] MatrixProduct33(double[] M1, double[] M2) 
        {
            double[] matrixProduct = new double[9];
            int matrixLen = 0;
            for (int rowNum = 0; rowNum < 3; rowNum ++)
            {
                for (int colNum = 0; colNum < 3; colNum++)
                {
                    matrixProduct[matrixLen] = M1[3 * rowNum] * M2[colNum] + M1[3 * rowNum + 1] * M2[colNum + 3] + M1[3 * rowNum + 2] * M2[colNum + 6];
                    matrixLen++;
                }
            }

            return matrixProduct;
        }
        /// <summary>
        /// M1*M2  3*1矩阵相乘
        /// </summary>
        /// <param name="M1"></param>
        /// <param name="M2"></param>
        /// <returns></returns>
        double[] MatrixProduct31(double[] M1, double[] M2)
        {
            double[] matrixProduct = new double[3];
            for (int rowNum = 0; rowNum < 3; rowNum++)
            {
                matrixProduct[rowNum] = M1[3 * rowNum] * M2[0] + M1[3 * rowNum + 1] * M2[1] + M1[3 * rowNum + 2] * M2[2];
            }

            return matrixProduct;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="M1"></param>
        /// <param name="M2"></param>
        /// <param name="M3"></param>
        /// <returns></returns>
        double[] AccCoordinateConversion3(double[] M1, double[] M2, double[] M3)
        {
            double[] accEarth = new double[3];
            double[] tempMatrix = new double[9];
            tempMatrix = MatrixProduct33(M1, M2);
            tempMatrix = MatrixProduct33(tempMatrix, M3);

            return tempMatrix;
        }
        /// <summary>
        /// M1*M2*M3*M4  矩阵相乘顺序
        /// </summary>
        /// <param name="M1"></param>
        /// <param name="M2"></param>
        /// <param name="M3"></param>
        /// <param name="M4"></param>
        /// <returns></returns>
        double[] AccCoordinateConversion(double[] M1,double[] M2,double[] M3,double[] M4)
        {
            double[] accEarth = new double[3];
            double[] tempMatrix = new double[9];
            tempMatrix = MatrixProduct33(M1, M2);
            tempMatrix = MatrixProduct33(tempMatrix, M3);
            accEarth = MatrixProduct31(tempMatrix, M4);

            return accEarth;
        }
        /// <summary>
        /// 3*3矩阵求逆  初等变换算法    经过验证函数算法正确
        /// </summary>
        /// <param name="M"></param>
        /// <returns></returns>
        double[] MatrixReverse(double[] M)
        {
            double[] reverseMtemp = new double[9];
            //double[,] reverseM = new double[3, 3];
            double[,] unit = new double[3, 6];
            
            int len = 0;                                    //将M转换为3*3的矩阵并赋给uInt
            for (int m = 0; m < 3; m++)
            {
                for (int n = 0; n < 3; n++)
                {
                    unit[m, n] = M[len];
                    len++;
                }
                for (int n = 3; n < 6; n++)
                {
                    if (m == (n - 3))
                    {
                        unit[m, n] = 1.0f;
                    }
                    else unit[m, n] = 0.0;
                }
            }
            ////////////////
            int i, j, k;
            int maxI = 0;
            for (i = 1; i < 3; i++)
            {
                if (Math.Abs(unit[maxI, 0]) < Math.Abs(unit[i, 0]))
                    maxI = i;
            }
            if (maxI != 0)
            {
                double temp;
                for (j = 0; j < 6; j++)
                {
                    temp = unit[0,j];
                    unit[0,j] = unit[maxI,j];
                    unit[maxI,j] = temp;
                }
            }

            double temp2 = 0;
            for (i = 0; i < 3; i++)
            {
                if (unit[i,i] != 0)
                    temp2 = 1.0 / unit[i,i];
                else
                {
                }
                for (j = 0; j < 6; j++)
                    unit[i,j] *= temp2;
                for (j = 0; j < 3; j++)
                {
                    if (j != i)
                    {
                        double temp3 = unit[j,i];
                        for (k = 0; k < 6; k++)
                            unit[j,k] -= temp3 * unit[i,k];
                    }
                }
            }
            ///////////
            len = 0;                                                  //将uInt转换为1*9的行向量并赋给reverseMtemp
            for (int m = 0; m < 3; m++)
            {
                for (int n = 3; n < 6; n++)
                {
                    reverseMtemp[len] = unit[m, n];
                    len++;
                }
            }

            return reverseMtemp;
        }
        #endregion 
        #region  深度数据获取
        /// <summary>
        /// 获取的4字节16进制数据转换为无符号整型数据    压力传感器经过AD转换后得到的码值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public uint SingleUintDataConvert(byte[] data, int offset)
        {
            uint i;
            i = BitConverter.ToUInt32(data, offset);
            return i;
        }
        /// <summary>
        /// 获取深度数据
        /// </summary>
        /// <param name="serialRcvData"></param>
        /// <returns></returns>
        //List<double> depth_avg = new List<double> { 0, 0, 0 }; 
        List<double> depth_avg_burrs = new List<double> {};  //存储5个数据后用于去毛刺滤波
        List<double> depth_avg = new List<double> { 0, 0 };  //存储滤波后的数据
        private void GetDepth(byte[] serialRcvData)
        {           
            depth = Math.Round((double)(((float)SingleUintDataConvert
                (serialRcvData, serialRcvData.Length-4) / 4096) * ((float)50000 / 9800)), effectiveDecimal);
            if (depth <= 5.0)
                depth_avg_burrs.Add(depth);
            if (depth_avg_burrs.Count == 5)
            {
                depth = myfiltering.FilteringAlgorithm("removeBurrs", depth_avg_burrs);
                depth_avg[0] = depth_avg[1];
                depth_avg[1] = depth;
                Write(depth);
                depth_avg_burrs.Clear();
            }            
        }
        #endregion
        #region   遥控器数据处理，遥控器用于显示当前航行器偏移中心线的距离
        //航行器航向偏移量
        double remote_eta = 0.0;
        private void remoteBtn_Click(object sender, EventArgs e)
        {
            if (RemoteSerialPort.IsOpen)
            {
                RemoteSerialPort.Close();
            }
            else
            {
                //电脑只能外接两个串口，其中一个用于遥控器
                string[] ports = SerialPort.GetPortNames();
                for (int i = 0; i < ports.Length; i++)
                {
                    if (ports[i] != serialPort.PortName || serialPort.PortName == null)
                    {
                        RemoteSerialPort.PortName = ports[i];
                        RemoteSerialPort.BaudRate = 115200;
                        RemoteSerialPort.DataBits = 8;
                        RemoteSerialPort.StopBits = StopBits.One;
                        RemoteSerialPort.Parity = Parity.None;
                    }
                }
                RemoteSerialPort.Open();
            }
            remoteBtn.Text = RemoteSerialPort.IsOpen ? "关闭遥控器" : "开启遥控器";
        }

        private void RemoteSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = 0;
                while (n < 18)
                {
                    if (RemoteSerialPort.IsOpen == false)
                    {
                        return;
                    }
                    n = RemoteSerialPort.BytesToRead;
                }
                //开始接收数据
                byte[] byteRead = new byte[18];
                RemoteSerialPort.Read(byteRead, 0, byteRead.Length);
                if (byteRead[0] == 0x5A && byteRead[17] == 0x33)
                {
                    //遥控器数据处理
                    remote_eta = RemoteDataPrecess(byteRead);
                }
                else
                    RemoteSerialPort.DiscardInBuffer();
                ShiftOffestTxtbox.Text = Convert.ToString(Math.Round(remote_eta, effectiveDecimal));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                RemoteSerialPort.Close();
            }
        }

        private double RemoteDataPrecess(byte[] remoteData) 
        {
            double i;
            i = BitConverter.ToSingle(remoteData, 1);
            return i;
        }

        public float SingleFloatDataConvert(byte[] data, int offset)
        {
            float i;
            i = BitConverter.ToSingle(data, offset);
            return i;
        }
        #endregion
        #region 手动键入航行器偏离中心线的位置

        #endregion
        #region 使用数据控制航行器  《潜艇和深潜器的现代操纵理论与应用》PID操舵规律
        //首尾升降舵PID控制  P81
        double kbtheta = 0.0; double kbq = 0.0; double kbz = 3.0; double kbdz = 0.0; double kz0 = 0.0;
        double kstheta = 0.0; double ksq = 0.0; double ksz = 3.0; double ksdz = 0.0; double km0 = 0.0;
        //方向舵PID控制  P157
        double k1 = 0.0; double k2 = 0; double k3 = 0; double k4 = 0;
        //参与PID控制的运动变量  角度单位都是°
        double theta = 0.0;                         //纵倾角
        double q = 0.0;                             //纵倾角速度
        double dz = 0.0;                            //z轴位移变化量
        double vz_pid = 0.0;                        //z轴速度
        double z0 = 0.0; double m0 = 0.0;           //零升力、零升力矩
        double r = 0.0;                             //偏航角速度
        double phi = 0.0;                           //偏航角
        double eta = 0.0;                           //偏航位移
        //十字舵PID操舵规律
        double deltab = 0.0;                        //艏升降舵舵角
        double deltas = 0.0;                        //艉升降舵舵角
        double deltar = 0.0;                        //方向舵舵角
        //X舵舵角
        double deltam = 0.0; double deltan = 0.0;   //右下角舵编号为1，顺时针编号，1,3为m组，2,4为n组
        //电机转速  恒定转速 60rad/s?    电机速度只能是40 50 60 分为低速 中速 高速三挡
        double motor_speed = 0.0;       
        /// <summary>
        /// PID控制函数
        /// </summary>
        int max_servo_angle = 0;
        private void ServoPidControl()
        {
            UpdateServoPidControlData();
            //PID参数调节后得到的十字舵首尾升降舵舵角以及方向舵舵角  方向舵左舵正、尾舵下潜为正
            deltab = kbtheta * theta + kbq * q - kbz * dz + kbdz * vz_pid + kz0 * z0;
            deltas = kstheta * theta + ksq * q - ksz * dz + ksdz * vz_pid + km0 * m0;
            //deltab = 20.0;//正舵角下潜
            //deltas = 20.0;//正舵角下潜
            deltar = k2 * r + k3 * phi + k4 * (-eta);
            //十字舵与X舵关系，转换得到X舵舵角  向右转舵为正
            //deltam = (Math.Sqrt(2) * (deltar + deltas)) / 2.0;
            //deltan = (Math.Sqrt(2) * (deltar - deltas)) / 2.0;
            deltam = -(Math.Sqrt(2) * (deltas + deltar)) / 2.0;
            deltan = -(Math.Sqrt(2) * (deltas - deltar)) / 2.0;
            //舵角限制
            if (deltab > max_servo_angle)
                deltab = max_servo_angle;
            if (deltab < -max_servo_angle)
                deltab = -max_servo_angle;
            if (deltam > max_servo_angle)
                deltam = max_servo_angle;
            if (deltam < -max_servo_angle)
                deltam = -max_servo_angle;
            if (deltan > max_servo_angle)
                deltan = max_servo_angle;
            if (deltan < -max_servo_angle)
                deltan = -max_servo_angle;
            //数据转换并且下发数据至下位机
            DataConvertAndSend2Lower();
        }
        /// <summary>
        /// 更新数据用于PID操作舵角计算
        /// </summary>
        double[] angle_init = new double[3] { 0, 0, 0 };
        double[] w_init = new double[3] { 0, 0, 0 };
        bool init_flag = false;
        private void UpdateServoPidControlData()
        {
            theta = angle[1] - angle_init[1];
            q = w[1] - w_init[1];
            dz = depth_avg[1] - 3;//depth_avg[0];  //深度差量
            //vz_pid = dz / h;// 深度差量值除以时间
            r = w[2] - w_init[2];
            phi = angle[2] - angle_init[2];
            eta = remote_eta -0;    //遥控器给的值
        }
        //发送数据BUFFER
        private byte[] ctlData = new byte[40] { 0x5A, 0x01,         //数据校验位
                                                0x00, 0x00, 0x00, 0x00,     //左上舵角度
                                                0x00, 0x00, 0x00, 0x00,     //右上舵角度
                                                0x00, 0x00, 0x00, 0x00,     //左下舵角度
                                                0x00, 0x00, 0x00, 0x00,     //右下舵角度
                                                0x00, 0x00, 0x00, 0x00,     //左水平舵角度
                                                0x00, 0x00, 0x00, 0x00,     //右水平舵角度
                                                0x00, 0x00, 0x00, 0x00,     //电机
                                                0x00,        //重置复位标志位30：0xFF为重置复位
                                                0x33,                   //数据校验位
                                                0x00,       //电机正反转标志位32
                                                0x00,       //左上舵卡舵标志位33：0xFF为卡舵
                                                0x00,       //右上舵卡舵标志位34：0xFF为卡舵
                                                0x00,       //左下舵卡舵标志位35：0xFF为卡舵
                                                0x00,       //右下舵卡舵标志位36：0xFF为卡舵
                                                0x00, 0x00, 0x00,//Motor speed pid paramater
        };
        /// <summary>
        /// 数据转换，并且下发至下位机STM32F103
        /// </summary>
        private void DataConvertAndSend2Lower()
        {
            float left_up_Angle = (float)deltam;         //3
            float right_down_Angle = (float)deltam;      //1
            float right_up_Angle = (float)deltan;        //4
            float left_down_Angle = (float)deltan;       //2
            float deltab_float = (float)deltab;
            float motorSpeed_float = (float)motor_speed;
            //float left_up_Angle = 0;         //3
            //float right_down_Angle = 0;      //1
            //float right_up_Angle = 0;        //4
            //float left_down_Angle = 0;       //2
            //float deltab_float = (float)deltab;
            //float motorSpeed_float = (float)motor_speed;

            //舵角限位
            if (left_up_Angle <= -50)        //3
                left_up_Angle = -50;
            else if (left_up_Angle >= 50)
                left_up_Angle = 50;

            if (right_down_Angle <= -50)     //1
                right_down_Angle = -50;
            else if (right_down_Angle >= 50)
                right_down_Angle = 50;

            if (right_up_Angle <= -50)       //4
                right_up_Angle = -50;
            else if (right_up_Angle >= 50)
                right_up_Angle = 50;

            if (left_down_Angle <= -50)      //2
                left_down_Angle = -50;
            else if (left_down_Angle >= 50)
                left_down_Angle = 50;

            ConvertDoubleToByte(left_up_Angle + 90, 2);
            ConvertDoubleToByte(right_up_Angle + 90, 6);
            ConvertDoubleToByte(left_down_Angle + 90, 10);
            ConvertDoubleToByte(right_down_Angle + 90, 14);
            ConvertDoubleToByte(deltab_float + 90, 18);
            ConvertDoubleToByte(deltab_float + 90, 22);
            ConvertDoubleToByte(motorSpeed_float, 26);

            SerialPortDataSend();
        }
        /// <summary>
        /// 浮点数转换为字节数组
        /// </summary>
        /// <param name="xRudderAngle">需要转换的数据</param>
        /// <param name="de">数据在ctlData数组中的起始位置</param>
        public void ConvertDoubleToByte(float xRudderAngle, int de)
        {
            byte[] tempAngle = new byte[4];
            //int d = 110;
            tempAngle = BitConverter.GetBytes(xRudderAngle);
            //tempAngle = BitConverter.GetBytes(d);
            for (int i = de, j = 0; i < de + 4; i++, j++)
            {
                ctlData[i] = tempAngle[j];
            }
        }
        /// <summary>
        /// 控制数据下发
        /// </summary>
        public void SerialPortDataSend()
        {
            if (serialPort.IsOpen == true)
                serialPort.Write(ctlData, 0, ctlData.Length);
            //else
            //    MessageBox.Show("请打开串口");
        }
        /// <summary>
        /// 测试舵机的上下位机串口
        /// </summary>
        void ServoTest()
        {
            //从窗体上获取水平舵和垂直舵的舵角
            deltar = -Convert.ToDouble(textBox1.Text);  //方向舵
            deltas = Convert.ToDouble(textBox2.Text);// 尾升降舵
            //十字舵与X舵关系，转换得到X舵舵角  向右转舵为正
            //deltam = (Math.Sqrt(2) * (deltar + deltas)) / 2.0;
            //deltan = (Math.Sqrt(2) * (deltar - deltas)) / 2.0;
            deltam = (Math.Sqrt(2) * (deltas + deltar)) / 2.0;
            deltan = (Math.Sqrt(2) * (deltas - deltar)) / 2.0;
            //舵角限制
            if (deltab > max_servo_angle)
                deltab = max_servo_angle;
            if (deltab < -max_servo_angle)
                deltab = -max_servo_angle;
            if (deltam > max_servo_angle)
                deltam = max_servo_angle;
            if (deltam < -max_servo_angle)
                deltam = -max_servo_angle;
            if (deltan > max_servo_angle)
                deltan = max_servo_angle;
            if (deltan < -max_servo_angle)
                deltan = -max_servo_angle;
            //数据转换并且下发数据至下位机
            DataConvertAndSend2Lower();
        }
        #endregion
        #region  C#与TXT  读写Txt
        /// <summary>
        /// 使用FileStream类创建文件，然后将数据写入到文件里  
        /// 向文本文件中写入十六进制文件
        /// </summary>
        public void Write(FileStream fs)
        {
            fs = new FileStream("E:\\ak.txt", FileMode.Create);
            //获得字节数组
            byte[] data = System.Text.Encoding.Default.GetBytes("Hello World!");
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }
        /// <summary>
        /// 使用FileStream类创建文件，使用StreamWriter类，将数据写入到文件。  
        /// 向文本文件中写入任意字符
        /// </summary>
        /// <param name="path"></param>
        public void Write(FileStream fs,string path,List<double> data)
        {
            //FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            foreach (double i in data)
            {
                sw.Write(i);
                sw.Write("  ");
            }
            sw.WriteLine();//写入换行符
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            //fs.Close();
        }
        /// <summary>
        /// 写入函数的重载
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="data"></param>
        public void Write(FileStream fs, List<double> data)
        {
            //FileStream fs = new FileStream(path, FileMode.Create);
            if (File.Exists(@"E:\test.txt"))
            {
                
                //fs = new FileStream(@"E:\test.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(@"E:\test.txt",true);
                //开始写入
                foreach (double i in data)
                {
                    sw.Write(i);
                    sw.Write("  ");
                }
                sw.WriteLine();//写入换行符
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
            }
            else
                MessageBox.Show("文件不存在！");
            //fs.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="data"></param>
        public void Write(List<double> data)
        {
            //FileStream fs = new FileStream(path, FileMode.Create);
            if (File.Exists(@"E:\test.txt"))
            {

                //fs = new FileStream(@"E:\test.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(@"E:\test.txt", true);
                //开始写入
                foreach (double i in data)
                {
                    sw.Write(i);
                    sw.Write("  ");
                }
                sw.WriteLine();//写入换行符
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
            }
            else
                MessageBox.Show("文件不存在！");
        }
        public void Write(double[] data)
        {
            //FileStream fs = new FileStream(path, FileMode.Create);
            if (File.Exists(@"E:\test.txt"))
            {

                //fs = new FileStream(@"E:\test.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(@"E:\test.txt", true);
                //开始写入
                foreach (double i in data)
                {
                    sw.Write(i);
                    sw.Write("  ");
                }
                sw.WriteLine();//写入换行符
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
            }
            else
                MessageBox.Show("文件不存在！");
        }
        public void Write(double data)
        {
            //FileStream fs = new FileStream(path, FileMode.Create);
            if (File.Exists(@"E:\test.txt"))
            {

                //fs = new FileStream(@"E:\test.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(@"E:\test.txt", true);
                //开始写入
                sw.Write(data);
                sw.WriteLine();//写入换行符
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
            }
            else
                MessageBox.Show("文件不存在！");
        }
        /// <summary>
        /// 使用FileStream类进行文件的读取，并将它转换成char数组，然后输出
        /// </summary>
        byte[] byData = new byte[100];
        char[] charData = new char[1000];
        public void Read()
        {
            try
            {
                FileStream file = new FileStream("E:\\test.txt", FileMode.Open);
                file.Seek(0, SeekOrigin.Begin);
                file.Read(byData, 0, 100); //byData传进来的字节数组,用以接受FileStream对象中的数据,第2个参数是字节数组中开始写入数据的位置,它通常是0,表示从数组的开端文件中向数组写数据,最后一个参数规定从文件读多少字符.
                Decoder d = Encoding.Default.GetDecoder();
                d.GetChars(byData, 0, byData.Length, charData, 0);
                Console.WriteLine(charData);
                file.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// 使用StreamReader读取文件，然后一行一行的输出
        /// </summary>
        /// <param name="path"></param>
        public void Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line.ToString());
            }
        }
        #endregion

        private void GlForm_Load(object sender, EventArgs e)
        {
            //txtfs = new FileStream("E:\\test.txt", FileMode.Create);
        }

        private void writeBtn_Click(object sender, EventArgs e)
        {
            GlTimer.Start();
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            GlTimer.Stop();
        }
        /// <summary>
        /// 紧急情况下,一键上浮功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmergencyBtn_Click(object sender, EventArgs e)
        {
            ctlData[30] = 0xFF;   // 下发数据第31个字节表示紧急情况，执行紧急状况处理
            //MessageBox.Show(Convert.ToString(ctlData[30]));
        }

        private void motorspeedBtn_Click(object sender, EventArgs e)
        {
            motor_speed = Convert.ToDouble(motorspeedTxt.Text);
            init_flag = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            deltab = Convert.ToDouble(textBox4.Text);
        }

        private void YBtn_Click(object sender, EventArgs e)
        {
            remote_eta = Convert.ToDouble(textBox3.Text);
        }

        private void label35_Click(object sender, EventArgs e)
        {

        }
        int motorP = 0;
        int motorI = 0;
        int motorD = 0;
        private void motorPidBtn_Click(object sender, EventArgs e)
        {
            motorP = Convert.ToInt32(motorPtbx.Text);
            motorI = Convert.ToInt32(motorItbx.Text);
            motorD = Convert.ToInt32(motorDtbx.Text);
            byte[] motorPidPara = new byte[3] { 0x00, 0x00, 0x00 };
            motorPidPara[0] = Convert.ToByte(motorP);
            //MessageBox.Show(Convert.ToString(motorPidPara[0]));
            motorPidPara[1] = Convert.ToByte(motorI);
            motorPidPara[2] = Convert.ToByte(motorD);
            //加入数据BUFFER
            for (int i = 0, j = 37; i < 3; i++, j++)
                ctlData[j] = motorPidPara[i];
        }

        /// <summary>
        /// pid模式和ServoTest模式切换按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        bool pid_flag = false;
        bool motor_go_flag = false;
        private void button2_Click(object sender, EventArgs e)
        {
            if (pid_flag == false)
            {
                pid_flag = true;
                motor_go_flag = true;
                button2.Text = "切换为test";
            }
            else
            {
                pid_flag = false;
                button2.Text = "切换为PID";
            }
        }

        private void max_angle9_btn_Click(object sender, EventArgs e)
        {
            max_servo_angle = Convert.ToInt32(textBox5.Text);
        }
    }     
}
