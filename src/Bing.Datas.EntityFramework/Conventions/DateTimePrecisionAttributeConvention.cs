using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Datas.Attributes;

namespace Bing.Datas.EntityFramework.Conventions
{
    /// <summary>
    /// DateTime 精确度约定，用于 ModelBuilder 全局设置 DateTime 精确度属性
    /// </summary>
    public class DateTimePrecisionAttributeConvention:PrimitivePropertyAttributeConfigurationConvention<DateTimePrecisionAttribute>
    {
        /// <summary>
        /// 应用
        /// </summary>
        public override void Apply(ConventionPrimitivePropertyConfiguration configuration, DateTimePrecisionAttribute attribute)
        {
            configuration.HasPrecision(attribute.Value);
        }
    }
}
