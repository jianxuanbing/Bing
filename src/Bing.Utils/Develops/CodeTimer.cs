using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bing.Utils.Develops
{
    /// <summary>
    /// 代码性能测试计时器
    /// </summary>
    public static class CodeTimer
    {
        #region Private(私有方法)
        /// <summary>
        /// 获取当前CPU循环次数
        /// </summary>
        /// <returns></returns>
        private static ulong GetCycleCount()
        {
            ulong cycleCount = 0;
            NativeMethods.QueryThreadCycleTime(NativeMethods.GetCurrentThread(), ref cycleCount);
            return cycleCount;
        }
        #endregion

        /// <summary>
        /// 计时器初始化，对计时器进行初始化操作，同时对后续操作进行预热，以避免初次操作带来的性能影响
        /// </summary>
        public static void Initialize()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Time(string.Empty, 1, () => { });
        }

        /// <summary>
        /// 计时器，传入操作标识名，重复次数，操作过程获取操作的性能数据
        /// </summary>
        /// <param name="name">操作标识名</param>
        /// <param name="iteration">重复次数</param>
        /// <param name="action">操作过程的Action</param>
        public static void Time(string name, int iteration, Action action)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            // 保留当前控制台前景色，并使用黄色输出名称参数
            ConsoleColor currentForeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(name);

            // 强制GC进行收集，并记录目前各代已经收集的次数
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            int[] gcCounts = new int[GC.MaxGeneration + 1];
            for (int i = 0; i < GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(1);
            }

            // 执行代码，记录下消耗的时间及CPU时钟周期
            Stopwatch watch = new Stopwatch();
            watch.Start();
            ulong cycleCount = GetCycleCount();
            for (int i = 0; i < iteration; i++)
            {
                action();
            }
            ulong cpuCycles = GetCycleCount() - cycleCount;
            watch.Stop();

            // 恢复控制台默认前景色，并打印出消耗时间及CPU时钟周期
            Console.ForegroundColor = currentForeColor;
            Console.WriteLine("\tTime Elapsed:\t" + watch.Elapsed.TotalMilliseconds + "ms");
            Console.WriteLine("\tCPU Cycles:\t" + cpuCycles.ToString("N0"));

            // 打印执行过程中各代垃圾收集回收次数
            for (int i = 0; i < GC.MaxGeneration; i++)
            {
                int count = GC.CollectionCount(i) - gcCounts[i];
                Console.WriteLine("\tGet" + i + "\t\t" + count);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 获取调用方法时间戳，执行一个方法并返回执行时间戳
        /// </summary>
        /// <param name="action">执行方法</param>
        /// <returns>执行时间戳</returns>
        public static TimeSpan InvokeAndGetTimeSpan(Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            action();
            sw.Start();
            return sw.Elapsed;
        }

        /// <summary>
        /// 输出调用方法实际执行时间间隔（毫秒）
        /// </summary>
        /// <param name="action">执行方法</param>                
        public static void InvokeAndWriteTimeSpan(Action action)
        {
            Console.WriteLine("执行时间:{0:N4}ms", InvokeAndGetTimeSpan(action).TotalMilliseconds);
        }

        /// <summary>
        /// 获取调用方法托管内存使用大小（可能内存回收会导致不准确）
        /// </summary>
        /// <param name="action">执行方法</param>
        /// <returns>托管内存使用大小</returns>
        public static long InvokeAndGetmemoryUsed(Action action)
        {
            var start = GC.GetTotalMemory(false);
            action();
            return GC.GetTotalMemory(false) - start;
        }

        /// <summary>
        /// 输出调用方法实际执行托管内存使用大小（字节）
        /// </summary>
        /// <param name="action">执行方法</param>
        public static void InvokeAndWriteMemoryUsed(Action action)
        {
            Console.WriteLine("使用内存:{0:N4}KB", InvokeAndGetmemoryUsed(action) / 1024F);
        }

        /// <summary>
        /// 输出调用方法实际执行的时间以及托管内存使用大小
        /// </summary>
        /// <param name="action">执行方法</param>
        public static void InvokeAndWriteAll(Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var start = GC.GetTotalMemory(false);
            action();
            var end = GC.GetTotalMemory(false);
            sw.Stop();
            Console.WriteLine("执行时间:{0:N4}ms", sw.ElapsedMilliseconds);
            Console.WriteLine("使用内存:{0:N4}:KB", (end - start) / 1024F);
            Console.WriteLine("CollectionCount(0):{0:N}", GC.CollectionCount(0));
        }

        /// <summary>
        /// 输出代码执行时间
        /// </summary>
        /// <param name="action">执行方法</param>
        public static void CodeExecuteTime(Action action)
        {
            TimeSpan ts = Process.GetCurrentProcess().TotalProcessorTime;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var start = GC.GetTotalMemory(false);
            action();
            double msecs = Process.GetCurrentProcess().TotalProcessorTime.Subtract(ts).TotalMilliseconds;
            var end = GC.GetTotalMemory(false);
            stopwatch.Stop();

            Console.WriteLine("总运行时间：{0}", stopwatch.Elapsed);
            Console.WriteLine("CPU运行时间(毫秒):{0}", msecs);
            Console.WriteLine("测量实例得出的总运行时间(毫秒为单位):{0}毫秒", stopwatch.ElapsedMilliseconds);
            Console.WriteLine("总运行时间(计时器刻度标识):{0}", stopwatch.ElapsedTicks);
            Console.WriteLine("计时器是否运行:{0}", stopwatch.IsRunning ? "是" : "否");
            Console.WriteLine("托管内存:{0:N4}:KB", (end - start) / 1024F);
        }

    }

    /// <summary>
    /// 本地方法
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// 查询线程循环时间
        /// </summary>
        /// <param name="threadHandle">线程句柄</param>
        /// <param name="cycleTime">循环时间</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool QueryThreadCycleTime(IntPtr threadHandle, ref ulong cycleTime);

        /// <summary>
        /// 获取当前线程
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetCurrentThread();
    }
}
