using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Events;

namespace Bing.Samples.Services.Events
{
    /// <summary>
    /// 测试事件
    /// </summary>
    public class TestEvent:Event
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }        
    }
}
