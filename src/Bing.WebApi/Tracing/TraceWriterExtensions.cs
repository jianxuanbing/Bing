using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace Bing.WebApi.Tracing
{
    /// <summary>
    /// 跟踪编写器 <see cref="ITraceWriter"/> 扩展
    /// </summary>
    public static class TraceWriterExtensions
    {
        #region Debug(调试)

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="tracer">跟踪编写器</param>
        /// <param name="request">Http请求消息</param>
        /// <param name="controller">WebApi控制器</param>
        /// <param name="exception">异常信息</param>
        public static void Debug(this ITraceWriter tracer, HttpRequestMessage request, ApiController controller,
            Exception exception)
        {
            tracer.Debug(request, controller.ControllerContext.ControllerDescriptor.ControllerType.FullName, exception);
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="tracer">跟踪编写器</param>
        /// <param name="request">Http请求消息</param>
        /// <param name="controller">WebApi控制器</param>
        /// <param name="exception">异常信息</param>
        /// <param name="messageFormat">消息</param>
        /// <param name="messageArguments">消息参数</param>
        public static void Debug(this ITraceWriter tracer, HttpRequestMessage request, ApiController controller,
            Exception exception, string messageFormat, params object[] messageArguments)
        {
            tracer.Debug(request, controller.ControllerContext.ControllerDescriptor.ControllerType.FullName, exception, messageFormat, messageArguments);
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="tracer">跟踪编写器</param>
        /// <param name="request">Http请求消息</param>
        /// <param name="controller">WebApi控制器</param>
        /// <param name="messageFormat">消息</param>
        /// <param name="messageArguments">消息参数</param>
        public static void Debug(this ITraceWriter tracer, HttpRequestMessage request, ApiController controller,
            string messageFormat, params object[] messageArguments)
        {
            tracer.Debug(request, controller.ControllerContext.ControllerDescriptor.ControllerType.FullName,
                messageFormat, messageArguments);
        }

        #endregion

        #region Error(错误)

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="tracer">跟踪编写器</param>
        /// <param name="request">Http请求消息</param>
        /// <param name="controller">WebApi控制器</param>
        /// <param name="exception">异常信息</param>
        public static void Error(this ITraceWriter tracer, HttpRequestMessage request, ApiController controller,
            Exception exception)
        {
            tracer.Error(request, controller.ControllerContext.ControllerDescriptor.ControllerType.FullName, exception);
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="tracer">跟踪编写器</param>
        /// <param name="request">Http请求消息</param>
        /// <param name="controller">WebApi控制器</param>
        /// <param name="exception">异常信息</param>
        /// <param name="messageFormat">消息</param>
        /// <param name="messageArguments">消息参数</param>
        public static void Error(this ITraceWriter tracer, HttpRequestMessage request, ApiController controller,
            Exception exception, string messageFormat, params object[] messageArguments)
        {
            tracer.Error(request, controller.ControllerContext.ControllerDescriptor.ControllerType.FullName, exception, messageFormat, messageArguments);
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="tracer">跟踪编写器</param>
        /// <param name="request">Http请求消息</param>
        /// <param name="controller">WebApi控制器</param>
        /// <param name="messageFormat">消息</param>
        /// <param name="messageArguments">消息参数</param>
        public static void Error(this ITraceWriter tracer, HttpRequestMessage request, ApiController controller,
            string messageFormat, params object[] messageArguments)
        {
            tracer.Error(request, controller.ControllerContext.ControllerDescriptor.ControllerType.FullName,
                messageFormat, messageArguments);
        }

        #endregion

        #region Fatal(致命错误)

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="tracer">跟踪编写器</param>
        /// <param name="request">Http请求消息</param>
        /// <param name="controller">WebApi控制器</param>
        /// <param name="exception">异常信息</param>
        public static void Fatal(this ITraceWriter tracer, HttpRequestMessage request, ApiController controller,
            Exception exception)
        {
            tracer.Fatal(request, controller.ControllerContext.ControllerDescriptor.ControllerType.FullName, exception);
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="tracer">跟踪编写器</param>
        /// <param name="request">Http请求消息</param>
        /// <param name="controller">WebApi控制器</param>
        /// <param name="exception">异常信息</param>
        /// <param name="messageFormat">消息</param>
        /// <param name="messageArguments">消息参数</param>
        public static void Fatal(this ITraceWriter tracer, HttpRequestMessage request, ApiController controller,
            Exception exception, string messageFormat, params object[] messageArguments)
        {
            tracer.Fatal(request, controller.ControllerContext.ControllerDescriptor.ControllerType.FullName, exception, messageFormat, messageArguments);
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="tracer">跟踪编写器</param>
        /// <param name="request">Http请求消息</param>
        /// <param name="controller">WebApi控制器</param>
        /// <param name="messageFormat">消息</param>
        /// <param name="messageArguments">消息参数</param>
        public static void Fatal(this ITraceWriter tracer, HttpRequestMessage request, ApiController controller,
            string messageFormat, params object[] messageArguments)
        {
            tracer.Fatal(request, controller.ControllerContext.ControllerDescriptor.ControllerType.FullName,
                messageFormat, messageArguments);
        }

        #endregion

        #region Info(信息)

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="tracer">跟踪编写器</param>
        /// <param name="request">Http请求消息</param>
        /// <param name="controller">WebApi控制器</param>
        /// <param name="exception">异常信息</param>
        public static void Info(this ITraceWriter tracer, HttpRequestMessage request, ApiController controller,
            Exception exception)
        {
            tracer.Info(request, controller.ControllerContext.ControllerDescriptor.ControllerType.FullName, exception);
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="tracer">跟踪编写器</param>
        /// <param name="request">Http请求消息</param>
        /// <param name="controller">WebApi控制器</param>
        /// <param name="exception">异常信息</param>
        /// <param name="messageFormat">消息</param>
        /// <param name="messageArguments">消息参数</param>
        public static void Info(this ITraceWriter tracer, HttpRequestMessage request, ApiController controller,
            Exception exception, string messageFormat, params object[] messageArguments)
        {
            tracer.Info(request, controller.ControllerContext.ControllerDescriptor.ControllerType.FullName, exception, messageFormat, messageArguments);
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="tracer">跟踪编写器</param>
        /// <param name="request">Http请求消息</param>
        /// <param name="controller">WebApi控制器</param>
        /// <param name="messageFormat">消息</param>
        /// <param name="messageArguments">消息参数</param>
        public static void Info(this ITraceWriter tracer, HttpRequestMessage request, ApiController controller,
            string messageFormat, params object[] messageArguments)
        {
            tracer.Info(request, controller.ControllerContext.ControllerDescriptor.ControllerType.FullName,
                messageFormat, messageArguments);
        }

        #endregion

        #region Warn(警告)

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="tracer">跟踪编写器</param>
        /// <param name="request">Http请求消息</param>
        /// <param name="controller">WebApi控制器</param>
        /// <param name="exception">异常信息</param>
        public static void Warn(this ITraceWriter tracer, HttpRequestMessage request, ApiController controller,
            Exception exception)
        {
            tracer.Warn(request, controller.ControllerContext.ControllerDescriptor.ControllerType.FullName, exception);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="tracer">跟踪编写器</param>
        /// <param name="request">Http请求消息</param>
        /// <param name="controller">WebApi控制器</param>
        /// <param name="exception">异常信息</param>
        /// <param name="messageFormat">消息</param>
        /// <param name="messageArguments">消息参数</param>
        public static void Warn(this ITraceWriter tracer, HttpRequestMessage request, ApiController controller,
            Exception exception, string messageFormat, params object[] messageArguments)
        {
            tracer.Warn(request, controller.ControllerContext.ControllerDescriptor.ControllerType.FullName, exception, messageFormat, messageArguments);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="tracer">跟踪编写器</param>
        /// <param name="request">Http请求消息</param>
        /// <param name="controller">WebApi控制器</param>
        /// <param name="messageFormat">消息</param>
        /// <param name="messageArguments">消息参数</param>
        public static void Warn(this ITraceWriter tracer, HttpRequestMessage request, ApiController controller,
            string messageFormat, params object[] messageArguments)
        {
            tracer.Warn(request, controller.ControllerContext.ControllerDescriptor.ControllerType.FullName,
                messageFormat, messageArguments);
        }

        #endregion
    }
}
