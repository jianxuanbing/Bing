using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.Utils.UnitTest.Helpers
{
    [TestClass]
    public class TimeTest
    {
        [TestMethod]
        public void Test_ParseIso8601()
        {
            var source = "2018-02-22T03:50:38Z";
            var result = Time.ParseIso8601(source);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void Test_FormatIso8601()
        {            
            var result = Time.FormatIso8601(DateTime.Now);
            Console.WriteLine(result);
        }
    }
}
