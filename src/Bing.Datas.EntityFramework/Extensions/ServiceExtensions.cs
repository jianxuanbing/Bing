using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Bing.Datas.EntityFramework.Configs;
using Bing.Datas.EntityFramework.Core;
using Bing.Datas.UnitOfWorks;
using Bing.Dependency;

namespace Bing.Datas.EntityFramework.Extensions
{
    public static partial class ServiceExtensions
    {
        /// <summary>
        /// 注册工作单元服务
        /// </summary>
        /// <typeparam name="TService">工作单元接口类型</typeparam>
        /// <typeparam name="TImplementation">工作单元实现类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="connectionName">参数名</param>
        /// <returns></returns>
        public static void AddUnitOfWork<TService, TImplementation>(this ContainerBuilder services,
            string connectionName)
            where TService : class, IUnitOfWork
            where TImplementation : UnitOfWorkBase, TService
        {
            services.AddScoped<TService, TImplementation>().WithParameter("connection", connectionName).PropertiesAutowired();
        }
    }
}
