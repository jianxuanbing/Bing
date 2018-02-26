using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Sms.Abstractions
{
    /// <summary>
    /// 短信策略
    /// </summary>
    public interface IStrategy
    {
        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="gateways">短信网关列表</param>
        IList<IGateway> Apply(IList<IGateway> gateways);
    }
}
