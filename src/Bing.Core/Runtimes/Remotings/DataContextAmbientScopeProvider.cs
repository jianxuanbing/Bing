using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using Bing.Dependency;
using Bing.Logs;
using Bing.Logs.Core;
using Bing.Utils;
using Bing.Utils.Exceptions;
using Bing.Utils.Extensions;
using Bing.Utils.Helpers;

namespace Bing.Runtimes.Remotings
{
    /// <summary>
    /// 数据上下文 环境范围 提供程序
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class DataContextAmbientScopeProvider<T>:IAmbientScopeProvider<T>
    {
        /// <summary>
        /// 日志提供程序
        /// </summary>
        public ILog Logger { get; set; }

        /// <summary>
        /// 范围字典
        /// </summary>
        private static readonly ConcurrentDictionary<string,ScopeItem> ScopeDictionary=new ConcurrentDictionary<string, ScopeItem>();

        /// <summary>
        /// 数据上下文
        /// </summary>
        private readonly IAmbientDataContext _dataContext;

        /// <summary>
        /// 初始化一个<see cref="DataContextAmbientScopeProvider{T}"/>类型的实例
        /// </summary>
        /// <param name="dataContext">数据上下文</param>
        public DataContextAmbientScopeProvider([NotNull]IAmbientDataContext dataContext)
        {
            Check.CheckNotNull(dataContext, nameof(dataContext));
            _dataContext = dataContext;
            Logger=NullLog.Instance;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="contextKey">上下文键</param>
        /// <returns></returns>
        public T GetValue(string contextKey)
        {
            var item = GetCurrentItem(contextKey);
            if (item == null)
            {
                return default(T);
            }
            return item.Value;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="contextKey">上下文键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public IDisposable BeginScope(string contextKey, T value)
        {
            var item = new ScopeItem(value, GetCurrentItem(contextKey));

            if (!ScopeDictionary.TryAdd(item.Id, item))
            {
                throw new Warning("Can not add item! ScopeDictionary.TryAdd returns false!");
            }

            _dataContext.SetData(contextKey, item.Id);

            return new DisposeAction(() =>
            {
                ScopeDictionary.TryRemove(item.Id, out item);

                if (item.Outer == null)
                {
                    _dataContext.SetData(contextKey, null);
                    return;
                }
                _dataContext.SetData(contextKey, item.Outer.Id);
            });
        }

        /// <summary>
        /// 获取当前项
        /// </summary>
        /// <param name="contextKey">上下文键</param>
        /// <returns></returns>
        private ScopeItem GetCurrentItem(string contextKey)
        {
            var objKey = _dataContext.GetData(contextKey) as string;
            return objKey != null ? ScopeDictionary.GetOrDefault(objKey) : null;
        }

        /// <summary>
        /// 作用域项
        /// </summary>
        private class ScopeItem
        {
            /// <summary>
            /// ID
            /// </summary>
            public string Id { get; }

            /// <summary>
            /// 输出者
            /// </summary>
            public ScopeItem Outer { get; }

            /// <summary>
            /// 值
            /// </summary>
            public T Value { get; }

            /// <summary>
            /// 输出者
            /// </summary>
            /// <param name="value">值</param>
            /// <param name="outer">输出者</param>
            public ScopeItem(T value, ScopeItem outer = null)
            {
                Id = Guid.NewGuid().ToString();
                Value = value;
                Outer = outer;
            }
        }
    }
}
