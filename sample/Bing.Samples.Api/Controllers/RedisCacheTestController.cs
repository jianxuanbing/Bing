using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Bing.Caching.Abstractions;

namespace Bing.Samples.Api.Controllers
{
    /// <summary>
    /// Redis缓存测试
    /// </summary>
    public class RedisCacheTestController:ApiController
    {
        private ICacheProvider _cacheProvider;

        public RedisCacheTestController(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
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
    }
}