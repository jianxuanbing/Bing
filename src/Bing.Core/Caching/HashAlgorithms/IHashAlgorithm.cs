using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.HashAlgorithms
{
    /// <summary>
    /// 一致性哈希算法
    /// </summary>
    public interface IHashAlgorithm
    {
        /// <summary>
        /// 获取哈希值
        /// </summary>
        /// <param name="item">字符串</param>
        /// <returns></returns>
        int Hash(string item);
    }
}
