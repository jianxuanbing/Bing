using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.SqlBuilder.Page
{
    /// <summary>
    /// 分页Sql语句生成器基类
    /// </summary>
    public abstract class PageBuilderBase:IPageBuilder
    {
        /// <summary>
        /// 生成查询总记录数Sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="where">where条件语句</param>
        /// <returns></returns>
        public virtual string GenerateRecordCount(string sql, string @where)
        {
            return $"select COUNT(1) from ({sql} {where}) CountTable";
        }

        /// <summary>
        /// 获取分页查询Sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="where">where 条件语句</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="order">排序方式</param>
        /// <returns></returns>
        public abstract string GeneratePagingWithRowNumberSql(string sql, string @where, int page, int pageSize, string order);

        /// <summary>
        /// 从sql语句中获取分页语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        protected string FetchPageBody(string sql)
        {
            int selectIndex = sql.IndexOf("select", StringComparison.InvariantCultureIgnoreCase) + 6;
            string body = sql.Substring(selectIndex, sql.Length - selectIndex);
            return body;
        }
    }
}
