using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Sms.Exceptions
{
    /// <summary>
    /// 网关错误异常
    /// </summary>
    public class GatewayErrorException:Exception
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 初始化一个<see cref="GatewayErrorException"/>类型的实例
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        public GatewayErrorException(string message,int code) : base(message)
        {
            Code = code;
        }
    }
}
