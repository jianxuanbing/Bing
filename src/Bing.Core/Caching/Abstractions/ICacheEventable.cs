using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Events;

namespace Bing.Caching.Abstractions
{
    /// <summary>
    /// 缓存事件
    /// </summary>
    public interface ICacheEventable
    {
        /// <summary>
        /// 设置缓存前操作
        /// </summary>
        event EventHandler<SetCacheEventArgs> SetBefore;

        /// <summary>
        /// 设置缓存后操作
        /// </summary>
        event EventHandler<SetCacheEventArgs> SetAfter;

        /// <summary>
        /// 获取缓存前操作
        /// </summary>
        event EventHandler<CacheEventArgs> GetBefore;

        /// <summary>
        /// 获取缓存后操作
        /// </summary>
        event EventHandler<GetCacheEventArgs> GetAfter;

        /// <summary>
        /// 移除缓存前操作
        /// </summary>
        event EventHandler<CacheEventArgs> RemoveBefore;

        /// <summary>
        /// 移除缓存后操作
        /// </summary>
        event EventHandler<CacheEventArgs> RemoveAfter;
    }
}
