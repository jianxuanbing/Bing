using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Offices.Imports
{
    /// <summary>
    /// 导入格式
    /// </summary>
    public enum ImportFormat
    {
        /// <summary>
        /// Excel 2003
        /// </summary>
        [Description("Excel2003")]
        Xls,
        /// <summary>
        /// Excel 2007
        /// </summary>
        [Description("Excel2007")]
        Xlsx
    }
}
