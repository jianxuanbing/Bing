using System;
using System.Collections.Concurrent;
using Bing.Dependency;
using System.Threading;
using System.Threading.Tasks;

namespace Bing.Runtimes.Remotings
{
    /// <summary>
    /// 异步本地环境数据上下文
    /// </summary>
    public class AsyncLocalAmbientDataContext:IAmbientDataContext,ISingletonDependency
    {
        /// <summary>
        /// 异步本地字典
        /// </summary>
        private static readonly ConcurrentDictionary<string, object> AsyncLocalDictionary = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetData(string key, object value)
        {
            AsyncLocalDictionary.GetOrAdd(key, value);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public object GetData(string key)
        {
            var asyncLocal = AsyncLocalDictionary.GetOrAdd(key, (k) => new object());
            return asyncLocal;
        }
    }
}
