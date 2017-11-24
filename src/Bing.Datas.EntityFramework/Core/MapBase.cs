using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Datas.EntityFramework.Core
{
    /// <summary>
    /// 映射配置
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class MapBase<TEntity> : IMap where TEntity : class
    {
        /// <summary>
        /// 模型生成器
        /// </summary>
        protected DbModelBuilder ModelBuilder { get; private set; }

        /// <summary>
        /// 映射配置
        /// </summary>
        /// <param name="modelBuilder">模型生成器</param>
        public void Map(DbModelBuilder modelBuilder)
        {
            ModelBuilder = modelBuilder;
            var builder = modelBuilder.Entity<TEntity>();
            MapTable(builder);
            MapVersion(builder);
            MapProperties(builder);
            MapAssociations(builder);
        }

        /// <summary>
        /// 映射表
        /// </summary>
        /// <param name="builder">实体类型生成器</param>
        protected abstract void MapTable(EntityTypeConfiguration<TEntity> builder);

        /// <summary>
        /// 映射乐观离线锁
        /// </summary>
        /// <param name="builder">实体类型生成器</param>
        protected virtual void MapVersion(EntityTypeConfiguration<TEntity> builder)
        {
        }

        /// <summary>
        /// 映射属性
        /// </summary>
        /// <param name="builder">实体类型生成器</param>
        protected virtual void MapProperties(EntityTypeConfiguration<TEntity> builder)
        {
        }

        /// <summary>
        /// 映射导航属性
        /// </summary>
        /// <param name="builder">实体类型生成器</param>
        protected virtual void MapAssociations(EntityTypeConfiguration<TEntity> builder)
        {
        }
    }
}
