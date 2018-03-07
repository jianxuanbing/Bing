using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Helpers;

namespace Bing.Utils.Webs
{
    /// <summary>
    /// Url 操作类
    /// </summary>
    public static class UrlUtil
    {
        /// <summary>
        /// 是否本地Url地址
        /// </summary>
        /// <param name="requestUri">Uri地址</param>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static bool IsLocalUrl(Uri requestUri, string url)
        {
            Check.CheckNotNull(requestUri,nameof(requestUri));
            Check.CheckNotNull(url,nameof(url));

            return IsRelativeLocalUrl(url) || url.StartsWith(GetLocalUrlRoot(requestUri));
        }

        /// <summary>
        /// 获取本地Url地址根目录
        /// </summary>
        /// <param name="requestUri">Uri地址</param>
        /// <returns></returns>
        private static string GetLocalUrlRoot(Uri requestUri)
        {
            var root = requestUri.Scheme + "://" + requestUri.Host;
            if ((string.Equals(requestUri.Scheme, "http", StringComparison.OrdinalIgnoreCase) &&
                 requestUri.Port != 80) ||
                (string.Equals(requestUri.Scheme, "https", StringComparison.OrdinalIgnoreCase) &&
                 requestUri.Port != 443))
            {
                root += ":" + requestUri.Port;
            }

            return root;
        }

        /// <summary>
        /// 是否本地相对Url地址
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static bool IsRelativeLocalUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            if ((int) url[0] == 47 && (url.Length == 1 || (int) url[1] != 47 && (int) url[1] != 92))
            {
                return true;
            }

            if (url.Length > 1 && (int) url[0] == 126)
            {
                return (int) url[1] == 47;
            }

            return false;
        }
    }
}
