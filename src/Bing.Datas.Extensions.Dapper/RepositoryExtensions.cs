using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Applications.Dtos;
using Bing.Datas.Configs;
using Bing.Domains.Repositories;
using Bing.SqlBuilder.Conditions;
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

        #region ProcSingleAsync(通过存储过程执行数据查询操作，返回对象)

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
        public static async Task<TResult> ProcSingleAsync<TResult>(this IRepository repository, string procName,
            object param = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var connection = repository.GetDbConnection();
            OrmConfig.AdoLogInterceptor?.Invoke("ProcSingleAsync", procName, param);
            return await connection.QueryFirstOrDefaultAsync<TResult>(procName, param, transaction, commandTimeout: commandTimeout,
                commandType: CommandType.StoredProcedure);
        }

        #endregion

        #region SqlQuery(通过Sql执行数据查询操作，返回集合)

        /// <summary>
        /// 通过Sql执行数据查询操作，返回集合
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="param">Sql参数，使用匿名对象传入，范例：new { Name = "A" }</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns></returns>
        public static IEnumerable<TResult> SqlQuery<TResult>(this IRepository repository, string sql,
            object param = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var connection = repository.GetDbConnection();
            OrmConfig.AdoLogInterceptor?.Invoke("SqlQuery", sql, param);
            return connection.Query<TResult>(sql, param, transaction, commandTimeout: commandTimeout);
        }

        /// <summary>
        /// 通过Sql执行数据查询操作，返回集合
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="condition">Where条件</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns></returns>
        public static IEnumerable<TResult> SqlQuery<TResult>(this IRepository repository, string sql,
            ConditionBuilder condition, IDbTransaction transaction = null, int? commandTimeout = null)
        {            
            return repository.SqlQuery<TResult>($"{sql}{condition.ToString()}", condition.GetParamDict());
        }

        #endregion

        #region SqlQueryAsync(通过Sql执行数据查询操作，返回集合)

        /// <summary>
        /// 通过Sql执行数据查询操作，返回集合
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="param">Sql参数，使用匿名对象传入，范例：new { Name = "A" }</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TResult>> SqlQueryAsync<TResult>(this IRepository repository, string sql,
            object param = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var connection = repository.GetDbConnection();
            OrmConfig.AdoLogInterceptor?.Invoke("SqlQueryAsync", sql, param);
            return await connection.QueryAsync<TResult>(sql, param, transaction, commandTimeout: commandTimeout);
        }

        /// <summary>
        /// 通过Sql执行数据查询操作，返回集合
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="condition">Where条件</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns></returns>
        public static async Task<IEnumerable<TResult>> SqlQueryAsync<TResult>(this IRepository repository, string sql,
            ConditionBuilder condition, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await repository.SqlQueryAsync<TResult>($"{sql}{condition.ToString()}", condition.GetParamDict());
        }

        #endregion

        #region SqlSingle(通过Sql执行数据查询操作，返回对象)

        /// <summary>
        /// 通过Sql执行数据查询操作，返回对象
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="param">Sql参数，使用匿名对象传入，范例：new { Name = "A" }</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns></returns>
        public static TResult SqlSingle<TResult>(this IRepository repository, string sql,
            object param = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var connection = repository.GetDbConnection();
            OrmConfig.AdoLogInterceptor?.Invoke("SqlSingle", sql, param);
            return connection.QueryFirstOrDefault<TResult>(sql, param, transaction, commandTimeout: commandTimeout);
        }

        /// <summary>
        /// 通过Sql执行数据查询操作，返回对象
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="condition">Where条件</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns></returns>
        public static TResult SqlSingle<TResult>(this IRepository repository, string sql,
            ConditionBuilder condition, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return repository.SqlSingle<TResult>($"{sql}{condition.ToString()}", condition.GetParamDict());
        }

        #endregion

        #region SqlSingleAsync(通过Sql执行数据查询操作，返回对象)

        /// <summary>
        /// 通过Sql执行数据查询操作，返回对象
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="param">Sql参数，使用匿名对象传入，范例：new { Name = "A" }</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns></returns>
        public static async Task<TResult> SqlSingleAsync<TResult>(this IRepository repository, string sql,
            object param = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var connection = repository.GetDbConnection();
            OrmConfig.AdoLogInterceptor?.Invoke("SqlSingleAsync", sql, param);
            return await connection.QueryFirstOrDefaultAsync<TResult>(sql, param, transaction, commandTimeout: commandTimeout);
        }

        /// <summary>
        /// 通过Sql执行数据查询操作，返回对象
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="condition">Where条件</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns></returns>
        public static async Task<TResult> SqlSingleAsync<TResult>(this IRepository repository, string sql,
            ConditionBuilder condition, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await repository.SqlSingleAsync<TResult>($"{sql}{condition.ToString()}", condition.GetParamDict());
        }

        #endregion

        #region SqlPage(通过Sql执行数据分页查询操作，返回分页列表)

        /// <summary>
        /// 通过Sql执行数据分页查询操作，返回分页列表
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="condition">Where条件</param>
        /// <param name="search">查询对象</param>
        /// <param name="order">排序方式，例如：name desc, nickname desc</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns></returns>
        public static PagerList<TResult> SqlPage<TResult>(this IRepository repository, string sql,
            ConditionBuilder condition, PagedSearch search, string order, IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            return repository.SqlPage<TResult>(sql, condition.ToString(), search.Page, search.PageSize, order,
                condition.GetParamDict(), transaction, commandTimeout);
        }

        /// <summary>
        /// 通过Sql执行数据分页查询操作，返回分页列表
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="condition">Where条件</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="order">排序方式，例如：name desc, nickname desc</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns></returns>
        public static PagerList<TResult> SqlPage<TResult>(this IRepository repository, string sql,
            ConditionBuilder condition, int page, int pageSize, string order, IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            return repository.SqlPage<TResult>(sql, condition.ToString(), page, pageSize, order,
                condition.GetParamDict(), transaction, commandTimeout);
        }

        /// <summary>
        /// 通过Sql执行数据分页查询操作，返回分页列表
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="condition">Where条件</param>
        /// <param name="search">查询对象</param>
        /// <param name="order">排序方式，例如：name desc, nickname desc</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns></returns>
        public static PagerList<TResult> SqlPage<TResult>(this IRepository repository, string sql, string condition,
            PagedSearch search, string order, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return repository.SqlPage<TResult>(sql, condition, search.Page, search.PageSize, order,
                transaction: transaction,
                commandTimeout: commandTimeout);
        }

        /// <summary>
        /// 通过Sql执行数据分页查询操作，返回分页列表
        /// </summary>
        /// <typeparam name="TResult">对象类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="where">Where条件</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="order">排序方式，例如：name desc, nickname desc</param>
        /// <param name="param">参数字典</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">命令超时时间</param>
        /// <returns></returns>
        public static PagerList<TResult> SqlPage<TResult>(this IRepository repository, string sql, string where,
            int page, int pageSize, string order, IDictionary<string, object> param = null,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var connection = repository.GetDbConnection();

            var whereSql = string.Empty;
            if (string.IsNullOrWhiteSpace(where.Trim()) ||
                !where.TrimStart().StartsWith("where", StringComparison.CurrentCultureIgnoreCase))
            {
                whereSql += " where ";
            }

            whereSql += where;
            var pageSql = OrmConfig.PageBuilder.GeneratePagingWithRowNumberSql(sql, whereSql, page, pageSize, order);
            var countSql = OrmConfig.PageBuilder.GenerateRecordCount(sql, whereSql);

            PagerList<TResult> result = new PagerList<TResult>
            {
                Page = page,
                PageSize = pageSize
            };

            OrmConfig.AdoLogInterceptor?.Invoke("SqlPage.Count", countSql, param);
            OrmConfig.AdoLogInterceptor?.Invoke("SqlPage.Data", pageSql, param);

            if (param == null || param.Count == 0)
            {
                result.Data = connection.Query<TResult>(pageSql).ToList();
                result.TotalCount = connection.QuerySingle<int>(countSql);
            }
            else
            {
                result.Data = connection.Query<TResult>(pageSql, param).ToList();
                result.TotalCount = connection.QuerySingle<int>(countSql, param);
            }

            IPager pager = new Pager(page, pageSize, result.TotalCount);
            result.PageCount = pager.GetPageCount();
            return result;
        }

        #endregion
    }
}
