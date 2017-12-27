using System.Collections.Generic;

namespace Bing.Offices.Abstractions
{
    /// <summary>
    /// Excel 行
    /// </summary>
    public interface IExcelRow
    {
        /// <summary>
        /// 单元格列表
        /// </summary>
        List<IExcelCell> Cells { get; set; }

        /// <summary>
        /// 单元格
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        IExcelCell this[int index] { get; }

        /// <summary>
        /// 行索引
        /// </summary>
        int RowIndex { get; set; }

        /// <summary>
        /// 列数
        /// </summary>
        int ColumnNumber { get; }

        /// <summary>
        /// 添加单元格
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="columnSpan">列跨度</param>
        /// <param name="rowSpan">行跨度</param>
        void Add(object value, int columnSpan = 1, int rowSpan = 1);

        /// <summary>
        /// 添加单元格
        /// </summary>
        /// <param name="cell">单元格</param>
        void Add(IExcelCell cell);

        /// <summary>
        /// 清空内容
        /// </summary>
        /// <returns></returns>
        IExcelRow ClearContent();
    }
}
