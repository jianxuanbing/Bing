using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Events.Messages;
using Bing.Logs.Aspects;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Bing.Events.RabbitMQ
{
    /// <summary>
    /// RabbitMQ 事件总线
    /// </summary>
    public class RabbitMqEventBus:IEventBus,IDisposable
    {
        /// <summary>
        /// 消息Channel
        /// </summary>
        private readonly IModel _channel;

        /// <summary>
        /// RabbitMQ 事件总线配置
        /// </summary>
        private readonly RabbitMqEventBusOptions _options;

        /// <summary>
        /// 初始化一个<see cref="RabbitMqEventBus"/>类型的实例
        /// </summary>
        /// <param name="messageEventBus">消息事件总线</param>
        /// <param name="options">RabbitMq 事件总线配置</param>
        public RabbitMqEventBus(IMessageEventBus messageEventBus, RabbitMqEventBusOptions options)
        {
            _channel = options.Channel;
            _options = options;

            _options.MessagePublisher = messageEventBus;
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="event">事件</param>
        [TraceLog]
        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event == null)
            {
                return;
            }

            var msg = JsonConvert.SerializeObject(@event);
            var sendBytes = Encoding.UTF8.GetBytes(msg);

            _channel.BasicPublish(_options.ExchangeName, _options.RouteKey, null, sendBytes);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _channel?.Dispose();
        }
    }
}
