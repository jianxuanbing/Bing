using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Offices.Imports
{
    /// <summary>
    /// 文件导入器工厂
    /// </summary>
    public interface IImportFactory
    {
        /// <summary>
        /// 创建文件导入器
        /// </summary>
        /// <param name="format">导入格式</param>
        /// <returns></returns>
        IImport Create(ImportFormat format);
    }
}
