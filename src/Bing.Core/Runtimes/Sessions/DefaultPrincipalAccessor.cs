using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bing.Dependency;

namespace Bing.Runtimes.Sessions
{
    /// <summary>
    /// 默认标识访问器
    /// </summary>
    public class DefaultPrincipalAccessor:IPrincipalAccessor,ISingletonDependency
    {
        /// <summary>
        /// 当前标识
        /// </summary>
        public virtual ClaimsPrincipal Principal =>Thread.CurrentPrincipal as ClaimsPrincipal;

        /// <summary>
        /// 实例
        /// </summary>
        public static DefaultPrincipalAccessor Instance => new DefaultPrincipalAccessor();
    }
}
