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
    }
}
