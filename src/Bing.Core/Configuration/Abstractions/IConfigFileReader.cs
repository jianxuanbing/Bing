using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Configuration.Core;

namespace Bing.Configuration.Abstractions
{
    /// <summary>
    /// 配置文件读取器
    /// </summary>
    public interface IConfigFileReader
    {
        /// <summary>
        /// 解析配置文件
        /// </summary>
        /// <param name="configFile">配置文件</param>
        /// <returns></returns>
        ConfigFileMetadata Parse(FileInfo configFile);
    }
}
