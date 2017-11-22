using System;
using System.Collections.Generic;
using System.Text;

namespace Bing.Utils.Extensions
{
    /// <summary>
    /// 系统扩展 - 字符串
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 返回与 Web 服务器上的指定虚拟路径相对应的物理文件路径。
        /// </summary>
        /// <param name="basePath">基础路径</param>
        /// <param name="path">相对路径</param>
        /// <returns></returns>
        public static string MapPath(this string basePath, string path)
        {
            return System.IO.Path.Combine(basePath, path);
        }
    }
}
