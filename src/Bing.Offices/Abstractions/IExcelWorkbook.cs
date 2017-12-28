using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Offices.Abstractions
{
    /// <summary>
    /// Excel 工作簿
    /// </summary>
    public interface IExcelWorkbook
    {
        /// <summary>
        /// 工作表列表
        /// </summary>
        List<IExcelSheet> Sheets { get; set; }

        /// <summary>
        /// 获取工作表
        /// </summary>
        /// <param name="name">工作表名称</param>
        /// <returns></returns>
        IExcelSheet GetSheet(string name);

        /// <summary>
        /// 获取工作表
        /// </summary>
        /// <param name="index">工作表索引</param>
        /// <returns></returns>
        IExcelSheet GetSheetAt(int index);

        /// <summary>
        /// 创建工作表
        /// </summary>
        /// <returns></returns>
        IExcelSheet CreateSheet();

        /// <summary>
        /// 创建工作表
        /// </summary>
        /// <param name="name">工作表名称</param>
        /// <returns></returns>
        IExcelSheet CreateSheet(string name);
    }
}
