using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Maths;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.Utils.UnitTest.Maths
{
    [TestClass]
    public class HexConvTest
    {
        [TestMethod]
        public void Test_X2X()
        {
            var source = "100";
            var result2 = HexConv.X2X(source, 10, 2);
            var result8 = HexConv.X2X(source, 10, 8);
            var result10 = HexConv.X2X(source, 10, 10);
            var result16 = HexConv.X2X(source, 10, 16);
            var result26 = HexConv.X2X(source, 10, 26);
            var result32 = HexConv.X2X(source, 10, 32);
            var result36 = HexConv.X2X(source, 10, 36);
            var result52 = HexConv.X2X(source, 10, 52);
            var result58 = HexConv.X2X(source, 10, 58);
            var result62 = HexConv.X2X(source, 10, 62);
            Console.WriteLine("2:" + result2);
            Console.WriteLine("8:" + result8);
            Console.WriteLine("10:" + result10);
            Console.WriteLine("16:" + result16);
            Console.WriteLine("26:" + result26);
            Console.WriteLine("32:" + result32);
            Console.WriteLine("36:" + result36);
            Console.WriteLine("52:" + result52);
            Console.WriteLine("58:" + result58);
            Console.WriteLine("62:" + result62);
        }
    }
}
