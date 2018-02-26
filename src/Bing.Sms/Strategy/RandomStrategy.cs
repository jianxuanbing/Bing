using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Sms.Abstractions;

namespace Bing.Sms.Strategy
{
    /// <summary>
    /// 随机策略
    /// </summary>
    public class RandomStrategy:IStrategy
    {
        public IList<IGateway> Apply(IList<IGateway> gateways)
        {
            Random random=new Random();
            List<IGateway> tempGateways=new List<IGateway>();
            foreach (var gateway in gateways)
            {
                tempGateways.Insert(random.Next(gateways.Count+1),gateway);
            }

            return tempGateways;
        }
    }
}
