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

namespace CsOpenGL
{
    class Filtering
    {
        #region kalman滤波算法变量
        public struct KalmanStruct
        {
            public double Cv;                         //测量值高斯白噪声方差
            public double Cw;                       //控制系统高斯白噪声方差
            public double h;                               //测量系统参数
            public double a;                               //预测系统参数  为1时认为预测的当前值等于上一个预测最优值
            public double[] p;        //预测系统方差
            public double[] X;        //预测过程值
            public double[] s;     //kalman最优值
            public double kg;                              //卡尔曼增益
        }
        KalmanStruct kalman;
        #endregion

        public Filtering()
        {
            kalman.Cv = 0.004964;
            kalman.Cw = 0.000025;
            kalman.h = 1;
            kalman.a = 1;
            kalman.p = new double[2] { 0, 1 };
            kalman.X = new double[2] { 0, 0 };
            kalman.s = new double[3] { 0, 0, 0 };
            kalman.kg = 0;
        }

        //滤波方法选择字段
        private enum selectFilter { movingAverage, average, kalman, removeBurrs};
        private selectFilter filter;
        //private string filterStr;
        ////滤波方法选择属性
        //public string SelectFilter
        //{
        //    set
        //    {
        //        filterStr = value;
        //    }
        //}
        /// <summary>
        /// 字符串转换为枚举能够识别的符号
        /// </summary>
        void EnumConvert(string filterStr)
        {
            filter = (selectFilter)Enum.Parse(typeof(selectFilter), filterStr, true);
        }
        /// <summary>
        /// 根据选择的滤波方法进行滤波
        ///  Array.Sort(data) 数组data中的数据从小到大快速排序  库函数中的函数
        ///  两个同类型的List相互相等的话即  list a = list b，此时a,b指向同一个对象
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public double FilteringAlgorithm(string filterStr, List<double> data)
        {
            EnumConvert(filterStr);
            
            double filteredData = 0.0;
            switch (filter)
            {
                case selectFilter.movingAverage:
                    List<double> dataTemp = new List<double> { };
                    foreach (double i in data)
                    {
                        dataTemp.Add(i);
                    }
                    dataTemp.Sort();
                    double sum = 0.0;
                    for (int i = 1; i < dataTemp.Count - 1; i++)                         //去掉最大最小值
                    {
                        sum += dataTemp[i];
                    }
                    filteredData = sum / (dataTemp.Count - 2);
                    break;
                case selectFilter.average:
                    double sumdata = 0.0;
                    for (int i = 0; i < data.Count; i++)                         //去掉最大最小值
                    {
                        sumdata += data[i];
                    }
                    filteredData = sumdata / data.Count;
                    break;
                case selectFilter.kalman:                                       //只需要一个测量值就可以进行滤波
                    kalman.X[1] = kalman.a * kalman.X[0];
                    kalman.p[1] = kalman.a * kalman.p[0] * kalman.a + kalman.Cw;
                    kalman.kg = kalman.p[1] * kalman.h / (kalman.h * kalman.p[1] * kalman.h + kalman.Cv);                   
                    kalman.s[0] = kalman.s[1];
                    kalman.s[1] = kalman.s[2];
                    kalman.s[2] = kalman.s[1] + kalman.kg * (data[data.Count - 1] - kalman.h * kalman.s[1]);
                    kalman.p[0] = (1 - kalman.kg * kalman.h) * kalman.p[1];
                    filteredData = (kalman.s[0] + kalman.s[1] + kalman.s[2]) / 3;
                    break;
                case selectFilter.removeBurrs:  //去尖峰平均滤波
                    List<double> dataBurrsTemp = new List<double> { };
                    foreach (double i in data)
                    {
                        dataBurrsTemp.Add(i);
                    }
                    dataBurrsTemp.Sort();
                    double sumBurrs = 0.0;
                    for (int i = 1; i < dataBurrsTemp.Count - 1; i++)                         //去掉最大最小值
                    {
                        sumBurrs += dataBurrsTemp[i];
                    }
                    filteredData = sumBurrs / (dataBurrsTemp.Count - 2);
                    break;
                default:
                    break;
            }
            return filteredData;
        }

        public double KalmanFilter(double data)
        {
            double filteredData = 0.0;

            kalman.X[1] = kalman.a * kalman.X[0];
            kalman.p[1] = kalman.a * kalman.p[0] * kalman.a + kalman.Cw;
            kalman.kg = kalman.p[1] * kalman.h / (kalman.h * kalman.p[1] * kalman.h + kalman.Cv);
            kalman.s[0] = kalman.s[1];
            kalman.s[1] = kalman.s[2];
            kalman.s[2] = kalman.s[1] + kalman.kg * (data - kalman.h * kalman.s[1]);
            kalman.p[0] = (1 - kalman.kg * kalman.h) * kalman.p[1];
            filteredData = (kalman.s[0] + kalman.s[1] + kalman.s[2]) / 3;

            return filteredData;
        }


        /// <summary>
        /// 快速排序（目标数组，数组的起始位置，数组的终止位置）
        /// </summary>
        /// <param name="array"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
         private static void QuickSortFunction(int[] array, int low, int high)
         {
             try
             {
                 int keyValuePosition;   //记录关键值的下标

                 //当传递的目标数组含有两个以上的元素时，进行递归调用。（即：当传递的目标数组只含有一个元素时，此趟排序结束）
                 if (low < high) 
                 {
                     keyValuePosition = keyValuePositionFunction(array, low, high);  //获取关键值的下标（快排的核心）
 
                     QuickSortFunction(array, low, keyValuePosition - 1);    //递归调用，快排划分出来的左区间
                     QuickSortFunction(array, keyValuePosition + 1, high);   //递归调用，快排划分出来的右区间
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "Error");
             }
         }
 
        /// <summary>
         /// 快速排序的核心部分：确定关键值在数组中的位置，以此将数组划分成左右两区间，关键值游离在外。（返回关键值应在数组中的下标）
        /// </summary>
        /// <param name="array"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
         private static int keyValuePositionFunction(int[] array, int low, int high)
         {
             int leftIndex = low;        //记录目标数组的起始位置（后续动态的左侧下标）
             int rightIndex = high;      //记录目标数组的结束位置（后续动态的右侧下标）
 
             int keyValue = array[low];  //数组的第一个元素作为关键值
             int temp;
 
             //当 （左侧动态下标 == 右侧动态下标） 时跳出循环
             while (leftIndex < rightIndex)
             {
                 while (leftIndex < rightIndex && array[leftIndex] <= keyValue)  //左侧动态下标逐渐增加，直至找到大于keyValue的下标
                 {
                     leftIndex++;
                 }
                 while (leftIndex < rightIndex && array[rightIndex] > keyValue)  //右侧动态下标逐渐减小，直至找到小于或等于keyValue的下标
                 {
                     rightIndex--;
                 }
                 if(leftIndex < rightIndex)  //如果leftIndex < rightIndex，则交换左右动态下标所指定的值；当leftIndex==rightIndex时，跳出整个循环
                 {
                     temp = array[leftIndex];
                     array[leftIndex] = array[rightIndex];
                     array[rightIndex] = temp;
                 }
             }
 
             //当左右两个动态下标相等时（即：左右下标指向同一个位置），此时便可以确定keyValue的准确位置
             temp = keyValue;
             if (temp < array[rightIndex])   //当keyValue < 左右下标同时指向的值，将keyValue与rightIndex - 1指向的值交换，并返回rightIndex - 1
             {
                 array[low] = array[rightIndex - 1];
                 array[rightIndex - 1] = temp;
                 return rightIndex - 1;
             }
             else //当keyValue >= 左右下标同时指向的值，将keyValue与rightIndex指向的值交换，并返回rightIndex
             {
                 array[low] = array[rightIndex];
                 array[rightIndex] = temp;
                 return rightIndex;
             }
         }
    }
}
