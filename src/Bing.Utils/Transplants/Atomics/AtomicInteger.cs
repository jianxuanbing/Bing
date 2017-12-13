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

        public int Get()
        {
            return _value;
        }

        public bool CompareAndSet(int expect, int update)
        {
            return Interlocked.CompareExchange(ref _value, update, expect) == expect;
        }

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
    }
}
