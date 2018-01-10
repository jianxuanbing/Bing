using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.Caching.Redis.UnitTest
{
    [TestClass]
    public class RedisClientTest
    {
        private RedisClient _client;
        [TestInitialize]
        public void Init()
        {
            RedisManager.SysCustomKey = "Test:";
            RedisManager.SetDefaultConnectionStr(new RedisEndpoint()
            {
                Host = "192.168.88.22",
                Port = 6379,
            });
            _client=new RedisClient();
        }

        [TestMethod]
        public void Test_Hash()
        {
            _client.HashSet("th0", "2017", "0101");
            _client.HashSet("th0", "2018", "0102");
            _client.HashSet("th0", "2019", "0103");
        }

        [TestMethod]
        public void Test_HashKeys()
        {
            var result=_client.HashGetAllKeys("th0");
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        [TestMethod]
        public void Test_HashIncrement()
        {
            for (int i = 0; i < 100; i++)
            {
                var result = _client.HashIncrement("ti0", "2017");
                Console.WriteLine(result);
            }
            
        }

        [TestMethod]
        public void Test_ListRightPush()
        {
            _client.ListRightPush("lrp0","20180101");
            _client.ListRightPush("lrp0", "20180102");
            _client.ListRightPush("lrp0", "20180103");
            _client.ListRightPush("lrp0", "20180104");
            _client.ListRightPush("lrp0", "20180105");
        }

        [TestMethod]
        public void Test_ListRightPop()
        {
            var result=_client.ListRightPop<string>("lrp0");            
            Console.WriteLine(result);
        }

        [TestMethod]
        public void Test_ListRemove()
        {
            _client.ListRemove("lrp0", "20180102");
        }
    }
}
