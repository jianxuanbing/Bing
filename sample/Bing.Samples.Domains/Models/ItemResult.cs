using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Samples.Domains.Models
{
    public class ItemResult
    {
        public object Value { get; set; }

        public string Text { get; set; }

        public int? SortId { get; set; }
    }
}
