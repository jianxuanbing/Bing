using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Validators.IdentityCards
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 女性
        /// </summary>
        [Description("女性")]
        Female = 0,
        /// <summary>
        /// 男性
        /// </summary>
        [Description("男性")]
        Male = 1
    }
}
