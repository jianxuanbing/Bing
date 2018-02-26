using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Sms.Abstractions;

namespace Bing.Sms.Core
{
    /// <summary>
    /// 短信配置
    /// </summary>
    public class SmsConfig
    {
        /// <summary>
        /// HTTP 请求的超时时间。单位：秒
        /// </summary>
        public int Timout { get; set; }

        /// <summary>
        /// 网关配置
        /// </summary>
        public IList<IGateway> Gateways { get; set; }



        /// <summary>
        /// 短信配置列表
        /// </summary>
        protected IList<SmsConfig> Configs { get; set; }

        public SmsConfig(IList<SmsConfig> configs)
        {
            Configs = configs;
        }        
    }
}
