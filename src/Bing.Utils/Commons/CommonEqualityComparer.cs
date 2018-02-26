using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Commons
{
    /// <summary>
    /// 通用比较器
    /// </summary>
    /// <typeparam name="T">要比较的实体类型</typeparam>
    /// <typeparam name="TValue">要比较实体中的具体属性类型</typeparam>
    public class CommonEqualityComparer<T,TValue>:IEqualityComparer<T>
    {
        /// <summary>
        /// 比较属性的选择器
        /// </summary>
        private Func<T, TValue> _keySelector;

        /// <summary>
        /// 自定义的属性比较器
        /// </summary>
        private IEqualityComparer<TValue> _comparer;

        /// <summary>
        /// 初始化一个<see cref="CommonEqualityComparer{T,TValue}"/>类型的实例
        /// </summary>
        /// <param name="keySelector">比较属性的选择器</param>
        /// <param name="comparer">自定义的属性比较器</param>
        public CommonEqualityComparer(Func<T, TValue> keySelector, IEqualityComparer<TValue> comparer)
        {
            _keySelector = keySelector;
            _comparer = comparer;
        }

        /// <summary>
        /// 初始化一个<see cref="CommonEqualityComparer{T,TValue}"/>类型的实例
        /// </summary>
        /// <param name="keySelector">比较属性的选择器</param>
        public CommonEqualityComparer(Func<T, TValue> keySelector) : this(keySelector, EqualityComparer<TValue>.Default)
        {
        }

        /// <summary>
        /// 确定指定的对象是否相等
        /// </summary>
        /// <param name="x">对象1</param>
        /// <param name="y">对象2</param>
        /// <returns></returns>
        public bool Equals(T x, T y)
        {
            return _comparer.Equals(_keySelector(x), _keySelector(y));
        }

        /// <summary>
        /// 获取指定对象的哈希代码
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public int GetHashCode(T obj)
        {
            return _comparer.GetHashCode(_keySelector(obj));
        }
    }
}
