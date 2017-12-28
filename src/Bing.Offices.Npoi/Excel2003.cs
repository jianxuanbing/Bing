using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Bing.Offices.Npoi
{
    /// <summary>
    /// Npoi Excel2003 操作
    /// </summary>
    public class Excel2003:ExcelBase
    {
        /// <summary>
        /// 创建工作簿
        /// </summary>
        /// <returns></returns>
        protected override IWorkbook GetWorkbook()
        {
            return new HSSFWorkbook();
        }
    }
}
