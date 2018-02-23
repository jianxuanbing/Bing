using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Events
{
    /// <summary>
    /// 事件
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// 事件标识
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// 事件时间
        /// </summary>
        DateTime Time { get; }
    }
}
