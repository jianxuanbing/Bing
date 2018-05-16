using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Applications.Dtos
{
    /// <summary>
    /// 登录信息
    /// </summary>
    /// <typeparam name="T">实体标识类型</typeparam>
    public interface ILoginInfo<T>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        T UserId { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        string Login { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
    }

    /// <summary>
    /// 登录信息
    /// </summary>
    public interface ILoginInfo : ILoginInfo<Guid> { }
}
