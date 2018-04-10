using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Core
{
    /// <summary>
    /// 混合缓存键类型
    /// </summary>
    public class HybridCacheKeyType
    {
        /// <summary>
        /// 本地键
        /// </summary>
        public const string LOCAL_KEY = "Local";

        /// <summary>
        /// 分布式键
        /// </summary>
        public const string DISTRIBUTED_KEY = "Distributed";
    }
}
