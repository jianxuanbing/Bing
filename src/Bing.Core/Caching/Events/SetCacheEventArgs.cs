using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Events
{
    /// <summary>
    /// 缓存设置事件参数
    /// </summary>
    public class SetCacheEventArgs:CacheEventArgs
    {
        /// <summary>
        /// 缓存值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 有效时间
        /// </summary>
        public TimeSpan LifeSpan { get; set; }

        /// <summary>
        /// 初始化一个<see cref="SetCacheEventArgs"/>类型的实例
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="lifeSpan">有效时间</param>
        public SetCacheEventArgs(string key,object value,TimeSpan lifeSpan) : base(key)
        {
            Value = value;
            LifeSpan = lifeSpan;
        }
    }
}
