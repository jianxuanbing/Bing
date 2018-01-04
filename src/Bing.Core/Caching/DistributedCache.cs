using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Helpers;

namespace Bing.Caching
{
    /// <summary>
    /// 分布式缓存
    /// </summary>
    public static class DistributedCache
    {
        public static IDistributedCache StaticProvider;

        public static IDistributedCache Provider => StaticProvider ?? Ioc.Create<IDistributedCache>();
        public static long Increment(string key, int amount = 1)
        {
            throw new NotImplementedException();
        }

        public static TValue Get<TValue>(string key)
        {
            throw new NotImplementedException();
        }

        public static void Set<TValue>(string key, TValue value)
        {
            throw new NotImplementedException();
        }

        public static void Set<TValue>(string key, TValue value, TimeSpan expiration)
        {
            throw new NotImplementedException();
        }
    }
}
