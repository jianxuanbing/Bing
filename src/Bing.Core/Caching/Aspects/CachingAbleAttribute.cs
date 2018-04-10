using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;

namespace Bing.Caching.Aspects
{
    /// <summary>
    /// 读取/写入缓存 属性
    /// </summary>
    public class CachingAbleAttribute:CachingAttributeBase
    {
        /// <summary>
        /// 获取或设置 到期时间。默认值：30秒
        /// </summary>
        public int Expiration { get; set; } = 30;

        /// <summary>
        /// 执行上下文
        /// </summary>
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var cacheKey =
                KeyGenerator.GetCacheKey(context.ServiceMethod, context.Parameters, CacheKeyPrefix);
            //var cacheValue = await CacheProvider.GetAsync<object>(cacheKey);// Redis 堵塞
            var cacheValue = CacheProvider.Get<object>(cacheKey);

            if (cacheValue.HasValue)
            {
                context.ReturnValue = cacheValue.Value;
            }
            else
            {
                await next(context);
                if (!string.IsNullOrWhiteSpace(cacheKey) && context.ReturnValue != null)
                {
                    //await CacheProvider.SetAsync(cacheKey, context.ReturnValue,
                    //    TimeSpan.FromSeconds(Expiration));// Redis 堵塞
                    CacheProvider.Set(cacheKey, context.ReturnValue,
                        TimeSpan.FromSeconds(Expiration));
                }
            }
        }
    }
}
