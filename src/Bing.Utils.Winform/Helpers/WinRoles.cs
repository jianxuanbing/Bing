using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Winform.Helpers
{
    /// <summary>
    /// Windows 角色 操作
    /// </summary>
    public class WinRoles
    {
        /// <summary>
        /// 是否超级管理员
        /// </summary>
        /// <returns></returns>
        public static bool IsAdministrator()
        {
            WindowsIdentity current=WindowsIdentity.GetCurrent();
            WindowsPrincipal windowsPrincipal=new WindowsPrincipal(current);
            return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
