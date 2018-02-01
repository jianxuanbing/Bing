using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Extensions;
using Bing.Utils.Json;
using Bing.Utils.Parameters.Formats;

namespace Bing.Utils.Parameters
{
    /// <summary>
    /// 参数生成器
    /// </summary>
    public class ParameterBuilder
    {
        /// <summary>
        /// 参数集合
        /// </summary>
        private readonly IDictionary<string, string> _params;

        /// <summary>
        /// 初始化一个<see cref="ParameterBuilder"/>类型的实例
        /// </summary>
        public ParameterBuilder()
        {
            _params=new Dictionary<string, string>();
        }

        /// <summary>
        /// 初始化一个<see cref="ParameterBuilder"/>类型的实例
        /// </summary>
        /// <param name="builder">参数生成器</param>
        public ParameterBuilder(ParameterBuilder builder) : this(builder.GetDictionary())
        {
        }

        /// <summary>
        /// 初始化一个<see cref="ParameterBuilder"/>类型的实例
        /// </summary>
        /// <param name="dictionary">字典</param>
        public ParameterBuilder(IDictionary<string, string> dictionary)
        {
            _params = dictionary == null
                ? new Dictionary<string, string>()
                : new Dictionary<string, string>(dictionary);
        }

        /// <summary>
        /// 添加参数，如果参数已存在则替换
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public ParameterBuilder Add(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return this;
            }

            string strValue = GetValue(value).Trim();
            if (string.IsNullOrWhiteSpace(strValue))
            {
                return this;
            }

            key = key.Trim();
            if (_params.ContainsKey(key))
            {
                _params[key] = strValue;
            }
            else
            {
                _params.Add(key,strValue);
            }

            return this;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="value">对象值</param>
        /// <returns></returns>
        private string GetValue(object value)
        {            
            if (value is DateTime)
            {
                var dateTime = (DateTime)value;
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (value is bool)
            {
                var boolValue = (bool)value;
                return boolValue.ToString().ToLower();
            }

            return value.SafeString();
        }

        /// <summary>
        /// 获取字典
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetDictionary()
        {
            return _params;
        }

        /// <summary>
        /// 获取键值对结合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs()
        {
            return _params;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            _params.Clear();
        }

        /// <summary>
        /// 移除参数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return _params.Remove(key);
        }

        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonUtil.ToJson(_params);
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <param name="format">参数格式化器</param>
        /// <param name="isSort">是否按参数名排序</param>
        /// <returns></returns>
        public string Result(IParameterFormat format, bool isSort = false)
        {
            if (format == null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            var result = string.Empty;
            foreach (var param in GetDictionary(isSort))
            {
                result = format.Join(result, format.Format(param.Key, param.Value));
            }

            return result;
        }

        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="isSort">是否按参数名排序</param>
        /// <returns></returns>
        private IDictionary<string, string> GetDictionary(bool isSort)
        {
            if (isSort == false)
            {
                return _params;
            }
            return new SortedDictionary<string, string>(_params);
        }
    }
}
