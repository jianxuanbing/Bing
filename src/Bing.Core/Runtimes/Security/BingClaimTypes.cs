using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Runtimes.Security
{
    /// <summary>
    /// Bing Claim类型
    /// </summary>
    public static class BingClaimTypes
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public static string UserName { get; set; } = ClaimTypes.Name;

        /// <summary>
        /// 用户ID
        /// </summary>
        public static string UserId { get; set; } = ClaimTypes.NameIdentifier;

        /// <summary>
        /// 角色
        /// </summary>
        public static string Role { get; set; } = ClaimTypes.Role;

        /// <summary>
        /// 租户ID
        /// </summary>
        public static string TenantId { get; set; } = "TenantId";
    }
}
