using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.MQ.RabbitMQ.Serializers;

namespace Bing.MQ.RabbitMQ.Events
{
    /// <summary>
    /// 消息事件工厂，创建EventMessage实例
    /// </summary>
    public class EventMessageFactory
    {
        /// <summary>
        /// 创建EventMessage传输对象
        /// </summary>
        /// <typeparam name="T">原始对象类型</typeparam>
        /// <param name="originObject">原始强类型对象实例</param>
        /// <param name="eventMessageMarkcode">消息的标识码</param>
        /// <returns></returns>
        public static EventMessage CreateEventMessageInstance<T>(T originObject, string eventMessageMarkcode)
            where T : class, new()
        {
            var result=new EventMessage()
            {
                CreateDateTime = DateTime.Now,
                EventMessageMarkcode = eventMessageMarkcode
            };

            var bytes = MessageSerializerFactory.CreateMessageSerializerInstance().SerializerBytes<T>(originObject);
            result.EventMessageBytes = bytes;
            return result;
        }
    }
}
