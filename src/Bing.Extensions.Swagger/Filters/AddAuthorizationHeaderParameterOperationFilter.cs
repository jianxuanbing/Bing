using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;
using Swashbuckle.Swagger;

namespace Bing.Extensions.Swagger.Filters
{
    /// <summary>
    /// 添加 授权认证请求头参数操作过滤
    /// </summary>
    public class AddAuthorizationHeaderParameterOperationFilter:IOperationFilter
    {
        /// <summary>
        /// 重写Apply方法，加入授权请求头参数过滤
        /// </summary>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
            {
                operation.parameters=new List<Parameter>();
            }

            // 判断是否添加权限过滤器
            var filterPileline = apiDescription.ActionDescriptor.GetFilterPipeline();
            var isAuthorized = filterPileline.Select(filterInfo => filterInfo.Instance)
                .Any(filter => filter is IAuthenticationFilter);
            // 判断是否匿名方法
            var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();

            if (isAuthorized && !allowAnonymous)
            {
                operation.parameters.Add(new Parameter()
                {
                    name = "Authorization",
                    @in="header",
                    description = "访问令牌",
                    type = "string",
                    @default = "Bearer ",
                    required = false
                });
            }
        }
    }
}
