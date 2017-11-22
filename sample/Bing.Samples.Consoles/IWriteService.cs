using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Core.Dependency;

namespace Bing.Samples.Consoles
{
    public interface IWriteService:IScopeDependency
    {
        void WriteContent(string content);
    }
}
