using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Abstractions;
using MessagePack;
using MessagePack.Resolvers;

namespace Bing.Caching.Serialization.MessagePack
{
    /// <summary>
    /// 默认消息包格式序列化器
    /// </summary>
    public class DefaultMessagePackSerializer:ICacheSerializer
    {
        /// <summary>
        /// 初始化一个<see cref="DefaultMessagePackSerializer"/>类型的实例
        /// </summary>
        public DefaultMessagePackSerializer()
        {
            MessagePackSerializer.SetDefaultResolver(ContractlessStandardResolver.Instance);
        }

        /// <summary>
        /// 序列化指定的对象为字节数组
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        public byte[] Serialize<T>(T value)
        {
            return MessagePackSerializer.Serialize(value);
        }

        /// <summary>
        /// 反序列化指定的字节数组为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public T Deserialize<T>(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<T>(bytes);
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public ArraySegment<byte> SerializeObject(object obj)
        {
            return MessagePackSerializer.SerializeUnsafe<object>(obj, TypelessContractlessStandardResolver.Instance);
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public object DeserializeObject(ArraySegment<byte> value)
        {
            return MessagePackSerializer.Deserialize<object>(value, TypelessContractlessStandardResolver.Instance);
        }
    }
}
