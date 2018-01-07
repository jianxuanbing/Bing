using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Bing.Caching.Redis
{
    /// <summary>
    /// Redis 客户端接口
    /// </summary>
    public interface IRedisClient:IDisposable
    {
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
        bool StringSet(string key, object value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None);

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
        bool StringSet<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis String类型 批量新增记录
        /// </summary>
        /// <param name="keyValues">键值对</param>
        /// <returns></returns>
        bool StringSet(List<KeyValuePair<RedisKey, RedisValue>> keyValues);

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
        bool StringUpdate<T>(string key, T value, TimeSpan expiresAt, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis String类型 获取一条记录
        /// </summary>
        /// <typeparam name="T">引用类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        T StringGet<T>(string key, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis String类型 批量获取记录
        /// </summary>
        /// <param name="listKey">键列表</param>
        /// <returns></returns>
        RedisValue[] StringGet(List<string> listKey);

        /// <summary>
        /// Redis String类型 获取指定key中字符串长度
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        long StringLength(string key, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis String类型 拼接内容并返回拼接后总长度
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="appendVal">拼接内容</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        long StringAppend(string key, string appendVal, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis String类型 设置新值并返回旧值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="newVal">新值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        string StringGetAnSet(string key, string newVal, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis String类型 为数字累加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">新值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        double StringIncrement(string key, double value = 1, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis String类型 为数字累减
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">新值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        double StringDecrement(string key, double value = 1, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// Redis String类型 模糊查询 key* 查询出所有key开头的键
        /// </summary>
        /// <typeparam name="T">引用类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        List<T> StringGetList<T>(string key, int pageSize = 100, CommandFlags commandFlags = CommandFlags.None) where T : class;

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
        Task<bool> StringSetAsync(string key, object value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None);

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
        Task<bool> StringSetAsync<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis String类型 批量新增记录
        /// </summary>
        /// <param name="keyValues">键值对</param>
        /// <returns></returns>
        Task<bool> StringSetAsync(List<KeyValuePair<RedisKey, RedisValue>> keyValues);

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
        Task<bool> StringUpdateAsync<T>(string key, T value, TimeSpan expiresAt, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis String类型 获取一条记录
        /// </summary>
        /// <typeparam name="T">引用类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<T> StringGetAsync<T>(string key, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis String类型 批量获取记录
        /// </summary>
        /// <param name="listKey">键列表</param>
        /// <returns></returns>
        Task<RedisValue[]> StringGetAsync(List<string> listKey);

        /// <summary>
        /// Redis String类型 获取指定key中字符串长度
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<long> StringLengthAsync(string key, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis String类型 拼接内容并返回拼接后总长度
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="appendVal">拼接内容</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<long> StringAppendAsync(string key, string appendVal, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis String类型 设置新值并返回旧值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="newVal">新值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<string> StringGetAnSetAsync(string key, string newVal, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis String类型 为数字累加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">新值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<double> StringIncrementAsync(string key, double value = 1, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis String类型 为数字累减
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">新值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<double> StringDecrementAsync(string key, double value = 1, CommandFlags commandFlags = CommandFlags.None);
        /// <summary>
        /// Redis String类型 模糊查询 key* 查询出所有key开头的键
        /// </summary>
        /// <typeparam name="T">引用类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<List<T>> StringGetListAsync<T>(string key, int pageSize = 100, CommandFlags commandFlags = CommandFlags.None) where T : class;
        #endregion

        #region Hash(哈希数据类型操作)
        /// <summary>
        /// Redis Hash类型 批量新增
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashEntries">哈希列表</param>
        /// <param name="commandFlags">命令标识，无</param>
        void HashSet(string key, List<HashEntry> hashEntries, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 新增一条记录
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        /// <param name="when">执行操作，默认当前操作是否已存在</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        bool HashSet<T>(string key, string field, T value, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 获取指定键、字段的记录
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        T HashGet<T>(string key, string field);

        /// <summary>
        /// Redis Hash类型 获取所有字段的所有值，以HashEntry[]形式返回
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        HashEntry[] HashGetAll(string key, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 获取键中所有字段的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        List<T> HashGetAllValues<T>(string key, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 获取所有键名称
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        string[] HashGetAllKeys(string key, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 单个删除字段
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashField">字段，需要删除的字段</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        bool HashDelete(string key, string hashField, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 批量删除字段
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashFields">字段集合，需要删除的字段集合</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        long HashDelete(string key, string[] hashFields, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 判断指定键是否存在此字段
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        bool HashExists(string key, string field, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 获取指定键中字段数量
        /// </summary>
        /// <param name="key">字段</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        long HashLength(string key, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 为键中指定字段累加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="incrValue">累加值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        double HashIncrement(string key, string field, double incrValue = 1,
            CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 为键中指定字段累减
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="incrValue">累减值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        double HashDecrement(string key, string field, double incrValue = 1,
            CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 批量新增
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashEntries">哈希列表</param>
        /// <param name="commandFlags">命令标识，无</param>
        Task HashSetAsync(string key, List<HashEntry> hashEntries, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 新增一条记录
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        /// <param name="when">执行操作，默认当前操作是否已存在</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        Task<bool> HashSetAsync<T>(string key, string field, T value, When when = When.Always,
            CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 获取指定键、字段的记录
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        Task<T> HashGetAsync<T>(string key, string field);

        /// <summary>
        /// Redis Hash类型 获取所有字段的所有值，以HashEntry[]形式返回
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<HashEntry[]> HashGetAllAsync(string key, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 获取键中所有字段的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<List<T>> HashGetAllValuesAsync<T>(string key, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 获取所有键名称
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<string[]> HashGetAllKeysAsync(string key, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 单个删除字段
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashField">字段，需要删除的字段</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<bool> HashDeleteAsync(string key, string hashField, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 批量删除字段
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashFields">字段集合，需要删除的字段集合</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<long> HashDeleteAsync(string key, string[] hashFields, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 判断指定键是否存在此字段
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<bool> HashExistsAsync(string key, string field, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 获取指定键中字段数量
        /// </summary>
        /// <param name="key">字段</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<long> HashLengthAsync(string key, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 为键中指定字段累加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="incrValue">累加值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<double> HashIncrementAsync(string key, string field, double incrValue = 1,
            CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// Redis Hash类型 为键中指定字段累减
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <param name="incrValue">累减值</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        Task<double> HashDecrementAsync(string key, string field, double incrValue = 1,
            CommandFlags commandFlags = CommandFlags.None);
        #endregion

        #region List(列表数据类型操作)
        /// <summary>
        /// Redis List类型 移除一条记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void ListRemove<T>(string key, T value);

        /// <summary>
        /// Redis List类型 获取指定键的全部记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        List<T> ListGetList<T>(string key);

        /// <summary>
        /// Redis List类型 将对象入队
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void ListRightPush<T>(string key, T value);

        /// <summary>
        /// Redis List类型 将对象出队
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        T ListRightPop<T>(string key);

        /// <summary>
        /// Redis List类型 将对象入栈
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void ListLeftPush<T>(string key, T value);

        /// <summary>
        /// Redis List类型 将对象出栈
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        T ListLeftPop<T>(string key);

        /// <summary>
        /// 获取指定键的全部记录的数量
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        long ListLength(string key);

        /// <summary>
        /// Redis List类型 移除一条记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        Task ListRemoveAsync<T>(string key, T value);

        /// <summary>
        /// Redis List类型 获取指定键的全部记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        Task<List<T>> ListGetListAsync<T>(string key);

        /// <summary>
        /// Redis List类型 将对象入队
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        Task ListRightPushAsync<T>(string key, T value);

        /// <summary>
        /// Redis List类型 将对象出队
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        Task<T> ListRightPopAsync<T>(string key);

        /// <summary>
        /// Redis List类型 将对象入栈
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        Task ListLeftPushAsync<T>(string key, T value);

        /// <summary>
        /// Redis List类型 将对象出栈
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        Task<T> ListLeftPopAsync<T>(string key);

        /// <summary>
        /// 获取指定键的全部记录的数量
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        Task<long> ListLengthAsync(string key);
        #endregion

        #region Set(集合数据类型操作)

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
        bool SortedSetAdd<T>(string key, T value, double score);

        /// <summary>
        /// Redis SortedSet类型 移除一条记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool SortedSetRemove<T>(string key, T value);

        /// <summary>
        /// Redis SortedSet类型 获取指定键的全部记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        List<T> SortedSetGetList<T>(string key);

        /// <summary>
        /// Redis SortedSet类型 获取指定键的全部记录的数量
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        long SortedSetLength(string key);

        /// <summary>
        /// Redis SortedSet类型 新增一条记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="score">分数</param>
        /// <returns></returns>
        Task<bool> SortedSetAddAsync<T>(string key, T value, double score);

        /// <summary>
        /// Redis SortedSet类型 移除一条记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        Task<bool> SortedSetRemoveAsync<T>(string key, T value);

        /// <summary>
        /// Redis SortedSet类型 获取指定键的全部记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        Task<List<T>> SortedSetGetListAsync<T>(string key);

        /// <summary>
        /// Redis SortedSet类型 获取指定键的全部记录的数量
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        Task<long> SortedSetLengthAsync(string key);
        #endregion

        #region Key(缓存键管理)
        /// <summary>
        /// Redis 中是否存在指定key
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        bool KeyExists(string key, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// 从Redis中删除指定键
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        bool KeyDelete(string key, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// 从Redis中删除多个键
        /// </summary>
        /// <param name="keys">键集合</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        long KeyDelete(RedisKey[] keys, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// 从Redis中删除多个键
        /// </summary>
        /// <param name="keys">键集合</param>
        /// <param name="commandFlags">命令标识，默认无</param>
        /// <returns></returns>
        long KeyDelete(List<string> keys, CommandFlags commandFlags = CommandFlags.None);

        /// <summary>
        /// 重命名缓存键
        /// </summary>
        /// <param name="key">旧的缓存键</param>
        /// <param name="newKey">新的缓存键</param>
        /// <returns></returns>
        bool KeyRename(string key, string newKey);

        /// <summary>
        /// 设置缓存键的时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        bool KeyExpire(string key, TimeSpan? expiry = default(TimeSpan?));

        /// <summary>
        /// 获取全部缓存键
        /// </summary>
        /// <returns></returns>
        List<string> GetAllKeys();

        /// <summary>
        /// 释放数据连接
        /// </summary>
        void DbConnectionStop();
        #endregion

        #region Subscribe(发布订阅)

        /// <summary>
        /// Redis发布订阅——订阅
        /// </summary>
        /// <param name="subChannel">订阅通道</param>
        /// <param name="handler">订阅处理器</param>
        void Subscribe(string subChannel, Action<RedisChannel, RedisValue> handler = null);

        /// <summary>
        /// Redis发布订阅——发布
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="channel">通道</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        long Publish<T>(string channel, T msg);

        /// <summary>
        /// Redis发布订阅——取消订阅
        /// </summary>
        /// <param name="channel"></param>
        void UnSubscribe(string channel);

        /// <summary>
        /// Redis发布订阅——取消全部订阅
        /// </summary>
        void UnSubscribeAll();
        #endregion

        #region Other(其他)

        /// <summary>
        /// 创建事务
        /// </summary>
        /// <returns></returns>
        ITransaction CreateTransaction();

        /// <summary>
        /// 获取Redis库
        /// </summary>
        /// <returns></returns>
        IDatabase GetDatabase();

        /// <summary>
        /// 获取Redis服务器
        /// </summary>
        /// <param name="hostAndPort">地址，主机名以及端口号</param>
        /// <returns></returns>
        IServer GetServer(string hostAndPort);

        /// <summary>
        /// 获取当前Redis服务器
        /// </summary>
        /// <returns></returns>
        IServer GetServer();

        /// <summary>
        /// 设置系统自定义缓存键
        /// </summary>
        /// <param name="customKey">自定义缓存键</param>
        void SetSysCustomKey(string customKey);

        /// <summary>
        /// 清空缓存数据
        /// </summary>
        void Clear();

        /// <summary>
        /// 清空服务器上所有库的缓存数据
        /// </summary>
        void ClearAll();
        #endregion
    }
}
