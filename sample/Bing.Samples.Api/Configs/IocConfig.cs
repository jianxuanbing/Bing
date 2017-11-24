using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using AspectCore.Configuration;
using AspectCore.DynamicProxy.Parameters;
using AspectCore.Extensions.Autofac;
using Autofac;
using Autofac.Integration.WebApi;
using Bing.Aspects;
using Bing.Dependency;
using Bing.Logs.Aspects;
using Bing.Logs.Exceptionless;
using Bing.Logs.Log4Net;
using Bing.Logs.NLog;

namespace Bing.Samples.Api.Configs
{
    public class IocConfig:ConfigBase
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            

            builder.RegisterDynamicProxy(config =>
            {
                config.EnableParameterAspect();
                //config.Interceptors.AddTyped<DebugLogAttribute>();
                config.Interceptors.AddTyped<DebugLogAttribute>(Predicates.ForService("*Service"));
            });

            builder.AddLog4Net();

            //builder.AddNLog();

            //builder.AddExceptionless(config =>
            //{
            //    config.ApiKey = "CqcBoQlNP1FBxCWLe0o5ZpX3eSmB3JqK4QUvDGUw";
            //    config.ServerUrl = "http://192.168.88.20:10240";
            //});
        }
    }
}