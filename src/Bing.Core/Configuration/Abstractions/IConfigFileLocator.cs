using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Configuration.Abstractions
{
    /// <summary>
    /// 配置文件定位器
    /// </summary>
    public interface IConfigFileLocator
    {
        /// <summary>
        /// 查找配置文件集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<FileInfo> FindConfigFiles();

        /// <summary>
        /// 获取已支持文件的扩展名
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetSupportedFileExtensions();
    }
}
