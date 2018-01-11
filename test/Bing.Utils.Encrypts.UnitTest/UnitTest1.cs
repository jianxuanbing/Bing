using System;
using Bing.Utils.Encrypts.Hash;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.Utils.Encrypts.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var key = "test001";
            var result=SHACryptor.Sha1(key);
            Console.WriteLine(result);
        }
    }
}
