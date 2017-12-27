using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Offices.Exports;

namespace Bing.Offices.Npoi.Exports
{
    /// <summary>
    /// 导出器工厂
    /// </summary>
    public class ExportFactory:IExportFactory
    {
        /// <summary>
        /// 创建导出器
        /// </summary>
        /// <param name="format">导出格式</param>
        /// <returns></returns>
        public IExport Create(ExportFormat format)
        {
            switch (format)
            {
                case ExportFormat.Xlsx:
                    return CreateExcel2007Export();
                case ExportFormat.Xls:
                    return CreateExcel2003Export();
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// 创建Npoi Excel2003 导出器
        /// </summary>
        /// <returns></returns>
        public static IExport CreateExcel2003Export()
        {
            return new Excel2003Export();
        }

        /// <summary>
        /// 创建Npoi Excel2007 导出器
        /// </summary>
        /// <returns></returns>
        public static IExport CreateExcel2007Export()
        {
            return new Excel2007Export();
        }
    }
}
