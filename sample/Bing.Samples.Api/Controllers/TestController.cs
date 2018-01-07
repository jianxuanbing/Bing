using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Bing.Caching.Redis;
using Bing.Contexts;
using Bing.Helpers;
using Bing.Logs;
using Bing.Logs.Extensions;
using Bing.Samples.Api.Models;
using Bing.Samples.Domains.Models;
using Bing.Samples.Domains.Request.Act;
using Bing.Samples.Services;
using Bing.Utils.Helpers;

namespace Bing.Samples.Api.Controllers
{
    public class TestController:ApiController
    {
        /// <summary>
        /// 日志操作
        /// </summary>
        public ILog Log { get; set; }

        private ITestService _testService;
        private ILoginService _loginService;
        public TestController(ILog log,ITestService testService,ILoginService loginService)
        {
            Log = log;
            _testService = testService;
            _loginService = loginService;
        }

        /// <summary>
        /// 测试获取内容
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        [HttpGet]
        public string GetContentInfo(string content)
        {
            return _testService.GetContent(content);
        }

        /// <summary>
        /// 测试空内容
        /// </summary>
        /// <param name="content"></param>
        [HttpPost]
        public void GetNotEmptyContent(string content)
        {
            _testService.GetContent(null);
        }

        /// <summary>
        /// 获取请求信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ReqInfo GetReqInfo()
        {
            return new ReqInfo()
            {
                Browser = Web.Browser,
                Host = Web.Host,
                Ip = Web.Ip,
                Url = Web.Url
            };
        }

        /// <summary>
        /// 发送日志消息
        /// </summary>
        /// <param name="content">日志内容</param>
        public void SendLogInfo(string content)
        {
            Log.BussinessId(Guid.NewGuid().ToString())
                .Module("订单")
                .Method("PlaceOrder")
                .Caption("有人下单了")
                .Params("int", "a", "1")
                .Params("string", "b", "c")
                .Content($"购买商品数量：{100}")
                .Content($"购买商品总额：{200}")
                .Content($"自定义内容：{content}")
                .Sql("select * from Users")
                .Sql("select * from Orders")
                .SqlParams($"@a={1},@b={2}")
                .SqlParams($"@userId={ Guid.NewGuid().ToString()}")
                .Info();
        }

        /// <summary>
        /// 写入信息
        /// </summary>
        /// <param name="content"></param>
        [HttpPost]
        public void WriteInfo(string content)
        {
            _testService.WriteOtherLog(content);
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="act"></param>
        [HttpPost]
        public string Register(RegisterAct act)
        {
            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(Bing.Runtimes.Security.BingClaimTypes.UserId, Id.ObjectId()));
            identity.AddClaim(new Claim(Bing.Runtimes.Security.BingClaimTypes.UserName, "123456"));

            Bing.Runtimes.Sessions.DefaultPrincipalAccessor.Instance.Principal.AddIdentity(identity);
            var result=_loginService.Register(act);
            return result.ToString();
        }

        /// <summary>
        /// 获取所有登录信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Login> GetAllLogin()
        {
            return _loginService.GetAllLogin();
        }

        [HttpGet]
        public List<Login> GetListByName(string name)
        {
            return _loginService.GetListByName(name);
        }

        [HttpPost]
        public void TestCache(int len)
        {
            Random random=new Random();
            for (int i = 0; i < len; i++)
            {
                RedisCache cache = new RedisCache();
                cache.Initialize(new RedisEndpoint()
                {
                    Host = "192.168.99.100",
                    Port = 32768,
                    MinSize = 1,
                    MaxSize = 50,
                    Timeout = 1000
                });
                cache.AddAsync<string>("test" + i, "神奇的傻逗" + random.Next(), TimeSpan.FromMinutes(1));
                var result = cache.GetAsync<string>("test" + i);
                //Console.WriteLine(result.Result);
            }
        }

        public void TestMuiltCache(int len)
        {
            RedisManager.SetDefaultConnectionStr(new RedisEndpoint()
            {
                Host = "192.168.99.100",
                Port = 32768,
            });
            RedisManager.SysCustomKey = "Test:";
            Random random = new Random();
            for (int i = 0; i < len; i++)
            {
                RedisClient client = new RedisClient();
                client.StringSet("tt" + i, "装逼的傻逗" + random.Next());
            }
        }
    }
}