using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Abstractions
{
    /// <summary>
    /// 缓存订阅者
    /// </summary>
    public interface ICacheSubscriber
    {
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="notifyType">通知类型</param>
        void Subscribe(string channel,NotifyType notifyType);

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="notifyType">通知类型</param>
        /// <returns></returns>
        Task SubscribeAsync(string channel, NotifyType notifyType);
    }
}
