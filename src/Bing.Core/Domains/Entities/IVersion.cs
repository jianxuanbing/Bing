using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Domains.Entities
{
    /// <summary>
    /// 乐观锁
    /// </summary>
    public interface IVersion
    {
        /// <summary>
        /// 版本号
        /// </summary>
        byte[] Version { get; set; }
    }
}
