using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bing.WebApi.Extensions
{
    /// <summary>
    /// Http配置 扩展
    /// </summary>
    public static class HttpConfigurationExtensions
    {
        /// <summary>
        /// 移除服务端对application/xml的支持
        /// </summary>
        /// <param name="config">Http配置</param>
        public static void RemoveXmlMediaTypeSupport(this HttpConfiguration config)
        {
            var appXmlType =
                config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(x =>
                    x.MediaType == "application/xml");
            if (appXmlType != null)
            {
                config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
            }
        }
    }
}
