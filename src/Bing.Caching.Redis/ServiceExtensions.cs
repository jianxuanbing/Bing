using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Bing.Caching.Abstractions;
using Bing.Dependency;
using Bing.Utils.Extensions;

namespace Bing.Caching.Redis
{
    /// <summary>
    /// Redis缓存服务 扩展
    /// </summary>
    public static partial class ServiceExtensions
    {
        public static void AddDefaultRedisCache(this ContainerBuilder services, Action<RedisCacheOptions> optionsAction)
        {
            services.CheckNotNull(nameof(services));
            optionsAction.CheckNotNull(nameof(optionsAction));

            var options=new RedisCacheOptions();
            optionsAction.Invoke(options);
            services.AddSingleton<ICacheSerializer, DefaultBinaryFormatterSerializer>();
            services.AddSingleton<IRedisDatabaseProvider, RedisDatabaseProvider>();
            services.AddSingleton(options);
            services.AddSingleton<ICacheProvider, DefaultRedisCacheProvider>();
        }
    }
}
