using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Applications.Dtos
{
    /// <summary>
    /// 结果码
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        [Description("请求成功")]
        Success = 0,

        /// <summary>
        /// 失败-业务错误
        /// </summary>
        [Description("失败")]
        Fail = 1,

        /// <summary>
        /// 找不到数据
        /// </summary>
        [Description("找不到数据")]
        NoResultFound = 2,

        /// <summary>
        /// 发生错误-系统错误
        /// </summary>
        [Description("发生错误")]
        Error = -1,

        /// <summary>
        /// 业务错误-登录超时
        /// </summary>
        [Description("登录超时")]
        LoginTimeout = 40001

    }
}
