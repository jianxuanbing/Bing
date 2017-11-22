using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.WebApi;
using Bing.Core.Dependency;

namespace Bing.Samples.Api.Configs
{
    public class IocConfig:ConfigBase
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            
        }
    }
}