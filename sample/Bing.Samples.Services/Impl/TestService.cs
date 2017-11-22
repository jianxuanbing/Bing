using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Samples.Services.Impl
{
    public class TestService:ITestService
    {
        public string GetContent(string content)
        {
            return content;
        }
    }
}
