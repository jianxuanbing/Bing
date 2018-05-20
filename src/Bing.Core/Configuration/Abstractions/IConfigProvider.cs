using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Configuration.Abstractions
{
    /// <summary>
    /// 配置 提供程序
    /// </summary>
    public interface IConfigProvider
    {
        /// <summary>
        /// 获取配置对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        T GetConfiguration<T>() where T : class, new();

        /// <summary>
        /// 设置配置对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="entity">对象</param>
        void SetConfiguration<T>(T entity) where T : class, new();

        /// <summary>
        /// 刷新配置
        /// </summary>
        void RefreshAll();
    }
}
