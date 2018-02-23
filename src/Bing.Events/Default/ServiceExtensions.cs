using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Bing.Dependency;
using Bing.Events.Handlers;

namespace Bing.Events.Default
{
    /// <summary>
    /// 事件总线 扩展
    /// </summary>
    public static partial class ServiceExtensions
    {
        /// <summary>
        /// 注册默认事件总线服务
        /// </summary>
        /// <param name="services">服务</param>
        public static void AddDefaultEventBus(this ContainerBuilder services)
        {
            services.AddSingleton<IEventHandlerFactory, EventHandlerFactory>();
            services.AddSingleton<IEventBus, Bing.Events.Default.EventBus>();
        }
    }
}
