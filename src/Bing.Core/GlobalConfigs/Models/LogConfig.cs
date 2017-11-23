using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.GlobalConfigs.Models
{
    /// <summary>
    /// 日志 配置
    /// </summary>
    public class LogConfig
    {
        /// <summary>
        /// 日志实现方式：File,Log4net,NLog,Exceptionless,MongoDB
        /// </summary>
        [DisplayName("日志实现方式：File,Log4net,NLog,Exceptionless,MongoDB")]
        public string Type { get; set; }

        /// <summary>
        /// 日志级别：DEBUG|INFO|WARN|ERROR|FATAL|OFF
        /// </summary>
        [DisplayName("日志级别：DEBUG|INFO|WARN|ERROR|FATAL|OFF")]
        public string Level { get; set; }

        /// <summary>
        /// 日志记录的项目名称
        /// </summary>
        [DisplayName("日志记录的项目名称")]
        public string ProjectName { get; set; }

        /// <summary>
        /// 是否启用调试
        /// </summary>
        [DisplayName("是否启用调试")]
        public bool EnabledDebug { get; set; }

        /// <summary>
        /// 是否启用跟踪
        /// </summary>
        [DisplayName("是否启用跟踪")]
        public bool EnabledTrace { get; set; }
    }
}
