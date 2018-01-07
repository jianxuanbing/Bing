using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.Caching.Redis.UnitTest
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void TestMethod1()
        {
            RedisCache cache=new RedisCache();
            cache.InitializeAsync(new Dictionary<string, string>()
            {
                {"Endpoint", "192.168.99.100:32768"},
                {"UseSsl", "False"}
            });
            cache.AddAsync<string>("test1", "123456111", TimeSpan.FromMinutes(10));
            cache.AddAsync<string>("test2", "123456", TimeSpan.FromMinutes(10));
            cache.AddAsync<string>("test3", "123456", TimeSpan.FromMinutes(10));
            cache.AddAsync<string>("test4", "123456", TimeSpan.FromMinutes(10));
            cache.AddAsync<string>("test5", "123456", TimeSpan.FromMinutes(10));
            cache.AddAsync<string>("test6", "123456", TimeSpan.FromMinutes(10));
            cache.AddAsync<string>("test7", "123456", TimeSpan.FromMinutes(10));

            var result=cache.GetAsync<string>("test1");
            Console.WriteLine(result.Result);
        }

        [TestMethod]
        public void Test_Other_Init()
        {
            
            for (int i = 0; i < 100000; i++)
            {
                RedisCache cache = new RedisCache();
                cache.Initialize(new RedisEndpoint()
                {
                    Host = "192.168.99.100",
                    Port = 32768,
                    MaxSize = 200
                });
                cache.AddAsync<string>("test" + i, "神奇的傻逗" + i, TimeSpan.FromMinutes(10));
                var result = cache.GetAsync<string>("test" + i);
                //Console.WriteLine(result.Result);
            }
            Console.WriteLine("OK");
            //RedisCache cache = new RedisCache();
            //cache.Initialize(new RedisEndpoint()
            //{
            //    Host = "192.168.99.100",
            //    Port = 32768
            //});
            //cache.AddAsync<string>("test", "神奇的傻逗", TimeSpan.FromMinutes(10));
            //var result = cache.GetAsync<string>("test");
            //Console.WriteLine(result.Result);
        }

        [TestMethod]
        public void Test_Muilte()
        {
            RedisManager.SetDefaultConnectionStr(new RedisEndpoint()
            {
                Host = "192.168.99.100",
                Port = 32768,
            });
            Random random=new Random();
            for (int i = 0; i < 10000; i++)
            {
                RedisClient client = new RedisClient();
                client.StringSet("tt" + i, "装逼的傻逗" + random.Next());                
            }            
        }
    }
}
