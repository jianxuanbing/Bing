using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Events.Messages;

namespace Bing.Samples.Services.Messages
{
    /// <summary>
    /// 测试消息事件
    /// </summary>
    public class TestMessageEvent:MessageEvent
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
