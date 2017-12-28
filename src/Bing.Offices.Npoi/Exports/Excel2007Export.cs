using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Offices.Exports;

namespace Bing.Offices.Npoi.Exports
{
    /// <summary>
    /// Npoi Excel2007 导出操作
    /// </summary>
    public class Excel2007Export:ExcelExportBase
    {

        /// <summary>
        /// 初始化一个<see cref="Excel2007Export"/>类型的实例
        /// </summary>
        public Excel2007Export() : base(ExportFormat.Xlsx,new Excel2007())
        {
        }
    }
}
