using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bing.Aspects.Configs
{
    /// <summary>
    /// Aop配置
    /// </summary>
    public class AspectConfig
    {
        /// <summary>
        /// 事务隔离级别，默认 <see cref="System.Transactions.IsolationLevel.ReadCommitted"/>
        /// </summary>
        public static IsolationLevel IsolationLevel = IsolationLevel.ReadCommitted;
    }
}
