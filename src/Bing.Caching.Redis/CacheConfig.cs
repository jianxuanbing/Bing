using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Bing.Caching.Redis
{
    /// <summary>
    /// 缓存配置
    /// </summary>
    public class CacheConfig
    {
        /// <summary>
        /// 缓存类型
        /// </summary>
        public string CacheType { get; set; }

        /// <summary>
        /// 主机名
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 连接池上限
        /// </summary>
        public int MaxSize { get; set; }

        /// <summary>
        /// 连接池下限
        /// </summary>
        public int MinSize { get; set; }

        /// <summary>
        /// 当前数据库索引
        /// </summary>
        public int Db { get; set; }

    }
}
