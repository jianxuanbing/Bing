using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Compression.Abstractions;

namespace Bing.Compression.Default
{
    /// <summary>
    /// 默认压缩管理器
    /// </summary>
    public class DefaultCompressionManager:ICompressionManager
    {
        /// <summary>
        /// 缓存区大小
        /// </summary>
        protected const int BUFFER_SIZE = 4096;

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="data">数据，需要压缩的内容</param>
        /// <returns></returns>
        public byte[] Compress(byte[] data)
        {
            byte[] compressArray;
            using (MemoryStream memoryStream=new MemoryStream())
            {
                using (DeflateStream deflateStream=new DeflateStream(memoryStream,CompressionMode.Compress))
                {
                    deflateStream.Write(data,0,data.Length);
                }

                compressArray = memoryStream.ToArray();
            }

            return compressArray;
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="data">数据，需要解压的内容</param>
        /// <returns></returns>
        public byte[] Decompress(byte[] data)
        {            
            byte[] decompressedArray=new byte[BUFFER_SIZE];

            using (MemoryStream outputStream=new MemoryStream())
            {
                using (MemoryStream compressedStream=new MemoryStream(data))
                {
                    using (DeflateStream deflateStream=new DeflateStream(compressedStream,CompressionMode.Decompress))
                    {
                        while (true)
                        {
                            int readBytes = deflateStream.Read(decompressedArray, 0, decompressedArray.Length);

                            if (readBytes <= 0)
                            {
                                break;
                            }

                            outputStream.Write(decompressedArray, 0, readBytes);
                        }

                        decompressedArray = outputStream.ToArray();
                    }
                }
            }

            return decompressedArray;
        }
    }
}
