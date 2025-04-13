using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Lesson5 : MonoBehaviour
{
    private Task<int> t5;

    private Task<int> t6;
    private Task<int> t4;
    CancellationTokenSource cts;


    private bool isRun = true;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 认识Task
        //命名空间:system.Threading.Tasks
        //类名:Task
        //Task顾名思义就是任务的意思
        //Task是在线程池基础上进行的改进，它拥有线程池的优点，同时解决了使用线程池不易控制的弊端
        //它是基于线程池的优点对线程的封装，可以让我们更方便高效的进行多线程开发

        //简单理解:
        //Task的本质是对线程Thread的封装，它的创建遵循线程池的优点，并且可以更方便的让我们控制线程    --性能更好
        //一个Task对象就是一个线程
        #endregion
    
        #region 知识点二 创建无返回值的Task的三种方式
        //1.通过new一个Task对象传入委托函数并启动
        // Task t1 = new Task(() =>
        // {
        //     int i = 0;
        //     while(isRun)
        //     {
        //         print("方式一:" + i++);
        //         Thread.Sleep(1000);
        //     }
        // });
        // t1.Start();
        // //2.通过Task中的Run静态方法传入委托函数
        // Task t2 = Task.Run(() =>
        // {
        //     int i = 0;
        //     while(isRun)
        //     {
        //         print("方式二:" + i++);
        //         Thread.Sleep(1000);
        //     }
        // });
        // //3.通过Task.Factory中的startNew静态方法传入委托函数
        // Task t3 = Task.Factory.StartNew(() =>
        // {
        //     int i = 0;
        //     while(isRun)
        //     {
        //         print("方式三:" + i++);
        //         Thread.Sleep(1000);
        //     }
        // });
        #endregion
    
        #region 知识点三 创建有返回值的Task的三种方式
        //1.通过new一个Task对象传入委托函数并启动
    //    t4 = new Task<int>(() =>
    //     {
    //         int i = 0;
    //         while(isRun)
    //         {
    //             print("方式一:" + i++);
    //             Thread.Sleep(1000);
    //         }
    //         return i;
    //     });
    //    t4.Start();
    //    //2.通过Task中的Run静态方法传入委托函数
    //     t5 = Task<int>.Run(() =>
    //     {
    //         int i = 0;
    //         while(isRun)
    //         {
    //             print("方式二:" + i++);
    //             Thread.Sleep(1000);
    //         }
    //         return i;
    //     });
    //     //3.通过Task.Factory中的startNew静态方法传入委托函数
    //     t6 = Task<int>.Factory.StartNew(() =>
    //     {
    //         int i = 0;
    //         while(isRun)
    //         {
    //             print("方式三:" + i++);
    //             Thread.Sleep(1000);
    //         }
    //         return i;
    //     });

        //获取返回值
        //注意:
        //Resut获取结果时会阻塞线程
        //即如果task没有执行完成
        //会等待task执行完成获取到Result
        //然后再执行后边的代码,
        // 也就是说 执行到这句代码时 如果我们的Task中是死循环  主线程就会被卡死
        #endregion

        #region 同步执行Task
        //刚才我们举的例子都是通过多线程异步执行的
        //如果你希望Task能够同步执行
        //只需要调用Task对象中的Runsynchronously方法
        //注意:需要使用 new Task对象的方式，因为Run和startNew在创建时就会启动
        // Task t = new Task(() =>
        // {
        //     // int i = 0;
        //     // while(isRun)
        //     {
        //         print("哈哈哈");
        //         Thread.Sleep(1000);
        //     }
        // });
        // t.RunSynchronously();  //使用Runsynchronously同步执行
        // print("主线程执行");
        #endregion

        #region Task线程阻塞的方法
        // //1.wait方法:等待任务执行完毕，再执行后面的内容
        // Task t1 = Task.Run(() =>
        // {
        //     for(int i = 0; i < 10; i++)
        //     {
        //         print("方式一:" + i);
        //     }
        // });

        // Task t2 = Task.Run(() =>
        // {
        //     for(int i = 0; i < 100; i++)
        //     {
        //         print("方式二:" + i);
        //     }
        // });
        // t1.Wait();    //t1执行之后 在执行之后的代码  
        //2.WaitAny静态方法:传入任务中任意一个任务结束就继续执行
        // Task.WaitAny(t1,t2);   //支持多个Task
        //3.waitAll静态方法:任务列表中所有任务执行结束就继续执行
        // Task.WaitAll(t1,t2);   //支持多个Task  要所有都执行结束
        
        // print("主线程执行");
        #endregion

        #region Task完成后继续其它Task(任务延续)
        //1.whenAll静态方法+continuewith方法:传入任务完毕后再执行某任务
        // Task.WhenAll(t1,t2).ContinueWith((obj)=>{
        //      int i = 0;
        //     while(isRun)
        //     {
        //         print(i++);
        //         Thread.Sleep(1000);
        //     }
        // });、

        //写法二
        // Task.Factory.ContinueWhenAll(new Task[]{t1,t2}, (obj)=>{
        //     int i = 0;
        //     while(isRun)
        //     {
        //         print(i++);
        //         Thread.Sleep(1000);
        //     }
        // });
        
        //2.whenAny静态方法 +continuewith方法:传入任务只要有一个执行完毕后再执行某任务
        // Task.WhenAny(t1,t2).ContinueWith((obj)=>{
        //         int i = 0;
        //         while(isRun)
        //         {
        //             print(i++);
        //             Thread.Sleep(1000);
        //         }
        //     });

        //写法二
        // Task.Factory.ContinueWhenAny(new Task[]{t1,t2}, (obj)=>{
        //     int i = 0;
        //     while(isRun)
        //     {
        //         print(i++);
        //         Thread.Sleep(1000);
        //     }
        // });
        #endregion

        #region 取消Task执行
        //方法一:通过加入bool标识 控制线程内死循环的结束
        
        //方法二:通过cancellationTokensource取消标识源类 来控制
        //cancellationTokensource对象可以达到延迟取消、取消回调等功能    --相较于方法一 多了延迟取消、取消回调等功能
        cts = new CancellationTokenSource();
        //延迟取消
        cts.CancelAfter(5000);   //启动后延迟5秒取消  不是取消后延迟5秒
        cts.Token.Register(()=>{    //当取消时执行
            print("取消了"); 
        });
        Task t3 = Task.Run(() =>
        {
            int i = 0;
            while(!cts.IsCancellationRequested)
            {
                print("方式一:" + i++);
                Thread.Sleep(1000);
            }
        },cts.Token);
        #endregion
    }

 
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // isRun = !isRun;
            // print("暂停");
            //打印Task的返回值
            // print(t4.Result);
            // print(t5.Result);
            // print(t6.Result);
            cts.Cancel();
        }
    }
}