using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Core.Dependency
{
    /// <summary>
    /// 实现此接口的类型将注册为<see cref="LifetimeStyle.Scoped"/>模式
    /// </summary>
    public interface IScopeDependency:IDependency
    {
    }
}
