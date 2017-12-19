using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.MQ.RabbitMQ.Serializers;

namespace Bing.MQ.RabbitMQ.Events
{
    /// <summary>
    /// 表示一个事件消息
    /// </summary>
    [Serializable]
    public sealed class EventMessage
    {
        /// <summary>
        /// 消息的标识码
        /// </summary>
        public string EventMessageMarkcode { get; set; }

        /// <summary>
        /// 消息的序列化字节流
        /// </summary>
        public byte[] EventMessageBytes { get; set; }

        /// <summary>
        /// 创建消息的时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 生成 EventMessageResult 对象
        /// </summary>
        /// <param name="bytes">流</param>
        /// <returns></returns>
        internal static EventMessageResult BuildEventMessageResult(byte[] bytes)
        {
            var eventMessage = MessageSerializerFactory.CreateMessageSerializerInstance()
                .Deserialize<EventMessage>(bytes);
            var result=new EventMessageResult()
            {
                MessageBytes = eventMessage.EventMessageBytes,
                EventMessageBytes = eventMessage
            };
            return result;
        }
    }
}
