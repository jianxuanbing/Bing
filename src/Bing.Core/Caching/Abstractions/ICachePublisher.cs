using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Abstractions
{
    /// <summary>
    /// 缓存发布者
    /// </summary>
    public interface ICachePublisher
    {
        /// <summary>
        /// 发布
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="channel">通道</param>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="cacheValue">缓存值</param>
        /// <param name="expiration">过期时间</param>
        void Publish<T>(string channel, string cacheKey, T cacheValue, TimeSpan expiration);

        /// <summary>
        /// 发布
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="channel">通道</param>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="cacheValue">缓存值</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        Task PublishAsync<T>(string channel, string cacheKey, T cacheValue, TimeSpan expiration);
    }
}
