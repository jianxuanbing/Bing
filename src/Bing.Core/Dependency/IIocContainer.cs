using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Dependency
{
    /// <summary>
    /// 容器
    /// </summary>
    public interface IIocContainer
    {

        /// <summary>
        /// 注册容器引擎
        /// </summary>
        /// <param name="container">容器</param>
        void UseEngine(object container);

        /// <summary>
        /// 获取容器引擎
        /// </summary>
        /// <typeparam name="T">容器引擎类型</typeparam>
        /// <returns></returns>
        T GetEngine<T>();

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        T Create<T>();

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="type">类型</param>
        /// <returns></returns>
        T Create<T>(Type type);

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="argumentsAsAnonymousType">匿名参数类型</param>
        /// <returns></returns>
        T Create<T>(object argumentsAsAnonymousType);

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        T CreateNamed<T>(string serviceName);

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        object Create(Type type);

        /// <summary>
        /// 该类型是否已注册
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        bool IsRegistered(Type type);

        /// <summary>
        /// 该类型是否已注册
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        bool IsRegistered<T>();

        void Register<T>(LifetimeStyle lifetimeStyle = LifetimeStyle.Singleton, string serviceName = null,
            bool isDefulat = false) where T:class;

        void Register(Type type, LifetimeStyle lifetimeStyle = LifetimeStyle.Singleton, string serviceName = null,
            bool propertiesAutowired = true, bool isDefault = false);

        void Register<T>(T impl, bool propertiesAutowired = true, bool isDefault = false) where T : class;

        void Register<TService, TImplementation>(LifetimeStyle lifetimeStyle = LifetimeStyle.Singleton,
            string serviceName = null, bool propertiesAutowired = true, bool isDefault = false) where TService : class
            where TImplementation : class, TService;

        void Register<TService, TImplementation>(TImplementation implementation, bool propertiesAutowired = true,
            bool isDefault = false) where TService : class where TImplementation : class, TService;

        void Register(Type service, Type implementation, LifetimeStyle lifetimeStyle = LifetimeStyle.Singleton,
            string serviceName = null, bool propertiesAutowired = true, bool isDefault = false);
    }
}
