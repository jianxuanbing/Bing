using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspectCore.Extensions.Autofac;
using Autofac;
using Bing.Dependency;
using Bing.Logs.Log4Net;
using AspectCore.DynamicProxy.Parameters;
using Bing.Caching;

namespace Bing.Samples.Consoles.Configs
{
    public class IocConfig:ConfigBase
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.AddLog4Net();
            //builder.RegisterDynamicProxy(config =>
            //{
            //    config.EnableParameterAspect();
            //});
        }
    }
}
