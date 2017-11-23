using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bing.Contexts
{
    /// <summary>
    /// Web上下文
    /// </summary>
    public class WebContext : IContext
    {
        /// <summary>
        /// 跟踪号
        /// </summary>
        public string TraceId { get; }

        /// <summary>
        /// 初始化一个<see cref="WebContext"/>类型的实例
        /// </summary>
        public WebContext()
        {
            TraceId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">键名</param>
        /// <param name="value">对象</param>
        public void Add<T>(string key, T value)
        {
            if (HttpContext.Current == null)
            {
                return;
            }
            HttpContext.Current.Items[key] = value;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            if (HttpContext.Current == null)
            {
                return default(T);
            }
            return Utils.Helpers.Conv.To<T>(HttpContext.Current.Items[key]);
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="key">键名</param>
        public void Remove(string key)
        {
            if (HttpContext.Current == null)
            {
                return;
            }
            HttpContext.Current.Items.Remove(key);
        }
    }
}
