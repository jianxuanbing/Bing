using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Events
{
    /// <summary>
    /// 缓存事件参数
    /// </summary>
    public class CacheEventArgs:EventArgs
    {
        /// <summary>
        /// 缓存提供程序名称
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// 缓存键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 初始化一个<see cref="CacheEventArgs"/>类型的实例
        /// </summary>
        /// <param name="key">缓存键</param>
        public CacheEventArgs(string key)
        {
            Key = key;
        }
    }
}
