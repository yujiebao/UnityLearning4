using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Lesson6 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是同步和异步
        //同步和异步主要用于修饰方法
        //同步方法:
        //当一个方法被调用时，调用者需要等待该方法执行完毕后返回才能继续执行
        //异步方法:
        //当一个方法被调用时立即返回，并获取一个线程执行该方法内部的逻辑，调用者不用等待该方法执行完毕

        //简单理解异步编程
        //我们会把一些不需要立即得到结果且耗时的逻辑设置为异步执行，这样可以提高程序的运行效率
        //避免由于复杂逻辑带来的的线程阻塞
        #endregion

        #region 知识点二 什么时候需要使用异步编程
        //需要处理的逻辑会严重影响主线程执行的流畅性时
        //我们需要使用异步编程
        //比如：
        //1.复杂逻辑计算时
        //2.资源下载 网络通讯
        //3.资源加载   大部分使用可以实现协同程序实现效果
        //等等
        #endregion

        #region 知识点三 异步方法async和await
        //async和await一般需要配合Task进行使用
        //async用于修饰函数、lambda表达式、匿名函数
        //await用于在函数中和async配对使用,主要作用是等待某个逻辑结束
        //此时逻辑会返回函数外部继续执行，直到等待的内容执行结束后，再继续执行异步函数内部逻辑
        //在一个async异步函数中可以有多个await等待关键字
        
        // TestAsync();
        // print("主线程执行");

        //使用async修饰异步方法
        //1.在异步方法中使用await关键字(不使用编译器会给出警告但不报错)，否则异步方法会以同步方式执行
        //2.异步方法名称建议以Async结尾
        //3.异步方法的返回值只能是void、Task、Task<>
        //4.异步方法中不能声明使用ref或out关键字修饰的变量
        
        //使用await等待异步内容执行完毕(一般和Task配合使用)
        //遇到await关键字时
        //1.异步方法将被挂起
        //2.将控制权返回给调用者
        //3.当await修饰内容异步执行结束后，继续通过调用者线程执行后面内容

        //举例说明
        //1.复杂逻辑计算 (利用Task新开线程进行计算 计算完毕后再使用 比如复杂的寻路算法)
        CalcPathAsync(this.gameObject);
        //2.计时器
        Time();

        //3.资源加载(Addressables的资源异步加载是可以使用async和await的)

        //注意:unity中大部分步方法是不支持异步关键字async和await的，我们只有使用协同程序进行使用
        //虽然官方 不支持 但是 存在第三方的工具(插件)可以让unity内部的一些异步加载的方法 支持 异步关键字
        //https://github.com/svermeulen/Unity3dAsyncAwaitUtil

        //虽然unity中的各种异步加载对异步方法支持不太好
        //但是当我们用到.Net 库中提供的一些API时，可以考虑使用异步方法
        //1.Web访问:Httpclient
        //2.文件使用:streamReader、StreamWriter、JsonSerializer、XmlReader、XmlWriter等等、
        //3.图像处理:BitmapEncoder、BitmapDecoder
        //一般.Net 提供的API中 方法名后面带有 Async的方法 都支持异步方法
        #endregion
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void TestAsync()  //确实await 就和同步方法一样
    {
        //1
        print("开始");
        //2
        await Task.Run(()=>{
            Thread.Sleep(1000);
        });

        //3
        print("结束");
    }

    public async void CalcPathAsync(GameObject gameObject)
    {
        int valua = 10;
        print("开始处理寻路逻辑");
        await Task.Run(()=>{
            //处理复杂计算(寻路逻辑) 这里通过休眠来模拟
            Thread.Sleep(1000);
            valua += 10;
            //是多线程  不能在多线程中访问场景中的对象
        });
        print("寻路逻辑处理完毕 "+ valua); //数据可以交互
        gameObject.transform.position = new Vector3(valua,0,0);  //外面可以访问  这里是主线程执行
    }

    public async void Time()
    {
        print("1");
        await Task.Delay(1000);
        print("2");
        await Task.Delay(1000);
        print("3");
        await Task.Delay(1000);
        print("4");
        await Task.Delay(1000);
        print("5");
    }
}
