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
    public static partial class Web
    {
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
                catch
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
            var ip = GetWebProxyRealIp() ?? GetWebRemoteIp();
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
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取Web代理真实IP
        /// </summary>
        /// <returns></returns>
        private static string GetWebProxyRealIp()
        {
            if (HttpContext.Current?.Request == null)
            {
                return string.Empty;
            }
            var request = HttpContext.Current.Request;
            
            string ip = request.Headers.Get("x-forwarded-for");

            if (string.IsNullOrEmpty(ip) || string.Equals("unknown", ip, StringComparison.OrdinalIgnoreCase))
            {
                ip = request.Headers.Get("Proxy-Client-IP");
            }

            if (string.IsNullOrEmpty(ip) || string.Equals("unknown", ip, StringComparison.OrdinalIgnoreCase))
            {
                ip = request.Headers.Get("WL-Proxy-Client-IP");
            }

            if (string.IsNullOrEmpty(ip) || string.Equals("unknown", ip, StringComparison.OrdinalIgnoreCase))
            {
                ip = request.UserHostAddress;
            }

            if (string.IsNullOrEmpty(ip))
            {
                return string.Empty;
            }
            // 可能存在如下格式：X-Forwarded-For: client, proxy1, proxy2
            if (ip.Contains(", "))
            {
                // 如果存在多个反向代理，获得的IP是一个用逗号分隔的IP集合，取第一个
                // X-Forwarded-For: client  第一个
                string[] ips = ip.Split(new string[1] {", "}, StringSplitOptions.RemoveEmptyEntries);
                var i = 0;
                for (i = 0; i < ips.Length; i++)
                {
                    if (ips[i] != "")
                    {
                        // 判断是否为内网IP
                        if (false == IsInnerIp(ips[i]))
                        {
                            IPAddress realIp;
                            if (IPAddress.TryParse(ips[i], out realIp) && ips[i].Split('.').Length == 4)
                            {
                                //合法IP
                                return ips[i];
                            }

                            return "";
                        }
                    }
                }

                ip = ips[0];// 默认获取第一个IP地址
            }

            return ip;

        }

        /// <summary>
        /// 判断IP地址是否为内网IP地址
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns></returns>
        private static bool IsInnerIp(string ip)
        {
            bool isInnerIp = false;
            ulong ipNum = Ip2Ulong(ip);

            /*
             * 私有IP
             * A类：10.0.0.0-10.255.255.255
             * B类：172.16.0.0-172.31.255.255
             * C类：192.168.0.0-192.168.255.255
             * 当然，还有127这个网段是环回地址
             */

            ulong aBegin = Ip2Ulong("10.0.0.0");
            ulong aEnd = Ip2Ulong("10.255.255.255");
            ulong bBegin = Ip2Ulong("172.16.0.0");
            ulong bEnd = Ip2Ulong("10.31.255.255");
            ulong cBegin = Ip2Ulong("192.168.0.0");
            ulong cEnd = Ip2Ulong("192.168.255.255");

            isInnerIp = IsInner(ipNum, aBegin, aEnd) || IsInner(ipNum, bBegin, bEnd) || IsInner(ipNum, cBegin, cEnd) ||
                        ip.Equals("127.0.0.1");
            return isInnerIp;
        }

        /// <summary>
        /// 将IP地址转换为Long型数字
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns></returns>
        private static ulong Ip2Ulong(string ip)
        {
            byte[] bytes = IPAddress.Parse(ip).GetAddressBytes();
            ulong ret = 0;
            foreach (var b in bytes)
            {
                ret <<= 8;
                ret |= b;
            }

            return ret;
        }

        /// <summary>
        /// 判断用户IP地址转换为Long型后是否在内网IP地址所在范围
        /// </summary>
        /// <param name="userIp">用户IP</param>
        /// <param name="begin">开始范围</param>
        /// <param name="end">结束范围</param>
        /// <returns></returns>
        private static bool IsInner(ulong userIp, ulong begin, ulong end)
        {
            return (userIp >= begin) && (userIp <= end);
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
            try
            {
                var result = Dns.GetHostEntry(IPAddress.Parse(address)).HostName;
                if (result == "localhost.localdomain")
                {
                    result = Dns.GetHostName();
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(address,e);
            }
            
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
                catch
                {
                    return string.Empty;
                }
                
            }
        }

        #endregion
    }
}
