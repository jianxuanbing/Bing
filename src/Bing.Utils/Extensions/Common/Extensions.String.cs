using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bing.Utils.Extensions
{
    /// <summary>
    /// 系统扩展 - 字符串
    /// </summary>
    public static partial class Extensions
    {
        #region MapPath(返回与 Web 服务器上的指定虚拟路径相对应的物理文件路径)

        /// <summary>
        /// 返回与 Web 服务器上的指定虚拟路径相对应的物理文件路径。
        /// </summary>
        /// <param name="basePath">基础路径</param>
        /// <param name="path">相对路径</param>
        /// <returns></returns>
        public static string MapPath(this string basePath, string path)
        {
            return System.IO.Path.Combine(basePath, path);
        }

        #endregion

        #region IsMatch(是否匹配正则表达式)
        /// <summary>
        /// 确定所指定的正则表达式在指定的输入字符串中是否找到了匹配项
        /// </summary>
        /// <param name="value">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false</returns>
        public static bool IsMatch(this string value, string pattern)
        {
            if (value == null)
            {
                return false;
            }
            return Regex.IsMatch(value, pattern);
        }
        /// <summary>
        /// 确定所指定的正则表达式在指定的输入字符串中找到匹配项
        /// </summary>
        /// <param name="value">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <param name="options">规则</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false</returns>
        public static bool IsMatch(this string value, string pattern, RegexOptions options)
        {
            if (value == null)
            {
                return false;
            }
            return Regex.IsMatch(value, pattern, options);
        }
        #endregion

        #region GetMatch(获取匹配项)
        /// <summary>
        /// 在指定的输入字符串中搜索指定的正则表达式的第一个匹配项
        /// </summary>
        /// <param name="value">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns>一个对象，包含有关匹配项的信息</returns>
        public static string GetMatch(this string value, string pattern)
        {
            if (value.IsEmpty())
            {
                return string.Empty;
            }
            return Regex.Match(value, pattern).Value;
        }

        /// <summary>
        /// 在指定的输入字符串中搜索指定的正则表达式的所有匹配项的字符串集合
        /// </summary>
        /// <param name="value">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns> 一个集合，包含有关匹配项的字符串值</returns>
        public static IEnumerable<string> GetMatchingValues(this string value, string pattern)
        {
            if (value.IsEmpty())
            {
                return new string[] { };
            }
            return GetMatchingValues(value, pattern, RegexOptions.None);
        }
        /// <summary>
        /// 使用正则表达式来确定一个给定的正则表达式模式的所有匹配的字符串返回的枚举
        /// </summary>
        /// <param name="value">输入字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="options">比较规则</param>
        /// <returns>匹配字符串的枚举</returns>
        public static IEnumerable<string> GetMatchingValues(this string value, string pattern, RegexOptions options)
        {
            return from Match match in GetMatches(value, pattern, options) where match.Success select match.Value;
        }
        /// <summary>
        /// 使用正则表达式来确定指定的正则表达式模式的所有匹配项
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="options">比较规则</param>
        /// <returns></returns>
        public static MatchCollection GetMatches(this string value, string pattern, RegexOptions options)
        {
            return Regex.Matches(value, pattern, options);
        }
        #endregion
    }
}
