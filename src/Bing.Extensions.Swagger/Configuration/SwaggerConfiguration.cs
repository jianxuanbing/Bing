using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bing.Extensions.Swagger.Configuration
{
    /// <summary>
    /// Swagger 配置
    /// </summary>
    public class SwaggerConfiguration
    {
        /// <summary>
        /// WebApi 命名空间
        /// </summary>
        public string ApiNamespace { get; set; }

        /// <summary>
        /// 命名空间索引
        /// </summary>
        [JsonIgnore]
        public int NamespaceIndex => ApiNamespace.Split('.').Length;

        /// <summary>
        /// 控制器目录名称
        /// </summary>
        public string ControllerDirectoryName { get; set; }

        /// <summary>
        /// 域类型备注
        /// </summary>
        public string ScopeDesc { get; set; }

        /// <summary>
        /// Jwt 路由。默认：/api/oauth2/token
        /// </summary>
        public string JwtRoute { get; set; } = "/api/oauth2/token";

        /// <summary>
        /// 默认控制器名。默认：v1
        /// </summary>
        public string DefaultController { get; set; } = "v1";
    }
}
