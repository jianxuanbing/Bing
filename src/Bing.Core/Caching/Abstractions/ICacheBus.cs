using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Abstractions
{
    /// <summary>
    /// 缓存总线
    /// </summary>
    public interface ICacheBus:ICachePublisher,ICacheSubscriber
    {
    }
}
