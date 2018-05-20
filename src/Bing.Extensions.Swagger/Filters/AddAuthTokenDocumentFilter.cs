using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Bing.Configuration.Core;
using Bing.Extensions.Swagger.Configuration;
using Swashbuckle.Swagger;

namespace Bing.Extensions.Swagger.Filters
{
    /// <summary>
    /// 添加 认证令牌 文档过滤器
    /// </summary>
    public class AddAuthTokenDocumentFilter:IDocumentFilter
    {
        /// <summary>
        /// 配置
        /// </summary>
        private readonly SwaggerConfiguration _config = ConfigProvider.Default.GetConfiguration<SwaggerConfiguration>();

        /// <summary>
        /// 重写Apply方法，实现授权Token
        /// </summary>
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.paths.Add(_config.JwtRoute,new PathItem()
            {
                post=new Operation()
                {
                    tags = new List<string>() { "Auth"},
                    consumes = new List<string>()
                    {
                        "application/x-www-form-urlencoded"
                    },
                    parameters = new List<Parameter>()
                    {
                        new Parameter()
                        {
                            name = "username",
                            @in = "formData",
                            required = true,
                            type = "string",
                            description = "账号"
                        },
                        new Parameter()
                        {
                            name = "password",
                            @in = "formData",
                            @default = null,
                            type = "string",
                            required = false,
                            description = "密码"
                        },
                        new Parameter()
                        {
                            name = "grant_type",
                            @in = "formData",
                            @default = "password",
                            required = true,
                            type = "string",
                            description = "授权类型,默认password"
                        },
                        new Parameter()
                        {
                            name="scope",
                            @in="formData",
                            required = true,
                            type = "Array[string]",
                            description =_config.ScopeDesc
                        }
                    }
                }
            });
        }
    }
}
