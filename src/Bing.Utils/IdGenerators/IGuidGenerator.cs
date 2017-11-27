using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.IdGenerators
{
    /// <summary>
    /// Id生成器
    /// </summary>
    public interface IGuidGenerator
    {
        /// <summary>
        /// 创建 Guid
        /// </summary>
        /// <returns></returns>
        Guid Create();
    }
}
