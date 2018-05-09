using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.DbGenerater.Abstract;

namespace Bing.DbGenerater.Realization.MySql.DbMaintenance
{
    /// <summary>
    /// MySql 数据库维护中心
    /// </summary>
    public class MySqlDbMaintenance:DbMaintenanceProvider
    {
        /// <summary>
        /// 获取视图信息列表 Sql语句
        /// </summary>
        protected override string GetViewInfoListSql =>
            @"select TABLE_NAME as Name,TABLE_COMMENT as Description from information_schema.tables
                         where  TABLE_SCHEMA=(select database()) AND TABLE_TYPE='VIEW'";

        /// <summary>
        /// 获取数据表信息列表 Sql语句
        /// </summary>
        protected override string GetTableInfoListSql => @"select TABLE_NAME as Name,TABLE_COMMENT as Description from information_schema.tables
                         where  TABLE_SCHEMA=(select database())  AND TABLE_TYPE='BASE TABLE'";

        /// <summary>
        /// 根据数据库表名获取列信息 Sql语句
        /// </summary>
        protected override string GetColumnInfosByTableNameSql
        {
            get
            {
                string sql = @"SELECT
                                    0 as TableId,
                                    TABLE_NAME as TableName, 
                                    column_name AS DbColumnName,
                                    CASE WHEN  left(COLUMN_TYPE,LOCATE('(',COLUMN_TYPE)-1)='' THEN COLUMN_TYPE ELSE  left(COLUMN_TYPE,LOCATE('(',COLUMN_TYPE)-1) END   AS DataType,
                                    CAST(SUBSTRING(COLUMN_TYPE,LOCATE('(',COLUMN_TYPE)+1,LOCATE(')',COLUMN_TYPE)-LOCATE('(',COLUMN_TYPE)-1) AS signed) AS Length,
                                    column_default  AS  `DefaultValue`,
                                    column_comment  AS  `ColumnDescription`,
                                    CASE WHEN COLUMN_KEY = 'PRI'
                                    THEN true ELSE false END AS `IsPrimaryKey`,
                                    CASE WHEN EXTRA='auto_increment' THEN true ELSE false END as IsIdentity,
                                    CASE WHEN is_nullable = 'YES'
                                    THEN true ELSE false END AS `IsNullable`
                                    FROM
                                    Information_schema.columns where TABLE_NAME='{0}' and  TABLE_SCHEMA=(select database()) ORDER BY TABLE_NAME";
                return sql;
            }
        }
    }
}
