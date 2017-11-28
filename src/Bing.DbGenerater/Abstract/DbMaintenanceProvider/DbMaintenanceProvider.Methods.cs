using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.DbGenerater.Entities;
using Bing.DbGenerater.Enums;
using Bing.DbGenerater.Interface;
using Dapper;

namespace Bing.DbGenerater.Abstract
{
    public abstract partial class DbMaintenanceProvider:IDbMaintenance
    {
        /// <summary>
        /// 获取视图信息列表
        /// </summary>
        /// <returns></returns>
        public List<DbTableInfo> GetViewInfoList()
        {
            var result = GetList<DbTableInfo>(this.GetViewInfoListSql);
            foreach (var item in result)
            {
                item.DbObjectType = DbObjectType.View;
            }
            return result;
        }

        /// <summary>
        /// 获取数据表信息列表
        /// </summary>
        /// <returns></returns>
        public List<DbTableInfo> GetTableInfoList()
        {
            var result = GetList<DbTableInfo>(this.GetTableInfoListSql);
            foreach (var item in result)
            {
                item.DbObjectType = DbObjectType.Table;
            }
            return result;
        }

        /// <summary>
        /// 根据表名获取列信息
        /// </summary>
        /// <param name="tableName">表名，视图名或数据表名</param>
        /// <returns></returns>
        public List<DbColumnInfo> GetColumnInfosByTableName(string tableName)
        {
            return GetList<DbColumnInfo>(string.Format(this.GetColumnInfosByTableNameSql, tableName));
        }

        /// <summary>
        /// 获取执行结果
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        private List<T> GetList<T>(string sql)
        {
            var result = this.Context.Query<T>(sql).ToList();
            return result;
        }
    }
}
