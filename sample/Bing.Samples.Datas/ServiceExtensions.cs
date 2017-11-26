using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Bing.Datas.EntityFramework;
using Bing.Datas.UnitOfWorks;
using Bing.Dependency;

namespace Bing.Samples.Datas
{
    /// <summary>
    /// 工作单元 扩展
    /// </summary>
    public static partial class ServiceExtensions
    {
        public static void AddBingUnitOfWork(this ContainerBuilder services, string connectionName)
        {
            services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
            services.AddUnitOfWork<IUnitOfWork,BingSampleUnitOfWork>(connectionName);
        }
    }
}
