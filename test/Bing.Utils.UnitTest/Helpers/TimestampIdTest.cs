using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Extensions;
using Bing.Utils.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.Utils.UnitTest.Helpers
{
    [TestClass]
    public class TimestampIdTest
    {

        [TestMethod]
        public void Test_GetId()
        {
            var instance = TimestampId.GetInstance();
            for (int i = 0; i < 1000; i++)
            {
                var result = instance.GetId().ToLong();
                Console.WriteLine(result);
            }
            
        }
    }
}
