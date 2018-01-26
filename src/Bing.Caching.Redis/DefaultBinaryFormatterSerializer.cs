using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Abstractions;

namespace Bing.Caching.Redis
{
    /// <summary>
    /// 默认的二进制格式化 序列化器
    /// </summary>
    public class DefaultBinaryFormatterSerializer:ICacheSerializer
    {
        /// <summary>
        /// 序列化指定的值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        public byte[] Serialize<T>(T value)
        {
            using (var ms=new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms,value);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 反序列指定的byte[]
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="bytes">值</param>
        /// <returns></returns>
        public T Deserialize<T>(byte[] bytes)
        {
            using (var ms=new MemoryStream(bytes))
            {
                return (T) new BinaryFormatter().Deserialize(ms);
            }
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public ArraySegment<byte> SerializeObject(object obj)
        {
            using (var ms=new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms,obj);
                return new ArraySegment<byte>(ms.GetBuffer(), 0, (int) ms.Length);
            }
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public object DeserializeObject(ArraySegment<byte> value)
        {
            using (var ms=new MemoryStream(value.Array,value.Offset,value.Count))
            {
                return new BinaryFormatter().Deserialize(ms);
            }
        }
    }
}
