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
        private IWriteService _writeService;
        public TestService(IWriteService writeService)
        {
            _writeService = writeService;
        }
        public void WriteContent(string content)
        {
            _writeService.WriteContent(content);
        }
    }
}
