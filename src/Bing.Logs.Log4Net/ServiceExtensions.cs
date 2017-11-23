using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Bing.Dependency;
using Bing.Logs.Abstractions;

namespace Bing.Logs.Log4Net
{
    /// <summary>
    /// 日志服务 扩展
    /// </summary>
    public static partial class ServiceExtensions
    {
        /// <summary>
        /// 注册Log4Net日志操作
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="log4NetConfigFile">log4net配置文件</param>
        public static void AddLog4Net(this ContainerBuilder services, string log4NetConfigFile = "log4net.config")
        {
            services.AddScoped<ILogProviderFactory, Bing.Logs.Log4Net.LogProviderFactory>();
            services.AddScoped<ILogFormat, Bing.Logs.Formats.ContentFormat>();
            services.AddScoped<ILogContext, Bing.Logs.Core.LogContext>();
            services.AddScoped<ILog, Bing.Logs.Log>();

            Log4NetProvider.InitRepository(log4NetConfigFile);
        }
    }
}
