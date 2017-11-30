using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Domains.Entities;
using Bing.Domains.Repositories;

namespace Bing.Datas.EntityFramework.Extensions
{
    /// <summary>
    /// 仓储扩展
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// 从数据上下文中 删除实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">实体标识类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="entity">实体</param>
        public static void DetachFromDbContext<TEntity, TKey>(this IRepository<TEntity, TKey> repository,
            TEntity entity) where TEntity : class, IAggregateRoot<TKey>
        {
            var context = (DbContext)repository.GetUnitOfWork();
            context.Entry(entity).State = EntityState.Deleted;
        }
    }
}
