using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Offices.Abstractions
{
    /// <summary>
    /// Excel 工作簿
    /// </summary>
    public interface IExcelWorkbook
    {
        /// <summary>
        /// 工作表列表
        /// </summary>
        List<IExcelSheet> Sheets { get; set; }

        /// <summary>
        /// 获取工作表
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns></returns>
        IExcelSheet GetSheet(string sheetName = "");
    }
}
