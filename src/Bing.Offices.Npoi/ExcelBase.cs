using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Offices.Abstractions;
using Bing.Offices.Core;
using Bing.Offices.Core.Styles;

namespace Bing.Offices.Npoi
{
    /// <summary>
    /// Excel 操作基类
    /// </summary>
    public abstract class ExcelBase:IExcel
    {
        public IExcel CreateWorkbook()
        {
            throw new NotImplementedException();
        }

        public IExcel CreateSheet(string sheetName = "")
        {
            throw new NotImplementedException();
        }

        public IExcel CreateRow(int rowIndex)
        {
            throw new NotImplementedException();
        }

        public IExcel CreateCell(IExcelCell cell)
        {
            throw new NotImplementedException();
        }

        public IExcel Write(Stream stream)
        {
            throw new NotImplementedException();
        }

        public IExcel DateFormat(string format = "yyyy-mm-dd")
        {
            throw new NotImplementedException();
        }

        public IExcel MergeCell(int startRowIndex, int endRowIndex, int startColumnIndex, int endColumnIndex)
        {
            throw new NotImplementedException();
        }

        public IExcel MergeCell(IExcelCell cell)
        {
            throw new NotImplementedException();
        }

        public IExcel HeadStyle(IExcelSheet table, CellStyle style)
        {
            throw new NotImplementedException();
        }

        public IExcel BodyStyle(IExcelSheet table, CellStyle style)
        {
            throw new NotImplementedException();
        }

        public IExcel FootStyle(IExcelSheet table, CellStyle style)
        {
            throw new NotImplementedException();
        }

        public IExcel ColumnWidth(int columnIndex, int width)
        {
            throw new NotImplementedException();
        }

        public IExcelWorkbook GetWorkbook(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
