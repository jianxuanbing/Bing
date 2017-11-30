using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac.Integration.WebApi;
using Bing;
using Bing.Dependency;
using Bing.Helpers;
using Bing.Logs;
using Bing.Samples.Api.Configs;

namespace Bing.Samples.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            IocConfigInitialize.Init(ScopeType.Http,new IConfig[] {new IocConfig(), });
            var logger = Ioc.Create<ILog>();
            logger.Info("初始化信息成功");
            GlobalConfiguration.Configuration.DependencyResolver=new AutofacWebApiDependencyResolver(Ioc.GetContainer());
        }
    }
}
