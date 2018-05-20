using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Bing.Extensions.Swagger.Attributes;
using Swashbuckle.Swagger;

namespace Bing.Extensions.Swagger.Filters
{
    /// <summary>
    /// 添加 上传操作过滤
    /// </summary>
    public class AddUploadOperationFilter:IOperationFilter
    {
        /// <summary>
        /// 重写Apply方法，加入Upload操作过滤
        /// </summary>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var upload = apiDescription.ActionDescriptor.GetCustomAttributes<UploadAttribute>().FirstOrDefault();
            if (upload == null)
            {
                return;
            }
            operation.consumes.Add("application/form-data");
            operation.parameters.Add(new Parameter()
            {
                name = upload.Name,
                @in = "formData",
                required = upload.Require,
                type = "file",
                description = upload.Description
            });
        }
    }
}
