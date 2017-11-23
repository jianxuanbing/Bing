using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Dependency;

namespace Bing
{
    /// <summary>
    /// IOC配置初始化
    /// </summary>
    public class IocConfigInitialize
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="scopeType">作用域类型</param>
        /// <param name="configs">依赖配置</param>
        public static void Init(ScopeType scopeType = ScopeType.None, params IConfig[] configs)
        {
            new DependencyConfiguration(configs).Config(scopeType);
        }
    }
}
