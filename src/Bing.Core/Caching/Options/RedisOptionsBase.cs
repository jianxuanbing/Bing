using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Core;

namespace Bing.Caching.Options
{
    /// <summary>
    /// Redis 配置基类
    /// </summary>
    public class RedisOptionsBase
    {
        /// <summary>
        /// 系统前缀
        /// </summary>
        public string SystemPrefix { get; set; } = null;

        /// <summary>
        /// 获取或设置 用于连接到Redis服务器的密码
        /// </summary>
        public string Password { get; set; } = null;

        /// <summary>
        /// 获取或设置 一个值，该值指示是否使用SSL加密
        /// </summary>
        public bool IsSsl { get; set; } = false;

        /// <summary>
        /// 获取或设置 SSL主机。如果设置，它将在服务器的证书上强制执行这个特定的主机
        /// </summary>
        public string SslHost { get; set; } = null;

        /// <summary>
        /// 获取或设置 连接有效时间（单位：毫秒，默认：5秒，除非<see cref="SyncTimeout"/>较高）
        /// </summary>
        public int ConnectionTimeout { get; set; } = 5000;

        /// <summary>
        /// 获取或设置 同步操作的时间（单位：毫秒，默认：5秒）
        /// </summary>
        public int SyncTimeout { get; set; } = 5000;

        /// <summary>
        /// 获取或设置 用于连接到Redis服务器的端点列表
        /// </summary>
        public IList<ServerEndPoint> EndPoints { get; }=new List<ServerEndPoint>();

        /// <summary>
        /// 获取或设置 是否允许执行管理操作
        /// </summary>
        public bool AllowAdmin { get; set; } = false;
    }
}
