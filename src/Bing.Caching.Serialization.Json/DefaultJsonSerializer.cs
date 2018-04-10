using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Abstractions;
using Bing.Utils.Helpers;
using Newtonsoft.Json;

namespace Bing.Caching.Serialization.Json
{
    /// <summary>
    /// 默认 Json格式 序列化器
    /// </summary>
    public class DefaultJsonSerializer:ICacheSerializer
    {
        /// <summary>
        /// Json序列化器
        /// </summary>
        static readonly JsonSerializer JsonSerializer=new JsonSerializer();

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
                using (var sr=new StreamWriter(ms,Encoding.UTF8))
                {
                    using (var jtr=new JsonTextWriter(sr))
                    {
                        JsonSerializer.Serialize(jtr,value);
                    }
                }

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
            using (var ms=new MemoryStream(bytes))
            {
                using (var sr = new StreamReader(ms, Encoding.UTF8))
                {
                    using (var jtr=new JsonTextReader(sr))
                    {
                        return JsonSerializer.Deserialize<T>(jtr);
                    }
                }
            }
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public ArraySegment<byte> SerializeObject(object obj)
        {
            var typeName = Reflection.GetTypeName(obj.GetType());

            using (var ms=new MemoryStream())
            {
                using (var tw=new StreamWriter(ms))
                {
                    using (var jw=new JsonTextWriter(tw))
                    {
                        jw.WriteStartArray();// [
                        jw.WriteValue(typeName);// "type"
                        JsonSerializer.Serialize(jw,obj);// obj

                        jw.WriteEndArray();// ]
                        jw.Flush();

                        return new ArraySegment<byte>(ms.ToArray(),0,(int)ms.Length);
                    }
                }
            }
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public object DeserializeObject(ArraySegment<byte> value)
        {
            using (var ms=new MemoryStream(value.Array,value.Offset,value.Count,writable:false))
            {
                using (var tr=new StreamReader(ms))
                {
                    using (var jr=new JsonTextReader(tr))
                    {
                        jr.Read();
                        if (jr.TokenType == JsonToken.StartArray)
                        {
                            // 读取类型
                            var typeName = jr.ReadAsString();
                            var type = Type.GetType(typeName, throwOnError: true);// 获取类型
                            // 读取对象
                            jr.Read();
                            return JsonSerializer.Deserialize(jr, type);
                        }
                        else
                        {
                            throw new InvalidDataException("JsonTranscoder 仅支持 [\"TypeName\", object]");
                        }
                    }
                }
            }
        }
    }
}
