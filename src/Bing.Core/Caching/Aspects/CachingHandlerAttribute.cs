using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using AspectCore.Injector;
using Bing.Aspects.Base;
using Bing.Caching.Abstractions;

namespace Bing.Caching.Aspects
{
    /// <summary>
    /// 缓存处理程序
    /// </summary>
    public class CachingHandlerAttribute:InterceptorBase
    {
        /// <summary>
        /// 获取或设置 缓存过期时间。单位：秒
        /// </summary>
        public int Expiration { get; set; } = 30;

        /// <summary>
        /// 获取或设置 参数数量
        /// </summary>
        public int ParamCount { get; set; } = 5;

        /// <summary>
        /// 获取或设置 缓存提供程序
        /// </summary>
        [FromContainer]
        public ICacheProvider CacheProvider { get; set; }

        /// <summary>
        /// 缓存键连接字符
        /// </summary>
        private char _linkChar = ':';

        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var cacheKey = GenerateCacheKey(context, ParamCount);
            var type = context.ServiceMethod.ReturnType;
            var cacheValue = CacheProvider.Get(cacheKey,type);
            if (cacheValue.HasValue)
            {
                context.ReturnValue = cacheValue.Value;
                return;
            }
            await next(context);

            if (!string.IsNullOrWhiteSpace(cacheKey))
            {
                CacheProvider.Set(cacheKey,context.ReturnValue,TimeSpan.FromSeconds(Expiration));
            }
        }

        /// <summary>
        /// 生成缓存键
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="paramCount">参数数量</param>
        /// <returns></returns>
        private string GenerateCacheKey(AspectContext context, int paramCount)
        {
            var typeName = context.ServiceMethod.DeclaringType?.Name;
            var methodName = context.ServiceMethod.Name;
            var methodArguments =
                this.FormatArgumentsToPartOfCacheKey(context.GetParameters(), paramCount);

            return this.GenerateCacheKey(typeName, methodName, methodArguments);
        }

        /// <summary>
        /// 生成缓存键
        /// </summary>
        /// <param name="typeName">类型名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        private string GenerateCacheKey(string typeName, string methodName, IList<string> parameters)
        {
            var builder=new StringBuilder();
            builder.Append(typeName);
            builder.Append(_linkChar);
            builder.Append(methodName);
            builder.Append(_linkChar);
            foreach (var parameter in parameters)
            {
                builder.Append(parameter);
                builder.Append(_linkChar);
            }
            return builder.ToString().TrimEnd(_linkChar);
        }

        /// <summary>
        /// 格式化参数并返回部分缓存键
        /// </summary>
        /// <param name="methodArguments">方法参数</param>
        /// <param name="paramCount">参数数量</param>
        /// <returns></returns>
        private IList<string> FormatArgumentsToPartOfCacheKey(ParameterCollection methodArguments, int paramCount = 5)
        {
            return methodArguments.Select(x=>this.GetArgumentValue(x.Value)).Take(paramCount).ToList();
        }

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="arg">参数</param>
        /// <returns></returns>
        private string GetArgumentValue(object arg)
        {
            if (arg is int || arg is long || arg is string || arg is Guid || arg is decimal)
            {
                return arg.ToString();
            }
            if (arg is DateTime)
            {
                return ((DateTime) arg).ToString("yyyyMMddHHmmss");
            }
            if (arg is ICacheable)
            {
                return ((ICacheable) arg).CacheKey;
            }
            return null;
        }
    }
}
