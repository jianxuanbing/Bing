using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Bing.Contexts;
using Bing.Helpers;
using Bing.Reflections;
using Bing.Utils.Helpers;

namespace Bing.Dependency
{
    /// <summary>
    /// 依赖配置
    /// </summary>
    public class DependencyConfiguration
    {
        #region Property(属性)

        /// <summary>
        /// 依赖配置
        /// </summary>
        private readonly IConfig[] _configs;

        /// <summary>
        /// 容器生成器
        /// </summary>
        private ContainerBuilder _builder;

        /// <summary>
        /// 类型查找器
        /// </summary>
        private ITypeFinder _finder;

        /// <summary>
        /// 程序集列表
        /// </summary>
        private List<Assembly> _assemblies;

        #endregion

        #region Constructor(构造函数)

        /// <summary>
        /// 初始化一个<see cref="DependencyConfiguration"/>类型的实例
        /// </summary>
        /// <param name="configs">依赖配置</param>
        public DependencyConfiguration(IConfig[] configs)
        {
            _configs = configs;
        }

        #endregion

        /// <summary>
        /// 配置依赖
        /// </summary>
        /// <param name="scopeType">作用域类型</param>
        public void Config(ScopeType scopeType=ScopeType.None)
        {
            Ioc.DefaultContainer.Register(RegistServices, _configs);
            Ioc.DefaultContainer.SetScope(scopeType);
        }

        /// <summary>
        /// 注册服务集合
        /// </summary>
        /// <param name="builder">容器生成器</param>
        private void RegistServices(ContainerBuilder builder)
        {
            _builder = builder;
            _finder = new WebAppTypeFinder();
            _assemblies = _finder.GetAssemblies();
            RegistInfrastracture();
            RegistEventHandlers();
            RegistDependency();
        }

        #region 基础设施注册

        /// <summary>
        /// 注册基础设施
        /// </summary>
        private void RegistInfrastracture()
        {
            EnableAop();
            RegistFinder();
            RegistContext();
        }

        /// <summary>
        /// 启用Aop
        /// </summary>
        private void EnableAop()
        {
            //_builder.EnableAop();
        }

        /// <summary>
        /// 注册类型查找器
        /// </summary>
        private void RegistFinder()
        {
            _builder.AddSingleton(_finder);
        }

        /// <summary>
        /// 注册上下文
        /// </summary>
        private void RegistContext()
        {
            _builder.AddScoped<IContext, WebContext>();// nfx使用每次请求新实例，netcore则使用单例
            _builder.AddScoped<IUserContext, NullUserContext>();
        }

        #endregion

        #region 事件处理器注册

        /// <summary>
        /// 注册事件处理器
        /// </summary>
        private void RegistEventHandlers()
        {
            //var handlerTypes = GetTypes(typeof(IEventHandler<>));
            //foreach (var handler in handlerTypes)
            //{
            //    _builder.RegisterType(handler)
            //        .As(handler.FindInterfaces(
            //            (filter, criteria) => filter.IsGenericType &&
            //                                  ((Type)criteria).IsAssignableFrom(filter.GetGenericTypeDefinition()),
            //            typeof(IEventHandler<>)
            //        )).InstancePerLifetimeScope();
            //}
        }

        #endregion

        #region 依赖自动注册

        /// <summary>
        /// 查找并注册依赖
        /// </summary>
        private void RegistDependency()
        {
            RegistSingletonDependency();
            RegistScopeDependency();
            RegistTransientDependency();
            ResolveDependencyRegistrar();
        }

        /// <summary>
        /// 注册单例依赖
        /// </summary>
        private void RegistSingletonDependency()
        {
            _builder.RegisterTypes(GetTypes<ISingletonDependency>()).AsImplementedInterfaces().PropertiesAutowired().SingleInstance();
        }

        /// <summary>
        /// 注册作用域依赖
        /// </summary>
        private void RegistScopeDependency()
        {
            _builder.RegisterTypes(GetTypes<IScopeDependency>()).AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();
        }

        /// <summary>
        /// 注册瞬态依赖
        /// </summary>
        private void RegistTransientDependency()
        {
            _builder.RegisterTypes(GetTypes<ITransientDependency>()).AsImplementedInterfaces().PropertiesAutowired().InstancePerDependency();
        }

        /// <summary>
        /// 解析依赖注册器
        /// </summary>
        private void ResolveDependencyRegistrar()
        {
            var types = GetTypes<IDependencyRegistrar>();
            types.Select(type => Reflection.CreateInstance<IDependencyRegistrar>(type)).ToList()
                .ForEach(t => t.Register(_builder));
        }

        #endregion

        /// <summary>
        /// 获取类型集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        private Type[] GetTypes<T>()
        {
            return _finder.Find<T>(_assemblies).ToArray();
        }

        /// <summary>
        /// 获取类型集合
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        private Type[] GetTypes(Type type)
        {
            return _finder.Find(type, _assemblies).ToArray();
        }
    }
}
