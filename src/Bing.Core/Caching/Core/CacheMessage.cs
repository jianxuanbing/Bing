using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Core
{
    /// <summary>
    /// 缓存消息
    /// </summary>
    public class CacheMessage
    {
        /// <summary>
        /// 缓存键
        /// </summary>
        public string CacheKey { get; set; }

        /// <summary>
        /// 缓存值
        /// </summary>
        public object CacheValue { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Expiration { get; set; }
    }
}
