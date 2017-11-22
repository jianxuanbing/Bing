using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Samples.Consoles
{
    /// <summary>
    /// 测试服务
    /// </summary>
    public class TestService:ITestService
    {
        public void WriteContent(string content)
        {
            Console.WriteLine(content);
        }
    }
}
