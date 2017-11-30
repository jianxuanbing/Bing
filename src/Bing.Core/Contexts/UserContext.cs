using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Bing.Runtimes;
using Bing.Runtimes.Security;
using Bing.Runtimes.Sessions;
using Bing.Utils.Helpers;
using Bing.Utils.Json;

namespace Bing.Contexts
{
    /// <summary>
    /// 用户上下文
    /// </summary>
    public class UserContext : UserContextBase,IUserContext
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public override string UserId
        {
            get
            {
                if (OverridedValue != null)
                {
                    return OverridedValue.UserId;
                }
                var userIdClaim =
                    PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == BingClaimTypes.UserId);
                if (string.IsNullOrWhiteSpace(userIdClaim?.Value))
                {
                    return null;
                }
                return userIdClaim.Value;
            } 
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public override string UserName
        {
            get
            {
                if (OverridedValue != null)
                {
                    return OverridedValue.UserName;
                }
                var userNameClaim =
                    PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == BingClaimTypes.UserName);
                if (string.IsNullOrWhiteSpace(userNameClaim?.Value))
                {
                    return null;
                }
                return userNameClaim.Value;
            } 
        }

        /// <summary>
        /// 租户ID
        /// </summary>
        public override string TenantId
        {
            get
            {
                if (OverridedValue != null)
                {
                    return OverridedValue.TenantId;
                }
                var tenantIdClaim =
                    PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == BingClaimTypes.TenantId);
                if (string.IsNullOrWhiteSpace(tenantIdClaim?.Value))
                {
                    return null;
                }
                return tenantIdClaim.Value;
            } 
        }

        /// <summary>
        /// 空用户上下文
        /// </summary>
        public static readonly IUserContext Null = new NullUserContext();

        /// <summary>
        /// 标识访问器
        /// </summary>
        protected IPrincipalAccessor PrincipalAccessor { get; }

        /// <summary>
        /// 初始化一个<see cref="UserContext"/>类型的实例
        /// </summary>
        /// <param name="principalAccessor">标识访问器</param>
        /// <param name="sessionOverrideScopeProvider">Session 重写作用域提供程序</param>
        public UserContext(IPrincipalAccessor principalAccessor,
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider):base(sessionOverrideScopeProvider)
        {
            PrincipalAccessor = principalAccessor;
        }

        /// <summary>
        /// 获取上下文信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetContextInfo<T>(string key)
        {
            var claim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == key);
            if (claim == null)
            {
                return default(T);
            }
            if (typeof(T).IsClass)
            {
                return claim.Value.ToObject<T>();
            }
            return Conv.To<T>(claim.Value);
        }
    }
}
