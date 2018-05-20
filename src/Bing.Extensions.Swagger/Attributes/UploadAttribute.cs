using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Extensions.Swagger.Attributes
{
    /// <summary>
    /// 上传属性，用于标识接口是否包含上传信息参数
    /// </summary>
    public class UploadAttribute:Attribute
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public string Name { get; set; } = "file";

        /// <summary>
        /// 是否必须包含文件
        /// </summary>
        public bool Require { get; set; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; } = "";
    }
}
