using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.ExceptionServices;
using System.Windows.Forms;
using System.ComponentModel;

//多媒体定时器
namespace CsOpenGL
{
    public class MmTimerNet : IDisposable
    {
        // 摘要:   
        //     设置或获取被执行的委托  
        public MmTimerNet.DgMmTimerShot MmTimerShot;

        // 摘要:   
        //     构造函数  
        public MmTimerNet();

        //// 摘要:   
        ////     设置或获取WPF异步调用方法对象  
        //public System.Windows.Threading.Dispatcher DispatcherObj { get; set; }  
        //  
        // 摘要:   
        //     获取或设置定时器的使能状态  
        public bool Enable { get; set; }
        //  
        // 摘要:   
        //     接收消息的窗口句柄  
        public ValueType Handle { get; set; }
        //  
        // 摘要:   
        //     获取或设置定时器的触发周期, ms  
        public ushort Interval { get; set; }
        //  
        // 摘要:   
        //     获取或设置定时器的触发方式, 0触发一次, 1按给定的周期触发  
        public ushort ShotMode { get; set; }
        //  
        // 摘要:   
        //     设置自定义消息的值, 必须大于0x0400,否则无效 获取自
        public uint ShotWindowMsg { get; set; }
        //  
        // 摘要:   
        //     设置或获取WinForm异步调用方法对象  
        public ISynchronizeInvoke SynchronizeObj { get; set; }
        //  
        // 摘要:   
        //     获取定时器的ID  
        public ushort TimerID { get; }
        //  
        // 摘要:   
        //     获取定时器的分辨率, ms  
        public static ushort TimerRes { get; }

        // 摘要:   
        //     异步调用触发的事件  
        public event EventHandler<EventArgs> Tick;

        // 摘要:   
        //     析构函数  
        public override sealed void Dispose();
        [HandleProcessCorruptedStateExceptions]
        protected virtual void Dispose(bool A_0);
        protected void raise_Tick(object value0, EventArgs value1);

        // 摘要:   
        //     多媒体定时器触发执行的委托类型  
        public delegate void DgMmTimerShot();
    }
}
