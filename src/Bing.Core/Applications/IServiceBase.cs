using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Dependency;

namespace Bing.Applications
{
    /// <summary>
    /// 应用服务基类接口
    /// </summary>
    public interface IServiceBase:IScopeDependency
    {
    }

    /// <summary>
    /// 应用服务基类接口
    /// </summary>
    /// <typeparam name="TDto">数据传输对象类型</typeparam>
    public interface IServiceBase<TDto> : IServiceBase where TDto : new()
    {
    }
}
