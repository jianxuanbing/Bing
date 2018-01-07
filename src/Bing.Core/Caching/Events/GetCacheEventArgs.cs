using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Events
{
    /// <summary>
    /// 缓存获取事件参数
    /// </summary>
    public class GetCacheEventArgs:CacheEventArgs
    {
        /// <summary>
        /// 缓存值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 缓存失效
        /// </summary>
        public bool CacheInvalidation => Value == null;

        /// <summary>
        /// 初始化一个<see cref="GetCacheEventArgs"/>类型的实例
        /// </summary>
        /// <param name="key"></param>
        public GetCacheEventArgs(string key) : base(key)
        {
        }
    }
}
