using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching
{
    /// <summary>
    /// 分布式缓存访问（例如：Redis、Memcached、Couchbase）
    /// </summary>
    public interface IDistributedCache
    {
        /// <summary>
        /// 用指定的缓存键递增值并返回新的值。如果值不存在，它的新值将是1
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="amount">递增数。如：amount+=1</param>
        /// <returns></returns>
        long Increment(string key, int amount = 1);

        /// <summary>
        /// 从缓存值获取数据。如果值不存在，返回 default(T)
        /// </summary>
        /// <typeparam name="TValue">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        TValue Get<TValue>(string key);

        /// <summary>
        /// 添加或替换缓存项
        /// </summary>
        /// <typeparam name="TValue">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        void Set<TValue>(string key, TValue value);

        /// <summary>
        /// 添加或替换缓存项并设置相对过期时间
        /// </summary>
        /// <typeparam name="TValue">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="expiration">滑动过期时间，在此时间内访问缓存，缓存将继续有效</param>
        void Set<TValue>(string key, TValue value, TimeSpan expiration);
    }
}
