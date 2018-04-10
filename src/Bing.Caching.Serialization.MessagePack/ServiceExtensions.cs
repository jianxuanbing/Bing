using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Bing.Caching.Abstractions;
using Bing.Dependency;

namespace Bing.Caching.Serialization.MessagePack
{
    /// <summary>
    /// 序列化 扩展
    /// </summary>
    public static partial class ServiceExtensions
    {
        /// <summary>
        /// 注册默认 MessagePack 序列化器
        /// </summary>
        /// <param name="services">服务</param>
        public static void AddDefaultMessagePackSerializer(this ContainerBuilder services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<ICacheSerializer, DefaultMessagePackSerializer>();
        }
    }
}
