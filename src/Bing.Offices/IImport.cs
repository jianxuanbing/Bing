using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Offices
{
    /// <summary>
    /// 导入器
    /// </summary>
    public interface IImport
    {
        /// <summary>
        /// 获取结果
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <returns></returns>
        List<T> GetResult<T>();
    }
}
