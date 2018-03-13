using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Datas.EntityFramework.SqlServer
{
    /// <summary>
    /// Sql操作
    /// </summary>
    public class SqlHelper
    {
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="destTableName">目标表名</param>
        /// <param name="source">数据源</param>
        /// <param name="batchSize">批大小</param>
        /// <param name="options">批量选项</param>
        /// <returns></returns>
        public static int BulkCopy(SqlConnection connection, string destTableName, DataTable source,
            int batchSize = 1000, SqlBulkCopyOptions options = SqlBulkCopyOptions.Default)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                var transaction = connection.BeginTransaction();
                try
                {
                    BulkCopy(connection, transaction, destTableName, source, batchSize, options);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            finally
            {
                connection.Close();
            }
            return source.Rows.Count;
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">事务</param>
        /// <param name="destTableName">目标表名</param>
        /// <param name="source">数据源</param>
        /// <param name="batchSize">批大小</param>
        /// <param name="options">批量选项</param>
        private static void BulkCopy(SqlConnection connection, SqlTransaction transaction, string destTableName,
            DataTable source, int batchSize, SqlBulkCopyOptions options)
        {
            using (var sqlBulkCopy=new SqlBulkCopy(connection,options,transaction){BulkCopyTimeout = 600,NotifyAfter = source.Rows.Count,BatchSize = batchSize,DestinationTableName = destTableName})
            {
                foreach (DataColumn column in source.Columns)
                {
                    sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }
                sqlBulkCopy.WriteToServer(source);
                transaction.Commit();
            }
        }
    }
}
