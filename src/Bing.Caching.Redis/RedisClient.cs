using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Extensions;
using Bing.Utils.Json;
using StackExchange.Redis;

namespace Bing.Caching.Redis
{
    /// <summary>
    /// Redis 客户端
    /// </summary>
    public class RedisClient:IRedisClient
    {
        /// <summary>
        /// Redis库编号
        /// </summary>
        private int DbNum { get; }

        /// <summary>
        /// redis多连接复用器
        /// </summary>
        private readonly ConnectionMultiplexer _redis;

        /// <summary>
        /// 自定义键
        /// </summary>
        private string CustomKey { get; set; }

        /// <summary>
        /// Redis连接字符串
        /// </summary>
        private string ConnectioNString { get; set; }

        /// <summary>
        /// 初始化一个<see cref="RedisClient"/>类型的实例
        /// </summary>
        /// <param name="dbNum">数据库索引</param>
        public RedisClient(int dbNum=0) : this(dbNum, null)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="RedisClient"/>类型的实例
        /// </summary>
        /// <param name="dbNum">数据库索引</param>
        /// <param name="readWriteHosts">读写主机列表</param>
        public RedisClient(int dbNum, string readWriteHosts)
        {
            DbNum = dbNum;
            _redis = string.IsNullOrWhiteSpace(readWriteHosts)
                ? RedisManager.Instance
                : RedisManager.GetConnectionMultiplexer(readWriteHosts);
        }

        #region 辅助方法

        /// <summary>
        /// 添加自定义缓存键
        /// </summary>
        /// <param name="oldKey">旧缓存键</param>
        /// <returns></returns>
        private string AddSysCustomKey(string oldKey)
        {
            var prefixKey = CustomKey ?? RedisManager.SysCustomKey;
            return prefixKey + oldKey;
        }

        /// <summary>
        /// 执行数据方法请求，返回结果
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="func">执行方法</param>
        /// <returns></returns>
        private T Do<T>(Func<IDatabase, T> func)
        {
            var database = _redis.GetDatabase(DbNum);
            return func(database);
        }

        /// <summary>
        /// 执行数据方法请求，不返回结果
        /// </summary>
        /// <param name="action">执行方法</param>
        private void Do(Action<IDatabase> action)
        {
            var database = _redis.GetDatabase(DbNum);
            action(database);
        }

        /// <summary>
        /// 将对象转换为Json字符串
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        private string ToJson(object value)
        {
            string result = value is string ? value.ToString() : value.ToJson();
            return result;
        }

        /// <summary>
        /// 将RedisValue反序列化成对象
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        private T ToObj<T>(RedisValue value)
        {
            return value.SafeString().ToObject<T>();
        }

        /// <summary>
        /// 将RedisValue反序列化成对象列表
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="values">值</param>
        /// <returns></returns>
        private List<T> ToList<T>(RedisValue[] values)
        {
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                var model = ToObj<T>(item);
                result.Add(model);
            }

            return result;
        }

        /// <summary>
        /// 将字符串列表转换成RedisKey[]
        /// </summary>
        /// <param name="redisKeys">字符串列表</param>
        /// <returns></returns>
        private RedisKey[] ConvertRedisKeys(List<string> redisKeys)
        {
            return redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray();
        }

        #endregion

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _redis?.Dispose();
        }

        #region String(字符串类型数据操作)

        public bool StringSet(string key, object value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var content = ToJson(value);
            key = AddSysCustomKey(key);
            return Do(db => db.StringSet(key, content, expiry, when, commandFlags));
        }

        public bool StringSet<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var content = ToJson(value);
            key = AddSysCustomKey(key);
            return Do(db => db.StringSet(key, content, expiry, when, commandFlags));
        }

        public bool StringSet(List<KeyValuePair<RedisKey, RedisValue>> keyValues)
        {
            List<KeyValuePair<RedisKey, RedisValue>> newKeyValues =
                keyValues.Select(p => new KeyValuePair<RedisKey, RedisValue>(AddSysCustomKey(p.Key), p.Value)).ToList();
            return Do(db => db.StringSet(newKeyValues.ToArray()));
        }

        public bool StringUpdate<T>(string key, T value, TimeSpan expiresAt, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var content = ToJson(value);
            key = AddSysCustomKey(key);
            return Do(db => db.StringSet(key, content, expiresAt, when, commandFlags));
        }

        public T StringGet<T>(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            try
            {
                key = AddSysCustomKey(key);
                return Do(db =>
                {
                    RedisValue str = db.StringGet(key, commandFlags);
                    if (str.HasValue && !str.IsNullOrEmpty)
                    {
                        return ToObj<T>(str);
                    }
                    return default(T);
                });
            }
            catch (Exception ex)
            {
                //输出错误日志
                return default(T);
            }
        }

        public RedisValue[] StringGet(List<string> listKey)
        {
            List<string> newKeys = listKey.Select(AddSysCustomKey).ToList();
            return Do(db => db.StringGet(ConvertRedisKeys(newKeys)));
        }

        public long StringLength(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.StringLength(key, commandFlags));
        }

        public long StringAppend(string key, string appendVal, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.StringAppend(key, appendVal, commandFlags));
        }

        public string StringGetAnSet(string key, string newVal, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return ToObj<string>(Do(db => db.StringGetSet(key, newVal, commandFlags)));
        }

        public double StringIncrement(string key, double value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.StringIncrement(key, value, commandFlags));
        }

        public double StringDecrement(string key, double value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.StringDecrement(key, value, commandFlags));
        }

        public List<T> StringGetList<T>(string key, int pageSize = 100, CommandFlags commandFlags = CommandFlags.None) where T : class
        {
            try
            {
                key = AddSysCustomKey(key);
                var server = GetServer();
                var keys = server.Keys(DbNum, key, pageSize, commandFlags);
                var keyValues = Do(db => db.StringGet(keys.ToArray(), commandFlags));

                var result = new List<T>();
                foreach (RedisValue redisValue in keyValues)
                {
                    if (redisValue.HasValue && !redisValue.IsNullOrEmpty)
                    {
                        var item = ToObj<T>(redisValue);
                        result.Add(item);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                //输出错误日志
                return null;
            }
        }

        public async Task<bool> StringSetAsync(string key, object value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var content = ToJson(value);
            key = AddSysCustomKey(key);
            return await Do(db => db.StringSetAsync(key, content, expiry, when, commandFlags));
        }

        public async Task<bool> StringSetAsync<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var content = ToJson(value);
            key = AddSysCustomKey(key);
            return await Do(db => db.StringSetAsync(key, content, expiry, when, commandFlags));
        }

        public async Task<bool> StringSetAsync(List<KeyValuePair<RedisKey, RedisValue>> keyValues)
        {
            List<KeyValuePair<RedisKey, RedisValue>> newKeyValues =
                keyValues.Select(p => new KeyValuePair<RedisKey, RedisValue>(AddSysCustomKey(p.Key), p.Value)).ToList();
            return await Do(db => db.StringSetAsync(newKeyValues.ToArray()));
        }

        public async Task<bool> StringUpdateAsync<T>(string key, T value, TimeSpan expiresAt, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var content = ToJson(value);
            key = AddSysCustomKey(key);
            return await Do(db => db.StringSetAsync(key, content, expiresAt, when, commandFlags));
        }

        public async Task<T> StringGetAsync<T>(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            try
            {
                key = AddSysCustomKey(key);
                var str = await Do(db => db.StringGetAsync(key, commandFlags));
                if (str.HasValue && !str.IsNullOrEmpty)
                {
                    return ToObj<T>(str);
                }
                return default(T);
            }
            catch (Exception ex)
            {
                //输出错误日志
                return default(T);
            }
        }

        public async Task<RedisValue[]> StringGetAsync(List<string> listKey)
        {
            List<string> newKeys = listKey.Select(AddSysCustomKey).ToList();
            return await Do(db => db.StringGetAsync(ConvertRedisKeys(newKeys)));
        }

        public async Task<long> StringLengthAsync(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await Do(db => db.StringLengthAsync(key, commandFlags));
        }

        public async Task<long> StringAppendAsync(string key, string appendVal, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await Do(db => db.StringAppendAsync(key, appendVal, commandFlags));
        }

        public async Task<string> StringGetAnSetAsync(string key, string newVal, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            var result = await Do(db => db.StringGetSetAsync(key, newVal, commandFlags));
            return ToObj<string>(result);
        }

        public async Task<double> StringIncrementAsync(string key, double value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await Do(db => db.StringIncrementAsync(key, value, commandFlags));
        }

        public async Task<double> StringDecrementAsync(string key, double value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await Do(db => db.StringDecrementAsync(key, value, commandFlags));
        }

        public async Task<List<T>> StringGetListAsync<T>(string key, int pageSize = 100, CommandFlags commandFlags = CommandFlags.None) where T : class
        {
            try
            {
                key = AddSysCustomKey(key);
                var server = GetServer();
                var keys = server.Keys(DbNum, key, pageSize, commandFlags);
                var keyValues = await Do(db => db.StringGetAsync(keys.ToArray(), commandFlags));

                var result = new List<T>();
                foreach (RedisValue redisValue in keyValues)
                {
                    if (redisValue.HasValue && !redisValue.IsNullOrEmpty)
                    {
                        var item = ToObj<T>(redisValue);
                        result.Add(item);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                //输出错误日志
                return null;
            }
        }

        #endregion

        #region Hash(哈希数据类型操作)

        public void HashSet(string key, List<HashEntry> hashEntries, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            Do(db => db.HashSet(key, hashEntries.ToArray(), commandFlags));
        }

        public bool HashSet<T>(string key, string field, T value, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.HashSet(key, field, ToJson(value), when, commandFlags));
        }

        public T HashGet<T>(string key, string field)
        {
            key = AddSysCustomKey(key);
            return ToObj<T>(Do(db => db.HashGet(key, field)));
        }

        public HashEntry[] HashGetAll(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.HashGetAll(key, commandFlags));
        }

        public List<T> HashGetAllValues<T>(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            List<T> list = new List<T>();
            key = AddSysCustomKey(key);
            var hashValues = Do(db => db.HashValues(key, commandFlags));
            foreach (RedisValue item in hashValues)
            {
                list.Add(ToObj<T>(item));
            }
            return list;
        }

        public string[] HashGetAllKeys(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.HashKeys(key, commandFlags).ToStringArray());
        }

        public bool HashDelete(string key, string hashField, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.HashDelete(key, hashField, commandFlags));
        }

        public long HashDelete(string key, string[] hashFields, CommandFlags commandFlags = CommandFlags.None)
        {
            List<RedisValue> list = new List<RedisValue>();
            for (int i = 0; i < hashFields.Length; i++)
            {
                list.Add(hashFields[i]);
            }
            key = AddSysCustomKey(key);
            return Do(db => db.HashDelete(key, list.ToArray(), commandFlags));
        }

        public bool HashExists(string key, string field, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.HashExists(key, field, commandFlags));
        }

        public long HashLength(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.HashLength(key, commandFlags));
        }

        public double HashIncrement(string key, string field, double incrValue = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.HashIncrement(key, field, incrValue, commandFlags));
        }

        public double HashDecrement(string key, string field, double incrValue = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.HashDecrement(key, field, incrValue, commandFlags));
        }

        public async Task HashSetAsync(string key, List<HashEntry> hashEntries, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            await Do(db => db.HashSetAsync(key, hashEntries.ToArray(), commandFlags));
        }

        public async Task<bool> HashSetAsync<T>(string key, string field, T value, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await Do(db => db.HashSetAsync(key, field, ToJson(value), when, commandFlags));
        }

        public async Task<T> HashGetAsync<T>(string key, string field)
        {
            key = AddSysCustomKey(key);
            var result = await Do(db => db.HashGetAsync(key, field));
            return ToObj<T>(result);
        }

        public async Task<HashEntry[]> HashGetAllAsync(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await Do(db => db.HashGetAllAsync(key, commandFlags));
        }

        public async Task<List<T>> HashGetAllValuesAsync<T>(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            List<T> list = new List<T>();
            key = AddSysCustomKey(key);
            var hashValues = await Do(db => db.HashValuesAsync(key, commandFlags));
            foreach (RedisValue item in hashValues)
            {
                list.Add(ToObj<T>(item));
            }
            return list;
        }

        public async Task<string[]> HashGetAllKeysAsync(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            var result = await Do(db => db.HashKeysAsync(key, commandFlags));
            return result.ToStringArray();
        }

        public async Task<bool> HashDeleteAsync(string key, string hashField, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await Do(db => db.HashDeleteAsync(key, hashField, commandFlags));
        }

        public async Task<long> HashDeleteAsync(string key, string[] hashFields, CommandFlags commandFlags = CommandFlags.None)
        {
            List<RedisValue> list = new List<RedisValue>();
            for (int i = 0; i < hashFields.Length; i++)
            {
                list.Add(hashFields[i]);
            }
            key = AddSysCustomKey(key);
            return await Do(db => db.HashDeleteAsync(key, list.ToArray(), commandFlags));
        }

        public async Task<bool> HashExistsAsync(string key, string field, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await Do(db => db.HashExistsAsync(key, field, commandFlags));
        }

        public async Task<long> HashLengthAsync(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await Do(db => db.HashLengthAsync(key, commandFlags));
        }

        public async Task<double> HashIncrementAsync(string key, string field, double incrValue = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await Do(db => db.HashIncrementAsync(key, field, incrValue, commandFlags));
        }

        public async Task<double> HashDecrementAsync(string key, string field, double incrValue = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await Do(db => db.HashDecrementAsync(key, field, incrValue, commandFlags));
        }

        #endregion

        #region List(列表数据类型操作)

        public void ListRemove<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            Do(db => db.ListRemove(key, ToJson(value)));
        }

        public List<T> ListGetList<T>(string key)
        {
            key = AddSysCustomKey(key);
            return Do(db =>
            {
                var values = db.ListRange(key);
                return ToList<T>(values);
            });
        }

        public void ListRightPush<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            Do(db => db.ListRightPush(key, ToJson(value)));
        }

        public T ListRightPop<T>(string key)
        {
            key = AddSysCustomKey(key);
            return Do(db =>
            {
                var value = db.ListRightPop(key);
                return ToObj<T>(value);
            });
        }

        public void ListLeftPush<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            Do(db => db.ListLeftPush(key, ToJson(value)));
        }

        public T ListLeftPop<T>(string key)
        {
            key = AddSysCustomKey(key);
            return Do(db =>
            {
                var value = db.ListLeftPop(key);
                return ToObj<T>(value);
            });
        }

        public long ListLength(string key)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.ListLength(key));
        }

        public async Task ListRemoveAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            await Do(db => db.ListRemoveAsync(key, ToJson(value)));
        }

        public async Task<List<T>> ListGetListAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var values = await Do(db => db.ListRangeAsync(key));
            return ToList<T>(values);
        }

        public async Task ListRightPushAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            await Do(db => db.ListRightPushAsync(key, ToJson(value)));
        }

        public async Task<T> ListRightPopAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var value = await Do(db => db.ListRightPopAsync(key));
            return ToObj<T>(value);
        }

        public async Task ListLeftPushAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            await Do(db => db.ListLeftPushAsync(key, ToJson(value)));
        }

        public async Task<T> ListLeftPopAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var value = await Do(db => db.ListLeftPopAsync(key));
            return ToObj<T>(value);
        }

        public async Task<long> ListLengthAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await Do(db => db.ListLengthAsync(key));
        }

        #endregion

        #region SortSet(有序集合数据类型操作)

        public bool SortedSetAdd<T>(string key, T value, double score)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.SortedSetAdd(key, ToJson(value), score));
        }

        public bool SortedSetRemove<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.SortedSetRemove(key, ToJson(value)));
        }

        public List<T> SortedSetGetList<T>(string key)
        {
            key = AddSysCustomKey(key);
            return Do(db =>
            {
                var values = db.SortedSetRangeByRank(key);
                return ToList<T>(values);
            });
        }

        public long SortedSetLength(string key)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.SortedSetLength(key));
        }

        public async Task<bool> SortedSetAddAsync<T>(string key, T value, double score)
        {
            key = AddSysCustomKey(key);
            return await Do(db => db.SortedSetAddAsync(key, ToJson(value), score));
        }

        public async Task<bool> SortedSetRemoveAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            return await Do(db => db.SortedSetRemoveAsync(key, ToJson(value)));
        }

        public async Task<List<T>> SortedSetGetListAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var values = await Do(db => db.SortedSetRangeByRankAsync(key));
            return ToList<T>(values);
        }

        public async Task<long> SortedSetLengthAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await Do(db => db.SortedSetLengthAsync(key));
        }

        #endregion

        #region Key(缓存键管理)

        public bool KeyExists(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.KeyExists(key, commandFlags));
        }

        public bool KeyRemove(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.KeyDelete(key, commandFlags));
        }

        public long KeyRemove(RedisKey[] keys, CommandFlags commandFlags = CommandFlags.None)
        {
            return
                Do(db => db.KeyDelete(ConvertRedisKeys(keys.Select(key => AddSysCustomKey(key)).ToList()), commandFlags));
        }

        public long KeyRemove(List<string> keys, CommandFlags commandFlags = CommandFlags.None)
        {
            return Do(db => db.KeyDelete(ConvertRedisKeys(keys.Select(AddSysCustomKey).ToList()), commandFlags));
        }

        public bool KeyRename(string key, string newKey)
        {
            key = AddSysCustomKey(key);
            return Do(db => db.KeyRename(key, newKey));
        }

        public bool KeyExpire(string key, TimeSpan? expiry = default(TimeSpan?))
        {
            key = AddSysCustomKey(key);
            return Do(db => db.KeyExpire(key, expiry));
        }

        public List<string> GetAllKeys()
        {
            var server = GetServer();
            return server.Keys(DbNum).Select(key => key.ToString()).ToList();
        }

        #endregion

        public void DbConnectionStop()
        {
            _redis.Dispose();
        }

        public void Subscribe(string subChannel, Action<RedisChannel, RedisValue> handler = null)
        {
            ISubscriber sub = _redis.GetSubscriber();
            sub.Subscribe(subChannel, (channel, message) =>
            {
                if (handler == null)
                {
                    Console.WriteLine(subChannel + " 订阅收到消息:" + message);
                }
                else
                {
                    handler(channel, message);
                }
            });
        }

        public long Publish<T>(string channel, T msg)
        {
            ISubscriber sub = _redis.GetSubscriber();
            return sub.Publish(channel, ToJson(msg));
        }

        public void UnSubscribe(string channel)
        {
            ISubscriber sub = _redis.GetSubscriber();
            sub.Unsubscribe(channel);
        }

        public void UnSubscribeAll()
        {
            ISubscriber sub = _redis.GetSubscriber();
            sub.UnsubscribeAll();
        }

        public ITransaction CreateTransaction()
        {
            return GetDatabase().CreateTransaction();
        }

        public IDatabase GetDatabase()
        {
            return _redis.GetDatabase(DbNum);
        }

        public IServer GetServer(string hostAndPort)
        {
            return _redis.GetServer(hostAndPort);
        }

        public IServer GetServer()
        {
            var endPoint = _redis.GetCounters().EndPoint;
            return _redis.GetServer(endPoint);
        }

        public void SetSysCustomKey(string customKey)
        {
            CustomKey = customKey;
        }

        public void Clear()
        {
            var server = GetServer();
            server.FlushDatabase(DbNum);
        }

        public void ClearAll()
        {
            var endPoints = _redis.GetEndPoints(true);
            foreach (EndPoint endPoint in endPoints)
            {
                var server = _redis.GetServer(endPoint);
                server.FlushAllDatabases();
            }
        }
    }
}
