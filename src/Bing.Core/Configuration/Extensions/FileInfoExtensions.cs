using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Configuration.Extensions
{
    /// <summary>
    /// 文件信息 扩展
    /// </summary>
    public static class FileInfoExtensions
    {
        /// <summary>
        /// 规范化文件名
        /// </summary>
        /// <param name="fileInfo">文件信息</param>
        /// <returns></returns>
        public static string Normalize(this FileInfo fileInfo) =>
            fileInfo.Name.Split('.')[0].Replace("-", "").Replace("_", "").ToLower();
    }
}
