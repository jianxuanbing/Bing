using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.DbGenerater.Abstract;

namespace Bing.DbGenerater.Realization.SqlServer.DbMaintenance
{
    /// <summary>
    /// SqlServer 数据库维护中心
    /// </summary>
    public class SqlServerDbMaintenance:DbMaintenanceProvider
    {
        /// <summary>
        /// 根据数据表名获取列信息 Sql语句
        /// </summary>
        protected override string GetColumnInfosByTableNameSql
        {
            get
            {
                string sql = @"SELECT sysobjects.name AS TableName,
                           syscolumns.Id AS TableId,
                           syscolumns.name AS DbColumnName,
                           systypes.name AS DataType,
                           syscolumns.length AS [Length],
                           sys.extended_properties.[value] AS [ColumnDescription],
                           syscomments.text AS DefaultValue,
                           syscolumns.isnullable AS IsNullable,
	                       columnproperty(syscolumns.id,syscolumns.name,'IsIdentity')as IsIdentity,
                           (CASE
                                WHEN EXISTS
                                       ( 
                                             	select 1
												from sysindexes i
												join sysindexkeys k on i.id = k.id and i.indid = k.indid
												join sysobjects o on i.id = o.id
												join syscolumns c on i.id=c.id and k.colid = c.colid
												where o.xtype = 'U' 
												and exists(select 1 from sysobjects where xtype = 'PK' and name = i.name) 
												and o.name=sysobjects.name and c.name=syscolumns.name
                                       ) THEN 1
                                ELSE 0
                            END) AS IsPrimaryKey,
							ISNULL(syscolumns.prec,0) as Scale,
							ISNULL(syscolumns.scale,0) as DecimalDigits
                    FROM syscolumns
                    INNER JOIN systypes ON syscolumns.xtype = systypes.xtype
                    LEFT JOIN sysobjects ON syscolumns.id = sysobjects.id
                    LEFT OUTER JOIN sys.extended_properties ON (sys.extended_properties.minor_id = syscolumns.colid
                                                                AND sys.extended_properties.major_id = syscolumns.id)
                    LEFT OUTER JOIN syscomments ON syscolumns.cdefault = syscomments.id
                    WHERE syscolumns.id IN
                        (SELECT id
                         FROM sysobjects
                         WHERE xtype IN('u',
                                        'v') )
                      AND (systypes.name <> 'sysname')
                      AND sysobjects.name='{0}'
                      AND systypes.name<>'geometry'
                      AND systypes.name<>'geography'
                    ORDER BY syscolumns.colid";
                return sql;
            }
        }

        /// <summary>
        /// 获取数据表信息列表 Sql语句
        /// </summary>
        protected override string GetTableInfoListSql
        {
            get
            {
                string sql = @"SELECT s.Name,Convert(varchar(max),tbp.value) as Description
                            FROM sysobjects s
					     	LEFT JOIN sys.extended_properties as tbp ON s.id=tbp.major_id and tbp.minor_id=0 AND (tbp.Name='MS_Description' OR tbp.Name is null)  WHERE s.xtype IN('U') ";
                return sql;
            }
        }

        /// <summary>
        /// 获取视图信息列表 Sql语句
        /// </summary>
        protected override string GetViewInfoListSql
        {
            get
            {
                string sql = @"SELECT s.Name,Convert(varchar(max),tbp.value) as Description
                            FROM sysobjects s
					     	LEFT JOIN sys.extended_properties as tbp ON s.id=tbp.major_id and tbp.minor_id=0  AND (tbp.Name='MS_Description' OR tbp.Name is null) WHERE s.xtype IN('V') ";
                return sql;
            }
        }
    }
}
