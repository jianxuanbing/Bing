using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Bing.Dependency;
using Bing.Logs.Abstractions;
using Bing.Logs.Formats;

namespace Bing.Logs.NLog
{
    /// <summary>
    /// 日志服务 扩展
    /// </summary>
    public static partial class ServiceExtensions
    {
        /// <summary>
        /// 注册NLog日志操作
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="name">服务名</param>
        /// <returns></returns>
        public static void AddNLog(this ContainerBuilder services,string name=null)
        {
            services.AddScoped<ILogProviderFactory, Bing.Logs.NLog.LogProviderFactory>(name);
            services.AddSingleton<ILogFormat, ContentFormat>(name);
            services.AddScoped<ILogContext, Bing.Logs.Core.LogContext>(name);
            services.AddScoped<ILog, Bing.Logs.Log>(name);
        }
    }
}
