using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Core;

namespace Bing.Caching
{
    /// <summary>
    /// 缓存提供程序
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="cacheValue">缓存值</param>
        /// <param name="expiration">过期时间</param>
        void Set<T>(string cacheKey, T cacheValue, TimeSpan expiration) where T : class;

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="cacheValue">缓存值</param>
        /// <param name="expiration">过期时间</param>
        Task SetAsync<T>(string cacheKey, T cacheValue, TimeSpan expiration) where T : class;

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="dataRetriever">数据检索器</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        CacheValue<T> Get<T>(string cacheKey, Func<T> dataRetriever, TimeSpan expiration) where T : class;

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="dataRetriever">数据检索器</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        Task<CacheValue<T>> GetAsync<T>(string cacheKey, Func<Task<T>> dataRetriever, TimeSpan expiration) where T : class;

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        CacheValue<T> Get<T>(string cacheKey) where T : class;

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="type">实体类型</param>
        /// <returns></returns>
        CacheValue<object> Get(string cacheKey,Type type);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        Task<CacheValue<T>> GetAsync<T>(string cacheKey) where T : class;

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        void Remove(string cacheKey);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        Task RemoveAsync(string cacheKey);

        /// <summary>
        /// 是否存在指定缓存键
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        bool Exists(string cacheKey);

        /// <summary>
        /// 是否存在指定缓存键
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string cacheKey);

        /// <summary>
        /// 获取 一个值，指示当前<see cref="ICacheProvider"/>是否分布式缓存。
        /// </summary>
        bool IsDistributedCache { get; }

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="cacheValue">缓存值</param>
        /// <param name="expiration">过期时间</param>
        void Refresh<T>(string cacheKey, T cacheValue, TimeSpan expiration) where T : class;

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="cacheValue">缓存值</param>
        /// <param name="expiration">过期时间</param>
        Task RefreshAsync<T>(string cacheKey, T cacheValue, TimeSpan expiration) where T : class;

        /// <summary>
        /// 移除缓存，根据缓存键的前缀
        /// </summary>
        /// <param name="prefix">缓存键前缀</param>
        /// <returns></returns>
        void RemoveByPrefix(string prefix);

        /// <summary>
        /// 移除缓存，根据缓存键的前缀
        /// </summary>
        /// <param name="prefix">缓存键前缀</param>
        /// <returns></returns>
        Task RemoveByPrefixAsync(string prefix);

        /// <summary>
        /// 设置全部缓存，根据字典
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        void SetAll<T>(IDictionary<string, T> value, TimeSpan expiration) where T : class;

        /// <summary>
        /// 设置全部缓存，根据字典
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        Task SetAllAsync<T>(IDictionary<string, T> value, TimeSpan expiration) where T : class;

        /// <summary>
        /// 获取全部缓存，根据缓存键列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKeys">缓存键列表</param>
        /// <returns></returns>
        IDictionary<string, CacheValue<T>> GetAll<T>(IEnumerable<string> cacheKeys) where T : class;

        /// <summary>
        /// 获取全部缓存，根据缓存键列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cacheKeys">缓存键列表</param>
        /// <returns></returns>
        Task<IDictionary<string, CacheValue<T>>> GetAllAsync<T>(IEnumerable<string> cacheKeys) where T : class;

        /// <summary>
        /// 获取缓存，根据缓存键前缀
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="prefix">缓存键前缀</param>
        /// <returns></returns>
        IDictionary<string, CacheValue<T>> GetByPrefix<T>(string prefix) where T : class;

        /// <summary>
        /// 获取缓存，根据缓存键前缀
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="prefix">缓存键前缀</param>
        /// <returns></returns>
        Task<IDictionary<string, CacheValue<T>>> GetByPrefixAsync<T>(string prefix) where T : class;

        /// <summary>
        /// 移除缓存，根据缓存键列表
        /// </summary>
        /// <param name="cacheKeys">缓存键列表</param>
        void RemoveAll(IEnumerable<string> cacheKeys);

        /// <summary>
        /// 移除缓存，根据缓存键列表
        /// </summary>
        /// <param name="cacheKeys">缓存键列表</param>
        /// <returns></returns>
        Task RemoveAllAsync(IEnumerable<string> cacheKeys);

        /// <summary>
        /// 获取缓存项总数
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        int GetCount(string prefix = "");

        /// <summary>
        /// 刷新所有缓存项
        /// </summary>
        void Flush();

        /// <summary>
        /// 刷新所有缓存项
        /// </summary>
        /// <returns></returns>
        Task FlushAsync();
    }
}
