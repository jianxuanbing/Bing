using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;

namespace Bing.Caching.Aspects
{
    /// <summary>
    /// 写入缓存 属性
    /// </summary>
    public class CachingPutAttribute:CachingAttributeBase
    {
        /// <summary>
        /// 获取或设置 到期时间。默认值：30秒
        /// </summary>
        public int Expiration { get; set; } = 30;

        /// <summary>
        /// 执行方法
        /// </summary>
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            await next(context);
            if (context.ReturnValue != null)
            {
                var cacheKey =
                    KeyGenerator.GetCacheKey(context.ServiceMethod, context.Parameters, CacheKeyPrefix);
                //await CacheProvider.SetAsync(cacheKey, context.ReturnValue, TimeSpan.FromSeconds(Expiration));// Redis 堵塞
                CacheProvider.Set(cacheKey, context.ReturnValue, TimeSpan.FromSeconds(Expiration));
            }            
        }
    }
}
