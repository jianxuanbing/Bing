using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Dependency
{
    /// <summary>
    /// 依赖注入 管理器
    /// </summary>
    public class IocManager
    {
        /// <summary>
        /// 容器
        /// </summary>
        internal static IIocContainer IocContainer;

        /// <summary>
        /// 设置 容器
        /// </summary>
        /// <param name="container">容器</param>
        public static void SetContainer(IIocContainer container)
        {
            if (IocContainer == null)
            {
                IocContainer = container;
            }
        }

        /// <summary>
        /// 获取 容器
        /// </summary>
        /// <returns></returns>
        public static IIocContainer GetContainer()
        {
            return IocContainer;
        }
    }
}
