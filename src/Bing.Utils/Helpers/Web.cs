using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bing.Utils.Helpers
{
    /// <summary>
    /// Web操作
    /// </summary>
    public static class Web
    {


        #region Client(客户端)
        ///// <summary>
        ///// Web客户端，用于发送Http请求
        ///// </summary>
        ///// <returns></returns>
        //public static JCE.Utils.Webs.Clients.WebClient Client()
        //{
        //    return new JCE.Utils.Webs.Clients.WebClient();
        //}

        ///// <summary>
        ///// Web客户端，用于发送Http请求
        ///// </summary>
        ///// <typeparam name="TResult">返回的结果类型</typeparam>
        ///// <returns></returns>
        //public static JCE.Utils.Webs.Clients.WebClient<TResult> Client<TResult>() where TResult : class
        //{
        //    return new JCE.Utils.Webs.Clients.WebClient<TResult>();
        //}

        #endregion

        #region Url(请求地址)

        /// <summary>
        /// 请求地址
        /// </summary>
        public static string Url
        {
            get
            {
                try
                {
                    return HttpContext.Current.Request.Url.ToString();
                }
                catch (Exception e)
                {
                    return string.Empty;
                }
            } 
        } 

        #endregion

        #region Ip(客户端IP地址)

        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public static string Ip
        {
            get
            {
                var result = string.Empty;
                if (HttpContext.Current != null)
                {
                    result = GetWebClientIp();
                }
                if (string.IsNullOrWhiteSpace(result))
                {
                    result = GetLanIp();
                }
                return result;
            }
        }

        /// <summary>
        /// 获取Web客户端的IP
        /// </summary>
        /// <returns></returns>
        private static string GetWebClientIp()
        {
            var ip = GetWebRemoteIp();
            foreach (var hostAddress in Dns.GetHostAddresses(ip))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostAddress.ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取Web远程IP
        /// </summary>
        /// <returns></returns>
        private static string GetWebRemoteIp()
        {
            try
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                       HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] ?? "";
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取局域网IP
        /// </summary>
        /// <returns></returns>
        private static string GetLanIp()
        {
            foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostAddress.ToString();
                }
            }
            return string.Empty;
        }

        #endregion

        #region Host(主机)

        /// <summary>
        /// 主机
        /// </summary>
        public static string Host => HttpContext.Current == null ? Dns.GetHostName() : GetClientHostName();

        /// <summary>
        /// 获取Web客户端主机名
        /// </summary>
        /// <returns></returns>
        private static string GetClientHostName()
        {
            var address = GetWebRemoteIp();
            if (string.IsNullOrWhiteSpace(address))
            {
                return Dns.GetHostName();
            }
            var result = Dns.GetHostEntry(IPAddress.Parse(address)).HostName;
            if (result == "localhost.localdomain")
            {
                result = Dns.GetHostName();
            }
            return result;
        }

        #endregion

        #region Browser(浏览器)

        /// <summary>
        /// 浏览器
        /// </summary>
        public static string Browser
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                try
                {
                    var browser = HttpContext.Current.Request.Browser;
                    return string.Format("{0} {1}", browser.Browser, browser.Version);
                }
                catch (Exception e)
                {
                    return string.Empty;
                }
                
            }
        }

        #endregion
    }
}
