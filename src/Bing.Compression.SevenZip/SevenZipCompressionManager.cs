using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Compression.Abstractions;
using SevenZip;

namespace Bing.Compression.SevenZip
{
    /// <summary>
    /// 7-zip 压缩管理器。
    /// https://github.com/tomap/SevenZipSharp
    /// </summary>
    public class SevenZipCompressionManager:ICompressionManager
    {
        /// <summary>
        /// 初始化一个<see cref="SevenZipCompressionManager"/>类型的实例
        /// </summary>
        public SevenZipCompressionManager()
        {
            const string SEVEN_ZIP_DLL_PATH = @"PATH_TO_7Z(or 7z64).DLL";
            SevenZipBase.SetLibraryPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,SEVEN_ZIP_DLL_PATH));
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="data">数据，需要压缩的内容</param>
        /// <returns></returns>
        public byte[] Compress(byte[] data)
        {
            byte[] result;

            SevenZipCompressor.LzmaDictionarySize = GetDictionarySize(data.LongLength);
            SevenZipCompressor compressor=new SevenZipCompressor()
            {
                ArchiveFormat = OutArchiveFormat.SevenZip,
                CompressionLevel = CompressionLevel.Ultra,
                CompressionMethod = CompressionMethod.Lzma2,
                CompressionMode = CompressionMode.Create,
                FastCompression = false
            };

            using (MemoryStream uncompressedStream=new MemoryStream(data))
            {
                using (MemoryStream compressedStream=new MemoryStream())
                {
                    compressor.CompressStream(uncompressedStream, compressedStream);
                    result = compressedStream.ToArray();
                }
            }

            return result;
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="data">数据，需要解压的内容</param>
        /// <returns></returns>
        public byte[] Decompress(byte[] data)
        {
            byte[] result;
            using (MemoryStream compressedStream=new MemoryStream(data))
            {
                using (MemoryStream uncompressedStream=new MemoryStream())
                {
                    using (SevenZipExtractor extractor=new SevenZipExtractor(compressedStream))
                    {
                        extractor.ExtractFile(0,uncompressedStream);
                        result = uncompressedStream.ToArray();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取目录大小
        /// </summary>
        /// <param name="length">内容长度</param>
        /// <returns></returns>
        private static int GetDictionarySize(long length)
        {
            const double BYTE_TO_MB_DIVIDER = 1024d * 1024d;
            int result = 1;
            double lengthInMb = length / BYTE_TO_MB_DIVIDER;
            if (Math.Abs(lengthInMb - 1d) > double.Epsilon)
            {
                while (result<=(int)lengthInMb)
                {
                    result <<= 1;
                }
            }

            return result;
        }
    }
}
