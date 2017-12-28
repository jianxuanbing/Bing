using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.Utils.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var path = @"H:\LXWL\SVN\KA项目";
            var result = Directory.GetFileSystemEntries(path,"*",SearchOption.AllDirectories);
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        [TestMethod]
        public void Test_Float_Type()
        {
            var type = typeof(float);
            Console.WriteLine(type.ToString());
        }
    }
}
