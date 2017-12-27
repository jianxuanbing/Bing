using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Offices.Abstractions;
using Bing.Offices.Core;
using Bing.Offices.Core.Styles;

namespace Bing.Offices
{
    /// <summary>
    /// Excel 操作接口
    /// </summary>
    public interface IExcel
    {
        /// <summary>
        /// 创建工作簿
        /// </summary>
        /// <returns></returns>
        IExcel CreateWorkbook();

        /// <summary>
        /// 创建工作表
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns></returns>
        IExcel CreateSheet(string sheetName = "");

        /// <summary>
        /// 创建单元行
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <returns></returns>
        IExcel CreateRow(int rowIndex);

        /// <summary>
        /// 创建单元格
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <returns></returns>
        IExcel CreateCell(IExcelCell cell);

        /// <summary>
        /// 写入流
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns></returns>
        IExcel Write(Stream stream);

        /// <summary>
        /// 设置日期格式
        /// </summary>
        /// <param name="format">日期格式，默认："yyyy-mm-dd"</param>
        /// <returns></returns>
        IExcel DateFormat(string format = "yyyy-mm-dd");

        /// <summary>
        /// 合并单元格。坐标：(x1,x2,y1,y2)
        /// </summary>
        /// <param name="startRowIndex">起始行索引</param>
        /// <param name="endRowIndex">结束行索引</param>
        /// <param name="startColumnIndex">开始列索引</param>
        /// <param name="endColumnIndex">结束列索引</param>
        /// <returns></returns>
        IExcel MergeCell(int startRowIndex, int endRowIndex, int startColumnIndex, int endColumnIndex);

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <returns></returns>
        IExcel MergeCell(IExcelCell cell);

        /// <summary>
        /// 设置表头样式
        /// </summary>
        /// <param name="table">单元表</param>
        /// <param name="style">单元格样式</param>
        /// <returns></returns>
        IExcel HeadStyle(IExcelSheet table, CellStyle style);

        /// <summary>
        /// 设置正文样式
        /// </summary>
        /// <param name="table">单元表</param>
        /// <param name="style">单元格样式</param>
        /// <returns></returns>
        IExcel BodyStyle(IExcelSheet table, CellStyle style);

        /// <summary>
        /// 设置页脚样式
        /// </summary>
        /// <param name="table">单元表</param>
        /// <param name="style">单元格样式</param>
        /// <returns></returns>
        IExcel FootStyle(IExcelSheet table, CellStyle style);

        /// <summary>
        /// 列宽
        /// </summary>
        /// <param name="columnIndex">列索引</param>
        /// <param name="width">列宽度，设置字符数</param>
        /// <returns></returns>
        IExcel ColumnWidth(int columnIndex, int width);

        /// <summary>
        /// 获取工作簿
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        IExcelWorkbook GetWorkbook(Stream stream);
    }
}
