using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Abstractions
{
    /// <summary>
    /// 缓存键生成器
    /// </summary>
    public interface ICachingKeyGenerator
    {
        /// <summary>
        /// 获取缓存键
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <param name="args">参数</param>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        string GetCacheKey(MethodInfo methodInfo, object[] args, string prefix);

        /// <summary>
        /// 获取缓存键前缀
        /// </summary>
        /// <param name="methodInfo">方法信息</param>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        string GetCacheKeyPrefix(MethodInfo methodInfo, string prefix);
    }
}
