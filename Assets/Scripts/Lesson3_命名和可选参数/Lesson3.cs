using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 命名和可选参数
        //命名
        Test(1,2,true);
        Test(f :2.5f, i : 3, b : true);   //命名参数
        // Test(i : 1, b : true);    //只能省略有默认值的参数

        Test2(1);
        Test2(i : 1, b : true);   //通过命名的方式赋值   减少重载的数目(为参数设置默认值 形成类似重载)
        #endregion

        #region 动态类型  运行时才确定是什么类型    不建议使用
        //关键词:dynamic
        //作用:通过dynamic类型标识变量的使用和对其成员的引用绕过编译时类型检查
        //改为在运行时解析这些操作。
        //在大多数情况下，dynamic类型和object类型行为类似
        //任何非Null表达式都可以以转换为dynamic类型。
        //dynamic类型和object类型不同之处在于，编译器不会对包含类型 dynamic 的表达式的操作进行解析或类型检查
        //编译器将有关该操作信息打包在一起，之后这些信息会用于在运行时评估操作。
        //在此过程中，dyhamic 类型的变量会编译为 object 类型的变量。因此，dynamic 类型只在编译时存在，在运行时则不存在。

        //注意:1.使用dynamic功能 需要将unity的.Net API兼容级别切换为.Net 4.x
              //2.IL2CPP 不支持 c# dynamic 关键字。它需要 JIT 编译，而 IL2CPP 无法实现
              //3.动态类型是无法自动补全方法的，我们在书写时一定要保证方法的拼写正确性所以该功能我们只做了解，不建议大家使用

        // dynamic dyn = 1;
        // object obj = 2;
        // print(dyn.GetType());
        // print(obj.GetType());

        // object a = new object();
        // dynamic b = a;
        // b.way();   //编译器不会报错   相比反射代码更少
        //好处:动态类型可以节约代码量 当不确定对象类型，但是确定对象成员时，可以使用动态类型
        //通过反射处理某些功能时，也可以考虑使用动态类型来替换它
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Test(int i ,float f , bool b)
    {
    }

    public void Test2(int i  = 1,float f = 2.52f, bool  b = true)
    {
    }
}
