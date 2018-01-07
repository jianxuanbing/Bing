using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bing.Pools
{

    /// <summary>
    /// 对象池
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class ObjectPool<T>
    {
        #region 字段

        /// <summary>
        /// 已获取对象数
        /// </summary>
        private int _isTaked = 0;

        /// <summary>
        /// 队列
        /// </summary>
        private Queue<T> _queue=new Queue<T>();

        /// <summary>
        /// 初始化对象方法
        /// </summary>
        private Func<T> _func = null;

        /// <summary>
        /// 当前对象数
        /// </summary>
        private int _currentResource = 0;

        /// <summary>
        /// 对象数
        /// </summary>
        private int _tryNewObject = 0;

        /// <summary>
        /// 对象池下限
        /// </summary>
        private readonly int _minSize = 1;

        /// <summary>
        /// 对象池上限
        /// </summary>
        private readonly int _maxSize = 50;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化一个<see cref="ObjectPool{T}"/>类型的实例
        /// </summary>
        /// <param name="func">用于初始化对象的函数</param>
        /// <param name="minSize">对象池下限</param>
        /// <param name="maxSize">对象池上限</param>
        public ObjectPool(Func<T> func, int minSize = 100, int maxSize = 100)
        {
            if (func == null)
            {
                throw new ArgumentException(nameof(func));
            }

            if (minSize < 0)
            {
                throw new ArgumentException(nameof(minSize));
            }

            if (maxSize < 0)
            {
                throw new ArgumentException(nameof(maxSize));
            }

            if (minSize > 0)
            {
                this._minSize = minSize;
            }

            if (maxSize > 0)
            {
                this._maxSize = maxSize;
            }

            for (var i = 0; i < this._minSize; i++)
            {
                this._queue.Enqueue(func());
            }

            this._currentResource = this._minSize;
            this._tryNewObject = this._minSize;
            this._func = func;
        }

        #endregion

        #region GetObject(从对象池中获取一个对象出来，执行完成后自动将对象放回池中)

        /// <summary>
        /// 从对象池中获取一个对象出来，执行完成后自动将对象放回池中
        /// </summary>
        /// <returns></returns>
        public T GetObject()
        {
            var t = default(T);
            try
            {
                if (this._currentResource < this._maxSize)
                {                    
                    //Interlocked.Increment(ref this._tryNewObject);                    
                    t = this._func();
                    Interlocked.Increment(ref this._currentResource);
                }
                else
                {
                    this.Enter();
                    t = this._queue.Dequeue();
                    this.Leave();
                    Interlocked.Decrement(ref this._currentResource);
                }

                return t;
            }
            finally
            {
                this.Enter();
                this._queue.Enqueue(t);
                this.Leave();
                Interlocked.Increment(ref this._currentResource);
            }
        }

        /// <summary>
        /// 入栈
        /// </summary>
        private void Enter()
        {
            while (Interlocked.Exchange(ref this._isTaked, 1) != 0)
            {
            }
        }

        /// <summary>
        /// 出栈
        /// </summary>
        private void Leave()
        {
            Thread.VolatileWrite(ref this._isTaked, 0);
        }

        #endregion
    }
}
