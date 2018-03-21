using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Abstractions;
using Newtonsoft.Json;

namespace Bing.Serialization.Json
{
    /// <summary>
    /// 默认Json序列化器
    /// </summary>
    public class DefaultJsonSerializer:ICacheSerializer
    {
        /// <summary>
        /// Json序列化器
        /// </summary>
        static readonly JsonSerializer JsonSerializer=new JsonSerializer();

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

        public T Deserialize<T>(byte[] bytes)
        {
            using (var ms=new MemoryStream(bytes))
            {
                using (var sr=new StreamWriter(ms,Encoding.UTF8))
                {                    
                }
            }
        }

        public ArraySegment<byte> SerializeObject(object obj)
        {
            throw new NotImplementedException();
        }

        public object DeserializeObject(ArraySegment<byte> value)
        {
            throw new NotImplementedException();
        }
    }
}
