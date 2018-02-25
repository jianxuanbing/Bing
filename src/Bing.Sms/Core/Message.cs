using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Sms.Abstractions;

namespace Bing.Sms.Core
{
    public class Message:IMessage
    {
        /// <summary>
        /// 短信网关列表
        /// </summary>
        protected List<IGateway> Gateways { get; set; } = new List<IGateway>();

        /// <summary>
        /// 消息类型
        /// </summary>
        protected string Type { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        protected string Content { get; set; }

        /// <summary>
        /// 消息模板ID
        /// </summary>
        protected string Template { get; set; }

        public string GetMessageType()
        {
            return Type;
        }

        public string GetContent(IGateway gateway = null)
        {
            return Content;
        }

        public string GetTemplate(IGateway gateway = null)
        {
            return Template;
        }

        public string GetData(IGateway gateway = null)
        {
            throw new NotImplementedException();
        }

        public IList<IGateway> GetGateways()
        {
            return Gateways;
        }
    }
}
