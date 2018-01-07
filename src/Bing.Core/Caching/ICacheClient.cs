using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Abstractions;

namespace Bing.Caching
{
    /// <summary>
    /// 缓存客户端
    /// </summary>
    /// <typeparam name="T">缓存客户端对象</typeparam>
    public interface ICacheClient<T>:IDisposable
    {
        /// <summary>
        /// 获取客户端
        /// </summary>
        /// <param name="endpoint">终端</param>
        /// <param name="connectionTimeout">连接超时时间</param>
        /// <returns></returns>
        T GetClient(ICacheEndpoint endpoint, int connectionTimeout);
    }
}
