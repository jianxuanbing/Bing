using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Datas.Attributes
{
    /// <summary>
    /// 自定义Decimal类型的精度
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,Inherited = false,AllowMultiple = false)]
    public class DecimalPrecisionAttribute:Attribute
    {
        /// <summary>
        /// 精确度，默认：18
        /// </summary>
        public byte Precision { get; set; }

        /// <summary>
        /// 保留小数位数，默认：4
        /// </summary>
        public byte Scale { get; set; }

        /// <summary>
        /// 初始化一个<see cref="DecimalPrecisionAttribute"/>类型的实例
        /// </summary>
        /// <param name="precision">精确度</param>
        /// <param name="scale">保留小数位数</param>
        public DecimalPrecisionAttribute(byte precision, byte scale)
        {
            Precision = precision;
            Scale = scale;
        }
    }
}
