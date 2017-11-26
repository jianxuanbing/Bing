using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Datas.UnitOfWorks;

namespace Bing.Samples.Datas
{
    public class BingSampleUnitOfWork:Bing.Datas.EntityFramework.SqlServer.UnitOfWork,IBingSampleUnitOfWork
    {
        /// <summary>
        /// 初始化威海项目工作单元
        /// </summary>
        static BingSampleUnitOfWork()
        {
            System.Data.Entity.Database.SetInitializer<BingSampleUnitOfWork>(null);
        }
        public BingSampleUnitOfWork(string connection, IUnitOfWorkManager manager) : base(connection, manager)
        {
        }
    }
}
