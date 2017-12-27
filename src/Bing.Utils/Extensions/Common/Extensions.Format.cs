using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Extensions
{
    /// <summary>
    /// 系统扩展 - 格式化扩展
    /// </summary>
    public static partial class Extensions
    {
        #region Description(获取布尔值描述)

        /// <summary>
        /// 获取布尔值描述
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string Description(this bool value)
        {
            return value ? "是" : "否";
        }

        /// <summary>
        /// 获取布尔值描述
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string Description(this bool? value)
        {
            return value == null ? "" : Description(value.Value);
        }

        #endregion
    }
}
