
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Events.Messages;
using RabbitMQ.Client;
using _ExchangeType = RabbitMQ.Client.ExchangeType;

namespace Bing.Events.RabbitMQ
{
    /// <summary>
    /// RabbitMQ 配置
    /// </summary>
    public class RabbitMqOptions
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 主机名
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 执行类型
        /// </summary>
        public string ExchangeType { get; set; } = _ExchangeType.Topic;

        /// <summary>
        /// 执行名称
        /// </summary>
        public string ExchangeName { get; set; }

        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// 路由键
        /// </summary>
        public string RouteKey { get; set; }

        /// <summary>
        /// 消息Channel
        /// </summary>
        internal IModel Channel { get; set; }

        /// <summary>
        /// 消息发布者
        /// </summary>
        internal IMessageEventBus MessagePublisher { get; set; }
    }
}
