using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Abstractions;
using Bing.Caching.Events;

namespace Bing.Caching.Core
{
    /// <summary>
    /// 异步缓存事件
    /// </summary>
    public sealed class EventableAsyncCache:IAsyncCache,ICacheEventable
    {
        /// <summary>
        /// 目标缓存
        /// </summary>
        private readonly IAsyncCache _targetCache;

        /// <summary>
        /// 初始化一个<see cref="EventableAsyncCache"/>类型的实例
        /// </summary>
        /// <param name="targetCache">目标缓存</param>
        public EventableAsyncCache(IAsyncCache targetCache)
        {
            this._targetCache = targetCache;
        }

        public List<string> InitializationProperties => this._targetCache.InitializationProperties;

        public string ProviderName => this._targetCache.ProviderName;

        public long DefaultExpireTime
        {
            get { return this._targetCache.DefaultExpireTime; }
            set { this._targetCache.DefaultExpireTime = value; }
        }

        public string KeySuffix
        {
            get { return this._targetCache.KeySuffix; }
            set { this._targetCache.KeySuffix = value; }
        }

        public async Task InitializeAsync(IDictionary<string, string> parameters)
        {
            await this._targetCache.InitializeAsync(parameters);
        }

        public async Task AddAsync(string key, object value)
        {
            await this._targetCache.AddAsync(key, value);
        }

        public Task AddAsync(string key, object value, bool defaultExpire)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(string key, object value, long numOfMinutes)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(string key, object value, TimeSpan timeSpan)
        {
            SetBefore?.Invoke(this, new SetCacheEventArgs(key, value, timeSpan));
            await this._targetCache.AddAsync(key, value, timeSpan);
            SetAfter?.Invoke(this, new SetCacheEventArgs(key, value, timeSpan));
        }

        public Task AddAsync<T>(string key, T value, TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(string key, object value, long numOfMinutes, bool flag)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<string, T>> GetAsync<T>(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetAsync(string key)
        {
            GetBefore?.Invoke(this, new GetCacheEventArgs(key));
            var result = await this._targetCache.GetAsync(key);
            GetAfter?.Invoke(this, new GetCacheEventArgs(key));
            return result;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            GetBefore?.Invoke(this,new GetCacheEventArgs(key));
            var result = await this._targetCache.GetAsync<T>(key);
            GetAfter?.Invoke(this, new GetCacheEventArgs(key));
            return result;
        }

        public async Task RemoveAsync(string key)
        {
            RemoveBefore?.Invoke(this,new CacheEventArgs(key));
            await this._targetCache.RemoveAsync(key);
            RemoveAfter?.Invoke(this,new CacheEventArgs(key));
        }

        public event EventHandler<SetCacheEventArgs> SetBefore;
        public event EventHandler<SetCacheEventArgs> SetAfter;
        public event EventHandler<CacheEventArgs> GetBefore;
        public event EventHandler<GetCacheEventArgs> GetAfter;
        public event EventHandler<CacheEventArgs> RemoveBefore;
        public event EventHandler<CacheEventArgs> RemoveAfter;
    }
}
