using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.MQ.RabbitMQ.Configs;
using RabbitMQ.Client;

namespace Bing.MQ.RabbitMQ
{
    /// <summary>
    /// RabbitMQ 客户端工厂
    /// </summary>
    public class RabbitMqClientFactory
    {

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns></returns>
        public static IRabbitMqClient CreateInstance()
        {
            var clientContext=new RabbitMqClientContext()
            {
                ListenQueueName = MqConfigFactory.CreateConfigInstance().ListenQueueName,
                InstanceCode = Guid.NewGuid().ToString()
            };

            return new RabbitMqClient()
            {
                Context = clientContext
            };
        }

        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns></returns>
        internal static IConnection CreateConnection()
        {
            var config = MqConfigFactory.CreateConfigInstance();
            const ushort heartbeat = 60;
            var factory=new ConnectionFactory()
            {
                HostName = config.Host,
                UserName = config.UserName,
                Password = config.Password,
                //RequestedHeartbeat = heartbeat,
                //AutomaticRecoveryEnabled = true
            };

            return factory.CreateConnection();
        }

        /// <summary>
        /// 创建通道
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        internal static IModel CreateModel(IConnection connection)
        {
            return connection.CreateModel();//创建通道
        }
    }
}
