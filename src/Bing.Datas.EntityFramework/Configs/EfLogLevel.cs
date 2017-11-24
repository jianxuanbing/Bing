using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Datas.EntityFramework.Configs
{
    /// <summary>
    /// Ef日志级别
    /// </summary>
    public enum EfLogLevel
    {
        /// <summary>
        /// 输出全部日志，包括连接数据库，提交事务等大量信息
        /// </summary>
        All,
        /// <summary>
        /// 仅输出Sql
        /// </summary>
        Sql,
        /// <summary>
        /// 关闭日志
        /// </summary>
        Off
    }
}
