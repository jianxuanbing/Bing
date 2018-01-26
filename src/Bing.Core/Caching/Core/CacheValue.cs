using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.Core
{
    /// <summary>
    /// 缓存值
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class CacheValue<T>
    {
        /// <summary>
        /// 获取 值
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// 获取 一个值，指示当前的<see cref="CacheValue{T}"/>对象是否有值。
        /// </summary>
        public bool HasValue { get; }

        /// <summary>
        /// 获取 一个值，指示当前的<see cref="CacheValue{T}"/>对象是否为空。
        /// </summary>
        public bool IsNull => Value == null;

        /// <summary>
        /// 获取 一个空对象
        /// </summary>
        public static CacheValue<T> Null { get; } = new CacheValue<T>(default(T), true);

        /// <summary>
        /// 获取 一个空值
        /// </summary>
        public static CacheValue<T> NoValue { get; } = new CacheValue<T>(default(T), false);

        /// <summary>
        /// 初始化一个<see cref="CacheValue{T}"/>类型的实例
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="hasValue">是否有值</param>
        public CacheValue(T value, bool hasValue)
        {
            Value = value;
            HasValue = hasValue;
        }

        /// <summary>
        /// 返回表示当前object的string。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value?.ToString() ?? "<null>";
        }
    }
}
