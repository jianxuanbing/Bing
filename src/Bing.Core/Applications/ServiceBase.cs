using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Domains.Entities;

namespace Bing.Applications
{
    /// <summary>
    /// 应用服务基类
    /// </summary>
    public abstract class ServiceBase:IServiceBase
    {
    }

    /// <summary>
    /// 应用服务基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体类型标识</typeparam>
    public abstract class ServiceBase<TEntity, TKey> : ServiceBase, IServiceBase<TEntity>
        where TEntity : class, IEntity<TEntity, TKey>, new()
    {
    }
}
