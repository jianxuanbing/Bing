using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Configuration.Abstractions;
using Bing.Configuration.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Bing.Configuration.Core
{
    /// <summary>
    /// 配置文件读取器
    /// </summary>
    public class ConfigFileReader:IConfigFileReader
    {
        /// <summary>
        /// 解析配置文件
        /// </summary>
        /// <param name="configFile">配置文件</param>
        /// <returns></returns>
        public ConfigFileMetadata Parse(FileInfo configFile)
        {
            using (var reader=new StreamReader(configFile.FullName))
            {
                var content = reader.ReadToEnd();
                return new ConfigFileMetadata()
                {
                    ConfigName = configFile.Normalize(),
                    ConfigFile = configFile,
                    Content = content
                };
            }
        }
    }
}
