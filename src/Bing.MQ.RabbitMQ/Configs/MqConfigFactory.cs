using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.MQ.RabbitMQ.Configs
{
    /// <summary>
    /// 消息队列配置 工厂
    /// </summary>
    internal class MqConfigFactory
    {
        /// <summary>
        /// 创建消息队列配置实例
        /// </summary>
        /// <returns></returns>
        internal static MqConfig CreateConfigInstance()
        {
            return GetConfigFromAppString();
        }

        /// <summary>
        /// 获取物理配置文件中的配置项目
        /// </summary>
        /// <returns></returns>
        private static MqConfig GetConfigFromAppString()
        {
            var result=new MqConfig();
            result.Host = "192.168.88.22";
            result.UserName = "jian";
            result.Password = "123456";
            result.ListenQueueName = "Demo.J";
            return result;
        }
    }
}
