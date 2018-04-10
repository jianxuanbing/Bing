using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;

namespace Bing.Caching.Aspects
{
    /// <summary>
    /// 移除缓存 属性
    /// </summary>
    public class CachingEvictAttribute:CachingAttributeBase
    {
        /// <summary>
        /// 获取或设置 是否通过缓存键前缀移除所有缓存值
        /// </summary>
        public bool IsAll { get; set; } = false;

        /// <summary>
        /// 获取或设置 是否允许操作之前
        /// </summary>
        public bool IsBefore { get; set; } = false;

        /// <summary>
        /// 执行上下文
        /// </summary>
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            await ProcessEvictAsync(context, true);

            await next(context);

            await ProcessEvictAsync(context, false);
        }

        /// <summary>
        /// 处理 移除 缓存操作
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="isBefore">是否操作之前执行</param>
        /// <returns></returns>
        private async Task ProcessEvictAsync(AspectContext context, bool isBefore)
        {
            if (IsBefore == isBefore)
            {
                if (IsAll)
                {
                    // 如果是全部，清除缓存键指定前缀开头的所有缓存项
                    var cachePrefix = KeyGenerator.GetCacheKeyPrefix(context.ServiceMethod, CacheKeyPrefix);

                    //await CacheProvider.RemoveByPrefixAsync(cachePrefix);// Redis 堵塞
                    CacheProvider.RemoveByPrefix(cachePrefix);
                }
                else
                {
                    // 如果不是全部，只需通过其缓存键删除缓存的项目
                    var cacheKey = KeyGenerator.GetCacheKey(context.ServiceMethod, context.Parameters,
                        CacheKeyPrefix);
                    //await CacheProvider.RemoveAsync(cacheKey);// Redis 堵塞
                    CacheProvider.Remove(cacheKey);
                }
            }
        }
    }
}
