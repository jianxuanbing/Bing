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

        #endregion

        #region ExecuteSqlAsync(通过Sql执行数据更新操作)

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

        #endregion

        #region ExecuteProcAsync(通过存储过程执行数据更新操作)

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
            OrmConfig.AdoLogInterceptor?.Invoke("ExecuteProcAsync", procName, param);
            return await connection.ExecuteAsync(procName, param, transaction, commandTimeout, CommandType.StoredProcedure);
        }

        #endregion

        #region ProcQuery(通过存储过程执行数据查询操作，返回集合)

        /// <summary>
        /// 通过存储过程执行数据查询操作，返回集合
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="procName">存储过程名</param>
        /// <param name="param">存储过程参数，使用匿名对象传入，范例：new { Name = "A" }</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns></returns>
        public static IEnumerable<TResult> ProcQuery<TResult>(this IRepository repository, string procName,
            object param = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var connection = repository.GetDbConnection();
            OrmConfig.AdoLogInterceptor?.Invoke("ProcQuery", procName, param);
            return connection.Query<TResult>(procName, param, transaction, commandTimeout: commandTimeout,
                commandType: CommandType.StoredProcedure);
        }

        #endregion

        #region ProcQueryAsync(通过存储过程执行数据查询操作，返回集合)

        /// <summary>
        /// 通过存储过程执行数据查询操作，返回集合
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="procName">存储过程名</param>
        /// <param name="param">存储过程参数，使用匿名对象传入，范例：new { Name = "A" }</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TResult>> ProcQueryAsync<TResult>(this IRepository repository, string procName,
            object param = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var connection = repository.GetDbConnection();
            OrmConfig.AdoLogInterceptor?.Invoke("ProcQueryAsync", procName, param);
            return await connection.QueryAsync<TResult>(procName, param, transaction, commandTimeout: commandTimeout,
                commandType: CommandType.StoredProcedure);
        }

        #endregion

        #region ProcSingle(通过存储过程执行数据查询操作，返回对象)

        /// <summary>
        /// 通过存储过程执行数据查询操作，返回对象
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="procName">存储过程名</param>
        /// <param name="param">存储过程参数，使用匿名对象传入，范例：new { Name = "A" }</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns></returns>
        public static TResult ProcSingle<TResult>(this IRepository repository, string procName,
            object param = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var connection = repository.GetDbConnection();
            OrmConfig.AdoLogInterceptor?.Invoke("ProcSingle", procName, param);
            return connection.QueryFirstOrDefault<TResult>(procName, param, transaction, commandTimeout: commandTimeout,
                commandType: CommandType.StoredProcedure);
        }

        #endregion
    }
}
