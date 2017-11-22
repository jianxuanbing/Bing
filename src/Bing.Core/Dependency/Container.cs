using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core.Lifetime;
using Bing.Core.Reflections;

namespace Bing.Core.Dependency
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

        public Container(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        /// <summary>
        /// 注册依赖
        /// </summary>
        protected void Register()
        {
            var builder=new ContainerBuilder();
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
        /// <param name="type"></param>
        protected void SetScope(ScopeType type)
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
        /// 释放容器
        /// </summary>
        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
