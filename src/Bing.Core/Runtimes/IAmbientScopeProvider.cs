using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Runtimes
{
    /// <summary>
    /// 环境范围 提供程序
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public interface IAmbientScopeProvider<T>
    {
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="contextKey">上下文键</param>
        /// <returns></returns>
        T GetValue(string contextKey);

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="contextKey">上下文键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        IDisposable BeginScope(string contextKey, T value);
    }
}
