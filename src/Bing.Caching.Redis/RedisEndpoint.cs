using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Abstractions;

namespace Bing.Caching.Redis
{
    /// <summary>
    /// Redis 终端
    /// </summary>
    public class RedisEndpoint:ICacheEndpoint
    {
        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 数据库
        /// </summary>
        public long DbIndex { get; set; }

        /// <summary>
        /// 连接数下限
        /// </summary>
        public int MinSize { get; set; }

        /// <summary>
        /// 连接数上限
        /// </summary>
        public int MaxSize { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Host) && Port > 0)
            {
                sb.Append($"{Host}:{Port}");
            }

            if (!string.IsNullOrWhiteSpace(Password))
            {
                sb.Append($",password={Password}");
            }

            if (DbIndex > 0)
            {
                sb.Append($",defaultDatabase={DbIndex}");
            }

            if (Timeout > 0)
            {
                sb.Append($",connectTimeout={Timeout}");
            }

            return sb.ToString();
        }
        
    }
}
