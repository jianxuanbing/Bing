using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.Injector;
using Bing.Aspects.Base;
using Bing.Caching.Abstractions;

namespace Bing.Caching.Aspects
{
    /// <summary>
    /// 缓存 属性基类
    /// </summary>
    public abstract class CachingAttributeBase:InterceptorBase
    {
        /// <summary>
        /// 获取或设置 是否混合缓存提供程序
        /// </summary>
        public bool IsHybridProvider { get; set; } = false;

        /// <summary>
        /// 获取或设置 缓存键前缀
        /// </summary>
        public string CacheKeyPrefix { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置 缓存提供程序
        /// </summary>
        [FromContainer]
        public ICacheProvider CacheProvider { get; set; }

        /// <summary>
        /// 获取或设置 缓存键生成器
        /// </summary>
        [FromContainer]
        public ICachingKeyGenerator KeyGenerator { get; set; }        
    }
}
