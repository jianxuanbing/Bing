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
    public interface ICache
    {
        /// <summary>
        /// 初始化属性列表
        /// </summary>
        List<string> InitializationProperties { get; }

        /// <summary>
        /// 提供程序名称
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// 默认过期时间
        /// </summary>
        long DefaultExpireTime { get; set; }

        /// <summary>
        /// 缓存键后缀
        /// </summary>
        string KeySuffix { get; set; }
    }
}
