using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Bing.Logs;
using Bing.Logs.Extensions;
using Bing.Utils.Exceptions;
using Bing.Utils.Extensions;
using Bing.Utils.Json;

namespace Bing.Samples.Api
{
    /// <summary>
    /// WebApi异常处理
    /// </summary>
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {

        // 重写基类的异常处理方法
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {

            if (actionExecutedContext.Exception == null)
            {
                return;
            }
            ILog logger = Log.GetLog(typeof(ExceptionHandlingAttribute));

            HttpRequestMessage request = actionExecutedContext.Request;
            Exception exception = actionExecutedContext.Exception;

            string ip = "127.0.0.1";
            string user = actionExecutedContext.ActionContext.RequestContext.Principal.Identity.Name;
            string msg = string.Format("User:{0}，IP:{1}，Message:{2}", user, ip, exception.Message);

            var param = actionExecutedContext.ActionContext.ActionArguments.ToJson();
            var headerParam = actionExecutedContext.ActionContext.Request.Headers.ToJson();
            var headerContent = $"请求头参数：{headerParam}";
            var content = $"请求参数：{param}";

            if (IsWarningException(exception))
            {
                actionExecutedContext.Response = request.CreateResponse(HttpStatusCode.OK,GetExceptionMessage(exception));
                logger.Caption("WebApi业务异常处理").Content(headerContent).Content(content).Debug(msg);
                base.OnException(actionExecutedContext);
                return;
            }

            logger.Caption("WebApi异常处理");
            logger.Content(headerContent);
            logger.Content(content);
            logger.Exception(exception);
            logger.Error();

            string message = "网络繁忙，请勿频繁点击!";

            actionExecutedContext.Response = request.CreateResponse(HttpStatusCode.OK,message);

            base.OnException(actionExecutedContext);
        }

        /// <summary>
        /// 获取异常消息
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        private string GetExceptionMessage(Exception exception)
        {
            return !IsInnerException(exception) ? exception.Message : exception.InnerException.Message;
        }

        /// <summary>
        /// 是否内部异常
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        private bool IsInnerException(Exception exception)
        {
            return  exception.InnerException is Warning;
        }

        /// <summary>
        /// 是否应用程序异常
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        private bool IsWarningException(Exception exception)
        {
            return (exception is Warning && !exception.Message.IsEmpty()) ||
                   (exception.InnerException is Warning && !exception.InnerException.Message.IsEmpty());
        }
    }
}