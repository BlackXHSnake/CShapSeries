using Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EventConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 事件初始化
            MyEvent.InitEvent();
            #endregion

            #region 事件调用
            #region 同步调用
            MyEvent.ExecuteEvent();
            #endregion

            #region 异步调用
            MyEvent.ExecuteEventAsync();
            #endregion
            #endregion

            #region 事件订阅/取消订阅
            Action action1 = () => { Console.WriteLine("事件1"); };
            Action action2 = () => { Console.WriteLine("事件2"); };

            //订阅
            MyEvent.actionEvent += action1;
            MyEvent.actionEvent += action2;

            //取消订阅
            MyEvent.actionEvent -= action1;
            #endregion

            Console.ReadKey();
        }
    }
}