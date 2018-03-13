using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Domains.Entities;
using Bing.Domains.Repositories;
using EntityFramework;
using EntityFramework.Extensions;
using EntityFramework.Mapping;

namespace Bing.Datas.EntityFramework.Extensions
{
    /// <summary>
    /// EF元数据扩展
    /// </summary>
    public static class MetaDataExtensions
    {
        /// <summary>
        /// 获取实体元数据
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="context">数据上下文</param>
        /// <returns></returns>
        public static EntityMap GetMetaData<TEntity>(this DbContext context) where TEntity : class
        {
            return context.Set<TEntity>().ToObjectQuery().GetEntityMap<TEntity>();
        }

        /// <summary>
        /// 获取实体元数据
        /// </summary>
        /// <param name="context">实体类型</param>
        /// <param name="type">数据上下文</param>
        /// <returns></returns>
        public static EntityMap GetMetaData(this DbContext context, Type type)
        {
            var provider = Locator.Current.Resolve<IMappingProvider>();
            return provider.GetEntityMap(type, context);
        }

        /// <summary>
        /// 获取实体元数据
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">实体标识类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <returns></returns>
        public static EntityMap GetMetaData<TEntity, TKey>(this IRepository<TEntity, TKey> repository)
            where TEntity : class, IAggregateRoot<TEntity, TKey>
        {
            var context = (DbContext)repository.GetUnitOfWork();
            return context.GetMetaData<TEntity>();
        }

        /// <summary>
        /// 获取实体元数据
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="repository">仓储</param>
        /// <returns></returns>
        public static EntityMap GetMetaData<TEntity>(this IRepository<TEntity> repository)
            where TEntity : class, IAggregateRoot<TEntity>
        {
            var context = (DbContext)repository.GetUnitOfWork();
            return context.GetMetaData<TEntity>();
        }
    }
}
