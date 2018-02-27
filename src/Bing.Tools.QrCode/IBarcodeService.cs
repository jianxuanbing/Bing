using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Tools.QrCode
{
    /// <summary>
    /// 条码 服务
    /// </summary>
    public interface IBarcodeService
    {
        /// <summary>
        /// 设置条码尺寸
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        IBarcodeService Size(int width,int height);

        /// <summary>
        /// 设置容错处理
        /// </summary>
        /// <param name="level">容错级别</param>
        /// <returns></returns>
        IBarcodeService Correction(ErrorCorrectionLevel level);

        /// <summary>
        /// 显示内容
        /// </summary>
        /// <returns></returns>
        IBarcodeService ShowContent();

        /// <summary>
        /// 生成条码并保存到指定位置，返回条码图片完整路径
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        string Save(string content);

        /// <summary>
        /// 生成条码并转换成Base64字符串
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        string SaveBase64(string content);
    }
}
