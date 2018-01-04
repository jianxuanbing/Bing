using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching
{
    /// <summary>
    /// 二级缓存。容器工具，用于本地和分布式失效缓存同步
    /// </summary>
    public static class TwoLevelCache
    {
        /// <summary>
        /// 生成缓存失效时间
        /// </summary>
        public static readonly TimeSpan GenerationCacheExpiration = TimeSpan.FromSeconds(5);

        /// <summary>
        /// 生成后缀
        /// </summary>
        public const string GenerationSuffix = "$Generation$";

        /// <summary>
        /// 随机数生成器
        /// </summary>
        private static readonly Random GenerationRandomizer;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static TwoLevelCache()
        {
            GenerationRandomizer=new Random(GetSeed());
        }

        /// <summary>
        /// 生成随机种子数
        /// </summary>
        /// <returns></returns>
        private static int GetSeed()
        {
            byte[] raw = Guid.NewGuid().ToByteArray();
            int i1 = BitConverter.ToInt32(raw, 0);
            int i2 = BitConverter.ToInt32(raw, 4);
            int i3 = BitConverter.ToInt32(raw, 8);
            int i4 = BitConverter.ToInt32(raw, 12);
            long val = i1 + i2 + i3 + i4;
            while (val > int.MaxValue)
            {
                val -= int.MaxValue;
            }

            return (int) val;
        }

        /// <summary>
        /// 从缓存中获取数据。尝试从本地缓存读取值。
        /// 如果没有找到，则尝试分布式缓存。
        /// 如果两者都不包含指定的缓存键，则通过调用加载函数产生缓存值，并将该值添加到本地和分布式缓存中，并设置指定的过期时间。
        /// 通过使用分组密钥，属于该组成员的两种缓存类型的所有项都可以立即过期。
        /// </summary>
        /// <typeparam name="TItem">数据类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="localExpiration">本地缓存过期时间</param>
        /// <param name="remoteExpireation">远程（分布式）缓存过期时间（通常与本地过期时间相同）</param>
        /// <param name="groupKey">分组密钥。可以用来过期所有缓存项。这可以是表名。当表更改时，你更改其版本，并且依赖于该表的所有缓存数据已过期</param>
        /// <param name="loader">数据加载器。如果在本地缓存、分布式缓存找不到将被调用来生成值得委托，则会过期</param>
        /// <returns></returns>
        public static TItem Get<TItem>(string cacheKey, TimeSpan localExpiration, TimeSpan remoteExpireation,
            string groupKey, Func<TItem> loader) where TItem : class
        {
            return GetInternal<TItem, TItem>(cacheKey, localExpiration, remoteExpireation, groupKey, loader, x => x, x => x);
        }


        public static TItem Get<TItem>(string cacheKey, TimeSpan expiration, string groupKey, Func<TItem> loader)
            where TItem : class
        {
            return GetInternal<TItem, TItem>(cacheKey, expiration, expiration, groupKey, loader, x => x, x => x);
        }

        public static TItem GetWithCustomSerializer<TItem, TSerialized>(string cacheKey, TimeSpan localExpiration,
            TimeSpan remoteExpiration, string groupKey, Func<TItem> loader, Func<TItem, TSerialized> serialize,
            Func<TSerialized, TItem> deserialize) where TItem : class where TSerialized : class
        {
            if (serialize == null)
            {
                throw new ArgumentNullException(nameof(serialize));
            }

            if (deserialize == null)
            {
                throw new ArgumentNullException(nameof(deserialize));
            }

            return GetInternal<TItem, TSerialized>(cacheKey, localExpiration, remoteExpiration, groupKey, loader,
                serialize, deserialize);
        }

        public static TItem GetLocalStoreOnly<TItem>(string cacheKey, TimeSpan localExpiration, string groupKey,
            Func<TItem> loader) where TItem : class
        {
            return GetInternal<TItem, TItem>(cacheKey, localExpiration, TimeSpan.FromSeconds(0), groupKey, loader, null,
                null);
        }

        private static TItem GetInternal<TItem, TSerialized>(string cacheKey, TimeSpan localExpiration,
            TimeSpan remoteExpiration, string groupKey, Func<TItem> loader, Func<TItem, TSerialized> serialize,
            Func<TSerialized, TItem> deserialize) where TItem : class where TSerialized : class
        {
            ulong? groupGeneration = null;
            ulong? groupGenerationCache = null;

            string itemGenerationKey = cacheKey + GenerationSuffix;

            var localCache = LocalCache.Provider;
            var distributedCache = DistributedCache.Provider;

            // 延迟生成分布式缓存分组密钥
            Func<ulong> getGroupGenerationValue = delegate()
            {
                if (groupGeneration != null)
                {
                    return groupGeneration.Value;
                }

                groupGeneration = distributedCache.Get<ulong?>(groupKey);
                if (groupGeneration == null || groupGeneration == 0)
                {
                    groupGeneration = RandomGeneration();
                    distributedCache.Set(groupKey, groupGeneration.Value);
                }

                groupGenerationCache = groupGeneration.Value;
                // 添加到本地缓存，有效期为5秒
                LocalCache.Add(groupKey, groupGenerationCache, GenerationCacheExpiration);
                return groupGeneration.Value;
            };

            // 延迟生成本地缓存分组密匙
            Func<ulong> getGroupGenerationCacheValue = delegate ()
            {
                if (groupGenerationCache != null)
                {
                    return groupGenerationCache.Value;
                }
                // 检查已缓存的分组密钥
                // 如果它在5秒内过期并再次从服务器读取
                groupGenerationCache = localCache.Get<object>(groupKey) as ulong?;

                // 如果已存在于本地缓存，则直接返回
                if (groupGenerationCache != null)
                {
                    return groupGenerationCache.Value;
                }

                return getGroupGenerationValue();
            };

            // 首先检查本地缓存，如果缓存项已存在并且没有过期（分组版本=缓存项版本）直接返回
            var cacheObj = localCache.Get<object>(cacheKey);
            if (cacheObj != null)
            {
                // 检查本地缓存，如果存在，比较版本
                var itemGenerationCache = localCache.Get<object>(itemGenerationKey) as ulong?;
                if (itemGenerationCache != null && itemGenerationCache == getGroupGenerationCacheValue())
                {
                    // 本地缓存项没有过期
                    if (cacheObj == DBNull.Value)
                    {
                        return null;
                    }

                    return (TItem) cacheObj;
                }
                // 本地缓存项已过期，移除所有信息
                if (itemGenerationCache != null)
                {
                    localCache.Remove(itemGenerationKey);                    
                }
                localCache.Remove(cacheKey);
                cacheObj = null;
            }

            // 如果序列化为空，比较本地仓储项
            if (serialize != null)
            {
                // 没有本地缓存项或已过期，现在检查分布式缓存
                var itemGeneration = distributedCache.Get<ulong?>(itemGenerationKey);

                // 如果缓存项有版本号在分布式缓存中，需要进行比较版本号
                if (itemGeneration != null && itemGeneration.Value == getGroupGenerationValue())
                {
                    // 从分布式缓存中获取缓存项
                    var serialized = distributedCache.Get<TSerialized>(cacheKey);
                    // 如果缓存项在分布式缓存中
                    if (serialized != null)
                    {
                        cacheObj = deserialize(serialized);
                        LocalCache.Add(cacheKey,(object)cacheObj??DBNull.Value,localExpiration);
                        LocalCache.Add(itemGenerationKey,getGroupGenerationValue(),localExpiration);
                        return (TItem) cacheObj;
                    }
                }
            }

            // 从本地或分布式缓存中找不到相应的缓存值，通过调用加载程序产生缓存值

            var item = loader();

            // 添加缓存项以及版本号到缓存中
            LocalCache.Add(cacheKey,(object)item??DBNull.Value,localExpiration);
            LocalCache.Add(itemGenerationKey,getGroupGenerationValue(),localExpiration);

            if (serialize != null)
            {
                var serializedItem = serialize(item);

                //添加缓存项和生成缓存键到分布式缓存中
                if (remoteExpiration == TimeSpan.Zero)
                {
                    distributedCache.Set(cacheKey,serializedItem);
                    distributedCache.Set(itemGenerationKey,getGroupGenerationCacheValue());
                }
                else
                {
                    distributedCache.Set(cacheKey,serializedItem,remoteExpiration);
                    distributedCache.Set(itemGenerationKey,getGroupGenerationCacheValue(),remoteExpiration);
                }
            }

            return item;
        }



        /// <summary>
        /// 生成64位的随机数（版本密钥）
        /// </summary>
        /// <returns></returns>
        private static ulong RandomGeneration()
        {
            var buffer=new byte[sizeof(ulong)];
            GenerationRandomizer.NextBytes(buffer);
            var value = BitConverter.ToUInt64(buffer, 0);

            if (value == 0)
            {
                return ulong.MaxValue;
            }

            return value;
        }

        /// <summary>
        /// 过期分组缓存
        /// </summary>
        /// <param name="groupKey">分组密钥</param>
        public static void ExpireGroupItems(string groupKey)
        {
            LocalCache.Provider.Remove(groupKey);
            DistributedCache.Set<object>(groupKey, null);
        }

        /// <summary>
        /// 从本地分布式缓存中删除缓存键，并删除其生成版本信息
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        public static void Remove(string cacheKey)
        {
            string itemGenerationKey = cacheKey + GenerationSuffix;

            var localCache = LocalCache.Provider;
            var distributedCache = DistributedCache.Provider;

            localCache.Remove(cacheKey);
            localCache.Remove(itemGenerationKey);
            distributedCache.Set<object>(cacheKey, null);
            distributedCache.Set<object>(itemGenerationKey, null);

        }
    }
}
