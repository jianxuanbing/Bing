using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Runtimes
{
    /// <summary>
    /// 环境 数据上下文
    /// </summary>
    public interface IAmbientDataContext
    {
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void SetData(string key, object value);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        object GetData(string key);
    }
}
