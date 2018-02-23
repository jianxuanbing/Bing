using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Timing;

namespace Bing.Events.Messages
{
    /// <summary>
    /// 消息事件
    /// </summary>
    public class MessageEvent : Event, IMessageEvent
    {
        /// <summary>
        /// 事件数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 发送目标
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 回调
        /// </summary>
        public string Callback { get; set; }

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"事件标识: {Id}");
            result.AppendLine($"事件时间: {Time.ToMillisecondString()}");
            if (string.IsNullOrWhiteSpace(Target) == false)
            {
                result.AppendLine($"发送目标: {Target}");
            }
            if (string.IsNullOrWhiteSpace(Callback) == false)
            {
                result.AppendLine($"回调: {Callback}");
            }
            result.Append($"事件数据: {Bing.Utils.Json.JsonUtil.ToJson(Data)}");
            return result.ToString();
        }
    }
}
