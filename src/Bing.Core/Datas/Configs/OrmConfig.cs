using System;

namespace Bing.Datas.Configs
{
    /// <summary>
    /// Orm配置
    /// </summary>
    public class OrmConfig
    {
        /// <summary>
        /// Ado 日志拦截器
        /// </summary>
        public static Action<string, string, object> AdoLogInterceptor = null;

        /// <summary>
        /// Orm 日志级别，默认<see cref="OrmLogLevel.Off"/>
        /// </summary>
        public static OrmLogLevel LogLevel = OrmLogLevel.Off;
    }
}
