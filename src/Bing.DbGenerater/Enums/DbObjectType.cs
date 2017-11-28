using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bing.DbGenerater.Enums
{
    /// <summary>
    /// 数据库对象类型
    /// </summary>
    public enum DbObjectType
    {
        /// <summary>
        /// 数据表
        /// </summary>
        Table=0,
        /// <summary>
        /// 视图
        /// </summary>
        View=1,
        /// <summary>
        /// 全部
        /// </summary>
        All=2
    }
}
