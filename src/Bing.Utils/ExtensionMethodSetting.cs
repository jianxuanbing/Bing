using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils
{
    /// <summary>
    /// 扩展方法设置
    /// </summary>
    public class ExtensionMethodSetting
    {
        /// <summary>
        /// 初始化<see cref="ExtensionMethodSetting"/>静态实例
        /// </summary>
        static ExtensionMethodSetting()
        {
            DefaultEncoding = Encoding.UTF8;
            DefaultCulture = CultureInfo.CurrentCulture;
        }

        /// <summary>
        /// 默认编码
        /// </summary>
        public static Encoding DefaultEncoding { get; set; }

        /// <summary>
        /// 默认区域设置
        /// </summary>
        public static CultureInfo DefaultCulture { get; set; }
    }
}
