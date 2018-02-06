using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Validators
{
    /// <summary>
    /// 验证字典，验证用的基础数据字典接口
    /// </summary>
    /// <typeparam name="TKey">键</typeparam>
    /// <typeparam name="TValue">值</typeparam>
    public interface IValidatonDictionary<TKey,TValue>
    {
        /// <summary>
        /// 获取验证字典
        /// </summary>
        /// <returns></returns>
        IDictionary<TKey, TValue> GetDictionary();
    }
}
