using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Winform.Registries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.Utils.Winform.UnitTest.Registries
{
    [TestClass]
    public class WinStartupTest
    {

        [TestMethod]
        public void Test_GetStatus()
        {
            var source = "Wechat";
            var result = WinStartup.GetStatus(source);
            Console.WriteLine(result);
        }
    }
}
