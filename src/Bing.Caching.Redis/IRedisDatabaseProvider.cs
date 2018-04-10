using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Bing.Caching.Redis
{
    /// <summary>
    /// Redis 数据库提供程序
    /// </summary>
    public interface IRedisDatabaseProvider
    {
        /// <summary>
        /// 获取 数据库
        /// </summary>
        /// <returns></returns>
        IDatabase GetDatabase();

        /// <summary>
        /// 获取 服务器列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<IServer> GetServerList();

        /// <summary>
        /// 获取当前Redis服务器
        /// </summary>
        /// <param name="hostAndPort">主机名和端口</param>
        /// <returns></returns>
        IServer GetServer(string hostAndPort);

        /// <summary>
        /// 获取 Redis多连接复用器
        /// </summary>
        /// <returns></returns>
        ConnectionMultiplexer GetConnectionMultiplexer();

        /// <summary>
        /// Redis 缓存配置
        /// </summary>
        RedisCacheOptions Options { get; }
    }
}
