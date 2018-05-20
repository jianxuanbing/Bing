using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using Bing.Configuration.Core;
using Bing.Extensions.Swagger.Configuration;
using Swashbuckle.Swagger;

namespace Bing.Extensions.Swagger.Filters
{
    /// <summary>
    /// 添加 Area 文档过滤器
    /// </summary>
    public class AddAreasSupportDocumentFilter:IDocumentFilter
    {
        /// <summary>
        /// 配置
        /// </summary>
        private readonly SwaggerConfiguration _config = ConfigProvider.Default.GetConfiguration<SwaggerConfiguration>();

        /// <summary>
        /// 重写Apply方法，加入Area文档处理
        /// </summary>
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            IDictionary<string,PathItem> replacePaths=new ConcurrentDictionary<string, PathItem>();
            foreach (var item in swaggerDoc.paths)
            {
                // api/Areas/Namespace.Controller/Action
                string key = item.Key;
                if (key.Contains(_config.JwtRoute))
                {
                    replacePaths.Add(item.Key,item.Value);
                    continue;                    
                }

                var value = item.Value;
                var keys = key.Split('/');

                // Areas路由：keys[0]:"",keys[1]:api,keys[2]:Areas,keys[3]:{ProjectName}.Api.Areas.{AreaName}.Controllers.{ControllerName}Controller,keys[4]:{ActionName}
                if (keys[3].IndexOf('.') != -1)
                {
                    // 区域路径
                    string areaName = keys[2];
                    string namespaceFullName = keys[3];
                    var directoryNames = namespaceFullName.Split('.');
                    string namespaceName = directoryNames[3];
                    if (areaName.Equals(namespaceName, StringComparison.OrdinalIgnoreCase))
                    {
                        string controllerName = directoryNames[5];
                        replacePaths.Add(
                            item.Key.Replace(namespaceFullName,
                                controllerName.Substring(0,
                                    controllerName.Length - DefaultHttpControllerSelector.ControllerSuffix.Length)),
                            value);
                    }
                }
                // 通用路由：keys[0]:"",keys[1]:api,keys[2]:{ProjectName}.Api.Controllers.{ControllerName}Controller,keys[3]:{ActionName}
                else if (keys[2].IndexOf('.') != -1)
                {
                    // 基础路径
                    string namespaceFullName = keys[2];
                    var directoryNames = namespaceFullName.Split('.');
                    bool isControllers = directoryNames[2].Equals("Controllers", StringComparison.OrdinalIgnoreCase);
                    string controllerName = directoryNames[3];
                    if (isControllers)
                    {
                        replacePaths.Add(
                            item.Key.Replace(namespaceFullName,
                                controllerName.Substring(0,
                                    controllerName.Length - DefaultHttpControllerSelector.ControllerSuffix.Length)), value);
                    }
                }

                swaggerDoc.paths = replacePaths;
            }
        }
    }
}
