using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Abstractions;
using Bing.Utils.Extensions;
using Bing.Utils.Json;

namespace Bing.Caching.Redis
{
    /// <summary>
    /// 默认Json格式化 序列化器
    /// </summary>
    public class DefaultJsonFormatterSerializer:ICacheSerializer
    {
        public T Deserialize<T>(string value)
        {
            return value.SafeString().ToObject<T>();
        }

        public Task<T> DeserializeAsync<T>(string value)
        {
            return Task.Factory.StartNew(() => value.SafeString().ToObject<T>());

        }

        public string Serialize<T>(T value)
        {
            return value is string ? value.ToString() : value.ToJson();
        }

        public Task<string> SerializeAsync<T>(T value)
        {
            return Task.Factory.StartNew(() => value is string ? value.ToString() : value.ToJson());
        }
    }
}
