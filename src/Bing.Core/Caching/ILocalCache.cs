using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching
{
    /// <summary>
    /// 本地缓存
    /// </summary>
    public interface ILocalCache
    {
        /// <summary>
        /// 添加缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="expiration">到期时间，使用<see cref="TimeSpan.Zero"/>则无过期时间</param>
        void Add(string key, object value, TimeSpan expiration);

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <typeparam name="TItem">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        TItem Get<TItem>(string key);

        /// <summary>
        /// 移除指定缓存键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        void Remove(string key);

        /// <summary>
        /// 移除所有缓存
        /// </summary>
        void RemoveAll();
    }
}
