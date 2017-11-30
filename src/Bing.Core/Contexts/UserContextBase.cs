using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Runtimes;
using Bing.Runtimes.Sessions;

namespace Bing.Contexts
{
    /// <summary>
    /// 用户上下文基类
    /// </summary>
    public abstract class UserContextBase:IUserContext
    {
        /// <summary>
        /// Session 覆盖上下文密钥
        /// </summary>
        public const string SessionOverrideContextKey = "Bing.Runtimes.Sessions.Override";

        /// <summary>
        /// 用户ID
        /// </summary>
        public abstract string UserId { get; }

        /// <summary>
        /// 用户名
        /// </summary>
        public abstract string UserName { get;}

        /// <summary>
        /// 租户ID
        /// </summary>
        public abstract string TenantId { get; }

        /// <summary>
        /// Session 重写值
        /// </summary>
        protected SessionOverride OverridedValue => SessionOverrideScopeProvider.GetValue(SessionOverrideContextKey);

        /// <summary>
        /// Session 重写作用域提供程序
        /// </summary>
        protected IAmbientScopeProvider<SessionOverride> SessionOverrideScopeProvider { get; }

        /// <summary>
        /// 初始化一个<see cref="UserContextBase"/>类型的实例
        /// </summary>
        /// <param name="sessionOverrideScopeProvider">Session 重写作用域提供程序</param>
        protected UserContextBase(IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider)
        {
            SessionOverrideScopeProvider = sessionOverrideScopeProvider;
        }
    }
}
