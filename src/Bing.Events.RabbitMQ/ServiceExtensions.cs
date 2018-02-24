using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Bing.Dependency;
using Bing.Events.Handlers;
using Bing.Events.Messages;
using Bing.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Bing.Events.RabbitMQ
{
    /// <summary>
    /// 事件总线 扩展
    /// </summary>
    public static partial class ServiceExtensions
    {
        /// <summary>
        /// 注册 RabbitMQ 事件总线服务
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="optionAction">RabbitMQ 配置</param>
        public static void AddRabbitMqEventBus(this ContainerBuilder services, Action<RabbitMqOptions> optionAction)
        {
            services.AddRabbitMq(optionAction);
            services.AddTransient<IEventBus, RabbitMqEventBus>();
        }

        /// <summary>
        /// 注册 RabbitMQ
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="optionAction">RabbitMQ 配置</param>
        public static void AddRabbitMq(this ContainerBuilder services, Action<RabbitMqOptions> optionAction)
        {
            var option=new RabbitMqEventBusOptions();
            optionAction(option);

            var factory = new ConnectionFactory()
            {
                HostName = option.HostName,
                UserName = option.UserName,
                Password = option.Password
            };

            // 创建连接
            var connection = factory.CreateConnection();
            // 创建通道
            var channel = connection.CreateModel();

            // 声明一个队列（durable=true 持久化消息）
            channel.QueueDeclare(option.QueueName, true, false, false, null);

            if (!string.IsNullOrWhiteSpace(option.ExchangeName))
            {
                channel.ExchangeDeclare(option.ExchangeName, option.ExchangeType, false, false, null);

                // 将队列绑定到交换机
                channel.QueueBind(option.QueueName, option.ExchangeName, option.RouteKey, null);
            }

            option.Channel = channel;

            // 事件基本消费者
            EventingBasicConsumer consumer=new EventingBasicConsumer(channel);

            // 接收到消息事件
            consumer.Received += (sender, args) =>
            {
                var json = Encoding.UTF8.GetString(args.Body);
                var o = JObject.Parse(json);
                var messageType = Type.GetType(o[nameof(MessageEvent.MessageType)].Value<string>());
                dynamic message = o.ToObject(messageType);

                try
                {
                    var handlers = Ioc.CreateList(typeof(IEventHandler<>).MakeGenericType(messageType));

                    foreach (var handler in handlers)
                    {
                        ((dynamic) handler).Handle((dynamic) message);
                    }

                    // 确认该消息已被消费
                    channel.BasicAck(args.DeliveryTag,false);
                }
                catch
                {
                    // ignored
                }
            };

            // 启动消费者 设置为手动应答消息
            channel.BasicConsume(option.QueueName, false, consumer);

            services.AddSingleton(option);
        }
    }
}
