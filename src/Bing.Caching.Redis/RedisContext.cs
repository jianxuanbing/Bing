using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.HashAlgorithms;
using Bing.Helpers;

namespace Bing.Caching.Redis
{
    /// <summary>
    /// Redis 数据上下文
    /// </summary>
    public class RedisContext
    {
        /// <summary>
        /// 缓存对象集合容器池
        /// </summary>
        internal Lazy<Dictionary<string, CacheConfig>> CachingContextPool;

        /// <summary>
        /// 默认缓存失效时间
        /// </summary>
        internal string DefaultExpireTime;

        /// <summary>
        /// 连接超时时间
        /// </summary>
        internal string ConnectionTimeout;

        /// <summary>
        /// 规则名（现在只实现哈希一致性）
        /// </summary>
        internal string RuleName;

        /// <summary>
        /// 哈希节点容器
        /// </summary>
        internal ConcurrentDictionary<string, ConsistentHash<ConsistentHashNode>> DicHash;

        /// <summary>
        /// 缓存对象集合容器池
        /// </summary>
        public Dictionary<string, CacheConfig> DataContextPool => CachingContextPool.Value;

        /// <summary>
        /// 初始化一个<see cref="RedisContext"/>类型的实例
        /// </summary>
        /// <param name="rule">规则</param>
        /// <param name="args">参数</param>
        public RedisContext(string rule, params CacheConfig[] args)
        {
            CachingContextPool=new Lazy<Dictionary<string, CacheConfig>>(() =>
            {
                var dataContextPool=new Dictionary<string,CacheConfig>();
                foreach (var arg in args)
                {
                    dataContextPool.Add(arg.CacheType,arg);
                }

                return dataContextPool;
            });
            RuleName = rule;
            DicHash=new ConcurrentDictionary<string, ConsistentHash<ConsistentHashNode>>();
            InitSettingHashStorage();
        }

        /// <summary>
        /// 初始化设置哈希节点容器
        /// </summary>
        private void InitSettingHashStorage()
        {
            foreach (var dataContext in DataContextPool)
            {
                CacheTargetType targetType;
                if (!Enum.TryParse(dataContext.Key, true, out targetType))
                {
                    continue;
                }

                var hash = new ConsistentHash<ConsistentHashNode>(Ioc.Create<IHashAlgorithm>());
                hash.Add(new ConsistentHashNode()
                {
                    Type = targetType,
                    Host = dataContext.Value.Host,
                    Port = dataContext.Value.Port.ToString(),
                    UserName = dataContext.Value.UserName,
                    Password = dataContext.Value.Password,
                    MaxSize = dataContext.Value.MaxSize.ToString(),
                    MinSize = dataContext.Value.MinSize.ToString(),
                    Db = dataContext.Value.Db.ToString()
                });
                DicHash.GetOrAdd(targetType.ToString(), hash);
            }
        }
    }
}
