using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Dependency;
using Bing.Logs.Aspects;

namespace Bing.Samples.Consoles
{
    /// <summary>
    /// 测试服务
    /// </summary>
    public interface ITestService:IScopeDependency
    {
        [DebugLog]
        void WriteContent(string content);
    }
}
