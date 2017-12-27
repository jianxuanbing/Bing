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
        public Excel2003Import(string path,string sheetName = "") : base(path, new Excel2003(), sheetName)
        {
        }
    }
}
