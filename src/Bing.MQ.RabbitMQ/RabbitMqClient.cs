using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.MQ.RabbitMQ.Events;
using Bing.MQ.RabbitMQ.Serializers;
using Bing.Utils.Modes;
using RabbitMQ.Client.Events;

namespace Bing.MQ.RabbitMQ
{
    /// <summary>
    /// 表示消息到达客户端发起的事件
    /// </summary>
    /// <param name="result">事件消息对象</param>
    public delegate void ActionEvent(EventMessageResult result);

    /// <summary>
    /// 表示RabbitMq客户端组件
    /// </summary>
    public class RabbitMqClient:IRabbitMqClient
    {
        /// <summary>
        /// 客户端实例私有字段
        /// </summary>
        private static IRabbitMqClient _instanceClient;

        /// <summary>
        /// 返回全局唯一的RabbitMqClient实例
        /// </summary>
        public static IRabbitMqClient Instance
        {
            get { return Singleton<IRabbitMqClient>.GetInstance(RabbitMqClientFactory.CreateInstance); }
        }

        /// <summary>
        /// 数据上下文
        /// </summary>
        public RabbitMqClientContext Context { get; set; }

        /// <summary>
        /// 事件激活委托实例
        /// </summary>
        private ActionEvent _actionMessage;

        /// <summary>
        /// 当前监听的队列中有消息到达时触发的执行事件
        /// </summary>
        public event ActionEvent ActionEventMessage
        {
            add
            {
                if (_actionMessage == null)
                {
                    _actionMessage += value;
                }
            }
            remove
            {
                if (_actionMessage != null)
                {
                    _actionMessage -= value;
                }
            }
        }

        public void Dispose()
        {
            if (Context.SendConnection == null)
            {
                return;
            }
            if (Context.SendConnection.IsOpen)
            {
                Context.SendConnection.Close();
            }
            Context.SendConnection.Dispose();
        }

        /// <summary>
        /// 触发一个事件并且将事件打包成消息发送到远程队列中
        /// </summary>
        /// <param name="eventMessage">发送的消息实例</param>
        /// <param name="exChange">Exchange名称</param>
        /// <param name="queue">队列名称</param>
        public void TriggerEventMessage(EventMessage eventMessage, string exChange, string queue)
        {
            using (var connection = RabbitMqClientFactory.CreateConnection())
            {
                //Context.SendChannel = RabbitMqClientFactory.CreateModel(connection);
                const byte deliveryMode = 2;
                using (var channel= RabbitMqClientFactory.CreateModel(connection))
                {
                    var messageSerializer = MessageSerializerFactory.CreateMessageSerializerInstance();
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = deliveryMode;
                    channel.BasicPublish(exChange, queue, properties,
                        messageSerializer.SerializerBytes(eventMessage));
                }
            }
        }

        /// <summary>
        /// 开始监听默认的队列
        /// </summary>
        public void OnListening()
        {
            Task.Factory.StartNew(ListenInit);
        }

        /// <summary>
        /// 监听初始化
        /// </summary>
        private void ListenInit()
        {
            Context.ListenConnection = RabbitMqClientFactory.CreateConnection();
            Context.ListenConnection.ConnectionShutdown += (o, e) =>
            {
                Console.WriteLine(e.ReplyText);
            };
            Context.ListenChannel = RabbitMqClientFactory.CreateModel(Context.ListenConnection);

            var consumer = new EventingBasicConsumer(Context.ListenChannel);//创建事件驱动的消费者类型
            consumer.Received += Consumer_Received;

            Context.ListenChannel.BasicQos(0,1,false);//一次只获取一个消息进行消费
            Context.ListenChannel.BasicConsume(Context.ListenQueueName, false, consumer);
        }

        /// <summary>
        /// 接收到的消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                Console.WriteLine(e.RoutingKey);
                var result = EventMessage.BuildEventMessageResult(e.Body);

                if (_actionMessage != null)
                {
                    _actionMessage(result);//触发外部监听事件
                }
                if (result.IsOperationOk == false)
                {
                    // 未能消费此消息，重新放入队列头
                    Context.ListenChannel.BasicReject(e.DeliveryTag,true);
                }
                else if (Context.ListenChannel.IsClosed == false)
                {
                    Context.ListenChannel.BasicAck(e.DeliveryTag,false);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);                
            }
        }
    }
}
