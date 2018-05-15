using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Helpers;
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

        [TestMethod]
        public void Test_LetterWatermark_LeftTop()
        {
            var letter = "SC0000000001";
            var path = "D:\\15259416500530001.jpg";
            ImageUtil.LetterWatermark(path, "D:\\", "B0001", letter, 16, Color.Black, ImageLocationMode.LeftTop);
        }

        [TestMethod]
        public void Test_LetterWatermark_Top()
        {
            var letter = "SC0000000001";
            var path = "D:\\15259416500530001.jpg";
            ImageUtil.LetterWatermark(path, "D:\\", "B0002", letter, 16, Color.Black, ImageLocationMode.Top);
        }

        [TestMethod]
        public void Test_LetterWatermark_RightTop()
        {
            var letter = "SC0000000001";
            var path = "D:\\15259416500530001.jpg";
            ImageUtil.LetterWatermark(path, "D:\\", "B0003", letter, 16, Color.Black, ImageLocationMode.RightTop);
        }

        [TestMethod]
        public void Test_LetterWatermark_LeftCenter()
        {
            var letter = "SC0000000001";
            var path = "D:\\15259416500530001.jpg";
            ImageUtil.LetterWatermark(path, "D:\\", "B0004", letter, 16, Color.Black, ImageLocationMode.LeftCenter);
        }

        [TestMethod]
        public void Test_LetterWatermark_Center()
        {
            var letter = "SC0000000001";
            var path = "D:\\15259416500530001.jpg";
            ImageUtil.LetterWatermark(path, "D:\\", "B0005", letter, 16, Color.Black, ImageLocationMode.Center);
        }

        [TestMethod]
        public void Test_LetterWatermark_RightCenter()
        {
            var letter = "SC0000000001";
            var path = "D:\\15259416500530001.jpg";
            ImageUtil.LetterWatermark(path, "D:\\", "B0006", letter, 16, Color.Black, ImageLocationMode.RightCenter);
        }

        [TestMethod]
        public void Test_LetterWatermark_LeftBottom()
        {
            var letter = "SC0000000001";
            var path = "D:\\15259416500530001.jpg";
            ImageUtil.LetterWatermark(path, "D:\\", "B0007", letter, 16, Color.Black, ImageLocationMode.LeftBottom);
        }

        [TestMethod]
        public void Test_LetterWatermark_Bottom()
        {
            var letter = "SC0000000001";
            var path = "D:\\15259416500530001.jpg";
            ImageUtil.LetterWatermark(path, "D:\\", "B0008", letter, 16, Color.Black, ImageLocationMode.Bottom);
        }

        [TestMethod]
        public void Test_LetterWatermark_RightBottom()
        {
            var letter = "SC0000000001";
            var path = "D:\\15259416500530001.jpg";
            ImageUtil.LetterWatermark(path, "D:\\", "B0009", letter, 16, Color.Black, ImageLocationMode.RightBottom);
        }

        [TestMethod]
        public void Test_MergeImage()
        {
            var bgImage = ImageUtil.FromFile(Sys.GetPhysicalPath("D:\\Test001.jpg"));
            var fgImage = ImageUtil.FromFile("D:\\15259416500530001.jpg");
            var bitmap = ImageUtil.MergeImage(bgImage, fgImage, 1000, 200);
            bitmap.Save("D:\\MergeImage_001.jpg",ImageFormat.Jpeg);
            bitmap.Dispose();
            bgImage.Dispose();
            fgImage.Dispose();
        }

        [TestMethod]
        public void Test_MergeImage_1()
        {
            var result = ImageUtil.MergeImage(Sys.GetPhysicalPath("D:\\Test001.jpg"),
                Sys.GetPhysicalPath("D:\\15259416500530001.jpg"), "D:\\MergeImage_003.jpg", 100, 200);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void Test_DownloadRemoteImage()
        {
            var imgUrl = "http://cdn.loonxierp.com//yuema_uploadfiles/img/20185/1526367137646_4524.jpg";
            var savePath = "D:\\0001.jpg";
            var result=ImageUtil.DownloadRemoteImage(imgUrl, savePath);
            Console.WriteLine(result);
        }
    }
}
