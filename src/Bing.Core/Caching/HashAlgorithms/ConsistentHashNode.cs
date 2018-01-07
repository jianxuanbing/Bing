using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace Bing.Caching.HashAlgorithms
{
    /// <summary>
    /// 哈希节点对象
    /// </summary>
    public class ConsistentHashNode
    {
        /// <summary>
        /// 缓存目标类型
        /// </summary>
        public CacheTargetType Type { get; set; }

        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 数据库
        /// </summary>
        public string Db { get; set; }

        /// <summary>
        /// 连接数上限
        /// </summary>
        public string MaxSize { get; set; } = "50";

        /// <summary>
        /// 连接数下限
        /// </summary>
        public string MinSize { get; set; } = "1";
    }
}
