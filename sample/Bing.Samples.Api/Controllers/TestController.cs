using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Bing.Samples.Services;

namespace Bing.Samples.Api.Controllers
{
    public class TestController:ApiController
    {
        private ITestService _testService;
        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public string GetContent(string content)
        {
            return _testService.GetContent(content);
        }
    }
}