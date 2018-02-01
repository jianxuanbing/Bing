using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Bing.Utils.Parameters.Formats;

namespace Bing.Utils.Parameters
{
    /// <summary>
    /// Url参数生成器
    /// </summary>
    public class UrlParameterBuilder
    {
        /// <summary>
        /// 参数生成器
        /// </summary>
        private ParameterBuilder ParameterBuilder { get; }

        /// <summary>
        /// 初始化一个<see cref="UrlParameterBuilder"/>类型的实例
        /// </summary>
        public UrlParameterBuilder():this("")
        {

        }

        /// <summary>
        /// 初始化一个<see cref="UrlParameterBuilder"/>类型的实例
        /// </summary>
        /// <param name="builder">Url参数生成器</param>
        public UrlParameterBuilder(UrlParameterBuilder builder):this("",builder)
        {

        }

        /// <summary>
        /// 初始化一个<see cref="UrlParameterBuilder"/>类型的实例
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="builder">Url参数生成器</param>
        public UrlParameterBuilder(string url, UrlParameterBuilder builder = null)
        {
            ParameterBuilder =
                builder == null ? new ParameterBuilder() : new ParameterBuilder(builder.ParameterBuilder);
            LoadUrl(url);
        }

        /// <summary>
        /// 加载Url
        /// </summary>
        /// <param name="url">Url</param>
        public void LoadUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }

            if (url.Contains("?"))
            {
                url = url.Substring(url.IndexOf("?", StringComparison.Ordinal) + 1);
            }

            var parameters = HttpUtility.ParseQueryString(url);
            foreach (var key in parameters.AllKeys)
            {
                Add(key, parameters.Get(key));
            }
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public UrlParameterBuilder Add(string key, object value)
        {
            ParameterBuilder.Add(key, value);
            return this;
        }

        /// <summary>
        /// 获取字典
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetDictionary()
        {
            return ParameterBuilder.GetDictionary();
        }

        /// <summary>
        /// 获取键值对集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, string>> GetKeyValuePairs()
        {
            return ParameterBuilder.GetKeyValuePairs();
        }

        /// <summary>
        /// 获取结果，格式：参数名=参数值&amp;参数名=参数值
        /// </summary>
        /// <param name="isSort">是否按参数名排序</param>
        /// <returns></returns>
        public string Result(bool isSort = false)
        {
            return ParameterBuilder.Result(UrlParameterFormat.Instance, isSort);
        }

        /// <summary>
        /// 连接Url
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns></returns>
        public string JoinUrl(string url)
        {
            return $"{GetUrl(url)}{Result()}";
        }

        /// <summary>
        /// 获取Url
        /// </summary>
        /// <param name="url">Url</param>
        /// <returns></returns>
        private string GetUrl(string url)
        {
            if (!url.Contains("?"))
            {
                return $"{url}?";
            }

            if (url.EndsWith("?"))
            {
                return url;
            }

            if (url.EndsWith("&"))
            {
                return url;
            }

            return $"{url}&";
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            ParameterBuilder.Clear();
        }

        /// <summary>
        /// 移除参数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return ParameterBuilder.Remove(key);
        }
    }
}
