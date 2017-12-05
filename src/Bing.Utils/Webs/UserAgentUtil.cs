using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Webs
{
    /// <summary>
    /// 用户代理 辅助操作类
    /// </summary>
    public static class UserAgentUtil
    {
        #region GetOperationSystemName(根据UserAgent获取操作系统名称)

        /// <summary>
        /// 根据 UserAgent 获取操作系统名称
        /// </summary>
        /// <param name="userAgent">用户代理</param>
        /// <returns></returns>
        public static string GetOperationSystemName(string userAgent)
        {
            if (userAgent.Contains("NT 6.1"))
            {
                return "Windows 7";
            }
            if (userAgent.Contains("NT 6.0"))
            {
                return "Windows Vista/Server 2008";
            }
            if (userAgent.Contains("NT 5.2"))
            {
                return "Windows Server 2003";
            }
            if (userAgent.Contains("NT 5.1"))
            {
                return "Windows XP";
            }
            if (userAgent.Contains("NT 5"))
            {
                return "Windows 2000";
            }
            if (userAgent.Contains("Mac"))
            {
                return "Mac";
            }
            if (userAgent.Contains("Unix"))
            {
                return "UNIX";
            }
            if (userAgent.Contains("Linux"))
            {
                return "Linux";
            }
            if (userAgent.Contains("SunOS"))
            {
                return "SunOS";
            }
            return "Other OperationSystem";
        }

        #endregion

        #region GetBrowserName(根据UserAgent获取浏览器名称)

        /// <summary>
        /// 根据 UserAgent 获取浏览器名称
        /// </summary>
        /// <param name="userAgent">用户代理</param>
        /// <returns></returns>
        public static string GetBrowserName(string userAgent)
        {
            if (userAgent.Contains("Maxthon"))
            {
                return "遨游浏览器";
            }
            if (userAgent.Contains("MetaSr"))
            {
                return "搜狗高速浏览器";
            }
            if (userAgent.Contains("BIDUBrowser"))
            {
                return "百度浏览器";
            }
            if (userAgent.Contains("QQBrowser"))
            {
                return "QQ浏览器";
            }
            if (userAgent.Contains("GreenBrowser"))
            {
                return "Green浏览器";
            }
            if (userAgent.Contains("360se"))
            {
                return "360安全浏览器";
            }
            if (userAgent.Contains("MSIE 6.0"))
            {
                return "Internet Explorer 6.0";
            }
            if (userAgent.Contains("MSIE 7.0"))
            {
                return "Internet Explorer 7.0";
            }
            if (userAgent.Contains("MSIE 8.0"))
            {
                return "Internet Explorer 8.0";
            }
            if (userAgent.Contains("MSIE 9.0"))
            {
                return "Internet Explorer 9.0";
            }
            if (userAgent.Contains("MSIE 10.0"))
            {
                return "Internet Explorer 10.0";
            }
            if (userAgent.Contains("Firefox"))
            {
                return "Firefox";
            }
            if (userAgent.Contains("Opera"))
            {
                return "Opera";
            }
            if (userAgent.Contains("Chrome"))
            {
                return "Chrome";
            }
            if (userAgent.Contains("Safari"))
            {
                return "Safari";
            }
            return "Other Browser";
        }

        #endregion

    }
}
