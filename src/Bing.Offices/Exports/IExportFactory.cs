using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Offices.Exports
{
    /// <summary>
    /// 文件导出器工厂
    /// </summary>
    public interface IExportFactory
    {
        /// <summary>
        /// 创建文件导出器
        /// </summary>
        /// <param name="format">导出格式</param>
        /// <returns></returns>
        IExport Create(ExportFormat format);
    }
}
