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
        public Excel2007Import(string path, string sheetName = "") : base(path, new Excel2007(), sheetName)
        {
        }
    }
}
