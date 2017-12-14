using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Helpers;
using Bing.Utils.IO;
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

        [TestMethod]
        public void Test_BatchNext()
        {
            List<string> list=new List<string>();
            Stopwatch stopwatch=new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                var result = SerialNumber.Next(12, 6);
                list.Add(result);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            StringBuilder sb=new StringBuilder();
            foreach (var item in list)
            {
                sb.AppendLine(item);
            }
            FileUtil.Write("D:\\sheet.txt",sb.ToString());
        }
        
    }
}
