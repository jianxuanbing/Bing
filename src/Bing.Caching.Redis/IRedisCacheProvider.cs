using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Redis
{
    /// <summary>
    /// Redis 缓存提供程序
    /// </summary>
    public interface IRedisCacheProvider:ICacheProvider
    {
        /// <summary>
        /// 获取Redis客户端
        /// </summary>
        /// <returns></returns>
        IRedisClient GetClient();
    }
}
