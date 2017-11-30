using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bing.Utils
{
    /// <summary>
    /// 释放操作，用于提供释放
    /// </summary>
    public class DisposeAction:IDisposable
    {
        /// <summary>
        /// 空操作
        /// </summary>
        public static readonly DisposeAction Emtpy=new DisposeAction(null);

        /// <summary>
        /// 操作
        /// </summary>
        private Action _action;

        /// <summary>
        /// 初始化一个<see cref="DisposeAction"/>类型的实例
        /// </summary>
        /// <param name="action">操作</param>
        public DisposeAction(Action action)
        {
            _action = action;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            // 加锁，防止多次执行 _action
            var action = Interlocked.Exchange(ref _action, null);
            action?.Invoke();
        }
    }
}
