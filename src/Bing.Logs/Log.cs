using System;
using System.Collections.Generic;
using System.Text;
using Bing.Aspects;
using Bing.Contexts;
using Bing.Helpers;
using Bing.Logs.Abstractions;
using Bing.Logs.Contents;
using Bing.Logs.Core;
using Bing.Security;

namespace Bing.Logs
{
    /// <summary>
    /// 日志操作
    /// </summary>
    public class Log : LogBase<LogContent>
    {
        /// <summary>
        /// 类名
        /// </summary>
        private readonly string _class;

        /// <summary>
        /// 初始化一个<see cref="Log"/>类型的实例
        /// </summary>
        /// <param name="providerFactory">日志提供程序工厂</param>
        /// <param name="context">日志上下文</param>
        /// <param name="format">日志格式器</param>
        /// <param name="userContext">用户上下文</param>
        public Log(ILogProviderFactory providerFactory, ILogContext context, ILogFormat format, IUserContext userContext)
            : base(providerFactory.Create("", format), context, userContext)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="Log"/>类型的实例
        /// </summary>
        /// <param name="provider">日志提供程序</param>
        /// <param name="context">日志上下文</param>
        /// <param name="userContext">用户上下文</param>
        /// <param name="class">类名</param>
        public Log(ILogProvider provider, ILogContext context, IUserContext userContext, string @class) : base(provider, context, userContext)
        {
            _class = @class;
        }

        /// <summary>
        /// 获取日志内容
        /// </summary>
        /// <returns></returns>
        protected override LogContent GetContent()
        {
            return new LogContent() { Class = _class };
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="content">日志内容</param>
        protected override void Init(LogContent content)
        {
            base.Init(content);
            content.Tenant = UserContext.GetTenant();
            content.Application = UserContext.GetApplication();
            content.Operator = UserContext.GetFullName();
            content.Role = UserContext.GetRoleName();
        }

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <returns></returns>
        public static ILog GetLog()
        {
            return GetLog(string.Empty);
        }

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <param name="instance">实例</param>
        /// <returns></returns>
        public static ILog GetLog(object instance)
        {
            if (instance == null)
            {
                return GetLog();
            }
            var className = instance.GetType().ToString();
            return GetLog(className, className);
        }

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <param name="logName">日志名称</param>
        /// <returns></returns>
        public static ILog GetLog(string logName)
        {
            return GetLog(logName, string.Empty);
        }

        /// <summary>
        /// 获取日志操作实例
        /// </summary>
        /// <param name="logName">日志名称</param>
        /// <param name="class">类名</param>
        /// <returns></returns>
        private static ILog GetLog(string logName, string @class)
        {
            var providerFactory = Ioc.Create<ILogProviderFactory>();
            var format = Ioc.Create<ILogFormat>();
            var context = Ioc.Create<ILogContext>();
            var userContext = Ioc.Create<IUserContext>();
            return new Log(providerFactory.Create(logName, format), context, userContext, @class);
        }

        /// <summary>
        /// 空日志操作
        /// </summary>
        public static readonly ILog Null = NullLog.Instance;
    }
}
