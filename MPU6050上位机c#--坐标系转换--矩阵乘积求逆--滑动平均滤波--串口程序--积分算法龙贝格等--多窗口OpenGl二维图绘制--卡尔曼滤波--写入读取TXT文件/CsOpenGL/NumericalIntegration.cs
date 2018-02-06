using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsOpenGL
{
    class NumericalIntegration
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NumericalIntegration()
        {
 
        }
        /// <summary>
        /// 梯形公式计算积分
        /// </summary>
        /// <param name="data"></param>
        /// <param name="N"></param>
        /// <param name="up"></param>
        /// <param name="down"></param>
        /// <returns></returns>
        public double TiXingFormula(double[] data, double up, double down)
        {
            return ((up - down) / 2) * (data[0] + data[1]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="N"></param>
        /// <param name="up"></param>
        /// <param name="down"></param>
        /// <returns></returns>
        //public double FuHuaTiXingFormula(List<double> data, int N, double up, double down)
        //{
        //    return (up - down) / 2 * (data[1] + data[0]);
        //}

        #region 网上下载代码，fun在工程中使用时只有一串函数值，可以参考其算法写法
        /// 我自己上面的代码没有考虑Romberg积分公式的精度问题，一般来说三次二分就可以达到计算精度
        /// <summary>
        /// 梯形公式
        /// </summary>
        /// <param name="fun">被积函数</param>
        /// <param name="up">积分上限</param>
        /// <param name="down">积分下限</param>
        /// <returns>积分值</returns>
        public static double TiXing(Func<double, double> fun, double up, double down)
        {
            return (up - down) / 2 * (fun(up) + fun(down));
        }
        /// <summary>
        /// 辛普森公式
        /// </summary>
        /// <param name="fun">被积函数</param>
        /// <param name="up">积分上限</param>
        /// <param name="down">积分下限</param>
        /// <returns>积分值</returns>
        public static double Simpson(Func<double, double> fun, double up, double down)
        {
            return (up - down) / 6 * (fun(up) + fun(down) + 4 * fun((up + down) / 2));
        }
        /// <summary>
        /// 科特克斯公式
        /// </summary>
        /// <param name="fun">被积函数</param>
        /// <param name="up">积分上限</param>
        /// <param name="down">积分下限</param>
        /// <returns>积分值</returns>
        public static double Cotes(Func<double, double> fun, double up, double down)
        {
            double C = (up - down) / 90 * (7 * fun(up) + 7 * fun(down) + 32 * fun((up + 3 * down) / 4)
                     + 12 * fun((up + down) / 2) + 32 * fun((3 * up + down) / 4));
            return C;
        }

        /// <summary>
        /// 复化梯形公式
        /// </summary>
        /// <param name="fun">被积函数</param>
        /// <param name="N">区间划分快数</param>
        /// <param name="up">积分上限</param>
        /// <param name="down">积分下限</param>
        /// <returns>积分值</returns>
        public static double FuHuaTiXing(Func<double, double> fun, int N, double up, double down)
        {
            double h = (up - down) / N;
            double result = 0;
            double x = down;
            for (int i = 0; i < N - 1; i++)
            {
                x += h;
                result += fun(x);
            }
            result = (fun(up) + result * 2 + fun(down)) * h / 2;
            return result;
        }

        /// <summary>
        /// 复化辛浦生公式
        /// </summary>
        /// <param name="fun">被积函数</param>
        /// <param name="N">区间划分快数</param>
        /// <param name="up">积分上限</param>
        /// <param name="down">积分下限</param>
        /// <returns>积分值</returns>
        public static double FSimpson(Func<double, double> fun, int N, double up, double down)
        {
            double h = (up - down) / N;
            double result = 0;
            for (int n = 0; n < N; n++)
            {
                result += h / 6 * (fun(down) + 4 * fun(down + h / 2) + fun(down + h));
                down += h;
            }
            return result;
        }
        /// <summary>
        /// 复化科特斯公式
        /// </summary>
        /// <param name="fun">被积函数</param>
        /// <param name="N">区间划分快数</param>
        /// <param name="up">积分上限</param>
        /// <param name="down">积分下限</param>
        /// <returns>积分值</returns>
        public static double FCotes(Func<double, double> fun, int N, double up, double down)
        {
            double h = (up - down) / N;
            double result = 0;
            for (int n = 0; n < N; n++)
            {
                result += h / 90 * (7 * fun(down) + 32 * fun(down + h / 4) + 12 * fun(down + h / 2) +
                        32 * fun(down + 3 * h / 4) + 7 * fun(down + h));
                down += h;
            }
            return result;
        }
        /// <summary>
        /// 龙贝格算法
        /// </summary>
        /// <param name="fun">被积函数</param>
        /// <param name="e">结果精度</param>
        /// <param name="up">积分上限</param>
        /// <param name="down">积分下限</param>
        /// <returns>积分值</returns>
        public static double Romberg(Func<double, double> fun, double e, double up, double down)
        {
            double R1 = 0, R2 = 0;
            int k = 0; //2的k次方即为N（划分的子区间数）
            R1 = (64 * C(fun, 2 * (int)Math.Pow(2, k), up, down) - C(fun, (int)Math.Pow(2, k++), up, down)) / 63;
            R2 = (64 * C(fun, 2 * (int)Math.Pow(2, k), up, down) - C(fun, (int)Math.Pow(2, k++), up, down)) / 63;
            while (Math.Abs(R2 - R1) > e)
            {
                R1 = R2;
                R2 = (64 * C(fun, 2 * (int)Math.Pow(2, k), up, down) - C(fun, (int)Math.Pow(2, k++), up, down)) / 63;
            }
            return R2;
        }
        private static double S(Func<double, double> fun, int N, double up, double down)
        {
            return (4 * FuHuaTiXing(fun, 2 * N, up, down) - FuHuaTiXing(fun, N, up, down)) / 3;
        }
        private static double C(Func<double, double> fun, int N, double up, double down)
        {
            return (16 * S(fun, 2 * N, up, down) - S(fun, N, up, down)) / 15;
        }
        #endregion
    }
}
