using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.MQ.RabbitMQ.Serializers
{
    /// <summary>
    /// IMessageSerializer 创建工厂
    /// </summary>
    public class MessageSerializerFactory
    {
        /// <summary>
        /// 创建一个消息序列化组件
        /// </summary>
        /// <returns></returns>
        public static IMessageSerializer CreateMessageSerializerInstance()
        {
            return new MessageXmlSerializer();
        }
    }
}
