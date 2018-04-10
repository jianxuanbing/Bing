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
        /// 内存缓存（本地）
        /// </summary>
        Memory=0,

        /// <summary>
        /// SQLite（本地）
        /// </summary>
        SQLite=1,

        /// <summary>
        /// Redis（远程）
        /// </summary>
        Redis=2,

        /// <summary>
        /// Memcached（远程）
        /// </summary>
        Memcached = 3,

        /// <summary>
        /// WebCache（远程）
        /// </summary>
        WebCache = 4,

        /// <summary>
        /// CouchBase（远程）
        /// </summary>
        CouchBase = 5,
        
    }
}
