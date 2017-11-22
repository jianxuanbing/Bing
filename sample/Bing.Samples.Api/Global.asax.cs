using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac.Integration.WebApi;
using Bing.Core;
using Bing.Core.Dependency;
using Bing.Core.Helpers;
using Bing.Samples.Api.Configs;

namespace Bing.Samples.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            IocConfigInitialize.Init(ScopeType.Http,new IConfig[] {new IocConfig(), });

            GlobalConfiguration.Configuration.DependencyResolver=new AutofacWebApiDependencyResolver(Ioc.GetContainer());
        }
    }
}
