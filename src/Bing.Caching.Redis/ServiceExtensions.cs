using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Bing.Caching.Abstractions;
using Bing.Caching.Default;
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
        /// 注册默认Redis缓存
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="optionsAction">配置</param>
        public static void AddDefaultRedisCache(this ContainerBuilder services, Action<RedisCacheOptions> optionsAction)
        {
            services.CheckNotNull(nameof(services));
            optionsAction.CheckNotNull(nameof(optionsAction));

            var options=new RedisCacheOptions();
            optionsAction.Invoke(options);

            services.AddSingleton(options);
            services.AddSingleton<ICacheSerializer, DefaultBinaryFormatterSerializer>();
            services.AddSingleton<IRedisDatabaseProvider, RedisDatabaseProvider>();
            services.AddSingleton<ICachingKeyGenerator, DefaultCachingKeyGenerator>();

            // 重复注入是否有问题?
            services.AddSingleton<IRedisCacheProvider, DefaultRedisCacheProvider>();
            services.AddSingleton<ICacheProvider, DefaultRedisCacheProvider>();
        }

        /// <summary>
        /// 注册默认 混合Redis缓存
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="optionsAction">配置</param>
        public static void AddDefaultRedisCacheForHybrid(this ContainerBuilder services,
            Action<RedisCacheOptions> optionsAction)
        {
            services.CheckNotNull(nameof(services));
            optionsAction.CheckNotNull(nameof(optionsAction));

            var options = new RedisCacheOptions();
            optionsAction.Invoke(options);
            services.AddSingleton(options);
            
            services.AddSingleton<ICacheSerializer, DefaultBinaryFormatterSerializer>();
            services.AddSingleton<IRedisDatabaseProvider, RedisDatabaseProvider>();
            services.AddSingleton<ICachingKeyGenerator, DefaultCachingKeyGenerator>();
            services.RegisterType<DefaultRedisCacheProvider>().SingleInstance();
        }

    }
}
