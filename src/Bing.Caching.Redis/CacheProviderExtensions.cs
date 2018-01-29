using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Redis
{
    /// <summary>
    /// 缓存提供程序 扩展
    /// </summary>
    public static class CacheProviderExtensions
    {
        /// <summary>
        /// 获取Redis客户端
        /// </summary>
        /// <param name="cacheProvider">缓存提供程序</param>
        /// <returns></returns>
        public static IRedisClient GetRedisClient(this ICacheProvider cacheProvider)
        {
            var redisCache = cacheProvider as DefaultRedisCacheProvider;
            return redisCache?.Client;
        }
    }
}
