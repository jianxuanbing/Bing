using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Bing.Dependency;
using Bing.Logs.Abstractions;
using Bing.Logs.Core;
using Exceptionless;

namespace Bing.Logs.Exceptionless
{
    /// <summary>
    /// 日志服务 扩展
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册Exceptionless日志操作
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configAction">配置操作</param>
        /// <param name="name">服务名</param>
        public static void AddExceptionless(this ContainerBuilder services,
            Action<ExceptionlessConfiguration> configAction, string name = null)
        {
            services.AddScoped<ILogProviderFactory, Bing.Logs.Exceptionless.LogProviderFactory>(name);
            services.AddSingleton<ILogFormat>(NullLogFormat.Instance, name);
            services.AddScoped<ILogContext, Bing.Logs.Exceptionless.LogContext>(name);
            services.AddScoped<ILog, Log>(name);
            configAction?.Invoke(ExceptionlessClient.Default.Configuration);
        }
    }
}
