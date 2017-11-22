using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Helpers
{
    /// <summary>
    /// 字符串操作 - 工具
    /// </summary>
    public partial class Str
    {
        #region Join(将集合连接为带分隔符的字符串)
        /// <summary>
        /// 将集合连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号"'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        /// <returns></returns>
        public static string Join<T>(IEnumerable<T> list, string quotes = "", string separator = ",")
        {
            if (list == null)
            {
                return string.Empty;
            }
            var result = new StringBuilder();
            foreach (var each in list)
            {
                result.AppendFormat("{0}{1}{0}{2}", quotes, each, separator);
            }
            if (separator == "")
            {
                return result.ToString();
            }
            return result.ToString().TrimEnd(separator.ToCharArray());
        }

        #endregion


    }
}
