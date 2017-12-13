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
    public class SerialNumberTest
    {
        [TestMethod]
        public void Test_Next()
        {
            var result = SerialNumber.Next(12, 6);
            Console.WriteLine(result);
        }
    }
}
