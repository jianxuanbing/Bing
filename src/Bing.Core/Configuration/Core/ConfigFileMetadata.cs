using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Configuration.Core
{
    /// <summary>
    /// 配置文件元数据
    /// </summary>
    public class ConfigFileMetadata
    {
        /// <summary>
        /// 配置文件名
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// 节点集合
        /// </summary>
        public IEnumerable<ConfigSections> Sections { get; set; }

        /// <summary>
        /// 配置文件
        /// </summary>
        public FileInfo ConfigFile { get; set; }

        /// <summary>
        /// 配置内容
        /// </summary>
        public string Content { get; set; }
    }
}
