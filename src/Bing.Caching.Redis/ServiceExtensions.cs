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
        /// <summary>
        /// 添加默认Redis缓存
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="optionsAction">配置</param>
        public static void AddDefaultRedisCache(this ContainerBuilder services, Action<RedisCacheOptions> optionsAction)
        {
            services.CheckNotNull(nameof(services));
            optionsAction.CheckNotNull(nameof(optionsAction));

            var options=new RedisCacheOptions();
            optionsAction.Invoke(options);
            services.AddSingleton<ICacheSerializer, DefaultJsonFormatterSerializer>();
            services.AddSingleton<IRedisDatabaseProvider, RedisDatabaseProvider>();
            services.AddSingleton(options);
            services.AddSingleton<IRedisCacheProvider, DefaultRedisCacheProvider>();
            services.AddSingleton<ICacheProvider, DefaultRedisCacheProvider>();
        }
    }
}
