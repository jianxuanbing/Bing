using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Configuration.Core;
using Bing.Samples.Consoles.JsonConfig.Models;
using Bing.Utils.Json;

namespace Bing.Samples.Consoles.JsonConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            var key = Console.ReadLine();
            SampleConfiguration config = new SampleConfiguration();
            while (key != "0")
            {                
                switch (key)
                {
                    case "1":
                        config = ConfigProvider.Default.GetConfiguration<SampleConfiguration>();
                        Console.WriteLine(config.ToJson());
                        break;
                    case "2":
                        ConfigProvider.Default.RefreshAll();
                        break;
                    case "3":
                        config.IntProp=new Random().Next(10);
                        ConfigProvider.Default.SetConfiguration(config);
                        break;
                }                                
                key = Console.ReadLine();
            }
        }
    }
}
