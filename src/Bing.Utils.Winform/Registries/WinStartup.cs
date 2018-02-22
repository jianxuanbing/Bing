using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Winform.Registries
{
    /// <summary>
    /// Windows 启动项 操作
    /// </summary>
    public class WinStartup
    {
        /// <summary>
        /// 启动并运行 Windows 启动项
        /// </summary>
        /// <param name="target">应用程序目标路径</param>
        /// <param name="name">应用程序名称，只能是字母</param>
        /// <returns></returns>
        public static bool Enable(string target, string name)
        {
            if (string.IsNullOrWhiteSpace(target) || string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            try
            {
                Microsoft.Win32.RegistryKey lmrk = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey srk = lmrk.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run",
                    true);
                srk.SetValue(name, target, Microsoft.Win32.RegistryValueKind.String);
                object obj = srk.GetValue(name, null);
                if (obj == null || string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 禁用并停止 Windwos 启动项
        /// </summary>
        /// <param name="name">应用程序名称，只能是字母</param>
        /// <returns></returns>
        public static bool Disable(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            try
            {
                Microsoft.Win32.RegistryKey lmrk = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey srk = lmrk.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run",
                    true);
                srk.DeleteValue(name,true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取Windows 启动项状态
        /// </summary>
        /// <param name="name">应用程序名称，只能是字母</param>
        /// <returns></returns>
        public static bool GetStatus(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            try
            {
                Microsoft.Win32.RegistryKey lmrk = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey srk = lmrk.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run",
                    true);

                object obj = srk.GetValue(name, null);
                if (obj == null || string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
