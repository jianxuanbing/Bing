using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Aspects;
using Bing.Logs;
using Bing.Logs.Aspects;
using Bing.Logs.Extensions;

namespace Bing.Samples.Services.Impl
{
    public class TestService:ITestService
    {
        [CachingHandler]
        public string GetContent(string content)
        {
            return content;
        }
        
        public void WriteOtherLog(string content)
        {
            Console.WriteLine(content);
        }
    }
}
