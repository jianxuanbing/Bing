using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bing.Contexts;
using Bing.Datas.EntityFramework.Configs;
using Bing.Datas.EntityFramework.Logs;
using Bing.Datas.UnitOfWorks;
using Bing.Domains.Entities;
using Bing.Domains.Entities.Auditing;
using Bing.Exceptions;
using Bing.Logs;

namespace Bing.Datas.EntityFramework.Core
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public abstract class UnitOfWorkBase : DbContext, IUnitOfWork
    {
        #region 属性

        /// <summary>
        /// 跟踪号
        /// </summary>
        public string TraceId { get; set; }

        /// <summary>
        /// 用户上下文
        /// </summary>
        public IUserContext UserContext { get; set; }

        /// <summary>
        /// EF日志操作
        /// </summary>
        private EfLog _log;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化一个<see cref="UnitOfWorkBase"/>类型的实例
        /// </summary>
        /// <param name="connectionName">数据库连接字符串的名称</param>
        /// <param name="manager">工作单元管理器</param>
        protected UnitOfWorkBase(string connectionName, IUnitOfWorkManager manager) : base(connectionName)
        {
            manager?.Register(this);
            TraceId = Guid.NewGuid().ToString();
            UserContext = Bing.Contexts.UserContext.Null;
            EnableLog();
        }        

        #endregion

        #region OnConfiguring(配置)

        /// <summary>
        /// 启用日志
        /// </summary>
        protected void EnableLog()
        {
            if (!IsEnabled())
            {
                return;
            }
            Database.Log = log =>
            {
                GetLog().Write(log);
            };
        }

        /// <summary>
        /// 是否启用EF日志
        /// </summary>
        /// <returns></returns>
        private bool IsEnabled()
        {
            return EfConfig.LogLevel != EfLogLevel.Off;
        }

        /// <summary>
        /// 获取日志记录器
        /// </summary>
        /// <returns></returns>
        protected virtual EfLog GetLog()
        {
            return _log ?? (_log = new EfLog(TraceId, Log.GetLog("SqlTraceLog")));
        }

        #endregion

        #region Commit(提交)
        /// <summary>
        /// 提交，返回影响的行数
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            try
            {
                return SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException(ex);
            }
        }

        #endregion

        #region CommitAsync(异步提交)
        /// <summary>
        /// 异步提交，返回影响的行数
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            try
            {
                return await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException(ex);
            }
        }

        #endregion

        #region OnModelCreating(配置映射)
        /// <summary>
        /// 配置映射
        /// </summary>
        /// <param name="modelBuilder">映射生成器</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            foreach (var mapper in GetMaps())
            {
                mapper.Map(modelBuilder);
            }
        }

        /// <summary>
        /// 获取映射配置列表
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IMap> GetMaps()
        {
            var result = new List<IMap>();
            foreach (var assembly in GetAssemblies())
            {
                result.AddRange(GetMapTypes(assembly));
            }
            return result;
        }

        /// <summary>
        /// 获取定义映射配置的程序集列表
        /// </summary>
        /// <returns></returns>
        protected virtual Assembly[] GetAssemblies()
        {
            return new[] { GetType().GetTypeInfo().Assembly };
        }

        /// <summary>
        /// 获取映射类型列表
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns></returns>
        protected virtual IEnumerable<IMap> GetMapTypes(Assembly assembly)
        {
            return Bing.Utils.Helpers.Reflection.GetTypesByInterface<IMap>(assembly);
        }

        #endregion

        #region SaveChanges(保存更改)

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            SaveChangesBefore();
            return base.SaveChanges();
        }

        /// <summary>
        /// 保存更改前操作
        /// </summary>
        protected virtual void SaveChangesBefore()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        InterceptAddedOperation(entry);
                        break;
                    case EntityState.Modified:
                        InterceptModifiedOperation(entry);
                        break;
                    case EntityState.Deleted:
                        InterceptDeletedOperation(entry);
                        break;
                }
            }
        }

        /// <summary>
        /// 拦截添加操作
        /// </summary>
        /// <param name="entry">输入实体</param>
        protected virtual void InterceptAddedOperation(DbEntityEntry entry)
        {
            InitCreationAudited(entry);
            InitModificationAudited(entry);
        }

        /// <summary>
        /// 初始化创建审计信息
        /// </summary>
        /// <param name="entry">输入实体</param>
        private void InitCreationAudited(DbEntityEntry entry)
        {
            CreationAuditedInitializer.Init(entry.Entity, GetUserContext());
        }

        /// <summary>
        /// 获取用户上下文
        /// </summary>
        /// <returns></returns>
        protected virtual IUserContext GetUserContext()
        {
            return UserContext;
        }

        /// <summary>
        /// 初始化修改审计信息
        /// </summary>
        /// <param name="entry">输入实体</param>
        private void InitModificationAudited(DbEntityEntry entry)
        {
            ModificationAuditedInitializer.Init(entry.Entity, GetUserContext());
        }

        /// <summary>
        /// 拦截修改操作
        /// </summary>
        /// <param name="entry">输入实体</param>
        protected virtual void InterceptModifiedOperation(DbEntityEntry entry)
        {
            InitModificationAudited(entry);
        }

        /// <summary>
        /// 拦截删除操作
        /// </summary>
        /// <param name="entry">输入实体</param>
        protected virtual void InterceptDeletedOperation(DbEntityEntry entry)
        {
        }

        #endregion

        #region SaveChangesAsync(异步保存更改)
        /// <summary>
        /// 异步保存更改
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            SaveChangesBefore();
            return base.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region InitVersion(初始化版本号)

        /// <summary>
        /// 初始化版本号
        /// </summary>
        /// <param name="entry">输入实体</param>
        protected void InitVersion(DbEntityEntry entry)
        {
            var entity = entry.Entity as IAggregateRoot;
            if (entity==null)
            {
                return;
            }
            entity.Version = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
        }

        #endregion
    }
}
