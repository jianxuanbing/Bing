using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching
{
    /// <summary>
    /// 本地缓存
    /// </summary>
    public interface ILocalCache
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiration"></param>
        void Set(string key, object value, TimeSpan expiration);


    }
}
