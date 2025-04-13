using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Lesson4 : MonoBehaviour
{
    // Thread t;
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 回顾
        //1.Unity支持多线程
        //2.unity中开启的多线程不能使用主线程中的对象
        //3.Unity中开启多线程后一定记住关闭
        // t = new Thread(() =>
        // {
        //     while (true)
        //     {

        //         Thread.Sleep(1000);
        //         Debug.Log("线程执行");
        //     }
        // });
        // t.Start();
        #endregion

        #region 补充知识 线程池
        //命名空间:System.Threading
        //类名:ThreadPool(线程池)

        //在多线程的应用程序开发中，频繁的创建删除线程会带来性能消耗，产生内存垃圾
        //为了避免这种开销  c#推出了 线程池ThreadPool类

        //ThreadPool中有若干数量的线程，如果有任务需要处理时，会从线程池中获取一个空闲的线程来执行任务
        //任务执行完毕后线程不会销毁，而是被线程池回收以供后续任务使用
        //当线程池中所有的线程都在忙碌时，又有新任务要处理时，线程池才会新建一个线程来处理该任务
        //如果线程数量达到设置的最大值，任务会排队，等待其他任务释放线程后再执行
        //线程池能减少线程的创建，节省开销，可以减少GC垃圾回收的触发

        //线程池相当于就是一个专门装线程的缓存池(unity小框架套课中有对缓存池的详细讲解)
        //优点:节省开销，减少线程的创建，进而有效减少GC触发
        //缺点:不能控制线程池中线程的执行顺序，也不能获取线程池内线程取消/异常/完成的通知

        //ThreadPool是一个静态类   里面提供了很多静态成员
        //其中相对重要的方法有

        //1.获取可用的工作线程数和I/O线程数  
        int num1;
        int num2;
        ThreadPool.GetAvailableThreads(out num1, out num2);
        print(num1+" "+num2);

        //3.设置线程池中可以同时处于活动状态的 工作线程的最大数目和I/O线程的最大数目
        // 大于次数的请求将保持排队状态，直到线程池线程变为可用
        // 更改成功返回true，失败返回false
        if(ThreadPool.SetMaxThreads(200, 200))
        {
            print("设置成功");
        }

        //2.获取线程池中工作线程的最大数目和I/O线程的最大数目
        ThreadPool.GetMaxThreads(out num1, out num2);
        print(num1+" "+num2);

        //4.获取线程池中工作线程的最小数目和I/0线程的最小数目
        ThreadPool.GetMinThreads(out num1, out num2);
        print(num1+" "+num2);
        
        //5.设置线程池中工作线程的最小数目和I/O线程的最小数目
        if(ThreadPool.SetMinThreads(10, 10))
        {
            print("设置成功");
        }
        ThreadPool.GetMinThreads(out num1, out num2);
        print(num1+" "+num2);
        
        //6.将方法排入队列以便执行，当线程池中线程变得可用时执行
        ThreadPool.QueueUserWorkItem((obj) =>
        {
            print(obj); 
            print("线程池执行");}
        ,"123");
        print("主线程执行");
        #endregion

        for(int i=0;i<100;i++)
        {
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                print("第"+obj+"个任务"); 
                print("线程池执行");}
            ,i);

        }
    }

    // void OnDestroy()
    // {
    //     t.Abort();
    // }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("主线程执行");
    }
}
