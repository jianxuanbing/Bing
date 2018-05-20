using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Configuration.Abstractions;

namespace Bing.Configuration.Core
{
    /// <summary>
    /// 配置文件写入器
    /// </summary>
    public class ConfigFileWriter:IConfigFileWriter
    {
        /// <summary>
        /// 写入内容
        /// </summary>
        /// <param name="configFile">配置文件</param>
        /// <param name="content">内容</param>
        public void Write(FileInfo configFile, string content)
        {
            using (var writer=new StreamWriter(configFile.FullName))
            {
                writer.Write(content);
            }
        }
    }
}
