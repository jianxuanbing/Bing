using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bing.Utils.Transplants.Atomics
{
    /// <summary>
    /// 原子整数，迁移至Java
    /// 参考：http://www.cnblogs.com/jobs/archive/2007/11/15/959798.html
    /// </summary>
    public class AtomicInteger
    {
        /// <summary>
        /// 值
        /// </summary>
        private int _value;

        /// <summary>
        /// 初始化一个<see cref="AtomicInteger"/>类型的实例
        /// </summary>
        public AtomicInteger() : this(0) { }

        /// <summary>
        /// 初始化一个<see cref="AtomicInteger"/>类型的实例
        /// </summary>
        /// <param name="initialValue"></param>
        public AtomicInteger(int initialValue)
        {
            _value = initialValue;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <returns></returns>
        public int Get()
        {
            return _value;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="newValue">新值</param>
        public void Set(int newValue)
        {
            _value = newValue;
        }

        /// <summary>
        /// 获取并设置值，获取原有值，设置新值
        /// </summary>
        /// <param name="newValue">新值</param>
        /// <returns>返回原有值</returns>
        public int GetAndSet(int newValue)
        {
            for (;;)
            {
                int current = Get();
                if (CompareAndSet(current, newValue))
                {
                    return current;
                }
            }
        }

        /// <summary>
        /// 比较并设置
        /// </summary>
        /// <param name="expect">目标值</param>
        /// <param name="update">更新值</param>
        /// <returns></returns>
        public bool CompareAndSet(int expect, int update)
        {
            return Interlocked.CompareExchange(ref _value, update, expect) == expect;
        }

        /// <summary>
        /// 获取并增加。先获取，后增加
        /// </summary>
        /// <returns>返回原有值</returns>
        public int GetAndIncrement()
        {
            for (;;)
            {
                int current = Get();
                int next = current + 1;
                if (CompareAndSet(current, next))
                {
                    return current;
                }
            }
        }

        /// <summary>
        /// 获取并减少。先获取，后减少
        /// </summary>
        /// <returns>返回原有值</returns>
        public int GetAndDecrement()
        {
            for (;;)
            {
                int current = Get();
                int next = current - 1;
                if (CompareAndSet(current, next))
                {
                    return current;
                }
            }
        }

        /// <summary>
        /// 获取并添加指定值。先获取，后增加
        /// </summary>
        /// <param name="delta">递增值</param>
        /// <returns></returns>
        public int GetAndAdd(int delta)
        {
            for (;;)
            {
                int current = Get();
                int next = current + delta;
                if (CompareAndSet(current, next))
                {
                    return current;
                }
            }
        }

        /// <summary>
        /// 获取并增加。先增加，后获取
        /// </summary>
        /// <returns></returns>
        public int IncrementAndGet()
        {
            for (;;)
            {
                int current = Get();
                int next = current + 1;
                if (CompareAndSet(current, next))
                {
                    return next;
                }
            }
        }

        /// <summary>
        /// 获取并减少。先减少，后获取
        /// </summary>
        /// <returns></returns>
        public int DecrementAndGet()
        {
            for (;;)
            {
                int current = Get();
                int next = current - 1;
                if (CompareAndSet(current, next))
                {
                    return next;
                }
            }
        }

        /// <summary>
        /// 获取并添加指定值。先增加，后获取
        /// </summary>
        /// <param name="delta">递增值</param>
        /// <returns></returns>
        public int AddAndGet(int delta)
        {
            for (;;)
            {
                int current = Get();
                int next = current + delta;
                if (CompareAndSet(current, next))
                {
                    return next;
                }
            }
        }

        /// <summary>
        /// 重写转换字符串方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Convert.ToString(Get());
        }
    }
}
