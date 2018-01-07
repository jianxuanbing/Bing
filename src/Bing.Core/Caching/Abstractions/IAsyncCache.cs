using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Abstractions
{
    /// <summary>
    /// 异步缓存
    /// </summary>
    public interface IAsyncCache:ICache
    {
        /// <summary>
        /// 初始化缓存连接对象
        /// </summary>
        /// <param name="parameters">参数字典</param>
        /// <returns></returns>
        Task InitializeAsync(IDictionary<string, string> parameters);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        Task AddAsync(string key, object value);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="defaultExpire">是否默认过期时间,true:是,false:否</param>
        /// <returns></returns>
        Task AddAsync(string key, object value, bool defaultExpire);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="numOfMinutes">过期时间。单位：分钟</param>
        /// <returns></returns>
        Task AddAsync(string key, object value, long numOfMinutes);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="timeSpan">滑动过期时间，在此时间内访问缓存，缓存将继续有效</param>
        /// <returns></returns>
        Task AddAsync(string key, object value, TimeSpan timeSpan);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="timeSpan">滑动过期时间，在此时间内访问缓存，缓存将继续有效</param>
        /// <returns></returns>
        Task AddAsync<T>(string key, T value, TimeSpan timeSpan);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="numOfMinutes">过期时间。单位：分钟</param>
        /// <param name="flag">标识是否永不过期（本地缓存有效）</param>
        /// <returns></returns>
        Task AddAsync(string key, object value, long numOfMinutes, bool flag);

        /// <summary>
        /// 获取缓存项字典
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="keys">缓存键列表</param>
        /// <returns></returns>
        Task<IDictionary<string, T>> GetAsync<T>(IEnumerable<string> keys);

        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        Task<object> GetAsync(string key);

        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// 移除缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        Task RemoveAsync(string key);

        
    }
}
