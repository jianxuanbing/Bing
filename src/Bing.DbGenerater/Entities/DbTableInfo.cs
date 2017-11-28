using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.DbGenerater.Enums;

namespace Bing.DbGenerater.Entities
{
    /// <summary>
    /// 数据表信息
    /// </summary>
    public class DbTableInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 数据库对象类型
        /// </summary>
        public DbObjectType DbObjectType { get; set; }
    }
}
