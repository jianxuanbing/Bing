using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var date = DateTime.Now.ToString("yyMMdd");
            Console.WriteLine(date);
        }

        [TestMethod]
        public void Test_GetType()
        {
            var logType = Type.GetType("Bing.Logs.ILog, Bing.Logs");
            Console.WriteLine(logType.Assembly);
        }
    }
}
