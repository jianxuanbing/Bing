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
using Bing.Caching.Core;
using Bing.Caching.Redis;
using Bing.Datas.EntityFramework;
using Bing.Datas.UnitOfWorks;
using Bing.Dependency;
using Bing.Logs.Aspects;
using Bing.Logs.Exceptionless;
using Bing.Logs.Log4Net;
using Bing.Logs.NLog;
using Bing.Samples.Datas;

namespace Bing.Samples.Api.Configs
{
    public class IocConfig:ConfigBase
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.AddLog4Net();
            builder.AddBingUnitOfWork("WeiHai");

            builder.RegisterDynamicProxy(config =>
            {
                config.EnableParameterAspect();
            });

            builder.AddNLog("nlog");

            builder.AddExceptionless(config =>
            {
                config.ApiKey = "CqcBoQlNP1FBxCWLe0o5ZpX3eSmB3JqK4QUvDGUw";
                config.ServerUrl = "http://192.168.3.113:8070";
            });
            builder.AddDefaultRedisCache(config =>
            {
                config.EndPoints.Add(new ServerEndPoint("192.168.3.115", 9494));
                config.Password = "";
            });
        }
    }
}