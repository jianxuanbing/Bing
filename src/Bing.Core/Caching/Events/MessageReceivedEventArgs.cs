using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Events
{
    /// <summary>
    /// 消息接收事件参数
    /// </summary>
    public class MessageReceivedEventArgs:EventArgs
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Value { get; set; }
    }
}
