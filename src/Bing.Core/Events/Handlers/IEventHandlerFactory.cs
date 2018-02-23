using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Events.Handlers
{
    /// <summary>
    /// 事件处理器工厂
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// 获取事件处理器列表
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns></returns>
        List<IEventHandler<TEvent>> GetHandlers<TEvent>() where TEvent : IEvent;
    }
}
