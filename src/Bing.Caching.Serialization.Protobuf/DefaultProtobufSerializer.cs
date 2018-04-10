using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Abstractions;
using Bing.Utils.Helpers;
using ProtoBuf;

namespace Bing.Caching.Serialization.Protobuf
{
    /// <summary>
    /// 默认 Protobuf 序列化器
    /// </summary>
    public class DefaultProtobufSerializer:ICacheSerializer
    {
        /// <summary>
        /// 序列化指定的对象为字节数组
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        public byte[] Serialize<T>(T value)
        {
            using (var ms=new MemoryStream())
            {
                Serializer.Serialize<T>(ms,value);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 反序列化指定的字节数组为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public T Deserialize<T>(byte[] bytes)
        {
            using (MemoryStream ms=new MemoryStream(bytes))
            {
                return Serializer.Deserialize<T>(ms);
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
                WriteType(ms,obj.GetType());
                Serializer.NonGeneric.Serialize(ms,obj);

                return new ArraySegment<byte>(ms.ToArray(), 0, (int) ms.Length);
            }
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public object DeserializeObject(ArraySegment<byte> value)
        {
            var raw = value.Array;
            var count = value.Count;
            var offset = value.Offset;
            var type = ReadType(raw, ref offset, ref count);

            using (var ms=new MemoryStream(raw,offset,count,writable:false))
            {
                return Serializer.NonGeneric.Deserialize(type, ms);
            }
        }

        /// <summary>
        /// 读取类型
        /// </summary>
        /// <param name="buffer">缓存流</param>
        /// <param name="offset">起始位置</param>
        /// <param name="count">总长度</param>
        /// <returns></returns>
        private Type ReadType(byte[] buffer, ref int offset, ref int count)
        {
            if (count < 4)
            {
                throw new EndOfStreamException();
            }
            // len is size of header typeName(string)
            var len = (int) buffer[offset++]
                      | (buffer[offset++] << 8)
                      | (buffer[offset++] << 16)
                      | (buffer[offset++] << 24);
            count -= 4;// count is message total size, decr typeName length(int)

            if (count < len)
            {
                throw new EndOfStreamException();
            }

            var keyOffset = offset;
            offset += len;// skip typeName body size
            count -= len;// decr typeName body size

            // avoid encode string
            var key = new ArraySegment<byte>(buffer, keyOffset, len);

            var typeName = Encoding.UTF8.GetString(key.Array, key.Offset, key.Count);

            return Type.GetType(typeName, throwOnError: true);
        }

        /// <summary>
        /// 写入类型
        /// </summary>
        /// <param name="ms">内存流</param>
        /// <param name="type">类型</param>
        private void WriteType(MemoryStream ms, Type type)
        {
            var typeName = Reflection.GetTypeName(type);
            var typeArray = Encoding.UTF8.GetBytes(typeName);

            var len = typeArray.Length;
            // BinaryWrite Int32
            ms.WriteByte((byte)len);
            ms.WriteByte((byte) (len >> 8));
            ms.WriteByte((byte) (len >> 16));
            ms.WriteByte((byte) (len >> 24));
            // BinaryWrite String
            ms.Write(typeArray, 0, len);
        }
    }
}
