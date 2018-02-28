using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.OneD;
using ZXing.QrCode;
using ZQI = global::ZXing.QrCode.Internal;


namespace Bing.Tools.QrCode.ZXing
{
    /// <summary>
    /// ZXing.Net 条码服务
    /// </summary>
    public class ZXingBarcodeService:IBarcodeService
    {
        /// <summary>
        /// 宽
        /// </summary>
        private int _width;

        /// <summary>
        /// 高
        /// </summary>
        private int _height;

        /// <summary>
        /// 容错级别
        /// </summary>
        private ZQI.ErrorCorrectionLevel _level;

        /// <summary>
        /// 是否显示内容
        /// </summary>
        private bool _showContent;

        /// <summary>
        /// 边距
        /// </summary>
        private int _margin;

        /// <summary>
        /// 初始化一个<see cref="ZXingBarcodeService"/>类型的实例
        /// </summary>
        public ZXingBarcodeService()
        {
            _width = 150;
            _height = 150;
            _level = ZQI.ErrorCorrectionLevel.L;
            _showContent = false;
        }

        /// <summary>
        /// 设置条码尺寸
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        public IBarcodeService Size(int width, int height)
        {
            _width = width;
            _height = height;
            return this;
        }

        /// <summary>
        /// 设置容错处理
        /// </summary>
        /// <param name="level">容错级别</param>
        /// <returns></returns>
        public IBarcodeService Correction(ErrorCorrectionLevel level)
        {
            switch (level)
            {
                case ErrorCorrectionLevel.L:
                    _level = ZQI.ErrorCorrectionLevel.L;
                    break;
                case ErrorCorrectionLevel.M:
                    _level = ZQI.ErrorCorrectionLevel.M;
                    break;
                case ErrorCorrectionLevel.Q:
                    _level = ZQI.ErrorCorrectionLevel.Q;
                    break;
                case ErrorCorrectionLevel.H:
                    _level = ZQI.ErrorCorrectionLevel.H;
                    break;
            }
            return this;
        }

        /// <summary>
        /// 显示内容
        /// </summary>
        /// <returns></returns>
        public IBarcodeService ShowContent()
        {
            _showContent = true;
            return this;
        }

        /// <summary>
        /// 设置边距
        /// </summary>
        /// <param name="margin">边距</param>
        /// <returns></returns>
        public IBarcodeService Margin(int margin)
        {
            _margin = margin;
            return this;
        }

        /// <summary>
        /// 生成条码并保存到指定位置，返回条码图片完整路径
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public string Save(string content)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 生成条码并转换成Base64字符串
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public string SaveBase64(string content)
        {
            var qrCode = CreateBarcode(content);
            var base64Str = "data:image/png;base64," + Convert.ToBase64String(qrCode);
            return base64Str;
        }

        private byte[] CreateBarcode(string content)
        {
            // 使用ITF 格式，不能被现在常用的支付宝、微信扫出来
            // 如果想生成可识别的可以使用 CODE_128 格式
            var qrCodeWriter = new BarcodeWriterPixelData()
            {
                Format = BarcodeFormat.CODE_128,
                Options = new Code128EncodingOptions()
                {
                    Height = _height,
                    Width = _width,
                    Margin = _margin,
                    PureBarcode = _showContent,                    
                    Hints =
                    {
                        {EncodeHintType.CHARACTER_SET,"UTF-8" },
                        {EncodeHintType.ERROR_CORRECTION,_level }
                    }
                }
            };

            var pixelData = qrCodeWriter.Write(content);
            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                using (var ms = new MemoryStream())
                {
                    var bitmapData =
                        bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
                            System.Drawing.Imaging.ImageLockMode.WriteOnly,
                            System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    try
                    {
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
                            pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }

                    bitmap.MakeTransparent();

                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
    }
}
