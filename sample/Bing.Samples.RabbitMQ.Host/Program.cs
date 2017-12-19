using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bing.MQ.RabbitMQ;
using Bing.MQ.RabbitMQ.Events;
using Bing.MQ.RabbitMQ.Serializers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Bing.Samples.RabbitMQ.Host
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            Listening();
            Console.ReadLine();
        }

        private void TestListening()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "192.168.88.22";
            factory.UserName = "jian";
            factory.Password = "123456";

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {                                  
                    channel.BasicQos(0,1,false);

                    channel.ExchangeDeclare("exchange-direct", "direct");
                    string name = channel.QueueDeclare().QueueName;
                    channel.QueueBind(name, "exchange-direct", "routing-delay");
                    Console.WriteLine(name);
                    var consumer=new EventingBasicConsumer(channel);
                    //channel.BasicConsume("Demo.I", false, consumer);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(ea.RoutingKey);

                        Console.WriteLine("{0} Received {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            message);
                        Console.WriteLine("Done");
                    };
                    channel.BasicConsume(name, true, consumer);

                    //while (true)
                    //{
                        
                        
                    //}
                }
            }
        }

        private static void Listening()
        {
            RabbitMqClient.Instance.ActionEventMessage += MqClient_ActionEventMessage;
            RabbitMqClient.Instance.OnListening();
        }        

        private static void MqClient_ActionEventMessage(EventMessageResult result)
        {
            if (result.EventMessageBytes.EventMessageMarkcode == "Test")
            {
                var message = MessageSerializerFactory.CreateMessageSerializerInstance()
                    .Deserialize<UpdatePurchaseOrderStatusByBillIdMqContract>(result.MessageBytes);
                result.IsOperationOk = true;//处理成功
                Console.WriteLine(message.ModifiedBy);
            }
        }
    }
}
