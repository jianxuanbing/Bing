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
        /// 缓存数据库
        /// </summary>
        private IDatabase _database;

        /// <summary>
        /// Redis 数据库提供程序
        /// </summary>
        private readonly IRedisDatabaseProvider _dbProvider;

        /// <summary>
        /// 初始化一个<see cref="RedisClient"/>类型的实例
        /// </summary>
        /// <param name="dbProvider">Redis 数据库提供程序</param>
        public RedisClient(IRedisDatabaseProvider dbProvider)
        {
            dbProvider.CheckNotNull(nameof(dbProvider));
            _dbProvider = dbProvider;
            _database = _dbProvider.GetDatabase();
        }
        #region 辅助方法

        /// <summary>
        /// 添加自定义缓存键
        /// </summary>
        /// <param name="oldKey">旧缓存键</param>
        /// <returns></returns>
        private string AddSysCustomKey(string oldKey)
        {
            var prefixKey = this._dbProvider.Options.SystemPrefix ?? RedisManager.SysCustomKey;
            return prefixKey + oldKey;
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

        #region String(字符串类型数据操作)

        /// <summary>
        /// Redis String类型 新增一条记录
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="when">执行操作，默认当前操作是否已存在</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public bool StringSet(string key, object value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var content = ToJson(value);
            key = AddSysCustomKey(key);
            return _database.StringSet(key, content, expiry, when, commandFlags);
        }

        /// <summary>
        /// Redis String类型 新增一条记录
        /// </summary>
        /// <typeparam name="T">引用类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="when">执行操作，默认当前操作是否已存在</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public bool StringSet<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var content = ToJson(value);
            key = AddSysCustomKey(key);
            return _database.StringSet(key, content, expiry, when, commandFlags);
        }

        /// <summary>
        /// Redis String类型 批量新增记录
        /// </summary>
        /// <param name="keyValues">键值对</param>
        /// <returns></returns>
        public bool StringSet(List<KeyValuePair<RedisKey, RedisValue>> keyValues)
        {
            List<KeyValuePair<RedisKey, RedisValue>> newKeyValues =
                keyValues.Select(p => new KeyValuePair<RedisKey, RedisValue>(AddSysCustomKey(p.Key), p.Value)).ToList();
            return _database.StringSet(newKeyValues.ToArray());
        }

        /// <summary>
        /// Redis String类型 更新一条记录，更新时应使用此方法
        /// </summary>
        /// <typeparam name="T">引用类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">过期时间</param>
        /// <param name="when">执行操作，默认当前操作是否已存在</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public bool StringUpdate<T>(string key, T value, TimeSpan expiresAt, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var content = ToJson(value);
            key = AddSysCustomKey(key);
            return _database.StringSet(key, content, expiresAt, when, commandFlags);
        }

        /// <summary>
        /// Redis String类型 获取一条记录
        /// </summary>
        /// <typeparam name="T">引用类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public T StringGet<T>(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            try
            {
                key = AddSysCustomKey(key);
                RedisValue str = _database.StringGet(key, commandFlags);
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

        /// <summary>
        /// Redis String类型 批量获取记录
        /// </summary>
        /// <param name="listKey">键列表</param>
        /// <returns></returns>
        public RedisValue[] StringGet(List<string> listKey)
        {
            List<string> newKeys = listKey.Select(AddSysCustomKey).ToList();
            return _database.StringGet(ConvertRedisKeys(newKeys));
        }

        /// <summary>
        /// Redis String类型 获取指定key中字符串长度
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public long StringLength(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return _database.StringLength(key, commandFlags);
        }

        /// <summary>
        /// Redis String类型 拼接内容并返回拼接后总长度
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="appendVal">拼接内容</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public long StringAppend(string key, string appendVal, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return _database.StringAppend(key, appendVal, commandFlags);
        }

        /// <summary>
        /// Redis String类型 设置新值并返回旧值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="newVal">新值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public string StringGetAnSet(string key, string newVal, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return ToObj<string>(_database.StringGetSet(key, newVal, commandFlags));
        }

        /// <summary>
        /// Redis String类型 为数字累加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">新值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public double StringIncrement(string key, double value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return _database.StringIncrement(key, value, commandFlags);
        }

        /// <summary>
        /// Redis String类型 为数字累减
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">新值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public double StringDecrement(string key, double value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return _database.StringDecrement(key, value, commandFlags);
        }

        /// <summary>
        /// Redis String类型 模糊查询 key* 查询出所有key开头的键
        /// </summary>
        /// <typeparam name="T">引用类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public List<T> StringGetList<T>(string key, int pageSize = 100, CommandFlags commandFlags = CommandFlags.None) where T : class
        {
            try
            {
                key = AddSysCustomKey(key);
                var server = GetServer();
                var keys = server.Keys(this._database.Database, key, pageSize, commandFlags);
                var keyValues = _database.StringGet(keys.ToArray(), commandFlags);

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

        /// <summary>
        /// Redis String类型 新增一条记录
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="when">执行操作，默认当前操作是否已存在</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(string key, object value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var content = ToJson(value);
            key = AddSysCustomKey(key);
            return await _database.StringSetAsync(key, content, expiry, when, commandFlags);
        }

        /// <summary>
        /// Redis String类型 新增一条记录
        /// </summary>
        /// <typeparam name="T">引用类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="when">执行操作，默认当前操作是否已存在</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var content = ToJson(value);
            key = AddSysCustomKey(key);
            return await _database.StringSetAsync(key, content, expiry, when, commandFlags);
        }

        /// <summary>
        /// Redis String类型 批量新增记录
        /// </summary>
        /// <param name="keyValues">键值对</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(List<KeyValuePair<RedisKey, RedisValue>> keyValues)
        {
            List<KeyValuePair<RedisKey, RedisValue>> newKeyValues =
                keyValues.Select(p => new KeyValuePair<RedisKey, RedisValue>(AddSysCustomKey(p.Key), p.Value)).ToList();
            return await _database.StringSetAsync(newKeyValues.ToArray());
        }

        /// <summary>
        /// Redis String类型 更新一条记录，更新时应使用此方法
        /// </summary>
        /// <typeparam name="T">引用类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">过期时间</param>
        /// <param name="when">执行操作，默认当前操作是否已存在</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<bool> StringUpdateAsync<T>(string key, T value, TimeSpan expiresAt, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            var content = ToJson(value);
            key = AddSysCustomKey(key);
            return await _database.StringSetAsync(key, content, expiresAt, when, commandFlags);
        }

        /// <summary>
        /// Redis String类型 获取一条记录
        /// </summary>
        /// <typeparam name="T">引用类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<T> StringGetAsync<T>(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            try
            {
                key = AddSysCustomKey(key);
                var str = await _database.StringGetAsync(key, commandFlags);
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

        /// <summary>
        /// Redis String类型 批量获取记录
        /// </summary>
        /// <param name="listKey">键列表</param>
        /// <returns></returns>
        public async Task<RedisValue[]> StringGetAsync(List<string> listKey)
        {
            List<string> newKeys = listKey.Select(AddSysCustomKey).ToList();
            return await _database.StringGetAsync(ConvertRedisKeys(newKeys));
        }

        /// <summary>
        /// Redis String类型 获取指定key中字符串长度
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<long> StringLengthAsync(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await _database.StringLengthAsync(key, commandFlags);
        }

        /// <summary>
        /// Redis String类型 拼接内容并返回拼接后总长度
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="appendVal">拼接内容</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<long> StringAppendAsync(string key, string appendVal, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await _database.StringAppendAsync(key, appendVal, commandFlags);
        }

        /// <summary>
        /// Redis String类型 设置新值并返回旧值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="newVal">新值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<string> StringGetAnSetAsync(string key, string newVal, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            var result = await _database.StringGetSetAsync(key, newVal, commandFlags);
            return ToObj<string>(result);
        }

        /// <summary>
        /// Redis String类型 为数字累加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">新值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<double> StringIncrementAsync(string key, double value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await _database.StringIncrementAsync(key, value, commandFlags);
        }

        /// <summary>
        /// Redis String类型 为数字累减
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">新值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<double> StringDecrementAsync(string key, double value = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await _database.StringDecrementAsync(key, value, commandFlags);
        }

        /// <summary>
        /// Redis String类型 模糊查询 key* 查询出所有key开头的键
        /// </summary>
        /// <typeparam name="T">引用类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<List<T>> StringGetListAsync<T>(string key, int pageSize = 100, CommandFlags commandFlags = CommandFlags.None) where T : class
        {
            try
            {
                key = AddSysCustomKey(key);
                var server = GetServer();
                var keys = server.Keys(this._database.Database, key, pageSize, commandFlags);
                var keyValues = await _database.StringGetAsync(keys.ToArray(), commandFlags);

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

        /// <summary>
        /// Redis Hash类型 批量新增
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashEntries">哈希列表</param>
        /// <param name="commandFlags">命令标识，无</param>
        public void HashSet(string key, List<HashEntry> hashEntries, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            _database.HashSet(key, hashEntries.ToArray(), commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 新增一条记录
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        /// <param name="when">执行操作，默认当前操作是否已存在</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        public bool HashSet<T>(string key, string field, T value, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return _database.HashSet(key, field, ToJson(value), when, commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 获取指定键、字段的记录
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public T HashGet<T>(string key, string field)
        {
            key = AddSysCustomKey(key);
            return ToObj<T>(_database.HashGet(key, field));
        }

        /// <summary>
        /// Redis Hash类型 获取所有字段的所有值，以HashEntry[]形式返回
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public HashEntry[] HashGetAll(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return _database.HashGetAll(key, commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 获取键中所有字段的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public List<T> HashGetAllValues<T>(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            List<T> list = new List<T>();
            key = AddSysCustomKey(key);
            var hashValues = _database.HashValues(key, commandFlags);
            foreach (RedisValue item in hashValues)
            {
                list.Add(ToObj<T>(item));
            }
            return list;
        }

        /// <summary>
        /// Redis Hash类型 获取所有键名称
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public string[] HashGetAllKeys(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return _database.HashKeys(key, commandFlags).ToStringArray();
        }

        /// <summary>
        /// Redis Hash类型 单个删除字段
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashField">字段，需要删除的字段</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public bool HashDelete(string key, string hashField, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return _database.HashDelete(key, hashField, commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 批量删除字段
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashFields">字段集合，需要删除的字段集合</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public long HashDelete(string key, string[] hashFields, CommandFlags commandFlags = CommandFlags.None)
        {
            List<RedisValue> list = new List<RedisValue>();
            for (int i = 0; i < hashFields.Length; i++)
            {
                list.Add(hashFields[i]);
            }
            key = AddSysCustomKey(key);
            return _database.HashDelete(key, list.ToArray(), commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 判断指定键是否存在此字段
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public bool HashExists(string key, string field, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return _database.HashExists(key, field, commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 获取指定键中字段数量
        /// </summary>
        /// <param name="key">字段</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public long HashLength(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return _database.HashLength(key, commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 为键中指定字段累加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="incrValue">累加值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public double HashIncrement(string key, string field, double incrValue = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return _database.HashIncrement(key, field, incrValue, commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 为键中指定字段累减
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="incrValue">累减值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public double HashDecrement(string key, string field, double incrValue = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return _database.HashDecrement(key, field, incrValue, commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 批量新增
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashEntries">哈希列表</param>
        /// <param name="commandFlags">命令标识，无</param>
        public async Task HashSetAsync(string key, List<HashEntry> hashEntries, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            await _database.HashSetAsync(key, hashEntries.ToArray(), commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 新增一条记录
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        /// <param name="when">执行操作，默认当前操作是否已存在</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        public async Task<bool> HashSetAsync<T>(string key, string field, T value, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await _database.HashSetAsync(key, field, ToJson(value), when, commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 获取指定键、字段的记录
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string key, string field)
        {
            key = AddSysCustomKey(key);
            var result = await _database.HashGetAsync(key, field);
            return ToObj<T>(result);
        }

        /// <summary>
        /// Redis Hash类型 获取所有字段的所有值，以HashEntry[]形式返回
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<HashEntry[]> HashGetAllAsync(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await _database.HashGetAllAsync(key, commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 获取键中所有字段的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<List<T>> HashGetAllValuesAsync<T>(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            List<T> list = new List<T>();
            key = AddSysCustomKey(key);
            var hashValues = await _database.HashValuesAsync(key, commandFlags);
            foreach (RedisValue item in hashValues)
            {
                list.Add(ToObj<T>(item));
            }
            return list;
        }

        /// <summary>
        /// Redis Hash类型 获取所有键名称
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<string[]> HashGetAllKeysAsync(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            var result = await _database.HashKeysAsync(key, commandFlags);
            return result.ToStringArray();
        }

        /// <summary>
        /// Redis Hash类型 单个删除字段
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashField">字段，需要删除的字段</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string key, string hashField, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await _database.HashDeleteAsync(key, hashField, commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 批量删除字段
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashFields">字段集合，需要删除的字段集合</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<long> HashDeleteAsync(string key, string[] hashFields, CommandFlags commandFlags = CommandFlags.None)
        {
            List<RedisValue> list = new List<RedisValue>();
            for (int i = 0; i < hashFields.Length; i++)
            {
                list.Add(hashFields[i]);
            }
            key = AddSysCustomKey(key);
            return await _database.HashDeleteAsync(key, list.ToArray(), commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 判断指定键是否存在此字段
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<bool> HashExistsAsync(string key, string field, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await _database.HashExistsAsync(key, field, commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 获取指定键中字段数量
        /// </summary>
        /// <param name="key">字段</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<long> HashLengthAsync(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await _database.HashLengthAsync(key, commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 为键中指定字段累加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="incrValue">累加值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<double> HashIncrementAsync(string key, string field, double incrValue = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await _database.HashIncrementAsync(key, field, incrValue, commandFlags);
        }

        /// <summary>
        /// Redis Hash类型 为键中指定字段累减
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="incrValue">累减值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public async Task<double> HashDecrementAsync(string key, string field, double incrValue = 1, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return await _database.HashDecrementAsync(key, field, incrValue, commandFlags);
        }

        #endregion

        #region List(列表数据类型操作)

        /// <summary>
        /// Redis List类型 移除一条记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void ListRemove<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            _database.ListRemove(key, ToJson(value));
        }

        /// <summary>
        /// Redis List类型 获取指定键的全部记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public List<T> ListGetList<T>(string key)
        {
            key = AddSysCustomKey(key);
            var values = _database.ListRange(key);
            return ToList<T>(values);
        }

        /// <summary>
        /// Redis List类型 将对象入队
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void ListRightPush<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            _database.ListRightPush(key, ToJson(value));
        }

        /// <summary>
        /// Redis List类型 将对象出队
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public T ListRightPop<T>(string key)
        {
            key = AddSysCustomKey(key);
            var value = _database.ListRightPop(key);
            return ToObj<T>(value);
        }

        /// <summary>
        /// Redis List类型 将对象入栈
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void ListLeftPush<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            _database.ListLeftPush(key, ToJson(value));
        }

        /// <summary>
        /// Redis List类型 将对象出栈
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public T ListLeftPop<T>(string key)
        {
            key = AddSysCustomKey(key);
            var value = _database.ListLeftPop(key);
            return ToObj<T>(value);
        }

        /// <summary>
        /// 获取指定键的全部记录的数量
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public long ListLength(string key)
        {
            key = AddSysCustomKey(key);
            return _database.ListLength(key);
        }

        /// <summary>
        /// Redis List类型 移除一条记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public async Task ListRemoveAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            await _database.ListRemoveAsync(key, ToJson(value));
        }

        /// <summary>
        /// Redis List类型 获取指定键的全部记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<List<T>> ListGetListAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var values = await _database.ListRangeAsync(key);
            return ToList<T>(values);
        }

        /// <summary>
        /// Redis List类型 将对象入队
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public async Task ListRightPushAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            await _database.ListRightPushAsync(key, ToJson(value));
        }

        /// <summary>
        /// Redis List类型 将对象出队
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var value = await _database.ListRightPopAsync(key);
            return ToObj<T>(value);
        }

        /// <summary>
        /// Redis List类型 将对象入栈
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public async Task ListLeftPushAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            await _database.ListLeftPushAsync(key, ToJson(value));
        }

        /// <summary>
        /// Redis List类型 将对象出栈
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var value = await _database.ListLeftPopAsync(key);
            return ToObj<T>(value);
        }

        /// <summary>
        /// 获取指定键的全部记录的数量
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await _database.ListLengthAsync(key);
        }

        #endregion

        #region SortSet(有序集合数据类型操作)

        /// <summary>
        /// Redis SortedSet类型 新增一条记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="score">分数</param>
        /// <returns></returns>
        public bool SortedSetAdd<T>(string key, T value, double score)
        {
            key = AddSysCustomKey(key);
            return _database.SortedSetAdd(key, ToJson(value), score);
        }

        /// <summary>
        /// Redis SortedSet类型 移除一条记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool SortedSetRemove<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            return _database.SortedSetRemove(key, ToJson(value));
        }

        /// <summary>
        /// Redis SortedSet类型 获取指定键的全部记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public List<T> SortedSetGetList<T>(string key)
        {
            key = AddSysCustomKey(key);
            var values = _database.SortedSetRangeByRank(key);
            return ToList<T>(values);
        }

        /// <summary>
        /// Redis SortedSet类型 获取指定键的全部记录的数量
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public long SortedSetLength(string key)
        {
            key = AddSysCustomKey(key);
            return _database.SortedSetLength(key);
        }

        /// <summary>
        /// Redis SortedSet类型 新增一条记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="score">分数</param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync<T>(string key, T value, double score)
        {
            key = AddSysCustomKey(key);
            return await _database.SortedSetAddAsync(key, ToJson(value), score);
        }

        /// <summary>
        /// Redis SortedSet类型 移除一条记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<bool> SortedSetRemoveAsync<T>(string key, T value)
        {
            key = AddSysCustomKey(key);
            return await _database.SortedSetRemoveAsync(key, ToJson(value));
        }

        /// <summary>
        /// Redis SortedSet类型 获取指定键的全部记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<List<T>> SortedSetGetListAsync<T>(string key)
        {
            key = AddSysCustomKey(key);
            var values = await _database.SortedSetRangeByRankAsync(key);
            return ToList<T>(values);
        }

        /// <summary>
        /// Redis SortedSet类型 获取指定键的全部记录的数量
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<long> SortedSetLengthAsync(string key)
        {
            key = AddSysCustomKey(key);
            return await _database.SortedSetLengthAsync(key);
        }

        #endregion

        #region Key(缓存键管理)

        /// <summary>
        /// Redis 中是否存在指定key
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public bool KeyExists(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return _database.KeyExists(key, commandFlags);
        }

        /// <summary>
        /// 从Redis中删除指定键
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        public bool KeyDelete(string key, CommandFlags commandFlags = CommandFlags.None)
        {
            key = AddSysCustomKey(key);
            return _database.KeyDelete(key, commandFlags);
        }

        /// <summary>
        /// 从Redis中删除多个键
        /// </summary>
        /// <param name="keys">键集合</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        public long KeyDelete(RedisKey[] keys, CommandFlags commandFlags = CommandFlags.None)
        {
            return
                _database.KeyDelete(ConvertRedisKeys(keys.Select(key => AddSysCustomKey(key)).ToList()), commandFlags);
        }

        /// <summary>
        /// 从Redis中删除多个键
        /// </summary>
        /// <param name="keys">键集合</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        public long KeyDelete(List<string> keys, CommandFlags commandFlags = CommandFlags.None)
        {
            return _database.KeyDelete(ConvertRedisKeys(keys.Select(AddSysCustomKey).ToList()), commandFlags);
        }

        /// <summary>
        /// 重命名缓存键
        /// </summary>
        /// <param name="key">旧的缓存键</param>
        /// <param name="newKey">新的缓存键</param>
        /// <returns></returns>
        public bool KeyRename(string key, string newKey)
        {
            key = AddSysCustomKey(key);
            return _database.KeyRename(key, newKey);
        }

        /// <summary>
        /// 设置缓存键的时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool KeyExpire(string key, TimeSpan? expiry = default(TimeSpan?))
        {
            key = AddSysCustomKey(key);
            return _database.KeyExpire(key, expiry);
        }

        /// <summary>
        /// 获取全部缓存键
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllKeys()
        {
            var server = GetServer();
            return server.Keys(this._database.Database).Select(key => key.ToString()).ToList();
        }

        #endregion

        #region Subscribe(发布订阅)

        /// <summary>
        /// Redis发布订阅——订阅
        /// </summary>
        /// <param name="subChannel">订阅通道</param>
        /// <param name="handler">订阅处理器</param>
        public void Subscribe(string subChannel, Action<RedisChannel, RedisValue> handler = null)
        {
            ISubscriber sub = this._dbProvider.GetConnectionMultiplexer().GetSubscriber();
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

        /// <summary>
        /// Redis发布订阅——发布
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="channel">通道</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public long Publish<T>(string channel, T msg)
        {
            ISubscriber sub = this._dbProvider.GetConnectionMultiplexer().GetSubscriber();
            return sub.Publish(channel, ToJson(msg));
        }

        /// <summary>
        /// Redis发布订阅——取消订阅
        /// </summary>
        /// <param name="channel"></param>
        public void UnSubscribe(string channel)
        {
            ISubscriber sub = this._dbProvider.GetConnectionMultiplexer().GetSubscriber();
            sub.Unsubscribe(channel);
        }

        /// <summary>
        /// Redis发布订阅——取消全部订阅
        /// </summary>
        public void UnSubscribeAll()
        {
            ISubscriber sub = this._dbProvider.GetConnectionMultiplexer().GetSubscriber();
            sub.UnsubscribeAll();
        }

        #endregion

        #region Other(其他)

        /// <summary>
        /// 创建事务
        /// </summary>
        /// <returns></returns>
        public ITransaction CreateTransaction()
        {
            return GetDatabase().CreateTransaction();
        }

        /// <summary>
        /// 获取Redis库
        /// </summary>
        /// <returns></returns>
        public IDatabase GetDatabase()
        {
            return this._dbProvider.GetConnectionMultiplexer().GetDatabase();
        }

        /// <summary>
        /// 获取Redis服务器
        /// </summary>
        /// <param name="hostAndPort">地址，主机名以及端口号</param>
        /// <returns></returns>
        public IServer GetServer(string hostAndPort)
        {
            return this._dbProvider.GetConnectionMultiplexer().GetServer(hostAndPort);
        }

        /// <summary>
        /// 获取当前Redis服务器
        /// </summary>
        /// <returns></returns>
        public IServer GetServer()
        {
            var endPoint = this._dbProvider.GetConnectionMultiplexer().GetCounters().EndPoint;
            return this._dbProvider.GetConnectionMultiplexer().GetServer(endPoint);

        }

        /// <summary>
        /// 设置系统自定义缓存键
        /// </summary>
        /// <param name="customKey">自定义缓存键</param>
        public void SetSysCustomKey(string customKey)
        {
            this._dbProvider.Options.SystemPrefix = customKey;
        }

        /// <summary>
        /// 清空缓存数据
        /// </summary>
        public void Clear()
        {
            var server = GetServer();
            server.FlushDatabase(this._database.Database);
        }

        /// <summary>
        /// 清空服务器上所有库的缓存数据
        /// </summary>
        public void ClearAll()
        {
            var endPoints = this._dbProvider.GetConnectionMultiplexer().GetEndPoints(true);
            foreach (EndPoint endPoint in endPoints)
            {
                var server = this._dbProvider.GetConnectionMultiplexer().GetServer(endPoint);
                server.FlushAllDatabases();
            }
        }

        /// <summary>
        /// 释放数据连接
        /// </summary>
        public void DbConnectionStop()
        {
            this._dbProvider.GetConnectionMultiplexer().Dispose();
        }

        #endregion


    }
}
