using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.MQ.RabbitMQ.Configs
{
    /// <summary>
    /// 消息队列配置
    /// </summary>
    public class MqConfig
    {
        /// <summary>
        /// 消息队列的地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 客户端默认监听的队列名称
        /// </summary>
        public string ListenQueueName { get; set; }
    }
}
