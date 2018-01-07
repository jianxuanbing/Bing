using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Events;

namespace Bing.Caching.Abstractions
{
    /// <summary>
    /// 异步缓存发布订阅
    /// </summary>
    public interface IAsyncPubSub
    {
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="topic">消息</param>
        /// <param name="value">消息内容</param>
        /// <returns></returns>
        Task PublishAsync(string topic, string value);

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="topic">消息</param>
        /// <returns></returns>
        Task SubscribeAsync(string topic);

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="topic">消息</param>
        /// <returns></returns>
        Task UnsubscribeAsync(string topic);

        /// <summary>
        /// 消息接收事件
        /// </summary>
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
    }
}
