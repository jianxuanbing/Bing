using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Datas.EntityFramework.Core;
using Bing.Datas.UnitOfWorks;
using Bing.Domains.Entities;

namespace Bing.Samples.Datas
{
    /// <summary>
    /// 应用仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ApplicationRepositoryBase<TEntity>:RepositoryBase<TEntity> where TEntity:class ,IAggregateRoot<TEntity,Guid>
    {
        public ApplicationRepositoryBase(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Context = (BingSampleUnitOfWork) unitOfWork;
        }

        protected BingSampleUnitOfWork Context { get; set; }
    }
}
