using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Validators
{
    /// <summary>
    /// 基于数据字典的验证基类
    /// </summary>
    /// <typeparam name="TResult">验证结果类型</typeparam>
    /// <typeparam name="TKey">字典键</typeparam>
    /// <typeparam name="TValue">字典值</typeparam>
    public abstract class ValidatorWithDictionaryBase<TResult,TKey,TValue>:ValidatorBase<TResult> where TResult:ValidationResult,new()
    {
        /// <summary>
        /// 验证字典
        /// </summary>
        private IValidatonDictionary<TKey, TValue> _dictionary;

        /// <summary>
        /// 默认验证字典
        /// </summary>
        protected abstract IValidatonDictionary<TKey,TValue> DefaultDictionary { get; }

        /// <summary>
        /// 获取或设置 验证字典
        /// </summary>
        public IValidatonDictionary<TKey, TValue> Dictionary
        {
            get { return this._dictionary ?? DefaultDictionary; }
            set { this._dictionary = value; }
        }
    }
}
