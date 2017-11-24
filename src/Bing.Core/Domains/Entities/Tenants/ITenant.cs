using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Domains.Entities.Tenants
{
    /// <summary>
    /// 租户
    /// </summary>
    public interface ITenant
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        string TenantId { get; set; }
    }
}
