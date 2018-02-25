using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Sms.Abstractions
{
    /// <summary>
    /// 消息
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// 获取消息类型
        /// </summary>
        /// <returns></returns>
        string GetMessageType();

        /// <summary>
        /// 获取消息内容
        /// </summary>
        /// <param name="gateway">短信网关</param>
        /// <returns></returns>
        string GetContent(IGateway gateway = null);

        /// <summary>
        /// 获取消息模板ID
        /// </summary>
        /// <param name="gateway">短信网关</param>
        /// <returns></returns>
        string GetTemplate(IGateway gateway = null);

        /// <summary>
        /// 获取消息模板数据
        /// </summary>
        /// <param name="gateway">短信网关</param>
        /// <returns></returns>
        string GetData(IGateway gateway = null);

        /// <summary>
        /// 获取短信网关列表
        /// </summary>
        /// <returns></returns>
        IList<IGateway> GetGateways();
    }
}
