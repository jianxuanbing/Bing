using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Bing.Caching;
using Bing.Caching.Aspects;
using Bing.Caching.Redis;
using Bing.Samples.Domains.Models;
using Bing.Samples.Services;

namespace Bing.Samples.Api.Controllers
{
    /// <summary>
    /// Redis缓存测试
    /// </summary>
    public class RedisCacheTestController:ApiController
    {
        private IRedisCacheProvider _cacheProvider;

        private ITestService _testService;



        public RedisCacheTestController(IRedisCacheProvider cacheProvider,ITestService testService)
        {
            _cacheProvider = cacheProvider;
            _testService = testService;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        [CachingAble]
        public string Get(int type = 1)
        {
            if (type == 1)
            {
                _cacheProvider.Remove("demo");
                return "removed";
            }
            if (type == 2)
            {
                _cacheProvider.Set("demo","123456",TimeSpan.FromMinutes(1));
                return "seted";
            }
            if (type == 3)
            {
                var res=_cacheProvider.Get("demo",()=>"9394",TimeSpan.FromMinutes(1));
                return $"cached value: {res}";
            }
            return "error";
        }

        [HttpGet]
        public List<ItemResult> GetItems()
        {
            return _testService.GetItems();
        }

        [HttpGet]
        public List<string> GetAllKeys(string key)
        {
            var client = _cacheProvider.GetClient();

            return client.GetAllKeys(key);
        }
    }
}