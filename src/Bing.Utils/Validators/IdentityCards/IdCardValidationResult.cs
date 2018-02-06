using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Validators.IdentityCards
{
    /// <summary>
    /// 身份证验证结果类
    /// </summary>
    public class IdCardValidationResult:ValidationResult
    {
        /// <summary>
        /// 长度
        /// </summary>
        public IdCardLength Length { get; internal set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; internal set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; internal set; }

        /// <summary>
        /// 行政区域编码
        /// </summary>
        public int AreaNumber { get; internal set; }

        /// <summary>
        /// 出生登记顺序号
        /// </summary>
        public int Sequence { get; internal set; }

        /// <summary>
        /// 身份证校验码
        /// </summary>
        public char CheckBit { get; internal set; }
    }
}
