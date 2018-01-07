using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Abstractions;
using Bing.Pools;
using Bing.Utils.Exceptions;
using StackExchange.Redis;

namespace Bing.Caching.Redis
{
    public class RedisCacheClient:ICacheClient<ConnectionMultiplexer>
    {
        /// <summary>
        /// 缓存连接池
        /// </summary>
        private static readonly ConcurrentDictionary<string, ObjectPool<ConnectionMultiplexer>> _pool =
            new ConcurrentDictionary<string, ObjectPool<ConnectionMultiplexer>>();

        /// <summary>
        /// 是否已释放连接
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// 读写锁
        /// </summary>
        private static object _lockObj = new object();

        /// <summary>
        /// 析构函数，当对象结束其生命周期时，系统自动执行析构函数，常用于清理善后
        /// </summary>
        ~RedisCacheClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// 获取客户端
        /// </summary>
        /// <param name="endpoint">终端</param>
        /// <param name="connectionTimeout">连接超时时间</param>
        /// <returns></returns>
        public ConnectionMultiplexer GetClient(ICacheEndpoint endpoint, int connectionTimeout)
        {
            try
            {
                var info = endpoint as RedisEndpoint;
                if (info == null)
                {
                    throw new ArgumentNullException(nameof(info));
                }

                var key = $"{info.Host}{info.Port}{info.Password}{info.DbIndex}";
                if (!_pool.ContainsKey(key))
                {
                    var objectPool=new ObjectPool<ConnectionMultiplexer>(() =>
                    {
                        var client = ConnectionMultiplexer.Connect(GetConnectionString(info));
                        return client;
                    },info.MinSize,info.MaxSize);
                    _pool.GetOrAdd(key, objectPool);
                    return objectPool.GetObject();
                }
                else
                {
                    return _pool[key].GetObject();
                }

            }
            catch (Exception e)
            {
                throw new Warning(e.Message);
            }
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="endpoint">Redis终端</param>
        /// <returns></returns>
        public string GetConnectionString(RedisEndpoint endpoint)
        {
            StringBuilder sb=new StringBuilder();
            if (!string.IsNullOrWhiteSpace(endpoint.Host) && endpoint.Port > 0)
            {
                sb.Append($"{endpoint.Host}:{endpoint.Port}");
            }

            if (!string.IsNullOrWhiteSpace(endpoint.Password))
            {
                sb.Append($",password={endpoint.Password}");
            }

            if (endpoint.DbIndex > 0)
            {
                sb.Append($",defaultDatabase={endpoint.DbIndex}");
            }

            if (endpoint.Timeout > 0)
            {
                sb.Append($",connectTimeout={endpoint.Timeout}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            // 显式释放
            Dispose(true);
            // 阻止被GC终结
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放资源
        /// 1、显式释放资源
        /// 2、终结器
        /// </summary>
        /// <param name="disposing">是否释放中</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // 释放托管资源
                    lock (_lockObj)
                    {
                        foreach (var client in _pool.Values)
                        {
                            try
                            {
                                client.GetObject().Dispose();
                            }
                            catch (Exception e)
                            {
                                throw new Exception("释放缓存连接资源异常!",e);
                            }
                        }
                        _pool.Clear();
                    }
                }
            }

            _disposed = true;
        }
    }
}
