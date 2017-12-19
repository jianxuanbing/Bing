using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bing.MQ.RabbitMQ.Serializers
{
    /// <summary>
    /// 面向XML的消息序列化
    /// </summary>
    public class MessageXmlSerializer:IMessageSerializer
    {
        public byte[] SerializerBytes<T>(T message) where T : class, new()
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var ms=new MemoryStream())
            {
                xmlSerializer.Serialize(ms,message);
                return ms.ToArray();
            }
        }

        public string SerializerXmlString<T>(T message) where T : class, new()
        {
            var xmlSerializer=new XmlSerializer(typeof(T));
            using (var sw=new StringWriter())
            {
                xmlSerializer.Serialize(sw,message);
                return message.ToString();
            }
        }

        public T Deserialize<T>(byte[] bytes) where T : class, new()
        {
            var xmlSerializer=new XmlSerializer(typeof(T));
            using (var ms=new MemoryStream(bytes))
            {
                return xmlSerializer.Deserialize(ms) as T;
            }
        }
    }
}
