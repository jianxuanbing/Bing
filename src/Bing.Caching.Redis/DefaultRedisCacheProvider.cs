using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Abstractions;
using Bing.Caching.Core;
using Bing.Utils.Extensions;
using Bing.Utils.Helpers;
using Bing.Utils.Json;
using StackExchange.Redis;

namespace Bing.Caching.Redis
{
    /// <summary>
    /// 默认的Redis缓存提供程序
    /// </summary>
    public class DefaultRedisCacheProvider: IRedisCacheProvider
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
        /// Redis 客户端
        /// </summary>
        internal readonly IRedisClient Client;

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
            Client = new RedisClient(_dbProvider);
        }

        /// <summary>
        /// 将对象转换成Json字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        private string ToJson(object value)
        {
            string result = value is string ? value.ToString() : value.ToJson();
            return result;
        }

        /// <summary>
        /// 将RedisValue反序列化成对象
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        private T ToObj<T>(RedisValue value)
        {
            if (typeof(T).IsValueType || typeof(T) == typeof(string))
            {
                return Conv.To<T>(value);
            }
            return value.SafeString().ToObject<T>();
        }

        /// <summary>
        /// 将RedisValue反序列化成对象
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="type">数据类型</param>
        /// <returns></returns>
        private object ToObj(RedisValue value, Type type)
        {
            return JsonUtil.ToObject(value.SafeString(), type);
        }

        /// <summary>
        /// 将RedisValue反序列成对象列表
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="values">值</param>
        /// <returns></returns>
        private List<T> ToList<T>(RedisValue[] values)
        {
            List<T> result=new List<T>();
            foreach (var item in values)
            {
                var model = ToObj<T>(item);
                result.Add(model);
            }
            return result;
        }

        /// <summary>
        /// 将字符串列表转换成RedisKey[]
        /// </summary>
        /// <param name="redisKeys">字符串列表</param>
        /// <returns></returns>
        private RedisKey[] ConvertRedisKeys(List<string> redisKeys)
        {
            return redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray();
        }

        /// <summary>
        /// 添加自定义缓存键
        /// </summary>
        /// <param name="oldKey">旧缓存键</param>
        /// <returns></returns>
        private string AddSysCustomKey(string oldKey)
        {
            var prefixKey = this._dbProvider.Options.SystemPrefix ?? RedisManager.SysCustomKey;
            return prefixKey + oldKey;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="cacheValue">缓存值</param>
        /// <param name="expiration">过期时间</param>
        public void Set<T>(string cacheKey, T cacheValue, TimeSpan expiration) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            cacheValue.CheckNotNull(nameof(cacheValue));
            expiration.CheckGreaterThan(nameof(expiration),TimeSpan.Zero);

            var content = ToJson(cacheValue);
            cacheKey = AddSysCustomKey(cacheKey);

            _database.StringSet(cacheKey, content, expiration);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="cacheValue">缓存值</param>
        /// <param name="expiration">过期时间</param>
        public async Task SetAsync<T>(string cacheKey, T cacheValue, TimeSpan expiration) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            cacheValue.CheckNotNull(nameof(cacheValue));
            expiration.CheckGreaterThan(nameof(expiration), TimeSpan.Zero);

            var content = ToJson(cacheValue);
            cacheKey = AddSysCustomKey(cacheKey);

            await _database.StringSetAsync(cacheKey, content, expiration);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="dataRetriever">数据检索器</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        public CacheValue<T> Get<T>(string cacheKey, Func<T> dataRetriever, TimeSpan expiration) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            expiration.CheckGreaterThan(nameof(expiration), TimeSpan.Zero);

            var tempCacheKey= AddSysCustomKey(cacheKey);

            var result = _database.StringGet(tempCacheKey);
            if (!result.IsNull)
            {
                var value = ToObj<T>(result);
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

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="dataRetriever">数据检索器</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        public async Task<CacheValue<T>> GetAsync<T>(string cacheKey, Func<Task<T>> dataRetriever, TimeSpan expiration) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            expiration.CheckGreaterThan(nameof(expiration), TimeSpan.Zero);

            var tempCacheKey = AddSysCustomKey(cacheKey);

            var result = await _database.StringGetAsync(tempCacheKey);
            if (!result.IsNull)
            {
                var value =ToObj<T>(result);
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

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        public CacheValue<T> Get<T>(string cacheKey) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));

            cacheKey = AddSysCustomKey(cacheKey);

            var result = _database.StringGet(cacheKey);
            if (!result.IsNull)
            {
                var value = ToObj<T>(result);
                return new CacheValue<T>(value,true);
            }
            return CacheValue<T>.NoValue;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="type">实体类型</param>
        /// <returns></returns>
        public CacheValue<object> Get(string cacheKey, Type type)
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));

            cacheKey = AddSysCustomKey(cacheKey);

            var result = _database.StringGet(cacheKey);
            if (!result.IsNull)
            {
                var value = ToObj(result,type);
                return new CacheValue<object>(value, true);
            }
            return CacheValue<object>.NoValue;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        public async Task<CacheValue<T>> GetAsync<T>(string cacheKey) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));

            cacheKey = AddSysCustomKey(cacheKey);

            var result = await _database.StringGetAsync(cacheKey);
            if (!result.IsNull)
            {
                var value = ToObj<T>(result);
                return new CacheValue<T>(value, true);
            }
            return CacheValue<T>.NoValue;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        public void Remove(string cacheKey)
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));

            cacheKey = AddSysCustomKey(cacheKey);

            _database.KeyDelete(cacheKey);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        public async Task RemoveAsync(string cacheKey)
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));

            cacheKey = AddSysCustomKey(cacheKey);

            await _database.KeyDeleteAsync(cacheKey);
        }

        /// <summary>
        /// 是否存在指定缓存键
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        public bool Exists(string cacheKey)
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));

            cacheKey = AddSysCustomKey(cacheKey);

            return _database.KeyExists(cacheKey);
        }

        /// <summary>
        /// 是否存在指定缓存键
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string cacheKey)
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));

            cacheKey = AddSysCustomKey(cacheKey);

            return await _database.KeyExistsAsync(cacheKey);
        }

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="cacheValue">缓存值</param>
        /// <param name="expiration">过期时间</param>
        public void Refresh<T>(string cacheKey, T cacheValue, TimeSpan expiration) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            cacheValue.CheckNotNull(nameof(cacheValue));
            expiration.CheckGreaterThan(nameof(expiration),TimeSpan.Zero);

            cacheKey = AddSysCustomKey(cacheKey);

            this.Remove(cacheKey);
            this.Set(cacheKey,cacheValue,expiration);
        }

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="cacheValue">缓存值</param>
        /// <param name="expiration">过期时间</param>
        public async Task RefreshAsync<T>(string cacheKey, T cacheValue, TimeSpan expiration) where T : class
        {
            cacheKey.CheckNotNullOrEmpty(nameof(cacheKey));
            cacheValue.CheckNotNull(nameof(cacheValue));
            expiration.CheckGreaterThan(nameof(expiration), TimeSpan.Zero);

            cacheKey = AddSysCustomKey(cacheKey);

            await this.RemoveAsync(cacheKey);
            await this.SetAsync(cacheKey, cacheValue, expiration);
        }

        /// <summary>
        /// 获取Redis客户端
        /// </summary>
        /// <returns></returns>
        public IRedisClient GetClient()
        {
            return this.Client;
        }
    }
}
