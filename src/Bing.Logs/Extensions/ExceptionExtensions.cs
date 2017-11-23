using System;
using System.Collections.Generic;
using System.Text;
using Bing.Utils.Exceptions;

namespace Bing.Logs.Extensions
{
    /// <summary>
    /// 异常扩展
    /// </summary>
    public static partial class ExceptionExtensions
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="exception">异常</param>
        /// <param name="log">日志</param>
        public static void Log(this Exception exception, ILog log)
        {
            var warning = exception as Warning;
            if (warning == null)
            {
                log.Exception(exception).Error();
                return;
            }
            log.Exception(exception,warning.Code).Warn();
        }
    }
}
