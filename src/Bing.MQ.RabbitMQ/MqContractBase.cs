using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.MQ.RabbitMQ
{
    /// <summary>
    /// 消息契约基类
    /// </summary>
    [Serializable]
    public abstract class MqContractBase
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        protected MqContractBase()
        {
            MessageId = Guid.NewGuid().ToString();
        }
    }
}
