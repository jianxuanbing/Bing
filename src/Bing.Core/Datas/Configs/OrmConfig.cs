using System;
using Bing.SqlBuilder.Page;

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
        public static Action<string, string, object> AdoLogInterceptor { get; set; } = null;

        /// <summary>
        /// Orm 日志级别，默认<see cref="OrmLogLevel.Off"/>
        /// </summary>
        public static OrmLogLevel LogLevel { get; set; } = OrmLogLevel.Off;

        /// <summary>
        /// 数据库类型，默认<see cref="DbType.SqlServer"/>
        /// </summary>
        public static DbType DbType { get; set; } = DbType.SqlServer;

        /// <summary>
        /// 分页生成器
        /// </summary>
        public static IPageBuilder PageBuilder { get; set; } = new SqlServerPageBuilder();
    }
}
