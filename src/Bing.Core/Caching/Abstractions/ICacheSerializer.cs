using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Abstractions
{
    /// <summary>
    /// 缓存序列化器
    /// </summary>
    public interface ICacheSerializer
    {
        T Deserialize<T>(string value);

        Task<T> DeserializeAsync<T>(string value);


        string Serialize<T>(T value);

        Task<string> SerializeAsync<T>(T value);
    }
}
