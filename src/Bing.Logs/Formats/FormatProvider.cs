using System;
using System.Collections.Generic;
using System.Text;
using Bing.Logs.Abstractions;

namespace Bing.Logs.Formats
{
    /// <summary>
    /// 日志格式化提供程序
    /// </summary>
    public class FormatProvider : IFormatProvider, ICustomFormatter
    {
        /// <summary>
        /// 日志格式化器
        /// </summary>
        private readonly ILogFormat _format;

        /// <summary>
        /// 初始化一个<see cref="FormatProvider"/>类型的实例
        /// </summary>
        /// <param name="format">日志格式化器</param>
        public FormatProvider(ILogFormat format)
        {
            if (format == null)
            {
                throw new ArgumentNullException(nameof(format));
            }
            _format = format;
        }

        /// <summary>
        /// 获取格式化器
        /// </summary>
        /// <param name="formatType">格式化器类型</param>
        /// <returns></returns>
        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="format">包含格式规范的格式字符串。</param>
        /// <param name="arg">要设置格式的对象。</param>
        /// <param name="formatProvider">一个对象，它提供有关当前实例的格式信息。</param>
        /// <returns></returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            var content = arg as ILogContent;
            if (content == null)
            {
                return string.Empty;
            }
            return _format.Format(content);
        }
    }
}
