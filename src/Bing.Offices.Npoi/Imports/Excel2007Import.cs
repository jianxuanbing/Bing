using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Offices.Imports;

namespace Bing.Offices.Npoi.Imports
{
    /// <summary>
    /// Npoi Excel2007 导入器
    /// </summary>
    public class Excel2007Import:ExcelImportBase
    {
        /// <summary>
        /// 初始化一个<see cref="Excel2007Import"/>类型的实例
        /// </summary>
        /// <param name="path">文件的绝对路径</param>
        /// <param name="sheetName">工作表名称</param>
        public Excel2007Import(string path, string sheetName = "") : base(path, new Excel2007(), sheetName)
        {
        }
    }
}
