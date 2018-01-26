using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Abstractions;
using Bing.Caching.Core;
using Bing.Utils.Extensions;
using StackExchange.Redis;

namespace Bing.Caching.Redis
{
    /// <summary>
    /// 默认的Redis缓存提供程序
    /// </summary>
    public class DefaultRedisCacheProvider:Bing.Caching.Abstractions.ICacheProvider
    {
        /// <summary>
        /// 缓存数据库
        /// </summary>
        private readonly IDatabase _database;

        /// <summary>
        /// Redis 数据库提供程序
        /// </summary>
        private readonly IRedisDatabaseProvider _dbProvider;

        /// <summary>
        /// 序列化器
        /// </summary>
        private readonly ICacheSerializer _serializer;

        /// <summary>
        /// <see cref="DefaultRedisCacheProvider"/>不是分布式缓存
        /// </summary>
        public bool IsDistributedCache => false;

        /// <summary>
        /// 初始化一个<see cref="DefaultRedisCacheProvider"/>类型的实例
        /// </summary>
        /// <param name="dbProvider">Redis 数据库提供程序</param>
        /// <param name="serializer">缓存序列化器</param>
        public DefaultRedisCacheProvider(IRedisDatabaseProvider dbProvider, ICacheSerializer serializer)
        {
            dbProvider.CheckNotNull(nameof(dbProvider));
            serializer.CheckNotNull(nameof(serializer));

            _dbProvider = dbProvider;
            _serializer = serializer;
            _database = _dbProvider.GetDatabase();
        }

        public void Set<T>(string cacheKey, T cacheValue, TimeSpan expiration) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            cacheValue.CheckNotNull(nameof(cacheValue));
            expiration.CheckGreaterThan(nameof(expiration),TimeSpan.Zero);

            _database.StringSet(cacheKey, _serializer.Serialize(cacheValue), expiration);
        }

        public async Task SetAsync<T>(string cacheKey, T cacheValue, TimeSpan expiration) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            cacheValue.CheckNotNull(nameof(cacheValue));
            expiration.CheckGreaterThan(nameof(expiration), TimeSpan.Zero);

            await _database.StringSetAsync(cacheKey, _serializer.Serialize(cacheValue), expiration);
        }

        public CacheValue<T> Get<T>(string cacheKey, Func<T> dataRetriever, TimeSpan expiration) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            expiration.CheckGreaterThan(nameof(expiration), TimeSpan.Zero);

            var result = _database.StringGet(cacheKey);
            if (!result.IsNull)
            {
                var value = _serializer.Deserialize<T>(result);
                return new CacheValue<T>(value, true);
            }

            var item = dataRetriever?.Invoke();
            if (item != null)
            {
                Set(cacheKey,item,expiration);
                return new CacheValue<T>(item,true);
            }
            return CacheValue<T>.NoValue;
        }

        public async Task<CacheValue<T>> GetAsync<T>(string cacheKey, Func<Task<T>> dataRetriever, TimeSpan expiration) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            expiration.CheckGreaterThan(nameof(expiration), TimeSpan.Zero);

            var result = await _database.StringGetAsync(cacheKey);
            if (!result.IsNull)
            {
                var value = _serializer.Deserialize<T>(result);
                return new CacheValue<T>(value, true);
            }

            var item = await dataRetriever?.Invoke();
            if (item != null)
            {
                await SetAsync(cacheKey, item, expiration);
                return new CacheValue<T>(item, true);
            }
            return CacheValue<T>.NoValue;
        }

        public CacheValue<T> Get<T>(string cacheKey) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));

            var result = _database.StringGet(cacheKey);
            if (!result.IsNull)
            {
                var value = _serializer.Deserialize<T>(result);
                return new CacheValue<T>(value,true);
            }
            return CacheValue<T>.NoValue;
        }

        public async Task<CacheValue<T>> GetAsync<T>(string cacheKey) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));

            var result = await _database.StringGetAsync(cacheKey);
            if (!result.IsNull)
            {
                var value = _serializer.Deserialize<T>(result);
                return new CacheValue<T>(value, true);
            }
            return CacheValue<T>.NoValue;
        }

        public void Remove(string cacheKey)
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            _database.KeyDelete(cacheKey);
        }

        public async Task RemoveAsync(string cacheKey)
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            await _database.KeyDeleteAsync(cacheKey);
        }

        public bool Exists(string cacheKey)
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            return _database.KeyExists(cacheKey);
        }

        public async Task<bool> ExistsAsync(string cacheKey)
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            return await _database.KeyExistsAsync(cacheKey);
        }

        
        public void Refresh<T>(string cacheKey, T cacheValue, TimeSpan expiration) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            cacheValue.CheckNotNull(nameof(cacheValue));
            expiration.CheckGreaterThan(nameof(expiration),TimeSpan.Zero);

            this.Remove(cacheKey);
            this.Set(cacheKey,cacheValue,expiration);
        }

        public async Task RefreshAsync<T>(string cacheKey, T cacheValue, TimeSpan expiration) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            cacheValue.CheckNotNull(nameof(cacheValue));
            expiration.CheckGreaterThan(nameof(expiration), TimeSpan.Zero);

            await this.RemoveAsync(cacheKey);
            await this.SetAsync(cacheKey, cacheValue, expiration);
        }
    }
}
