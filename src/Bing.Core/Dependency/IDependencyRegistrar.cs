using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Bing.Core.Reflections;

namespace Bing.Core.Dependency
{
    /// <summary>
    /// 依赖注册器
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="builder">容器生成器</param>
        /// <param name="typeFinder">类型查找器</param>
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        /// <summary>
        /// 排序
        /// </summary>
        int Order { get; }
    }
}
