using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Sms.Abstractions;

namespace Bing.Sms.Core
{
    public class EasySms
    {
        /// <summary>
        /// 配置
        /// </summary>
        protected SmsConfig Config { get; set; }

        /// <summary>
        /// 默认网关
        /// </summary>
        protected string DefaultGateway { get; set; }

        /// <summary>
        /// 网关列表
        /// </summary>
        protected IList<IGateway> Gateways { get; set; }=new List<IGateway>();

        /// <summary>
        /// 消息
        /// </summary>
        protected Messenger Messenger { get; set; }

        /// <summary>
        /// 策略列表
        /// </summary>
        protected IList<IStrategy> Strategies { get; set; }=new List<IStrategy>();

        public EasySms(IList<SmsConfig> configs)
        {

        }
    }
}
