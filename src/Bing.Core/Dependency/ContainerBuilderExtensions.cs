using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;

namespace Bing.Core.Dependency
{
    /// <summary>
    /// Autofac 容器生成器 扩展
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        #region AddTransient(注册服务，生命周期为 InstancePerDependency - 每次创建一个新实例)

        /// <summary>
        /// 注册服务，生命周期为 InstancePerDependency(每次创建一个新实例)
        /// </summary>
        /// <typeparam name="TService">接口类型</typeparam>
        /// <typeparam name="TImplementation">实现类型</typeparam>
        /// <param name="builder">容器生成器</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public static IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            AddTransient<TService, TImplementation>(this ContainerBuilder builder, string name = null)
            where TService : class where TImplementation : class, TService
        {
            if (name == null)
            {
                return builder.RegisterType<TImplementation>().As<TService>().InstancePerDependency();
            }
            return builder.RegisterType<TImplementation>().Named<TService>(name).InstancePerDependency();
        }

        #endregion

        #region AddScoped(注册服务，生命周期为 InstancePerLifetimeScope - 每个请求一个实例)

        /// <summary>
        /// 注册服务，生命周期为 InstancePerLifetimeScope(每个请求一个实例)
        /// </summary>
        /// <typeparam name="TService">接口类型</typeparam>
        /// <typeparam name="TImplementation">实现类型</typeparam>
        /// <param name="builder">容器生成器</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public static IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            AddScoped<TService, TImplementation>(this ContainerBuilder builder, string name = null)
            where TService : class where TImplementation : class, TService
        {
            if (name == null)
            {
                return builder.RegisterType<TImplementation>().As<TService>().InstancePerLifetimeScope();
            }
            return builder.RegisterType<TImplementation>().Named<TService>(name).InstancePerLifetimeScope();
        }

        /// <summary>
        /// 注册服务，生命周期为 InstancePerLifetimeScope(每个请求一个实例)
        /// </summary>
        /// <typeparam name="TImplementation">实现类型</typeparam>
        /// <param name="builder">容器生成器</param>
        /// <returns></returns>
        public static IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            AddScoped<TImplementation>(this ContainerBuilder builder) where TImplementation : class
        {
            return builder.RegisterType<TImplementation>().InstancePerLifetimeScope();
        }

        #endregion

        #region AddSingleton(注册服务，生命周期为 SingleInstance - 单例)

        /// <summary>
        /// 注册服务，生命周期为 SingleInstance(单例)
        /// </summary>
        /// <typeparam name="TService">接口类型</typeparam>
        /// <typeparam name="TImplementation">实现类型</typeparam>
        /// <param name="builder">容器生成器</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public static IRegistrationBuilder<TImplementation, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            AddSingleton<TService, TImplementation>(this ContainerBuilder builder, string name = null)
            where TService : class where TImplementation : class, TService
        {
            if (name == null)
            {
                return builder.RegisterType<TImplementation>().As<TService>().SingleInstance();
            }
            return builder.RegisterType<TImplementation>().Named<TService>(name).SingleInstance();
        }

        /// <summary>
        /// 注册服务，生命周期为 SingleInstance(单例)
        /// </summary>
        /// <typeparam name="TService">接口类型</typeparam>
        /// <param name="builder">容器生成器</param>
        /// <param name="instance">服务实例</param>
        /// <returns></returns>
        public static IRegistrationBuilder<TService, SimpleActivatorData, SingleRegistrationStyle>
            AddSingleton<TService>(this ContainerBuilder builder, TService instance)
            where TService : class
        {
            return builder.RegisterInstance(instance).As<TService>().SingleInstance();
        }

        #endregion
    }
}
