using System;
using System.Collections.Generic;
using System.Reflection;
using Library;

namespace AttributeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 获取全部特性
            Type type = typeof(MyClass);
            object[] attributes = type.GetCustomAttributes(true);//获取全部特性
            if (type.IsDefined(typeof(MyTestAttribute), true))//判断是否存在指定类型的特性
            {
                attributes = type.GetCustomAttributes(typeof(MyTestAttribute), true);//获取指定类型的全部特性
            }
            #endregion

            #region 获取成员特性
            PropertyInfo propertyInfo = type.GetProperty("IntProperty");
            attributes = propertyInfo.GetCustomAttributes(true);//获取属性全部特性
            if (propertyInfo.IsDefined(typeof(MyTestAttribute), true))//判断是否存在指定类型的特性
            {
                attributes = propertyInfo.GetCustomAttributes(typeof(MyTestAttribute), true);//获取指定类型的全部特性
            }
            #endregion

            Console.ReadKey();
        }
    }
}
