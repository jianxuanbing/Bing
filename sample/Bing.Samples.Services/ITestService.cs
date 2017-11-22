using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Core.Dependency;

namespace Bing.Samples.Services
{
    public interface ITestService: IScopeDependency
    {
        string GetContent(string content);
    }
}
