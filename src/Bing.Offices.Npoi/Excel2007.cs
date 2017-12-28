using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Bing.Offices.Npoi
{
    /// <summary>
    /// Npoi Excel2007 操作
    /// </summary>
    public class Excel2007:ExcelBase
    {
        /// <summary>
        /// 创建工作簿
        /// </summary>
        /// <returns></returns>
        protected override IWorkbook GetWorkbook()
        {
            return new XSSFWorkbook();
        }
    }
}
