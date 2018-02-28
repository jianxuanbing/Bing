using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Tools.QrCode
{
    /// <summary>
    /// 二维码 服务
    /// </summary>
    public interface IQrCodeService
    {
        /// <summary>
        /// 设置二维码尺寸
        /// </summary>
        /// <param name="size">二维码尺寸</param>
        /// <returns></returns>
        IQrCodeService Size(QrSize size);

        /// <summary>
        /// 设置二维码尺寸
        /// </summary>
        /// <param name="size">二维码尺寸</param>
        /// <returns></returns>
        IQrCodeService Size(int size);

        /// <summary>
        /// 设置二维码尺寸
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        IQrCodeService Size(int width, int height);

        /// <summary>
        /// 设置边距
        /// </summary>
        /// <param name="margin">边距</param>
        /// <returns></returns>
        IQrCodeService Margin(int margin);

        /// <summary>
        /// 设置容错处理
        /// </summary>
        /// <param name="level">容错级别</param>
        /// <returns></returns>
        IQrCodeService Correction(ErrorCorrectionLevel level);

        /// <summary>
        /// 设置二维码Logo
        /// </summary>
        /// <param name="filePath">Logo文件路径</param>
        /// <returns></returns>
        IQrCodeService Logo(string filePath);

        /// <summary>
        /// 生成二维码并保存到指定位置，返回二维码图片完整路径
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        string Save(string content);

        /// <summary>
        /// 生成二维码并转换成Base64字符串
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        string SaveBase64(string content);
    }
}
