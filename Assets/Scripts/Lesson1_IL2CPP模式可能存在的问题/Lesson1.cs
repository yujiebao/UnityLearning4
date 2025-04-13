using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class A
{
 
}

public class B
{
}

public class C
{
}

//解决IL2CPP泛型类问题
public class IL2CPP_info
{
    public List<A> list1;
    public List<B> list2;
    public List<C> list3;

    public Dictionary<int, A> dict1;

    public void Test<T>()
    {
    }

    public static void Test()
    {
        //解决泛型方法问题
        IL2CPP_info info = new IL2CPP_info();
        info.Test<A>();
        info.Test<B>();
    }
}

public class Lesson1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 安装Unity IL2CPP
        //在unityhub中安装对应平台的unity IL2CPP
        #endregion

        #region 知识点二 IL2CPP存在的问题————类型裁剪
        //IL2CPP在打包时会自动对Unity工程的DLL进行裁剪，将代码中没有引用到的类型裁剪掉
        //以达到减小发布后包的尺寸的目的。
        //然而在实际使用过程中，很多类型有可能会被意外剪裁掉
        //造成运行时抛出找不到某个类型的异常。
        //特别是通过反射等方式使用类在编译时无法得知的函数调用，在运行时都很有可能遇到问题

        //解决方案:
        //1.IL2CPP处理模式时，将Playersetting->other setting->Managed stripping Level(代码剥离)设置为Low
        // Disable:Mono模式下才能设置为不删除任何代码
        // Low:默认低级别，保守的删除代码，删除大多数无法访问的代码，同时也最大程度减少剥离实际使用的代码的可能
        // Medium:中等级别，不如低级别剥离谨慎，也不会达到高级别的极端
        // Hight:高级别，尽可能多的删除无法访问的代码，有限优化尺寸减小。如果选择该模式一般需要配合link.xm1使用

        //2.通过unity提供的1ink.xm1方式来告诉unity引擎，哪些类型是不能够被剪裁掉的
        //在unity工程的Assets日录中(或其任何子目录中)建立个叫link.xm1的XML文件
        #endregion

        #region 知识点三 IL2CPP打包存在的问题————泛型问题
        //我们上节课提到了IL2CPP和Mono最大的区别是 不能在运行时动态生成代码和类型
        //就是说 泛型相关的内容，如果你在打包生成前没有把之后想要使用的泛型类型显示使用一次
        //那么之后如果使用没有被编译的类型，就会出现找不到类型的报错
        
        //举例:List<A>和List<B>中A和B是我们自定义的类，
        //我能必须在代码中显示的调用过，IL2CPP才能保留List<A>和List<B>两个类型。
        //如果在热更新时我们调用List<c>，但是它之前并没有在代码中显示调用过，
        //那么这时就会出现报错等问题。主要就是因为JIT和AOT两个编译模式的不同造成的

        //解决方案:
        //泛型类:声明一个类，然后在这个类中声明一些public的泛型类变量
        //泛型方法:随便写一个静态方法，在这个将泛型方法在其中调用一下。这个静态方法无需被调用
        //这样做的目的其实就是在预言编译之前让IL2CPP知道我们需要使用这个内容
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
