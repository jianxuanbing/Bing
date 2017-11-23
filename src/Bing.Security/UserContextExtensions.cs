using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Contexts;

namespace Bing.Security
{
    /// <summary>
    /// 用户上下文 扩展
    /// </summary>
    public static partial class UserContextExtensions
    {
        /// <summary>
        /// 获取当前应用程序
        /// </summary>
        /// <param name="context">用户上下文</param>
        /// <returns></returns>
        public static string GetApplication(this IUserContext context)
        {
            return "";
        }

        /// <summary>
        /// 获取当前租户
        /// </summary>
        /// <param name="context">用户上下文</param>
        /// <returns></returns>
        public static string GetTenant(this IUserContext context)
        {
            return "";
        }

        /// <summary>
        /// 获取当前操作姓名
        /// </summary>
        /// <param name="context">用户上下文</param>
        /// <returns></returns>
        public static string GetFullName(this IUserContext context)
        {
            return "";
        }

        /// <summary>
        /// 获取当前操作人角色名
        /// </summary>
        /// <param name="context">用户上下文</param>
        /// <returns></returns>
        public static string GetRoleName(this IUserContext context)
        {
            return "";
        }
    }
}
