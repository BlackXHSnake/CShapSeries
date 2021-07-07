using System;
using System.Reflection;
using System.Runtime.Remoting;
using Library;

namespace ReflectConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 加载程序集
            //通过程序集文件名或路径加载程序集
            Assembly assembly = Assembly.LoadFrom("Library.dll");
            #endregion

            #region 获取类型
            Type[] types = assembly.GetTypes();//获取全部类型
            Type myReflectType = assembly.GetType("Library.MyReflect");//获取指定类型

            //通过实例获取类型
            MyReflect myReflect = new MyReflect();
            myReflectType = myReflect.GetType();

            myReflectType = typeof(MyReflect);//通过类型名称获取类型
            #endregion

            #region 创建实例
            object objMyReflect = Activator.CreateInstance(myReflectType);//通过类型创建实例

            //通过程序集创建实例
            ObjectHandle objHandle = Activator.CreateInstanceFrom("Library.dll", "Library.MyReflect");
            objMyReflect = objHandle.Unwrap();//解包获取对象

            //创建私有构造实例
            Type privateType = assembly.GetType("Library.PrivateMyReflect");
            object objPrivate = Activator.CreateInstance(privateType, true);

            //创建泛型实例
            Type genericType = assembly.GetType("Library.MyReflect`1");//获取泛型类型
            genericType = genericType.MakeGenericType(typeof(string));//设置类型参数
            object objGenericType = Activator.CreateInstance(genericType);
            #endregion

            #region 获取信息
            #region 获取全部公共信息
            //获取全部公共的字段、属性、方法
            FieldInfo[] fieldInfos = myReflectType.GetFields();
            PropertyInfo[] propertyInfos = myReflectType.GetProperties();
            MethodInfo[] methodInfos = myReflectType.GetMethods();

            //获取全部指定标记的字段、属性、方法
            fieldInfos = myReflectType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfos = myReflectType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            methodInfos = myReflectType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            #endregion

            #region 获取指定名称、标识信息
            //获取指定名称的字段、属性、方法
            FieldInfo fieldInfo = myReflectType.GetField("intField");
            PropertyInfo propertyInfo = myReflectType.GetProperty("IntProperty");
            MethodInfo methodInfo = myReflectType.GetMethod("PublicFunction");

            //获取指定名称、参数类型的方法
            Type[] paraTypes = new Type[] { typeof(int) };//设置方法参数类型
            methodInfo = myReflectType.GetMethod("PublicFunction2", paraTypes);

            //获取指定标记、名称的字段、属性、方法
            fieldInfo = myReflectType.GetField("_intField", BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfo = myReflectType.GetProperty("_IntProperty", BindingFlags.NonPublic | BindingFlags.Instance);
            methodInfo = myReflectType.GetMethod("PrivateFunction", BindingFlags.NonPublic | BindingFlags.Instance);

            //获取指定标记、名称、参数类型的方法
            paraTypes = new Type[] { typeof(int) };//设置方法参数类型
            methodInfo = myReflectType.GetMethod("PublicFunction2", BindingFlags.Public | BindingFlags.Instance
                , null, paraTypes, null);
            #endregion

            #region 获取泛型信息
            MethodInfo genericMethodInfo = genericType.GetMethod("GenericsFunction", 1, Type.EmptyTypes);//获取泛型方法

            //获取泛型参数方法
            Type genericTypePara = Type.MakeGenericMethodParameter(0);//创建泛型参数
            paraTypes = new Type[] { genericTypePara };//设置方法参数类型
            genericMethodInfo = genericType.GetMethod("GenericsFunction", 2, paraTypes);
            #endregion
            #endregion

            #region 动态调用
            fieldInfo.SetValue(objMyReflect, 520);//设置字段值
            object field = fieldInfo.GetValue(objMyReflect);//获取字段值

            propertyInfo.SetValue(objMyReflect, 1314);//设置属性值
            object property = propertyInfo.GetValue(objMyReflect);//获取属性值

            //调用方法
            object[] objectParas = new object[] { 9527 };//创建参数值
            object result = methodInfo.Invoke(objMyReflect, objectParas);

            //调用泛型方法
            objectParas = new object[] { 666 };//创建参数值
            genericMethodInfo = genericMethodInfo.MakeGenericMethod(typeof(int), typeof(string));
            result = genericMethodInfo.Invoke(objGenericType, objectParas);
            #endregion

            Console.ReadKey();
        }
    }
}
