using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using Bing.Aspects.Base;
using Bing.Aspects.Configs;
using Bing.Datas.UnitOfWorks;
using Bing.Dependency;
using Bing.Logs;
using Bing.Logs.Extensions;

namespace Bing.Aspects
{
    /// <summary>
    /// 事务 拦截器
    /// </summary>
    public class UnitOfWorkAttribute: InterceptorBase,IScopeDependency
    {
        /// <summary>
        /// 超时时间
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// 事务范围
        /// </summary>
        public TransactionScopeOption ScopeOption { get; set; }

        /// <summary>
        /// 事务隔离级别
        /// </summary>
        public IsolationLevel IsolationLevel { get; set; }       
       
        /// <summary>
        /// 初始化一个<see cref="UnitOfWorkAttribute"/>类型的实例
        /// </summary>
        public UnitOfWorkAttribute()
        {
            Timeout = 60;
            ScopeOption = TransactionScopeOption.Required;
            IsolationLevel = AspectConfig.IsolationLevel;            
        }

        /// <summary>
        /// 执行
        /// </summary>
        public override async Task Invoke(AspectCore.DynamicProxy.AspectContext context, AspectCore.DynamicProxy.AspectDelegate next)
        {
            //TransactionOptions transactionOptions=new TransactionOptions();
            //// 设置事务隔离级别
            //transactionOptions.IsolationLevel = IsolationLevel;
            //// 设置事务超时时间为60秒
            //transactionOptions.Timeout = new TimeSpan(0, 0, Timeout);
            //using (TransactionScope scope=new TransactionScope(ScopeOption,transactionOptions))
            //{
            //    try
            //    {
            //        // 实现事务性工作
            //        await next(context);                    
            //        scope.Complete();
            //    }
            //    catch (Exception e)
            //    {
            //        throw e;
            //    }
            //}

            await next(context);
            var manager = context.ServiceProvider.GetService(typeof(IUnitOfWorkManager)) as IUnitOfWorkManager;
            manager?.Commit();
        }
    }
}
