using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.MQ.RabbitMQ.Serializers
{
    /// <summary>
    /// 消息序列化
    /// </summary>
    public interface IMessageSerializer
    {
        /// <summary>
        /// 序列化消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="message">消息实例</param>
        /// <returns></returns>
        byte[] SerializerBytes<T>(T message) where T : class, new();
        
        /// <summary>
        /// 序列化消息为Xml字符串
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="message">消息实例</param>
        /// <returns></returns>
        string SerializerXmlString<T>(T message) where T : class, new();

        /// <summary>
        /// 反序列化消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="bytes">流</param>
        /// <returns></returns>
        T Deserialize<T>(byte[] bytes) where T : class, new();
    }
}
