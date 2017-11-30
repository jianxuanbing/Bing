using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Runtimes.Sessions
{
    /// <summary>
    /// Session 重写
    /// </summary>
    public class SessionOverride
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 初始化一个<see cref="SessionOverride"/>类型的实例
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="tenantId">租户ID</param>
        public SessionOverride(string userId, string tenantId)
        {
            UserId = userId;
            TenantId = tenantId;
        }
    }
}
