using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Applications.Dtos
{
    /// <summary>
    /// 时间范围
    /// </summary>
    public class DateTimeRange : IRange<DateTime?>
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? Begin { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? End { get; set; }
    }
}
