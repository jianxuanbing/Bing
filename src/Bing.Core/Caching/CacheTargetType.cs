using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching
{
    /// <summary>
    /// 缓存目标类型
    /// </summary>
    public enum CacheTargetType
    {
        /// <summary>
        /// Redis
        /// </summary>
        Redis=0,

        /// <summary>
        /// CouchBase
        /// </summary>
        CouchBase=1,

        /// <summary>
        /// Memcached
        /// </summary>
        Memcached=2,

        /// <summary>
        /// WebCache
        /// </summary>
        WebCache=3
    }
}
