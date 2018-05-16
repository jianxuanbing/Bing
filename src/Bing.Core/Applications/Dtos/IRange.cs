using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Applications.Dtos
{
    /// <summary>
    /// 范围
    /// </summary>
    /// <typeparam name="T">数据类型，字符串、数值、时间</typeparam>
    public interface IRange<T>
    {
        /// <summary>
        /// 开始值
        /// </summary>
        T Begin { get; set; }

        /// <summary>
        /// 结束值
        /// </summary>
        T End { get; set; }
    }
}
