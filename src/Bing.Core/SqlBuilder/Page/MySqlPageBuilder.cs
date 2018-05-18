using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.SqlBuilder.Page
{
    /// <summary>
    /// MySql 分页Sql语句生成器
    /// </summary>
    public class MySqlPageBuilder:PageBuilderBase,IPageBuilder
    {

        /// <summary>
        /// 获取分页查询Sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="where">where 条件语句</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="order">排序方式</param>
        /// <returns></returns>
        public override string GeneratePagingWithRowNumberSql(string sql, string @where, int page, int pageSize, string order)
        {
            page = page < 1 ? 1 : page;
            int pageStart = pageSize * (page - 1) + 1;
            return $"select {FetchPageBody(sql)} {where} order by {order} limit {pageStart},{pageSize}";
        }
    }
}
