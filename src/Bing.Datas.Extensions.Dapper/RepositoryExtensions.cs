using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Datas.Configs;
using Bing.Domains.Repositories;
using Dapper;

namespace Bing.Datas.Extensions.Dapper
{
    /// <summary>
    /// 仓储 扩展
    /// </summary>
    public static partial class RepositoryExtensions
    {
        #region ExecuteSql(通过Sql执行数据更新操作)

        /// <summary>
        /// 通过Sql执行数据更新操作
        /// </summary>
        /// <param name="repository">仓储</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="param">Sql参数，使用匿名对象传入，范例：new { Name = "A" }</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns>Sql影响行数</returns>
        public static int ExecuteSql(this IRepository repository, string sql, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var connection = repository.GetDbConnection();
            OrmConfig.AdoLogInterceptor?.Invoke("Execute", sql, param);
            return connection.Execute(sql, param, transaction, commandTimeout);
        }

        /// <summary>
        /// 通过Sql执行数据更新操作
        /// </summary>
        /// <param name="repository">仓储</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="param">Sql参数，使用匿名对象传入，范例：new { Name = "A" }</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns>Sql影响行数</returns>
        public static async Task<int> ExecuteSqlAsync(this IRepository repository, string sql, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var connection = repository.GetDbConnection();
            OrmConfig.AdoLogInterceptor?.Invoke("ExecuteAsync", sql, param);
            return await connection.ExecuteAsync(sql, param, transaction, commandTimeout);
        }

        #endregion

        #region ExecuteProc(通过存储过程执行数据更新操作)

        /// <summary>
        /// 通过存储过程执行数据更新操作
        /// </summary>
        /// <param name="repository">仓储</param>
        /// <param name="procName">存储过程名</param>
        /// <param name="param">存储过程参数，使用匿名对象传入，范例：new { Name = "A" }</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns>Sql影响行数</returns>
        public static int ExecuteProc(this IRepository repository, string procName, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var connection = repository.GetDbConnection();
            OrmConfig.AdoLogInterceptor?.Invoke("ExecuteProc",procName,param);
            return connection.Execute(procName, param, transaction, commandTimeout, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 通过存储过程执行数据更新操作
        /// </summary>
        /// <param name="repository">仓储</param>
        /// <param name="procName">存储过程名</param>
        /// <param name="param">存储过程参数，使用匿名对象传入，范例：new { Name = "A" }</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns>Sql影响行数</returns>
        public static async Task<int> ExecuteProcAsync(this IRepository repository, string procName, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var connection = repository.GetDbConnection();
            OrmConfig.AdoLogInterceptor?.Invoke("ExecuteProc", procName, param);
            return await connection.ExecuteAsync(procName, param, transaction, commandTimeout, CommandType.StoredProcedure);
        }

        #endregion
    }
}
