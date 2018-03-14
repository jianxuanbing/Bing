using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Datas.EntityFramework.SqlServer.Bulks;
using Bing.Domains.Entities;
using Bing.Domains.Repositories;

namespace Bing.Datas.EntityFramework.SqlServer.Extensions
{
    /// <summary>
    /// 仓储扩展
    /// </summary>
    public static partial class RepositoryExtensions
    {
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">实体标识类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <param name="entities">实体集合</param>
        /// <param name="batchSize">批大小</param>
        /// <param name="options">批量选项</param>
        /// <returns></returns>
        public static int BulkInsert<TEntity, TKey>(this IRepository<TEntity, TKey> repository,
            IEnumerable<TEntity> entities, int batchSize = 1000,
            SqlBulkCopyOptions options = SqlBulkCopyOptions.Default)
            where TEntity : class, IAggregateRoot<TEntity, TKey>
        {
            return new BulkInsert<TEntity>((DbContext) repository.GetUnitOfWork(), entities).Insert(batchSize, options);
        }
    }
}
