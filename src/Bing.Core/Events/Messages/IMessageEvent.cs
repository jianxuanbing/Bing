using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Events.Messages
{
    /// <summary>
    /// 消息事件
    /// </summary>
    public interface IMessageEvent : IEvent
    {
        /// <summary>
        /// 事件数据
        /// </summary>
        object Data { get; set; }

        /// <summary>
        /// 发送目标
        /// </summary>
        string Target { get; set; }

        /// <summary>
        /// 回调
        /// </summary>
        string Callback { get; set; }
    }
}
