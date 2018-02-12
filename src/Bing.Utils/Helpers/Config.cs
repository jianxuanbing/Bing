using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Extensions;

namespace Bing.Utils.Helpers
{
    /// <summary>
    /// 配置文件信息操作
    /// </summary>
    public static class Config
    {
        #region GetAppSettings(获取AppSettings)

        /// <summary>
        /// 获取AppSettings
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetAppSettings(string key)
        {
            key.CheckNotNull(nameof(key));
            return ConfigurationManager.AppSettings[key].SafeString().Trim();
        }        

        #endregion

        #region SetAppSettings(设置AppSettings)

        /// <summary>
        /// 设置AppSettings
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="configPath">配置文件路径</param>
        public static void SetAppSettings(string key, string value, string configPath = "")
        {
            Configuration config = string.IsNullOrWhiteSpace(configPath)
                ? ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                : ConfigurationManager.OpenExeConfiguration(configPath);

            var settings = config.AppSettings.Settings;
            if (settings.AllKeys.Contains(key))
            {
                settings[key].Value = value;
            }
            else
            {
                settings.Add(key, value);
            }
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        #endregion

        #region RemoveAppSettings(移除AppSettings)

        /// <summary>
        /// 移除AppSettings
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="configPath">配置文件路径</param>
        public static void RemoveAppSettings(string key, string configPath = "")
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }
            Configuration config = string.IsNullOrWhiteSpace(configPath)
                ? ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                : ConfigurationManager.OpenExeConfiguration(configPath);

            if (config.AppSettings?.Settings[key] == null)
            {
                return;
            }

            config.AppSettings.Settings.Remove(key);
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        #endregion

        #region GetConnectionString(获取数据库连接字符串)

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {
            key.CheckNotNull(nameof(key));
            return ConfigurationManager.ConnectionStrings[key].ConnectionString.SafeString().Trim();
        }

        #endregion

        #region SetConnectionString(设置数据库连接字符串)

        /// <summary>
        /// 设置数据库连接字符串
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="connectionStr">数据库连接字符串</param>
        /// <param name="providerName">数据提供程序名称</param>
        /// <param name="configPath">配置文件路径</param>
        public static void SetConnectionString(string key, string connectionStr, string providerName = "",string configPath="")
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }
            Configuration config = string.IsNullOrWhiteSpace(configPath)
                ? ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                : ConfigurationManager.OpenExeConfiguration(configPath);
            var connectionSettings = config.ConnectionStrings.ConnectionStrings;
            if (connectionSettings[key] != null)
            {
                connectionSettings[key].ConnectionString = connectionStr;
                if (!string.IsNullOrWhiteSpace(providerName))
                {
                    connectionSettings[key].ProviderName = providerName;
                }
            }
            else
            {
                connectionSettings.Add(new ConnectionStringSettings(key, connectionStr, providerName));
            }
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        #endregion

        #region GetProviderName(获取数据提供程序名称)

        /// <summary>
        /// 获取数据提供程序名称
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public static string GetProviderName(string key)
        {
            key.CheckNotNull(nameof(key));
            return ConfigurationManager.ConnectionStrings[key].ProviderName.SafeString().Trim();
        }

        #endregion        
    }
}
