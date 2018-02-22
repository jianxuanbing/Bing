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
    public class ValidTest
    {
        [TestMethod]
        public void Test_IsBase64String()
        {
            var source = "测试0001";
            var source1 = "5rWL6K+VMDAwMQ==";
            Console.WriteLine(Convert.ToBase64String(Encoding.UTF8.GetBytes(source)));
            
            var result1 = Valid.IsBase64String(source1);
            Console.WriteLine(result1);
        }

        [TestMethod]
        public void Test_IsMainDomain()
        {
            var source = "www.baidu.com";
            var source1 = "baidu.com";
            var source2 = "test.baidu.com";
            var result = Valid.IsMainDomain(source);
            var result1 = Valid.IsMainDomain(source1);
            var result2 = Valid.IsMainDomain(source2);
            Console.WriteLine(result);
            Console.WriteLine(result1);
            Console.WriteLine(result2);
        }

        [TestMethod]
        public void Test_IsDomain()
        {
            var source = "www.baidu.com";
            var source1 = "baidu.com";
            var source2 = "test.baidu.com";
            var result = Valid.IsDomain(source);
            var result1 = Valid.IsDomain(source1);
            var result2 = Valid.IsDomain(source2);
            Console.WriteLine(result);
            Console.WriteLine(result1);
            Console.WriteLine(result2);
        }

        [TestMethod]
        public void Test_IsPostfix()
        {
            var source = "test.txt";
            var result = Valid.IsPostfix(source, new string[] {"txt"});
            Console.WriteLine(result);
        }
    }
}
