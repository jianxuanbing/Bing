using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Samples.Consoles.JsonConfig.Models
{
    /// <summary>
    /// 简单配置
    /// </summary>
    public class SampleConfiguration
    {
        public string StringProp { get; set; }

        public int IntProp { get; set; }

        public decimal DecimalProp { get; set; }

        public DateTime DateTimeProp { get; set; }

        public long LongProp { get; set; }
    }
}
