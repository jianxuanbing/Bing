using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Offices.Exports;

namespace Bing.Offices.Npoi.Exports
{
    /// <summary>
    /// Npoi Excel2003 导出操作
    /// </summary>
    public class Excel2003Export: ExcelExportBase
    {
        public Excel2003Export() : base(ExportFormat.Xls, new Excel2003())
        {
        }
    }
}
