using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Runtimes.Sessions
{
    /// <summary>
    /// 标识访问器
    /// </summary>
    public interface IPrincipalAccessor
    {
        /// <summary>
        /// 当前标识
        /// </summary>
        ClaimsPrincipal Principal { get; }
    }
}
