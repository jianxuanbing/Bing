using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Compression.Abstractions
{
    /// <summary>
    /// 压缩管理器
    /// </summary>
    public interface ICompressionManager
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="data">数据，需要压缩的内容</param>
        /// <returns></returns>
        byte[] Compress(byte[] data);

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="data">数据，需要解压的内容</param>
        /// <returns></returns>
        byte[] Decompress(byte[] data);
    }
}
