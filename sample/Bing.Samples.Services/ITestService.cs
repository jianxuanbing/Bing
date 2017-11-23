using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Aspects;
using Bing.Dependency;
using Bing.Logs.Aspects;

namespace Bing.Samples.Services
{
    public interface ITestService: IScopeDependency
    {
        string GetContent([NotEmpty]string content);

        [DebugLog]
        void WriteOtherLog(string content);
    }
}
