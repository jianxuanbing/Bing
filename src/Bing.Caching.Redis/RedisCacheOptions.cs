using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Options;

namespace Bing.Caching.Redis
{
    /// <summary>
    /// Redis 缓存配置
    /// </summary>
    public class RedisCacheOptions:RedisOptionsBase
    {
        /// <summary>
        /// 获取或设置 Redis 数据库索引
        /// </summary>
        public int Database { get; set; } = 0;
    }
}
