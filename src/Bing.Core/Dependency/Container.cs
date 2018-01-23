using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core.Lifetime;
using Bing.Reflections;

namespace Bing.Dependency
{
    /// <summary>
    /// Autofac 对象容器
    /// </summary>
    internal class Container:IContainer
    {
        /// <summary>
        /// 容器
        /// </summary>
        private Autofac.IContainer _container;

        /// <summary>
        /// 作用域
        /// </summary>
        private Func<Autofac.IContainer, ILifetimeScope> _scope;

        /// <summary>
        /// 类型查找器
        /// </summary>
        private ITypeFinder _typeFinder;

        /// <summary>
        /// 作用域
        /// </summary>
        public ILifetimeScope Scope
        {
            get { return _scope.Invoke(_container); }
        }

        /// <summary>
        /// 内部容器
        /// </summary>
        public Autofac.IContainer InternalContainer
        {
            get { return _container; }
        }

        /// <summary>
        /// 设置类型查找器
        /// </summary>
        /// <param name="typeFinder">类型查找器</param>
        public void SetTypeFinder(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        /// <summary>
        /// 注册依赖
        /// </summary>
        protected void Register()
        {
            var builder=new ContainerBuilder();
            if (_typeFinder == null)
            {
                _typeFinder=new AppDomainTypeFinder();
            }
            builder.AddSingleton(_typeFinder);
            var drTypes = _typeFinder.Find<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
            {
                drInstances.Add((IDependencyRegistrar) Activator.CreateInstance(drType));
            }
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
            {
                dependencyRegistrar.Register(builder, _typeFinder);
            }

            var container = builder.Build();

            _container = container;
        }

        /// <summary>
        /// 获取默认作用域
        /// </summary>
        /// <param name="container">容器</param>
        /// <returns></returns>
        protected ILifetimeScope DefaultScope(Autofac.IContainer container)
        {
            return container;
        }

        /// <summary>
        /// 设置作用域
        /// </summary>
        /// <param name="type">作用域类型</param>
        internal void SetScope(ScopeType type)
        {
            switch (type)
            {
                case ScopeType.Http:
                    _scope = (e) =>
                    {
                        return e.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
                    };
                    break;
                default:
                    _scope = DefaultScope;
                    break;
            }
        }

        /// <summary>
        /// 初始化容器
        /// </summary>
        /// <param name="forecRecreate">是否强制创建容器</param>
        /// <param name="type">作用域类型</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Autofac.IContainer Initialize(bool forecRecreate, ScopeType type = ScopeType.None)
        {
            if (_container == null || forecRecreate)
            {
                Register();
            }
            SetScope(type);
            return _container;
        }

        /// <summary>
        /// 替换容器
        /// </summary>
        /// <param name="container">容器</param>
        /// <param name="type">作用域类型</param>
        public void Replace(Autofac.IContainer container, ScopeType type = ScopeType.None)
        {
            SetScope(type);
            _container = container;
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public T Create<T>(string name = null)
        {
            return (T) Create(typeof(T), name);
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public object Create(Type type, string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Scope.Resolve(type);
            }
            return Scope.ResolveNamed(name, type);
        }
           
        /// <summary>
        /// 创建集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public List<T> CreateList<T>(string name = null)
        {
            var result = CreateList(typeof(T), name);
            if (result == null)
            {
                return new List<T>();
            }
            return ((IEnumerable<T>) result).ToList();
        }

        /// <summary>
        /// 创建集合
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public object CreateList(Type type, string name = null)
        {
            Type serviceType = typeof(IEnumerable<>).MakeGenericType(type);
            return Create(serviceType, name);
        }

        /// <summary>
        /// 作用域开始
        /// </summary>
        /// <returns></returns>
        public IScope BeginScope()
        {
            return new Scope(_container.BeginLifetimeScope());
        }

        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="configs">依赖配置</param>
        public Autofac.IContainer Register(params IConfig[] configs)
        {
            return Register(null,configs);
        }

        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="actionBefore">注册前执行的操作</param>
        /// <param name="configs">依赖配置</param>
        public Autofac.IContainer Register(Action<ContainerBuilder> actionBefore, params IConfig[] configs)
        {
            var builder = CreateBuilder(actionBefore, configs);
            _container = builder.Build();
            return _container;
        }

        /// <summary>
        /// 创建容器生成器
        /// </summary>
        /// <param name="actionBefore">注册前执行的操作</param>
        /// <param name="configs">依赖配置</param>
        /// <returns></returns>
        public ContainerBuilder CreateBuilder(Action<ContainerBuilder> actionBefore, params IConfig[] configs)
        {
            var builder=new ContainerBuilder();
            actionBefore?.Invoke(builder);
            foreach (var config in configs)
            {
                builder.RegisterModule(config);
            }
            return builder;
        }

        /// <summary>
        /// 释放容器
        /// </summary>
        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
