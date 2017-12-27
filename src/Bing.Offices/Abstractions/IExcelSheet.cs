using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Offices.Abstractions
{
    /// <summary>
    /// Excel 工作表
    /// </summary>
    public interface IExcelSheet
    {
        /// <summary>
        /// 总标题
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 列数
        /// </summary>
        int ColumnNumber { get; }

        /// <summary>
        /// 表头行数
        /// </summary>
        int HeadRowCount { get; }

        /// <summary>
        /// 正文行数
        /// </summary>
        int BodyRowCount { get; }

        /// <summary>
        /// 页脚行数
        /// </summary>
        int FootRowCount { get; }

        /// <summary>
        /// 总行数
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 获取表头
        /// </summary>
        /// <returns></returns>
        List<IExcelRow> GetHeader();

        /// <summary>
        /// 获取表格正文
        /// </summary>
        /// <returns></returns>
        List<IExcelRow> GetBody();

        /// <summary>
        /// 获取页脚
        /// </summary>
        /// <returns></returns>
        List<IExcelRow> GetFooter();

        /// <summary>
        /// 添加表头
        /// </summary>
        /// <param name="titles">标题</param>
        void AddHeadRow(params string[] titles);

        /// <summary>
        /// 添加表头
        /// </summary>
        /// <param name="cells">表头</param>
        void AddHeadRow(params IExcelCell[] cells);

        /// <summary>
        /// 添加正文
        /// </summary>
        /// <param name="cellValues">值</param>
        void AddBodyRow(params object[] cellValues);

        /// <summary>
        /// 添加正文
        /// </summary>
        /// <param name="cells">单元格集合</param>
        void AddBodyRow(IEnumerable<IExcelCell> cells);

        /// <summary>
        /// 添加页脚
        /// </summary>
        /// <param name="cellValues">值</param>
        void AddFootRow(params string[] cellValues);

        /// <summary>
        /// 添加页脚
        /// </summary>
        /// <param name="cells">单元格集合</param>
        void AddFootRow(params IExcelCell[] cells);

        /// <summary>
        /// 清空表头
        /// </summary>
        void ClearHeader();
    }
}
