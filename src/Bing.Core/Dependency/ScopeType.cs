using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Dependency
{
    /// <summary>
    /// 作用域类型
    /// </summary>
    public enum ScopeType
    {
        /// <summary>
        /// 默认作用域，使用自带解析
        /// </summary>
        None=0,
        /// <summary>
        /// Http作用域，使用WebApi解析
        /// </summary>
        Http=1
    }
}
