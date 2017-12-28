using System;
using System.Collections.Generic;
using System.IO;
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
        protected override IWorkbook CreateInternalWorkbook()
        {
            return new HSSFWorkbook();
        }

        /// <summary>
        /// 创建工作簿
        /// </summary>
        /// <param name="stream">文件流，传递过来的创建的工作簿对象</param>
        /// <returns></returns>
        protected override IWorkbook CreateInternalWorkbook(Stream stream)
        {
            return new HSSFWorkbook(stream);
        }
    }
}
