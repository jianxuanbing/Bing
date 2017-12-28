using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Offices.Abstractions;
using Bing.Offices.Core;
using Bing.Offices.Core.Styles;
using Bing.Offices.Npoi.Resolvers;
using Bing.Utils.Extensions;
using Bing.Utils.Helpers;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace Bing.Offices.Npoi
{
    /// <summary>
    /// Excel 操作基类
    /// </summary>
    public abstract class ExcelBase:IExcel
    {
        #region 字段

        /// <summary>
        /// Excel工作簿
        /// </summary>
        private IWorkbook _workbook;

        /// <summary>
        /// Excel工作表
        /// </summary>
        private ISheet _sheet;

        /// <summary>
        /// 当前行
        /// </summary>
        private IRow _row;

        /// <summary>
        /// 当前单元格
        /// </summary>
        private ICell _cell;

        /// <summary>
        /// 日期格式
        /// </summary>
        private string _dateFormat;

        /// <summary>
        /// 表头样式
        /// </summary>
        private ICellStyle _headStyle;

        /// <summary>
        /// 正文样式
        /// </summary>
        private ICellStyle _bodyStyle;

        /// <summary>
        /// 页脚样式
        /// </summary>
        private ICellStyle _footStyle;

        /// <summary>
        /// 日期样式
        /// </summary>
        private ICellStyle _dateStyle;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化一个<see cref="ExcelBase"/>类型的实例
        /// </summary>
        protected ExcelBase()
        {
            _dateFormat = "yyyy-mm-dd";
            CreateWorkbook().CreateSheet();
        }

        #endregion

        #region CreateWorkbook(创建工作簿)

        /// <summary>
        /// 创建工作簿
        /// </summary>
        /// <returns></returns>
        public IExcel CreateWorkbook()
        {
            _workbook = GetWorkbook();
            return this;
        }

        /// <summary>
        /// 创建工作簿
        /// </summary>
        /// <returns></returns>
        protected abstract IWorkbook GetWorkbook();

        #endregion

        #region CreateSheet(创建工作表)

        /// <summary>
        /// 创建工作表
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns></returns>
        public IExcel CreateSheet(string sheetName = "")
        {
            _sheet = sheetName.IsEmpty() ? _workbook.CreateSheet() : _workbook.CreateSheet(sheetName);
            return this;
        }

        #endregion

        #region CreateRow(创建行)

        /// <summary>
        /// 创建行
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <returns></returns>
        public IExcel CreateRow(int rowIndex)
        {
            _row = GetOrCreateRow(rowIndex);
            return this;
        }

        /// <summary>
        /// 获取行，如果不存在则创建
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <returns></returns>
        private IRow GetOrCreateRow(int rowIndex)
        {
            return _sheet.GetRow(rowIndex) ?? _sheet.CreateRow(rowIndex);
        }

        #endregion

        #region CreateCell(创建单元格)

        /// <summary>
        /// 创建单元格
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <returns></returns>
        public IExcel CreateCell(IExcelCell cell)
        {
            if (cell.IsNull())
            {
                return this;
            }
            _cell = GetOrCreateCell(_row, cell.ColumnIndex);
            SetCellValue(cell.Value);
            MergeCell(cell);
            return this;
        }

        /// <summary>
        /// 获取单元格，如果不存在则创建
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="columnIndex">单元格索引</param>
        /// <returns></returns>
        private ICell GetOrCreateCell(IRow row, int columnIndex)
        {
            return row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);
        }

        /// <summary>
        /// 设置单元格的值
        /// </summary>
        /// <param name="value">值</param>
        private void SetCellValue(object value)
        {
            if (value == null)
            {
                return;
            }
            switch (value.GetType().ToString())
            {
                case "System.String":
                    _cell.SetCellValue(value.ToString());
                    break;
                case "System.DateTime":
                    _cell.SetCellValue(Conv.ToDate(value));
                    _cell.CellStyle = CreateDateStyle();
                    break;
                case "System.Boolean":
                    _cell.SetCellValue(Conv.ToBool(value));
                    break;
                case "System.Byte":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                    _cell.SetCellValue(Conv.ToInt(value));
                    break;
                case "System.Single":
                case "System.Double":
                case "System.Decimal":
                    _cell.SetCellValue(Conv.ToDouble(value));
                    break;
                default:
                    _cell.SetCellValue("");
                    break;

            }
        }

        /// <summary>
        /// 创建日期样式
        /// </summary>
        /// <returns></returns>
        private ICellStyle CreateDateStyle()
        {
            if (_dateStyle != null)
            {
                return _dateStyle;
            }
            _dateStyle = CellStyleResolver.Resolve(_workbook, CellStyle.Body());
            var format = _workbook.CreateDataFormat();
            _dateStyle.DataFormat = format.GetFormat(_dateFormat);
            return _dateStyle;
        }

        #endregion

        #region Write(写入流)

        /// <summary>
        /// 写入流
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns></returns>
        public IExcel Write(Stream stream)
        {
            _workbook.Write(stream);
            return this;
        }

        #endregion

        #region DateFormat(设置日期格式)

        /// <summary>
        /// 设置日期格式
        /// </summary>
        /// <param name="format">日期格式，默认：yyyy-mm-dd</param>
        /// <returns></returns>
        public IExcel DateFormat(string format = "yyyy-mm-dd")
        {
            _dateFormat = format;
            return this;
        }

        #endregion

        #region MergeCell(合并单元格)

        /// <summary>
        /// 合并单元格。坐标：(x1,x2,y1,y2)
        /// </summary>
        /// <param name="startRowIndex">起始行索引</param>
        /// <param name="endRowIndex">结束行索引</param>
        /// <param name="startColumnIndex">开始列索引</param>
        /// <param name="endColumnIndex">结束列索引</param>
        /// <returns></returns>
        public IExcel MergeCell(int startRowIndex, int endRowIndex, int startColumnIndex, int endColumnIndex)
        {
            var region = new CellRangeAddress(startRowIndex, endRowIndex, startColumnIndex, endColumnIndex);
            _sheet.AddMergedRegion(region);
            return this;
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <returns></returns>
        public IExcel MergeCell(IExcelCell cell)
        {
            if (cell.NeedMerge)
            {
                MergeCell(cell.RowIndex, cell.EndRowIndex, cell.ColumnIndex, cell.EndColumnIndex);
            }
            return this;
        }

        #endregion

        #region HeadStyle(设置表头样式)

        /// <summary>
        /// 设置表头样式
        /// </summary>
        /// <param name="table">工作表</param>
        /// <param name="style">单元格样式</param>
        /// <returns></returns>
        public IExcel HeadStyle(IExcelSheet table, CellStyle style)
        {
            if (_headStyle == null)
            {
                _headStyle = CellStyleResolver.Resolve(_workbook, style);
            }
            Style(0, table.HeadRowCount - 1, 0, table.ColumnNumber - 1, _headStyle);
            return this;
        }

        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="startRowIndex">起始行索引</param>
        /// <param name="endRowIndex">结束行索引</param>
        /// <param name="startColumnIndex">起始列索引</param>
        /// <param name="endColumnIndex">结束列索引</param>
        /// <param name="style">单元格样式</param>
        /// <returns></returns>
        protected IExcel Style(int startRowIndex, int endRowIndex, int startColumnIndex, int endColumnIndex,
            ICellStyle style)
        {
            for (var i = startRowIndex; i <= endColumnIndex; i++)
            {
                var row = GetOrCreateRow(i);
                for (var j = startColumnIndex; j <= endColumnIndex; j++)
                {
                    GetOrCreateCell(row, j).CellStyle = style;
                }
            }
            return this;
        }

        #endregion

        #region BodyStyle(设置正文样式)

        /// <summary>
        /// 设置正文样式
        /// </summary>
        /// <param name="table">工作表</param>
        /// <param name="style">单元格样式</param>
        /// <returns></returns>
        public IExcel BodyStyle(IExcelSheet table, CellStyle style)
        {
            if (_bodyStyle == null)
            {
                _bodyStyle = CellStyleResolver.Resolve(_workbook, style);
            }
            Style(table.HeadRowCount, table.HeadRowCount + table.BodyRowCount - 1, 0, table.ColumnNumber - 1,
                _bodyStyle);
            return this;
        }

        #endregion

        #region FootStyle(设置页脚样式)

        /// <summary>
        /// 设置页脚样式
        /// </summary>
        /// <param name="table">工作表</param>
        /// <param name="style">单元格样式</param>
        /// <returns></returns>
        public IExcel FootStyle(IExcelSheet table, CellStyle style)
        {
            if (_footStyle == null)
            {
                _footStyle = CellStyleResolver.Resolve(_workbook, style);
            }
            Style(table.HeadRowCount + table.BodyRowCount, table.Count - 1, 0, table.ColumnNumber - 1, _footStyle);
            return this;
        }

        #endregion

        #region ColumnWidth(设置单元格列宽)

        /// <summary>
        /// 设置单元格列宽
        /// </summary>
        /// <param name="columnIndex">列索引</param>
        /// <param name="width">列宽度，设置字符数</param>
        /// <returns></returns>
        public IExcel ColumnWidth(int columnIndex, int width)
        {
            _sheet.SetColumnWidth(columnIndex, width * 256);
            return this;
        }

        #endregion


        public IExcelWorkbook GetWorkbook(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
