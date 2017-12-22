using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Bing.Datas.Attributes;

namespace Bing.Datas.EntityFramework.Conventions
{
    /// <summary>
    /// Decimal精确度约定，用于ModelBuilder全局设置Decimal精确度属性
    /// </summary>
    public class DecimalPrecisionAttributeConvention:PrimitivePropertyAttributeConfigurationConvention<DecimalPrecisionAttribute>
    {
        /// <summary>
        /// 应用
        /// </summary>
        public override void Apply(ConventionPrimitivePropertyConfiguration configuration, DecimalPrecisionAttribute attribute)
        {
            if (attribute.Precision < 1 || attribute.Precision > 38)
            {
                throw new InvalidOperationException("Precision must be between 1 and 38.");
            }
            if (attribute.Scale > attribute.Precision)
            {
                throw new InvalidOperationException("Scale must be between 0 and the Precision value.");
            }
            configuration.HasPrecision(attribute.Precision, attribute.Scale);
        }
    }
}
