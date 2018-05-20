using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Bing.Configuration.Core;
using Bing.Extensions.Swagger.Configuration;
using Swashbuckle.Swagger;

namespace Bing.Extensions.Swagger.Providers
{
    /// <summary>
    /// Swagger缓存提供程序
    /// </summary>
    public class SwaggerCachingProvider:ISwaggerProvider
    {
        /// <summary>
        /// 缓存字典
        /// </summary>
        private static ConcurrentDictionary<string, SwaggerDocument> _cache = new ConcurrentDictionary<string, SwaggerDocument>();

        /// <summary>
        /// Swagger提供程序
        /// </summary>
        private readonly ISwaggerProvider _swaggerProvider;

        /// <summary>
        /// 配置
        /// </summary>
        private static readonly SwaggerConfiguration _config= ConfigProvider.Default.GetConfiguration<SwaggerConfiguration>();

        /// <summary>
        /// 初始化一个<see cref="SwaggerCachingProvider"/>类型的实例
        /// </summary>
        /// <param name="swaggerProvider">Swagger提供程序</param>
        public SwaggerCachingProvider(ISwaggerProvider swaggerProvider)
        {
            _swaggerProvider = swaggerProvider;
        }

        /// <summary>
        /// 获取Swagger文档
        /// </summary>
        /// <param name="rootUrl">路由</param>
        /// <param name="apiVersion">api版本</param>
        /// <returns></returns>
        public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
        {
            var cacheKey = string.Format("{0}_{1}", rootUrl, apiVersion);
            SwaggerDocument srcDoc = null;
            if (!_cache.TryGetValue(cacheKey, out srcDoc))
            {
                srcDoc = _swaggerProvider.GetSwagger(rootUrl, apiVersion);
                srcDoc.vendorExtensions =
                    new Dictionary<string, object>()
                    {
                        {"ControllerDesc", GetControllerDesc(_config.DefaultController, apiVersion)}
                    };
                _cache.TryAdd(cacheKey, srcDoc);
            }
            return srcDoc;
        }

        /// <summary>
        /// 从API文档中获取控制器描述
        /// </summary>
        /// <param name="apiPath">api路径</param>
        /// <param name="apiVersion">api版本</param>
        /// <returns></returns>
        private static ConcurrentDictionary<string, string> GetControllerDesc(string apiPath, string apiVersion)
        {
            ConcurrentDictionary<string, string> controllerDescDict = new ConcurrentDictionary<string, string>();
            string areaName = string.Empty;
            if (File.Exists(apiPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(apiPath);
                string type = string.Empty, path = string.Empty, controllerName = string.Empty;

                string[] arrPath;
                List<string> arrPathList;
                int length = -1, cCount = "Controller".Length;
                XmlNode summaryNode = null;
                foreach (XmlNode node in xmlDoc.SelectNodes("//member"))
                {
                    type = node.Attributes["name"].Value;
                    if (type.StartsWith("T:"))
                    {
                        //控制器
                        arrPath = type.Split('.');
                        arrPathList = arrPath.ToList();
                        length = arrPath.Length;
                        controllerName = arrPath[length - 1];
                        if (controllerName.EndsWith("Controller"))
                        {
                            // 区域处理
                            if (type.Contains("Areas"))
                            {
                                areaName = arrPath[arrPathList.IndexOf("Areas") + 1];
                            }
                            else
                            {
                                areaName = string.Empty;
                            }

                            //获取控制器注释
                            summaryNode = node.SelectSingleNode("summary");
                            string key = controllerName.Remove(controllerName.Length - cCount, cCount);
                            if (summaryNode != null && !string.IsNullOrEmpty(summaryNode.InnerText) &&
                                !controllerDescDict.ContainsKey(key))
                            {
                                if (apiVersion == _config.DefaultController)
                                {
                                    if (string.IsNullOrWhiteSpace(areaName))
                                    {
                                        controllerDescDict.TryAdd(key, summaryNode.InnerText.Trim());
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(areaName) && areaName.Equals(apiVersion,
                                            StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        controllerDescDict.TryAdd(key, summaryNode.InnerText.Trim());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return controllerDescDict;
        }
    }
}
