using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Contexts
{
    /// <summary>
    /// 空用户上下文
    /// </summary>
    public class NullUserContext : IUserContext
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId => string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName =>string.Empty;

        /// <summary>
        /// 租户ID
        /// </summary>
        public string TenantId => string.Empty;

        /// <summary>
        /// 获取上下文信息
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public T GetContextInfo<T>(string key)
        {
            return default(T);
        }
    }
}
