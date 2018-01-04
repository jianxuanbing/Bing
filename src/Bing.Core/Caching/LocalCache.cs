using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Helpers;

namespace Bing.Caching
{
    /// <summary>
    /// 本地缓存
    /// </summary>
    public class LocalCache
    {
        /// <summary>
        /// 本地静态缓存提供程序，仅在要求性能的时候才使用 Ioc.Create
        /// </summary>
        public static ILocalCache StaticProvider;

        /// <summary>
        /// 本地缓存提供程序
        /// </summary>
        public static ILocalCache Provider => StaticProvider ?? Ioc.Create<ILocalCache>();

        public static void Add(string key, object value, TimeSpan expiration)
        {
            Provider.Add(key, value, expiration);
        }

        public TItem Get<TItem>(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }
    }
}
