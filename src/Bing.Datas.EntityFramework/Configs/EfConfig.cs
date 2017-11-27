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
        /// EF日志级别
        /// </summary>
        public static EfLogLevel LogLevel = EfLogLevel.Sql;

        /// <summary>
        /// 自动提交
        /// </summary>
        public static bool AutoCommit = false;
    }
}
