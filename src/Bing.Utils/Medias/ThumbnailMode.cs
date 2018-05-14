using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Medias
{
    /// <summary>
    /// 缩略图枚举
    /// </summary>
    public enum ThumbnailMode
    {
        /// <summary>
        /// 指定高宽裁剪（不变形）
        /// </summary>
        Cut = 1,
        /// <summary>
        /// 指定宽度，高度自动
        /// </summary>
        FixedW = 2,
        /// <summary>
        /// 指定高度，宽度自动
        /// </summary>
        FixedH = 4,
        /// <summary>
        /// 宽度跟高度都制定，但是会变形
        /// </summary>
        FixedBoth = 8
    }
}
