using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing;
using Bing.Dependency;
using Bing.Helpers;

namespace Bing.Samples.Consoles
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();

            var service = Ioc.Create<ITestService>();
            service.WriteContent("试试自动注入");

            //var  mainService = Ioc.Create<ITestService>("main");
            //mainService.WriteContent("测试一下装逼技能先");
            //var colorService = Ioc.Create<ITestService>("color");
            //colorService.WriteContent("不想装逼了，好像坑爹一下");
            Console.ReadKey();
        }

        static void Init()
        {
            IocConfigInitialize.Init(ScopeType.None);
        }
    }
}
