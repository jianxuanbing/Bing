using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.GlobalConfigs.Models
{
    /// <summary>
    /// 用户上下文 配置
    /// </summary>
    public class UserContextConfig
    {
        /// <summary>
        /// 是否启用用户名，用于设置审计创建人以及修改人
        /// </summary>
        [DisplayName("是否启用用户名")]
        public bool EnabledUserName { get; set; }
    }
}
