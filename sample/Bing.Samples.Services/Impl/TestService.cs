using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Logs;
using Bing.Logs.Extensions;

namespace Bing.Samples.Services.Impl
{
    public class TestService:ITestService
    {
        public ILog Logger = Log.GetLog("wechat");
        public string GetContent(string content)
        {
            return content;
        }

        public void WriteOtherLog(string content)
        {
            Logger.BussinessId(Guid.NewGuid().ToString())
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
    }
}
