using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using Bing.Utils.Extensions;
using Bing.Utils.Helpers;
using Enum = System.Enum;

namespace Bing.Utils.Medias
{
    /// <summary>
    /// 图片操作工具类
    /// </summary>
    public class ImageUtil
    {
        #region 变量
        /// <summary>
        /// 文本字体数组
        /// </summary>
        private static readonly string[] Fonts = { "Arial", "courier new", "微软雅黑", "宋体" };
        /// <summary>
        /// 文本样式数组
        /// </summary>
        private static readonly FontStyle[] Styles = { FontStyle.Regular, FontStyle.Bold, FontStyle.Bold, FontStyle.Bold };
        #endregion

        #region MakeThumbnail(生成缩略图)
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height,
            ThumbnailMode mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case ThumbnailMode.FixedBoth:
                    break;
                case ThumbnailMode.FixedW:
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case ThumbnailMode.FixedH:
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ThumbnailMode.Cut:
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }
            //1、新建一个BMP图片
            Image bitmap = new Bitmap(towidth, toheight);
            //2、新建一个画板
            Graphics g = Graphics.FromImage(bitmap);
            //3、设置高质量插值法
            g.InterpolationMode = InterpolationMode.High;
            //4、设置高质量，低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;
            //5、清空画布并以透明背景色填充
            g.Clear(Color.Transparent);
            //6、在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        /// <summary>
        /// 转换生成缩略图
        /// </summary>
        /// <param name="imgByte">缓存字节流</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="mode">缩略图方式</param>
        /// <returns></returns>
        public static Image MakeThumbnail(byte[] imgByte, int width, int height, ThumbnailMode mode)
        {
            Image originalImage = ByteToImage(imgByte);
            return MakeThumbnail(originalImage, width, height, mode);
        }
        /// <summary>
        /// 转换生成缩略图
        /// </summary>
        /// <param name="originalImage">原图</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="mode">生成缩略图的方式</param>
        /// <returns></returns>
        public static Image MakeThumbnail(Image originalImage, int width, int height, ThumbnailMode mode)
        {
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case ThumbnailMode.FixedBoth:
                    break;
                case ThumbnailMode.FixedW:
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case ThumbnailMode.FixedH:
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ThumbnailMode.Cut:
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }
            //1、新建一个BMP图片
            Image bitmap = new Bitmap(towidth, toheight);
            //2、新建一个画板
            Graphics g = Graphics.FromImage(bitmap);
            try
            {
                //3、设置高质量插值法
                g.InterpolationMode = InterpolationMode.High;
                //4、设置高质量，低速度呈现平滑程度
                g.SmoothingMode = SmoothingMode.HighQuality;
                //5、清空画布并以透明背景色填充
                g.Clear(Color.Transparent);
                //6、在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh),
                    GraphicsUnit.Pixel);
                return bitmap;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                // TODO:由于要返回Bitmap对象，因此不释放资源
                //bitmap.Dispose();
                g.Dispose();
            }
        }
        #endregion

        #region ByteToImage(将字节数组转换成图片)
        /// <summary>
        /// 将字节数组转换成图片
        /// </summary>
        /// <param name="buffer">缓存字节流</param>
        /// <returns></returns>
        public static Image ByteToImage(byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }
        #endregion

        #region ImageToByte(将图片转换成字节数组)
        /// <summary>
        /// 将图片转换成字节数组
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns></returns>
        public static byte[] ImageToByte(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                byte[] buffer = new byte[ms.Length];
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }
        #endregion

        #region ImageToStream(将图片转换成字节流)
        /// <summary>
        /// 将图片转换成字节流
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns></returns>
        public static Stream ImageToStream(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            return ms;
        }
        #endregion

        #region ImageToBase64(将图片转换成Base64编码)
        /// <summary>
        /// 将图片转换成Base64编码
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns></returns>
        public static string ImageToBase64(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, GetImageFormate(image));
            byte[] bytes = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(bytes, 0, (int)ms.Length);
            ms.Close();
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// 将图片转换成Base64编码，带有头部
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string ImageToBase64WithHeader(Image image)
        {
            return string.Format(@"data:image/{0};base64,{1}", GetImageExtension(image), ImageToBase64(image));
        }
        #endregion

        #region Base64ToImage(将Base64编码转换成图片)
        /// <summary>
        /// 将Base64编码转换成图片
        /// </summary>
        /// <param name="base64">Base64编码</param>
        /// <returns></returns>
        public static Image Base64ToImage(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream ms = new MemoryStream(bytes);
            Image image = Image.FromStream(ms);
            return image;
        }
        /// <summary>
        /// 将带有头部编码的Base64编码转换成图片
        /// </summary>
        /// <param name="base64">Base64编码</param>
        /// <returns></returns>
        public static Image Base64ToImageWithHeader(string base64)
        {
            base64 = base64.Substring(base64.IndexOf(',') + 1);
            return Base64ToImage(base64);
        }
        #endregion

        #region ImageWatermark(图片水印)
        /// <summary>
        /// 设置图片水印
        /// </summary>
        /// <param name="path">需要加载水印的图片路径（绝对路径）</param>
        /// <param name="waterpath">水印图片（绝对路径）</param>
        /// <param name="location">水印位置</param>
        /// <returns></returns>
        public static string ImageWatermark(string path, string waterpath, ImageLocationMode location)
        {
            string extName = Path.GetExtension(path);
            if (extName == ".jpg" || extName == ".bmp" || extName == ".jpeg" || extName == ".png")
            {
                DateTime time = DateTime.Now;
                string fileName = "" + time.Year.ToString() + time.Month.ToString() + time.Day.ToString() +
                                  time.Hour.ToString() + time.Minute.ToString() + time.Second.ToString() +
                                  time.Millisecond.ToString();

                //Image img = null;
                //Graphics g = null;
                //try
                //{
                //    img = Bitmap.FromFile(path);
                //    g = Graphics.FromImage(img);
                //    Image waterImg = Image.FromFile(waterpath);
                //    ArrayList coors = GetLocation(location, img, waterImg);
                //    g.DrawImage(waterImg, new Rectangle(int.Parse(coors[0].ToString()), int.Parse(coors[1].ToString()),
                //    waterImg.Width, waterImg.Height));
                //    waterImg.Dispose();
                //    string newPath = Path.GetDirectoryName(path) + fileName + extName;
                //    img.Save(newPath);
                //    File.Copy(newPath, path, true);
                //    if (File.Exists(newPath))
                //    {
                //        File.Delete(newPath);
                //    }
                //}
                //catch (OutOfMemoryException ex)
                //{
                //    if (img != null)
                //    {
                //        img.Dispose();
                //        img = null;
                //    }
                //    throw ex;
                //    //return ImageWatermarkByMagick(path, waterpath, location);
                //}
                //finally
                //{
                //    if (img != null)
                //    {
                //        img.Dispose();
                //    }
                //    if (g != null)
                //    {
                //        g.Dispose();
                //    }
                //}
                Image img = Image.FromFile(path);
                // 处理不在指定范围内的图片
                if (!Enum.IsDefined(typeof(PixelFormat), img.PixelFormat))
                {
                    int width = img.Width;
                    int height = img.Height;
                    img.Dispose();
                    img = null;
                    using (Bitmap temp = new Bitmap(path))
                    {
                        img = temp.Clone(new Rectangle(0, 0, width, height), PixelFormat.Format32bppRgb);
                    }
                }
                Image waterImg = Image.FromFile(waterpath);
                using (Graphics g = Graphics.FromImage(img))
                {
                    ArrayList coors = GetLocation(location, img, waterImg);
                    g.DrawImage(waterImg, new Rectangle(int.Parse(coors[0].ToString()), int.Parse(coors[1].ToString()),
                        waterImg.Width, waterImg.Height));
                    waterImg.Dispose();
                }
                string newPath = Path.GetDirectoryName(path) + fileName + extName;
                img.Save(newPath);
                img.Dispose();
                File.Copy(newPath, path, true);
                if (File.Exists(newPath))
                {
                    File.Delete(newPath);
                }
            }
            return path;
        }

        ///// <summary>
        ///// 设置图片水印，使用MagickImage.Net
        ///// </summary>
        ///// <param name="path">需要加载水印的图片路径（绝对路径）</param>
        ///// <param name="waterpath">水印图片（绝对路径）</param>
        ///// <param name="location">水印位置</param>
        ///// <returns></returns>
        //public static string ImageWatermarkByMagick(string path, string waterpath, ImageLocationMode location)
        //{
        //    string extName = Path.GetExtension(path);
        //    if (string.IsNullOrEmpty(extName))
        //    {
        //        return path;
        //    }
        //    extName = extName.ToLower();
        //    if (!(extName == ".jpg" || extName == ".bmp" || extName == ".jpeg" || extName == ".png"))
        //    {
        //        return path;
        //    }
        //    // 读取需要水印的图片
        //    using (ImageMagick.MagickImage image = new ImageMagick.MagickImage(path))
        //    {
        //        // 读取水印图片
        //        using (ImageMagick.MagickImage watermark = new ImageMagick.MagickImage(waterpath))
        //        {
        //            // 设置水印透明度
        //            //watermark.Evaluate(Channels.Alpha, EvaluateOperator.Divide, 7);
        //            // 设置绘制水印位置
        //            image.Composite(watermark, GetLocation(location), CompositeOperator.Over);
        //        }
        //        image.Resize(image.Width, image.Height);
        //        image.Quality = 75;
        //        image.CompressionMethod = ImageMagick.CompressionMethod.JPEG;
        //        image.Write(path);
        //        return path;
        //    }
        //}

        ///// <summary>
        ///// 获取水印位置
        ///// </summary>
        ///// <param name="location">水印位置</param>
        ///// <returns></returns>
        //private static Gravity GetLocation(ImageLocationMode location)
        //{
        //    switch (location)
        //    {
        //        case ImageLocationMode.LeftTop:
        //            return Gravity.Northwest;
        //        case ImageLocationMode.Top:
        //            return Gravity.North;
        //        case ImageLocationMode.RightTop:
        //            return Gravity.Northeast;
        //        case ImageLocationMode.RightCenter:
        //            return Gravity.East;
        //        case ImageLocationMode.RightBottom:
        //            return Gravity.Southeast;
        //        case ImageLocationMode.Bottom:
        //            return Gravity.South;
        //        case ImageLocationMode.LeftBottom:
        //            return Gravity.Southwest;
        //        case ImageLocationMode.LeftCenter:
        //            return Gravity.West;
        //        case ImageLocationMode.Center:
        //            return Gravity.Center;
        //        default:
        //            return Gravity.Center;
        //    }
        //}

        /// <summary>
        /// 获取水印位置
        /// </summary>
        /// <param name="location">水印位置</param>
        /// <param name="img">需要添加水印的图片</param>
        /// <param name="waterImg">水印图片</param>
        /// <returns></returns>
        private static ArrayList GetLocation(ImageLocationMode location, Image img, Image waterImg)
        {
            ArrayList coords = new ArrayList();
            int x = 0;
            int y = 0;
            switch (location)
            {
                case ImageLocationMode.LeftTop:
                    x = 10;
                    y = 10;
                    break;
                case ImageLocationMode.Top:
                    x = img.Width / 2 - waterImg.Width / 2;
                    y = img.Height - waterImg.Height;
                    break;
                case ImageLocationMode.RightTop:
                    x = img.Width - waterImg.Width;
                    y = 10;
                    break;
                case ImageLocationMode.LeftCenter:
                    x = 10;
                    y = img.Height / 2 - waterImg.Height / 2;
                    break;
                case ImageLocationMode.Center:
                    x = img.Width / 2 - waterImg.Width / 2;
                    y = img.Height / 2 - waterImg.Height / 2;
                    break;
                case ImageLocationMode.RightCenter:
                    x = img.Width - waterImg.Width;
                    y = img.Height / 2 - waterImg.Height / 2;
                    break;
                case ImageLocationMode.LeftBottom:
                    x = 10;
                    y = img.Height - waterImg.Height;
                    break;
                case ImageLocationMode.Bottom:
                    x = img.Width / 2 - waterImg.Width / 2;
                    y = img.Height - waterImg.Height;
                    break;
                case ImageLocationMode.RightBottom:
                    x = img.Width - waterImg.Width;
                    y = img.Height - waterImg.Height;
                    break;
                default:
                    break;
            }
            coords.Add(x);
            coords.Add(y);
            return coords;
        }
        #endregion

        #region LetterWatermark(文字水印)

        /// <summary>
        /// 设置文字水印
        /// </summary>
        /// <param name="path">图片路径（绝对路径）</param>
        /// <param name="outPath">输出路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="letter">水印文字</param>
        /// <param name="size">字体大小</param>
        /// <param name="color">颜色</param>
        /// <param name="location">水印位置</param>
        /// <returns></returns>
        public static void LetterWatermark(string path, string outPath, string fileName, string letter, int size,
            Color color, ImageLocationMode location)
        {
            string extName = Path.GetExtension(path);
            if (extName == ".jpg" || extName == ".bmp" || extName == ".jpeg" || extName == ".png")
            {
                Image img = Bitmap.FromFile(path);
                SizeF sizeF = GetFontSize(letter, img.Width, img.Height, size, "宋体");
                if (location == ImageLocationMode.Bottom)
                {
                    img = ResizeImage(img, img.Width, img.Height + (int) sizeF.Height, Brushes.White);
                }

                Graphics g = Graphics.FromImage(img);
                Font font = new Font("宋体", size);
                ArrayList coors = GetLocation(location, img, (int) sizeF.Width, (int) sizeF.Height);

                Brush br = new SolidBrush(color);
                g.DrawString(letter, font, br, float.Parse(coors[0].ToString()), float.Parse(coors[1].ToString()));
                g.Dispose();

                string newPath = Sys.GetPhysicalPath(outPath) + fileName + extName;
                img.Save(newPath);
                img.Dispose();
            }
        }

        /// <summary>
        /// 设置文字水印
        /// </summary>
        /// <param name="path">图片路径（绝对路径）</param>
        /// <param name="size">字体大小</param>
        /// <param name="letter">水印文字</param>
        /// <param name="color">颜色</param>
        /// <param name="location">水印位置</param>
        /// <returns></returns>
        public static string LetterWatermark(string path, int size, string letter, Color color,
            ImageLocationMode location)
        {
            string extName = Path.GetExtension(path);
            if (extName == ".jpg" || extName == ".bmp" || extName == ".jpeg" || extName == ".png")
            {
                DateTime time = DateTime.Now;
                string fileName = "" + time.Year.ToString() + time.Month.ToString() + time.Day.ToString() +
                                  time.Hour.ToString() + time.Minute.ToString() + time.Second.ToString() +
                                  time.Millisecond.ToString();
                Image img = Bitmap.FromFile(path);
                SizeF sizeF = GetFontSize(letter, img.Width, img.Height, size, "宋体");
                if (location == ImageLocationMode.Bottom)
                {
                    img = ResizeImage(img, img.Width, img.Height + (int) sizeF.Height, Brushes.White);
                }

                Graphics g = Graphics.FromImage(img);
                Font font = new Font("宋体", size);
                ArrayList coors = GetLocation(location, img, (int)sizeF.Width,(int)sizeF.Height);
                
                Brush br = new SolidBrush(color);
                g.DrawString(letter, font, br, float.Parse(coors[0].ToString()), float.Parse(coors[1].ToString()));
                g.Dispose();
                string newPath = Path.GetDirectoryName(path) + fileName + extName;
                img.Save(newPath);
                img.Dispose();
                File.Copy(newPath, path, true);
                if (File.Exists(newPath))
                {
                    File.Delete(newPath);
                }
            }
            return path;
        }

        /// <summary>
        /// 获取字体尺寸
        /// </summary>
        /// <param name="letter">文本内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="size">字号大小</param>
        /// <param name="fontName">字体名称</param>
        /// <returns></returns>
        private static SizeF GetFontSize(string letter,int width,int height, int size, string fontName)
        {
            using (Bitmap bitmap = new Bitmap(width, height))
            {
                using (Graphics g=Graphics.FromImage(bitmap))
                {
                    Font font=new Font(fontName,size);
                    SizeF sizeF = g.MeasureString(letter, font);
                    return sizeF;
                }
            }
        }

        /// <summary>
        /// 获取水印位置
        /// </summary>
        /// <param name="location">水印位置</param>
        /// <param name="img">需要添加水印的图片</param>
        /// <param name="width">宽(当水印类型为文字时,传过来的就是字体的大小)</param>
        /// <param name="height">高(当水印类型为文字时,传过来的就是字符的长度)</param>
        /// <returns></returns>
        private static ArrayList GetLocation(ImageLocationMode location, Image img, int width, int height)
        {
            ArrayList coords = new ArrayList();
            float x = 10;
            float y = 10;
            switch (location)
            {
                case ImageLocationMode.LeftTop:
                    x = 10;
                    y = 10;
                    break;
                case ImageLocationMode.Top:
                    x = img.Width / 2 - width / 2;
                    break;
                case ImageLocationMode.RightTop:
                    x = img.Width - width;
                    break;
                case ImageLocationMode.LeftCenter:
                    y = img.Height / 2;
                    break;
                case ImageLocationMode.Center:
                    x = img.Width / 2 - width / 2;
                    y = img.Height / 2;
                    break;
                case ImageLocationMode.RightCenter:
                    x = img.Width - width;
                    y = img.Height / 2;
                    break;
                case ImageLocationMode.LeftBottom:
                    y = img.Height - height - 5;
                    break;
                case ImageLocationMode.Bottom:
                    x = img.Width / 2 - width/2;
                    y = img.Height - height - 5;
                    break;
                case ImageLocationMode.RightBottom:
                    x = img.Width - width;
                    y = img.Height - height - 5;
                    break;
                default:
                    break;
            }
            coords.Add(x);
            coords.Add(y);
            return coords;
        }
        #endregion

        #region BrightnessHandle(亮度处理)
        /// <summary>
        /// 亮度处理
        /// </summary>
        /// <param name="bitmap">原始图片</param>
        /// <param name="width">原始图片的长度</param>
        /// <param name="height">原始图片的高度</param>
        /// <param name="val">增加或减少的光暗值</param>
        /// <returns></returns>
        public static Bitmap BrightnessHandle(Bitmap bitmap, int width, int height, int val)
        {
            Bitmap bm = new Bitmap(width, height);//初始化一个记录经过处理后的图片对象
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    //红绿蓝三值
                    var resultR = pixel.R + val;
                    var resultG = pixel.G + val;
                    var resultB = pixel.B + val;
                    bm.SetPixel(x, y, Color.FromArgb(resultR, resultG, resultB));//绘图
                }
            }
            return bm;
        }
        #endregion

        #region FilterColor(滤色处理)
        /// <summary>
        /// 滤色处理
        /// </summary>
        /// <param name="bitmap">原始图片</param>
        /// <returns></returns>
        public static Bitmap FilterColor(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    bitmap.SetPixel(x, y, Color.FromArgb(0, pixel.G, pixel.B));
                }
            }
            return bitmap;
        }
        #endregion

        #region StretchImage(拉伸图片)
        /// <summary>
        /// 拉伸图片?
        /// </summary>
        /// <param name="bitmap">原始图片</param>
        /// <param name="width">新的宽度</param>
        /// <param name="height">新的高度</param>
        /// <returns></returns>
        public static Bitmap StretchImage(Bitmap bitmap, int width, int height)
        {
            try
            {
                Bitmap bm = new Bitmap(width, height);
                Graphics g = Graphics.FromImage(bm);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bm, new Rectangle(0, 0, width, height), new Rectangle(0, 0, bm.Width, bm.Height),
                    GraphicsUnit.Pixel);
                g.Dispose();
                return bm;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region LeftRightTurn(左右翻转)
        /// <summary>
        /// 左右翻转
        /// </summary>
        /// <param name="bitmap">原始图片</param>
        /// <returns></returns>
        public static Bitmap LeftRightTurn(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = width - 1, z = 0; x >= 0; x--)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    bitmap.SetPixel(z++, y, Color.FromArgb(pixel.R, pixel.G, pixel.B));
                }
            }
            return bitmap;
        }
        #endregion

        #region TopBottomTurn(上下翻转)
        /// <summary>
        /// 上下翻转
        /// </summary>
        /// <param name="bitmap">原始图片</param>
        /// <returns></returns>
        public static Bitmap TopBottomTurn(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = height - 1, z = 0; y >= 0; y--)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    bitmap.SetPixel(x, z++, Color.FromArgb(pixel.R, pixel.G, pixel.B));
                }
            }
            return bitmap;
        }
        #endregion

        #region Compress(压缩图片)
        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <param name="oldFile">源文件路径</param>
        /// <param name="newFile">新文件路径</param>
        /// <returns></returns>
        public static bool Compress(string oldFile, string newFile)
        {
            try
            {
                Image image = Image.FromFile(oldFile);
                ImageFormat thisFormat = image.RawFormat;
                Size newSize = new Size(100, 125);
                Bitmap outBmp = new Bitmap(newSize.Width, newSize.Height);
                Graphics g = Graphics.FromImage(outBmp);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, new Rectangle(0, 0, newSize.Width, newSize.Height), 0, 0, image.Width, image.Height,
                    GraphicsUnit.Pixel);
                g.Dispose();
                EncoderParameters encoderParams = new EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;
                EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                ImageCodecInfo[] array = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = array.FirstOrDefault(t => t.FormatDescription.Equals("JPEG"));
                image.Dispose();
                if (ici != null)
                {
                    outBmp.Save(newFile, ImageFormat.Jpeg);
                }
                outBmp.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region ToBlackWhiteImage(转换为黑白图片)
        /// <summary>
        /// 转换为黑白图片
        /// </summary>
        /// <param name="bitmap">要进行处理的图片</param>
        /// <returns></returns>
        public static Bitmap ToBlackWhiteImage(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    int result = (pixel.R + pixel.G + pixel.B) / 3;
                    bitmap.SetPixel(x, y, Color.FromArgb(result, result, result));
                }
            }
            return bitmap;
        }
        #endregion

        #region TwistImage(扭曲图片，滤镜效果)
        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="bitmap">图片</param>
        /// <param name="isTwist">是否扭曲，true:扭曲,false:不扭曲</param>
        /// <param name="shapeMultValue">波形的幅度倍数，越大扭曲的程度越高，默认为3</param>
        /// <param name="shapePhase">波形的起始相位，取值区间[0-2*PI]</param>
        /// <returns></returns>
        public static Bitmap TwistImage(Bitmap bitmap, bool isTwist, double shapeMultValue, double shapePhase)
        {
            Bitmap destBmp = new Bitmap(bitmap.Width, bitmap.Height);
            //将位图背景填充为白色
            Graphics g = Graphics.FromImage(destBmp);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            g.Dispose();
            double dBaseAxisLen = isTwist ? (double)destBmp.Height : (double)destBmp.Width;
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = isTwist ? 2 * Math.PI * (double)j / dBaseAxisLen : 2 * Math.PI * (double)i / dBaseAxisLen;
                    dx += shapePhase;
                    double dy = Math.Sin(dx);
                    //取当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = isTwist ? i + (int)(dy * shapeMultValue) : i;
                    nOldY = isTwist ? j : j + (int)(dy * shapeMultValue);
                    Color color = bitmap.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX <= destBmp.Width && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            return destBmp;
        }
        #endregion

        #region GetFrames(获取图片帧)
        /// <summary>
        /// 获取图片帧
        /// </summary>
        /// <param name="imgPath">图片路径</param>
        /// <param name="savePath">帧保存路径</param>
        public static void GetFrames(string imgPath, string savePath)
        {
            Image gif = Image.FromFile(imgPath);
            FrameDimension fd = new FrameDimension(gif.FrameDimensionsList[0]);
            int count = gif.GetFrameCount(fd);//获取帧数(gif图片可能包含多帧，其它格式图片一般仅一帧)
            for (int i = 0; i < count; i++)
            {
                gif.SelectActiveFrame(fd, i);
                gif.Save(savePath + "\\frame_" + i + ".jpg", ImageFormat.Jpeg);
            }
        }
        #endregion

        #region GetImageExtension(获取图片扩展名)
        /// <summary>
        /// 获取图片扩展名
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns></returns>
        public static string GetImageExtension(Image image)
        {
            Type type = typeof(ImageFormat);
            PropertyInfo[] imageFormatList = type.GetProperties(BindingFlags.Static | BindingFlags.Public);
            for (int i = 0; i != imageFormatList.Length; i++)
            {
                ImageFormat formatClass = (ImageFormat)imageFormatList[i].GetValue(null, null);
                if (formatClass.Guid.Equals(image.RawFormat.Guid))
                {
                    return imageFormatList[i].Name;
                }
            }
            return "";
        }
        #endregion

        #region GetImageFormate(获取图片格式)
        /// <summary>
        /// 获取图片格式
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns></returns>
        public static ImageFormat GetImageFormate(Image image)
        {
            Type type = typeof(ImageFormat);
            PropertyInfo[] imageFormatList = type.GetProperties(BindingFlags.Static | BindingFlags.Public);
            for (int i = 0; i != imageFormatList.Length; i++)
            {
                ImageFormat formatClass = (ImageFormat)imageFormatList[i].GetValue(null, null);
                if (formatClass.Guid.Equals(image.RawFormat.Guid))
                {
                    return formatClass;
                }
            }
            return ImageFormat.Jpeg;
        }
        #endregion

        #region GetCodecInfo(获取特定图像编解码信息)
        /// <summary>
        /// 获取特定图像编解码信息
        /// </summary>
        /// <param name="format">图片格式</param>
        /// <returns></returns>
        public static ImageCodecInfo GetCodecInfo(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
        }
        #endregion

        #region CreateImageByChar(根据字符内容生成图片)
        ///// <summary>
        ///// 根据字符内容生成图片
        ///// </summary>
        ///// <param name="content">内容</param>
        ///// <returns></returns>
        //public static Image CreateImageByChar(string content)
        //{
        //    int fontSize = 20;//字体大小
        //    int fontWidth = fontSize;//字体宽度
        //    int imageWidth = (int)(content.Length * fontWidth) * 2;//图片宽度，字体宽度*字数*2
        //    int imageHeight = fontSize * 2;//图片高度，字体高度*2
        //    Bitmap bitmap = new Bitmap(imageWidth, imageHeight);
        //    Graphics g = Graphics.FromImage(bitmap);
        //    //获取品质（压缩率）编码
        //    //System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;
        //    //EncoderParameter paramter = new EncoderParameter(encoder, 30L);
        //    //EncoderParameters paramters = new EncoderParameters(1);
        //    //paramters.Param[0] = paramter;
        //    //ImageCodecInfo jpgInfo = GetCodecInfo(ImageFormat.Jpeg);
        //    //g.CompositingQuality=CompositingQuality.HighSpeed;
        //    //g.SmoothingMode=SmoothingMode.HighSpeed;
        //    //g.InterpolationMode=InterpolationMode.Low;
        //    g.Clear(Color.White);//背景图
        //    //设置字体
        //    Random random = new Random(VerifyCodeUtil.Instance.GetRandomSeed());
        //    string fontName = Fonts[random.Next(Fonts.Length - 1)];
        //    FontStyle fontStyle = Styles[random.Next(Styles.Length - 1)];
        //    Font font = new Font(fontName, fontSize, fontStyle);
        //    //验证码颜色
        //    Brush brush = new SolidBrush(Color.Blue);
        //    int minLeft = 0;
        //    int maxLeft = imageWidth - content.Length * fontSize;
        //    int left = random.Next(minLeft, maxLeft);
        //    for (int i = 0; i < content.Length; i++)
        //    {
        //        int rLeft = left + (i * fontSize);//需要写入的字符
        //        //上下移动
        //        //int minTop = fontSize/8;
        //        //int maxTop = fontSize/4;
        //        //int top = random.Next(minTop, maxTop);
        //        //旋转
        //        int minRotation = -5;
        //        int maxRotation = 5;
        //        int rotation = random.Next(minRotation, maxRotation);
        //        g.RotateTransform(rotation);
        //        g.DrawString(content[i].ToString(), font, brush, rLeft, 0);
        //        g.RotateTransform(-rotation);
        //    }
        //    //画边框
        //    //g.DrawRectangle(new Pen(Color.Gainsboro, 0), 0, 0, bitmap.Width - 1, bitmap.Height - 1);
        //    g.Dispose();
        //    //产生波形
        //    bitmap = TwistImage(bitmap, true, 2, 4);
        //    return bitmap;
        //}
        #endregion

        #region DownImage(下载图片到本地)
        /// <summary>
        /// 获取图片标签
        /// </summary>
        /// <param name="htmlStr">html字符串</param>
        /// <returns></returns>
        private static string[] GetImageTag(string htmlStr)
        {
            Regex regex = new Regex("<img.+?>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string[] strArray = new string[regex.Matches(htmlStr).Count];
            int i = 0;
            foreach (Match match in regex.Matches(htmlStr))
            {
                strArray[i] = GetImageUrl(match.Value);
                i++;
            }
            return strArray;
        }

        /// <summary>
        /// 获取图片Url地址
        /// </summary>
        /// <param name="imgTagStr">图片标签字符串</param>
        /// <returns></returns>
        private static string GetImageUrl(string imgTagStr)
        {
            string str = "";
            Regex regex = new Regex("http://.+.(?:jpg|gif|bmp|png)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (Match match in regex.Matches(imgTagStr))
            {
                str = match.Value;
            }
            return str;
        }

        /// <summary>
        /// 下载图片到本地
        /// </summary>
        /// <param name="html">Html字符串</param>
        /// <param name="path">本地路径</param>
        /// <returns></returns>
        public static string DownImage(string html, string path)
        {
            string year = DateTime.Now.ToString("yyyy-MM");
            string day = DateTime.Now.ToString("dd");
            path = path + year + "/" + day;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] imgUrlArray = GetImageTag(html);
            try
            {
                for (int i = 0; i < imgUrlArray.Length; i++)
                {
                    string preStr = DateTime.Now.ToString() + "_";
                    preStr = preStr.Replace("-", "");
                    preStr = preStr.Replace(":", "");
                    preStr = preStr.Replace(" ", "");
                    WebClient wc = new WebClient();
                    wc.DownloadFile(imgUrlArray[i],
                        path + "/" + preStr + imgUrlArray[i].Substring(imgUrlArray[i].LastIndexOf("/") + 1));
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return html;
        }

        /// <summary>
        /// 下载远程图片
        /// </summary>
        /// <param name="imgUrl">远程图片地址</param>
        /// <param name="path">保存图片路径</param>
        /// <param name="timeout">请求超时时间</param>
        public static bool DownloadRemoteImage(string imgUrl, string path,int timeout=-1)
        {
            bool value = false;
            WebResponse response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(imgUrl);
                request.Method = "GET";
                if (timeout != -1)
                {
                    request.Timeout = timeout;
                }

                response = request.GetResponse();

                if (!response.ContentType.ToLower().StartsWith("text/"))
                {
                    value = SaveBinaryFile(response, Sys.GetPhysicalPath(path));
                }
            }
            catch (Exception e)
            {
                value = false;
                Console.WriteLine(e.Message);
            }
            finally
            {
                response?.Close();
            }

            return value;
        }

        /// <summary>
        /// 保存二进制文件
        /// </summary>
        /// <param name="response">响应流</param>
        /// <param name="savePath">保存路径</param>
        /// <returns></returns>
        private static bool SaveBinaryFile(WebResponse response, string savePath)
        {
            bool value = false;
            byte[] buffer=new byte[1024];
            Stream outStream = null;
            Stream inStream = null;
            try
            {
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }

                outStream = File.Create(savePath);
                inStream = response.GetResponseStream();
                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                    {
                        outStream.Write(buffer, 0, l);
                    }
                } while (l > 0);

                value = true;
            }
            finally
            {
                outStream?.Close();
                inStream?.Close();
            }

            return value;
        }
        #endregion

        #region From(从指定文件创建图片)
        /// <summary>
        /// 从指定文件创建图片
        /// </summary>
        /// <param name="filePath">图片文件的绝对路径</param>
        /// <returns></returns>
        public static Image FromFile(string filePath)
        {
            return Image.FromFile(filePath);
        }
        /// <summary>
        /// 从指定流创建图片
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns></returns>
        public static Image FromStream(Stream stream)
        {
            return Image.FromStream(stream);
        }
        /// <summary>
        /// 从指定字节流创建图片
        /// </summary>
        /// <param name="buffer">字节流</param>
        /// <returns></returns>
        public static Image FromStream(byte[] buffer)
        {
            using (var stream = new MemoryStream(buffer))
            {
                return FromStream(stream);
            }
        }
        #endregion

        #region UndamageCompress(无损压缩图片)
        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="oldFile">原文件路径</param>
        /// <param name="newFile">新文件路径</param>
        /// <param name="flag">压缩质量（数字越小压缩率越高）1-100</param>
        /// <param name="size">压缩后图片的最大大小</param>
        /// <param name="sfsc">是否第一次调用</param>
        /// <returns></returns>
        public static bool UndamageCompress(string oldFile, string newFile, int flag = 90, int size = 300,
            bool sfsc = true)
        {
            //如果是第一次调用，原始图像的大小小于要压缩的大小，则直接复制文件，并且返回true
            FileInfo firstFileInfo = new FileInfo(oldFile);
            if (sfsc == true && firstFileInfo.Length < size * 1024)
            {
                firstFileInfo.CopyTo(newFile);
                return true;
            }
            Image iSource = Image.FromFile(oldFile);
            ImageFormat tFormat = iSource.RawFormat;
            int dHeight = iSource.Height / 2;
            int dWidth = iSource.Width / 2;
            int sW = 0, sH = 9;
            //按比例压缩
            Size temSize = new Size(iSource.Width, iSource.Height);
            if (temSize.Width > dHeight || temSize.Width > dWidth)
            {
                if (temSize.Width * dHeight > temSize.Width * dWidth)
                {
                    sW = dWidth;
                    sH = dWidth * temSize.Height / temSize.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = temSize.Width * dHeight / temSize.Height;
                }
            }
            else
            {
                sW = temSize.Width;
                sH = temSize.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);
            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width,
                iSource.Height, GraphicsUnit.Pixel);
            g.Dispose();

            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;

            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(newFile, jpegICIinfo, ep);
                    FileInfo fi = new FileInfo(newFile);
                    if (fi.Length > 1024 * size)
                    {
                        flag = flag - 10;
                        UndamageCompress(oldFile, newFile, flag, size, false);
                    }
                }
                else
                {
                    ob.Save(newFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }
        }
        #endregion

        #region ResizeImage(重置图片大小)

        /// <summary>
        /// 重置图片大小
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="fillBgColor">填充画笔颜色</param>
        /// <returns></returns>
        public static Image ResizeImage(Image image, int width, int height,Brush fillBgColor)
        {
            try
            {
                Image result = new Bitmap(width, height);
                using (Graphics graphics = Graphics.FromImage(result))
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    if (fillBgColor != null)
                    {
                        graphics.FillRectangle(fillBgColor, new Rectangle(0, 0, result.Width, result.Height));
                    }
                    graphics.DrawImage(image, 0, 0, image.Width, image.Height);
                    image.Dispose();
                }
                return result;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region MergeImage(合并图片)

        /// <summary>
        /// 合并图片
        /// </summary>
        /// <param name="bgImg">背景图片</param>
        /// <param name="img">前景图片</param>
        /// <param name="xDeviation">x轴偏移量</param>
        /// <param name="yDeviation">y轴偏移量</param>
        /// <returns></returns>
        public static Bitmap MergeImage(Image bgImg, Image img, int xDeviation, int yDeviation)
        {
            Bitmap bmp = new Bitmap(bgImg.Width, bgImg.Height);

            Graphics g=Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.DrawImage(bgImg, 0, 0, bgImg.Width, bgImg.Height);

            g.DrawImage(img, xDeviation, yDeviation, img.Width, img.Height);

            GC.Collect();
            return bmp;
        }

        /// <summary>
        /// 合并图片
        /// </summary>
        /// <param name="bgImgPath">背景图片路径，绝对路径</param>
        /// <param name="imgPath">前景图片路径，绝对路径</param>
        /// <param name="outputPath">输出文件路径，相对路径</param>
        /// <param name="xDeviation">x轴偏移量</param>
        /// <param name="yDeviation">y轴偏移量</param>
        /// <returns></returns>
        public static string MergeImage(string bgImgPath, string imgPath,string outputPath, int xDeviation, int yDeviation)
        {
            var bgImage = FromFile(Sys.GetPhysicalPath(bgImgPath));
            var fgImage = FromFile(Sys.GetPhysicalPath(imgPath));
            var bitmap = MergeImage(bgImage, fgImage, xDeviation, yDeviation);
            string physicalPath = Sys.GetPhysicalPath(outputPath);
            string extName = Path.GetExtension(physicalPath);
            if (extName.IsEmpty())
            {
                string fileName= DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                outputPath += fileName;
                physicalPath += fileName;
            }
            
            bitmap.Save(physicalPath, ImageFormat.Jpeg);
            bitmap.Dispose();
            bgImage.Dispose();
            fgImage.Dispose();
            return outputPath;
        }

        #endregion
    }
}
