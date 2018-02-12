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
    public class StrTest
    {
        [TestMethod]
        public void Test_ToUnicode()
        {
            var source = "ABC";
            var result = Str.ToUnicode(source);
            var twoResult = Str.ToUnicode(result);
            Console.WriteLine(result);
            Console.WriteLine(twoResult);
        }

        [TestMethod]
        public void Test_ToUnicodeByCn()
        {
            var source = "测试语句ABC";
            var result = Str.ToUnicodeByCn(source);
            Console.WriteLine(result);
            Assert.AreEqual(result, @"\u6d4b\u8bd5\u8bed\u53e5ABC");
        }

        [TestMethod]
        public void Test_UnicodeToStr()
        {
            var source = @"\u6d4b\u8bd5\u8bed\u53e5\u0041\u0042\u0043";
            var result = Str.UnicodeToStr(source);
            Console.WriteLine(result);
            Assert.AreEqual(result, "测试语句ABC");
        }
    }
}
