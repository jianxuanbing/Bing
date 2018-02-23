using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bing.Events.Handlers;
using Bing.Logs.Aspects;
using Bing.Samples.Services.Events;

namespace Bing.Samples.Services.Handlers
{
    public class TestEventHandler:IEventHandler<TestEvent>
    {
        [DebugLog]
        public void Handle(TestEvent @event)
        {
            Thread.Sleep(3000);
        }
    }
}
