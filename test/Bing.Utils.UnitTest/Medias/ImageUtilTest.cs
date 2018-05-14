using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Medias;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.Utils.UnitTest.Medias
{
    [TestClass]
    public class ImageUtilTest
    {
        [TestMethod]
        public void Test_LetterWatermark()
        {
            var letter = "SC0000000001";
            var path = "D:\\15259416500530001.jpg";
            var result = ImageUtil.LetterWatermark(path, 16, letter, Color.Black, ImageLocationMode.Bottom);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void Test_LetterWatermark_1()
        {
            var letter = "SC0000000001";
            var path = "D:\\15259416500530001.jpg";
            ImageUtil.LetterWatermark(path,"D:\\", "SC0000000002",letter,16,Color.Black, ImageLocationMode.Bottom);
        }
    }
}
