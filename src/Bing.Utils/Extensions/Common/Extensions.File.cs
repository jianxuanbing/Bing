using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bing.Utils.Extensions
{
    /// <summary>
    /// 系统扩展 - 文件或流相关扩展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 将字节流写入文件
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="filePath">文件绝对路径</param>
        public static void ToFile(this byte[] stream, string filePath)
        {
            var directoryPath = Path.GetDirectoryName(filePath);
            if (Directory.Exists(directoryPath) == false)
            {
                Directory.CreateDirectory(directoryPath);
            }
            File.WriteAllBytes(filePath,stream);
        }
    }
}
