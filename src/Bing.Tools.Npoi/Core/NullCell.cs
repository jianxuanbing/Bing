using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Tools.Npoi.Core
{
    /// <summary>
    /// 空单元格
    /// </summary>
    public class NullCell:Cell
    {
        /// <summary>
        /// 初始化一个<see cref="NullCell"/>类型的实例
        /// </summary>
        public NullCell() : base("", 1, 1)
        {
        }
    }
}
