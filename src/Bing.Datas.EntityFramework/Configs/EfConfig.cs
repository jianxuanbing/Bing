using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Datas.EntityFramework.Configs
{
    /// <summary>
    /// EF配置
    /// </summary>
    public class EfConfig
    {
        /// <summary>
        /// EF日志级别，默认<see cref="EfLogLevel.Sql"/>
        /// </summary>
        public static EfLogLevel LogLevel = EfLogLevel.Sql;

        /// <summary>
        /// 自动提交，默认禁用
        /// </summary>
        public static bool AutoCommit = false;

        /// <summary>
        /// 是否启用验证版本号，默认启用
        /// </summary>
        public static bool EnabledValidateVersion = true;
    }
}
