using System;
using Bing.Utils.Webs.Clients;
using Bing.Utils.Webs.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.Utils.Webs.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string key = "";
            decimal lat = 39.984154M;
            decimal lng = 116.307490M;
            var url = $"http://apis.map.qq.com/ws/geocoder/v1/?location={lat},{lng}&get_poi=0&key={key}";
            var result = Web.Client()
                .Get(url)
                .Result();

            Console.WriteLine(result);
        }

        [TestMethod]
        public void Test_Get()
        {
            var authorization =
                "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJyb2xlIjoibWluaSxzdG9yZSIsIm5hbWVpZCI6Ijc2YTU2OTBiLWRjN2UtYzk5MC1iNGU3LTM5ZTYxODVhZDk5NCIsInVuaXF1ZV9uYW1lIjoiamlhbueOhOWGsCIsIklwIjoiMTkyLjE2OC41LjIyIiwibG9naW4iOiJ7XCJMb2dpblwiOlwib2JMWTk1YkluRVpaendnTmhWVDhJc3pfTXRLVVwiLFwiTmFtZVwiOlwiamlhbueOhOWGsFwiLFwiVXNlcklkXCI6XCI3NmE1NjkwYi1kYzdlLWM5OTAtYjRlNy0zOWU2MTg1YWQ5OTRcIixcIklkZW50aXR5VHlwZVwiOjQsXCJTeXN0ZW1UeXBlXCI6MixcIkFwcGxpY2F0aW9uVHlwZVwiOjEsXCJNZXJjaGFudElkXCI6XCIwZDZlODVhMS01YjRlLTJlNzUtZWNhNy0zOWU2MTg1YWQ4YzBcIixcIklzRXhwaXJlZFwiOnRydWV9IiwiaXNzIjoibHh6eWwiLCJhdWQiOiJBbnkiLCJleHAiOjE1MjU3ODk2MDksIm5iZiI6MTUyNTc0NjQwOX0.ArzE9ZXvWKFFzJ8zzXDQzWwSq00I1ViyfmEGBvtc_l0";
            var url = $"http://localhost:14307/api/Store/Account/GetLoginInfo";
            var result = Web.Client()
                .Get(url)
                .Header("Authorization", authorization)
                .Result();
            Console.WriteLine(result);
        }
    }
}
