using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Aspects;
using Bing.Dependency;

namespace Bing.Samples.Services
{
    public interface ITestService: IScopeDependency
    {
        string GetContent([NotEmpty]string content);

        void WriteOtherLog(string content);
    }
}
