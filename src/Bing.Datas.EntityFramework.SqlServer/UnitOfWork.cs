using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bing.Datas.EntityFramework.Core;
using Bing.Datas.UnitOfWorks;

namespace Bing.Datas.EntityFramework.SqlServer
{
    /// <summary>
    /// SqlServer 工作单元
    /// </summary>
    public abstract class UnitOfWork : UnitOfWorkBase
    {
        /// <summary>
        /// 初始化一个<see cref="UnitOfWork"/>类型的实例
        /// </summary>
        /// <param name="connection">连接字符串</param>
        /// <param name="manager">工作单元管理器</param>
        protected UnitOfWork(string connection, IUnitOfWorkManager manager) : base(
            connection, manager)
        {
        }

        /// <summary>
        /// 获取映射类型列表
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns></returns>
        protected override IEnumerable<Bing.Datas.EntityFramework.Core.IMap> GetMapTypes(Assembly assembly)
        {
            return Utils.Helpers.Reflection.GetTypesByInterface<IMap>(assembly);
        }
    }
}
