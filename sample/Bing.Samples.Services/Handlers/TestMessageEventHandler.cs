using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bing.Caching.Redis;
using Bing.Events.Handlers;
using Bing.Logs.Aspects;
using Bing.Samples.Services.Messages;

namespace Bing.Samples.Services.Handlers
{
    public class TestMessageEventHandler:IEventHandler<TestMessageEvent>
    {
        private IRedisCacheProvider _redisCacheProvider;
        private IRedisClient _redisClient;

        public TestMessageEventHandler(IRedisCacheProvider redisCacheProvider)
        {
            _redisCacheProvider = redisCacheProvider;
            _redisClient = _redisCacheProvider.GetClient();
        }

        [DebugLog]
        public void Handle(TestMessageEvent @event)
        {
            Thread.Sleep(3000);
            Debug.WriteLine("测试输出内容");
            _redisClient.StringSet("EventHandle:" + @event.Id, @event);
        }
    }
}
