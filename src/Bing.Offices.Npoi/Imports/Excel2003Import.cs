using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Offices.Imports;

namespace Bing.Offices.Npoi.Imports
{
    /// <summary>
    /// Npoi Excel2003 导入器
    /// </summary>
    public class Excel2003Import:ExcelImportBase
    {
        /// <summary>
        /// 初始化一个<see cref="Excel2003Import"/>类型的实例
        /// </summary>
        /// <param name="path">文件的绝对路径</param>
        /// <param name="sheetName">工作表名称</param>
        public Excel2003Import(string path, string sheetName = "") : base(path, new Excel2003(), sheetName)
        {
        }
    }
}
