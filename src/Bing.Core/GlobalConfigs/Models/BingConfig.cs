using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.GlobalConfigs.Models
{
    /// <summary>
    /// 框架配置信息实体
    /// </summary>
    public class BingConfig
    {
        /// <summary>
        /// 日志 相关
        /// </summary>
        public LogConfig Logger { get; set; }

        /// <summary>
        /// 用户上下文 相关
        /// </summary>
        public UserContextConfig UserContext { get; set; }

        /// <summary>
        /// 初始化一个<see cref="BingConfig"/>类型的实例
        /// </summary>
        public BingConfig()
        {
            Logger = new LogConfig();
            UserContext = new UserContextConfig();
        }
    }
}
