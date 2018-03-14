using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Bing.Datas.EntityFramework.Extensions;
using EntityFramework.Mapping;

namespace Bing.Datas.EntityFramework.SqlServer.Bulks
{
    /// <summary>
    /// 批量插入
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class BulkInsert<TEntity> where TEntity:class
    {
        /// <summary>
        /// 数据上下文
        /// </summary>
        protected DbContext Context { get; set; }

        /// <summary>
        /// 实体集
        /// </summary>
        protected IEnumerable<TEntity> Entities { get; set; }

        /// <summary>
        /// 实体映射
        /// </summary>
        protected EntityMap Map { get; set; }

        /// <summary>
        /// 初始化一个<see cref="BulkInsert{TEntity}"/>类型的实例
        /// </summary>
        /// <param name="context">数据上下文</param>
        /// <param name="entities">实体集</param>
        public BulkInsert(DbContext context, IEnumerable<TEntity> entities)
        {
            Context = context;
            Entities = entities;
            Map = Context.GetMetaData<TEntity>();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="batchSize">批大小</param>
        /// <param name="options">批量选项</param>
        /// <returns></returns>
        public int Insert(int batchSize = 1000, SqlBulkCopyOptions options = SqlBulkCopyOptions.Default)
        {
            var dt = Bing.Datas.EntityFramework.Helper.ToDataTable(Entities.ToList(), Map);
            return SqlHelper.BulkCopy((SqlConnection) Context.Database.Connection, Map.TableName, dt, batchSize,
                options);
        }
    }
}
