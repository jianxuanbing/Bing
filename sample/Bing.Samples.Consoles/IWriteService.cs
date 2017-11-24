using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Dependency;
using Bing.Logs.Aspects;

namespace Bing.Samples.Consoles
{
    public interface IWriteService:IScopeDependency
    {
        [DebugLog]
        void WriteContent(string content);
    }
}
