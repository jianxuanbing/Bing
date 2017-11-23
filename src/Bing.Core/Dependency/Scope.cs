using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Bing.Dependency
{
    /// <summary>
    /// 作用域
    /// </summary>
    internal class Scope:IScope
    {
        #region Property(属性)

        /// <summary>
        /// Autofac作用域
        /// </summary>
        private readonly ILifetimeScope _scope;

        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="Scope"/>类型的实例
        /// </summary>
        /// <param name="scope">Autofac作用域</param>
        public Scope(ILifetimeScope scope)
        {
            _scope = scope;
        }
        #endregion

        /// <summary>
        /// 释放对象
        /// </summary>
        public void Dispose()
        {
            _scope.Dispose();
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        public T Create<T>(string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return _scope.Resolve<T>();
            }
            return _scope.ResolveNamed<T>(name);
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
                return _scope.Resolve(type);
            }
            return _scope.ResolveNamed(name, type);
        }
    }
}
