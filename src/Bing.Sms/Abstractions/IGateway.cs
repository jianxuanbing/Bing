using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Sms.Core;

namespace Bing.Sms.Abstractions
{
    /// <summary>
    /// 网关
    /// </summary>
    public interface IGateway
    {
        /// <summary>
        /// 获取网关名称
        /// </summary>
        /// <returns></returns>
        string GetName();

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="to">目标</param>
        /// <param name="message">消息</param>
        /// <param name="config">短信配置</param>
        void Send(IList<string> to, IMessage message, SmsConfig config);
    }
}
