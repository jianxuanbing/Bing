using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Domains.Repositories;

namespace Bing.Datas.EntityFramework.Extensions
{
    /// <summary>
    /// <see cref="IQueryable{T}"/> 扩展
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// 转换为分页列表
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pager">分页对象</param>
        /// <returns></returns>
        public static async Task<PagerList<TEntity>> ToPagerListAsync<TEntity>(this IQueryable<TEntity> source,
            IPager pager)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (pager == null)
            {
                throw new ArgumentNullException(nameof(pager));
            }
            var result = new PagerList<TEntity>(pager);
            result.AddRange(await source.ToListAsync());
            return result;
        }
    }
}
