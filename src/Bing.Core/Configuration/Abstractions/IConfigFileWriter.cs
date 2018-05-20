using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Configuration.Abstractions
{
    /// <summary>
    /// 配置文件写入器
    /// </summary>
    public interface IConfigFileWriter
    {
        /// <summary>
        /// 写入内容
        /// </summary>
        /// <param name="configFile">配置文件</param>
        /// <param name="content">内容</param>
        void Write(FileInfo configFile, string content);
    }
}
