using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.DbGenerater.Entities;

namespace Bing.DbGenerater.Interface
{
    /// <summary>
    /// 数据库维护中心
    /// </summary>
    public interface IDbMaintenance
    {
        /// <summary>
        /// 获取视图信息列表
        /// </summary>
        /// <returns></returns>
        List<DbTableInfo> GetViewInfoList();

        /// <summary>
        /// 获取数据表信息列表
        /// </summary>
        /// <returns></returns>
        List<DbTableInfo> GetTableInfoList();

        /// <summary>
        /// 根据表名获取列信息
        /// </summary>
        /// <param name="tableName">表名，视图名或数据表名</param>
        /// <returns></returns>
        List<DbColumnInfo> GetColumnInfosByTableName(string tableName);
    }
}
