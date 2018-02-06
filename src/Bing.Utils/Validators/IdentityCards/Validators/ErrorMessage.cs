using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Validators.IdentityCards.Validators
{
    /// <summary>
    /// 错误提示信息
    /// </summary>
    internal class ErrorMessage
    {
        /// <summary>
        /// 身份证号码为空
        /// </summary>
        public const string EMPTY = "身份证号码为空";

        /// <summary>
        /// 错误的身份证号码
        /// </summary>
        public const string ERROR = "错误的身份证号码";

        /// <summary>
        /// 无效的出生日期
        /// </summary>
        public const string INVALID_BIRTHDAY = "无效的出生日期";

        /// <summary>
        /// 出生日期超出允许的年份范围
        /// </summary>
        public const string BIRTHDAY_YEAR_OUT_OF_RANGE = "出生日期超出允许的年份范围{0} ~ {1}";

        /// <summary>
        /// 行政区划识别失败
        /// </summary>
        public const string INVALID_AREA = "行政区划识别失败";

        /// <summary>
        /// 行政区划识别度不足
        /// </summary>
        public const string AREA_LIMIT_OUT_OF_RANGE = "行政区划识别度低于识别级别 {0}";

        /// <summary>
        /// 错误的校验码
        /// </summary>
        public const string INVALID_CHECK_BIT = "错误的校验码";

        /// <summary>
        /// 无效实现
        /// </summary>
        public const string INVALID_IMPLEMENT = "未能找到或无效的 {0} 位身份证实现";

        /// <summary>
        /// 长度错误
        /// </summary>
        public const string LENGTH_OUT_OF_RANGE = "身份证号码非 {0} 位";
    }
}
