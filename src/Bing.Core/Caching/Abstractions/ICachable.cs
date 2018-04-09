using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Abstractions
{
    /// <summary>
    /// 缓存
    /// </summary>
    public interface ICachable
    {
        /// <summary>
        /// 获取 缓存键
        /// </summary>
        string CacheKey { get; }
    }
}
