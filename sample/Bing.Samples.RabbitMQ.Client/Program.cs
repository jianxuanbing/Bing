using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.MQ.RabbitMQ;
using Bing.MQ.RabbitMQ.Events;
using RabbitMQ.Client;

namespace Bing.Samples.RabbitMQ.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SendEventMessage();
            //SendTestMessage(args);
            Console.ReadLine();
            
        }

        private static void SendTestMessage(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "192.168.88.22";
            factory.UserName = "jian";
            factory.Password = "123456";

            using (var connection=factory.CreateConnection())
            {
                using (var channel=connection.CreateModel())
                {
                    Dictionary<string,object> dic=new Dictionary<string, object>();
                    dic.Add("x-expires",30000);
                    dic.Add("x-message-ttl",12000);
                    dic.Add("x-dead-letter-exchange","exchange-direct");
                    dic.Add("x-dead-letter-routing-key","routing-delay");

                    channel.QueueDeclare("Demo.I", true, false, false, dic);

                    string message = GetMessage(args);
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2;

                    var body = Encoding.UTF8.GetBytes(message);
                    //for (int i = 0; i < 10; i++)
                    //{
                    //    channel.BasicPublish("", "Demo.I", properties, body);
                    //}
                    channel.BasicPublish("", "Demo.I", properties, body);
                    Console.WriteLine("set {0}",message);
                }
            }
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0)) ? string.Join(" ", args) : "Hello World!";
        }

        private static void SendEventMessage()
        {
            var originObject = new UpdatePurchaseOrderStatusByBillIdMqContract()
            {
                UpdatePurchaseOrderStatusType = 1,
                RelationBillType = 10,
                RelationBillId = 10016779,
                UpdateStatus = 30,
                ModifiedBy = 100
            };

            var sendMessage = EventMessageFactory.CreateEventMessageInstance(originObject, "Test");            
            RabbitMqClient.Instance.TriggerEventMessage(sendMessage, "", "Demo.J");
            //for (int i = 0; i < 100; i++)
            //{
            //    var originObject = new UpdatePurchaseOrderStatusByBillIdMqContract()
            //    {
            //        UpdatePurchaseOrderStatusType = 1,
            //        RelationBillType = 10,
            //        RelationBillId = 10016779,
            //        UpdateStatus = 30,
            //        ModifiedBy = i
            //    };

            //    var sendMessage = EventMessageFactory.CreateEventMessageInstance(originObject, "Test");
            //    RabbitMqClient.Instance.TriggerEventMessage(sendMessage, "Demo.Purchase", "Demo.Purchase");
            //}

        }
    }
}
