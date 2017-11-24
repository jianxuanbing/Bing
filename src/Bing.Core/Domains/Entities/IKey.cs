using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Domains.Entities
{
    /// <summary>
    /// 标识
    /// </summary>
    /// <typeparam name="TKey">标识类型</typeparam>
    public interface IKey<out TKey>
    {
        /// <summary>
        /// 标识
        /// </summary>
        TKey Id { get; }
    }
}
