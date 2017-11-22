using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Samples.Consoles
{
    public class TestColorService:ITestService
    {
        public void WriteContent(string content)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(content);
        }
    }
}
