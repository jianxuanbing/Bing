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
    public class ConvTest
    {
        [TestMethod]
        public void Test_ToInt_Default_0()
        {
            var obj = "";
            var result = Conv.ToInt(obj);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Test_ToInt_Default_1()
        {
            var obj = "";
            var result = Conv.ToInt(obj,1);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Test_ToInt_32()
        {
            var obj = "32";
            var result = Conv.ToInt(obj);
            Assert.AreEqual(32,result);
        }

        [TestMethod]
        public void Test_ToLong_Default_0()
        {
            var obj = "";
            var result = Conv.ToLong(obj);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Test_ToLong_Default_1()
        {
            var obj = "";
            var result = Conv.ToLong(obj, 1);
            Assert.AreEqual(1,result);
        }

        [TestMethod]
        public void Test_ToLong_64()
        {
            var obj = "64";
            var result = Conv.ToLong(obj);
            Assert.AreEqual(64, result);
        }
    }
}
