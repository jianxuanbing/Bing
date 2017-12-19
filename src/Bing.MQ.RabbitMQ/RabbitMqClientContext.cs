using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Bing.MQ.RabbitMQ
{
    public class RabbitMqClientContext
    {
        /// <summary>
        /// 用于发送消息的Connection
        /// </summary>
        public IConnection SendConnection { get; internal set; }

        /// <summary>
        /// 用于发送消息的Channel
        /// </summary>
        public IModel SendChannel { get; internal set; }

        /// <summary>
        /// 用户监听的Connection
        /// </summary>
        public IConnection ListenConnection { get; internal set; }

        /// <summary>
        /// 用户监听的Channel
        /// </summary>
        public IModel ListenChannel { get; internal set; }

        /// <summary>
        /// 默认监听的队列名称
        /// </summary>
        public string ListenQueueName { get; internal set; }

        /// <summary>
        /// 实例编号
        /// </summary>
        public string InstanceCode { get; set; }
    }
}
