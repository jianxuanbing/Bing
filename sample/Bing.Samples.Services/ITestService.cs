using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Aspects;
using Bing.Dependency;
using Bing.Logs.Aspects;
using Bing.Samples.Domains.Models;

namespace Bing.Samples.Services
{
    public interface ITestService: IScopeDependency
    {
        string GetContent([NotEmpty]string content);

        [DebugLog]
        void WriteOtherLog(string content);

        List<ItemResult> GetItems();

        void PublishEvent(string name);

        void PublishMessageEvent(string name);
    }
}
