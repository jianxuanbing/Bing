using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Domains.Entities;

namespace Bing.Datas.UnitOfWorks
{
    /// <summary>
    /// 标准过滤器
    /// </summary>
    public static class DataFilters
    {
        /// <summary>
        /// "IsDeleted"。
        /// 软删除过滤。
        /// 防止从数据库中获取已删除的数据。
        /// 查看<see cref="ISoftDelete"/>接口
        /// </summary>
        public const string SoftDelete = "IsDeleted";
    }
}
