using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Events.Handlers;
using Bing.Helpers;

namespace Bing.Events.Default
{
    /// <summary>
    /// 事件处理器工厂
    /// </summary>
    public class EventHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// 获取事件处理器列表
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns></returns>
        public List<IEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent : IEvent
        {
            return Ioc.CreateList<IEventHandler<TEvent>>();
        }
    }
}
