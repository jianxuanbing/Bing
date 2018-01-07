using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Caching.Abstractions;
using Bing.Caching.Events;
using Bing.Pools;
using Bing.Utils.Extensions;
using StackExchange.Redis;
using Bing.Utils.Json;

namespace Bing.Caching.Redis
{
    public sealed class RedisCache:IAsyncCache,ISyncCache,IAsyncPubSub
    {
        private readonly Lazy<RedisContext> _context;
        private Lazy<long> _defaultExpireTime;
        private const double ExpireTime = 60D;
        private string _keySuffix;
        private Lazy<int> _connectionTimeout;
        private static readonly ConcurrentDictionary<string,ObjectPool<ConnectionMultiplexer>> _pool=new ConcurrentDictionary<string, ObjectPool<ConnectionMultiplexer>>();

        private IDatabase _db;
        private ConnectionMultiplexer _redis;

        public List<string> InitializationProperties =>new List<string>() {"Endpoint","Key","UseSsl"};
        public string ProviderName => "Redis";
        public long DefaultExpireTime { get; set; }
        public string KeySuffix { get; set; }

        public ICacheClient<ConnectionMultiplexer> Client { get; set; }


        public async Task InitializeAsync(IDictionary<string, string> parameters)
        {
            var connectionStr = GetConnectionString(parameters);
            _redis = await ConnectionMultiplexer.ConnectAsync(connectionStr);
            _db = _redis.GetDatabase();
        }

        public Task AddAsync(string key, object value)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(string key, object value, bool defaultExpire)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(string key, object value, long numOfMinutes)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(string key, object value, TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync<T>(string key, T value, TimeSpan timeSpan)
        {
            var stringValue = value.ToJson();
            await _db.StringSetAsync(key, stringValue, timeSpan);
        }

        public Task AddAsync(string key, object value, long numOfMinutes, bool flag)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<string, T>> GetAsync<T>(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetAsync(string key)
        {
            var value = await _db.StringGetAsync(key);
            return value;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var json = await _db.StringGetAsync(key);
            if (string.IsNullOrWhiteSpace(json))
            {
                return default(T);
            }

            var value = json.SafeString().ToObject<T>();
            return value;
        }

        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public void Initialize(IDictionary<string, string> parameters)
        {
            var connectionStr = GetConnectionString(parameters);
            _redis = ConnectionMultiplexer.Connect(connectionStr);
            _db = _redis.GetDatabase();
        }

        public void Initialize(RedisEndpoint endpoint)
        {
            Client=new RedisCacheClient();
            _redis = Client.GetClient(endpoint, 1000);                       
            _db = _redis.GetDatabase();
        }

        public void Add(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void Add(string key, object value, bool defaultExpire)
        {
            throw new NotImplementedException();
        }

        public void Add(string key, object value, long numOfMinutes)
        {
            throw new NotImplementedException();
        }

        public void Add(string key, object value, TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        public void Add<T>(string key, T value, TimeSpan timeSpan)
        {
            var stringValue = value.ToJson();
            _db.StringSet(key, stringValue);
        }

        public void Add(string key, object value, long numOfMinutes, bool flag)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, T> Get<T>(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            return Get<object>(key);
        }

        public T Get<T>(string key)
        {
            var json = _db.StringGet(key);
            if (string.IsNullOrWhiteSpace(json))
            {
                return default(T);
            }

            var value = json.SafeString().ToObject<T>();
            return value;
        }

        public void Remove(string key)
        {
            _db.KeyDelete(key);
        }

        public async Task PublishAsync(string topic, string value)
        {
            var subscriber = _redis.GetSubscriber();
            await subscriber.PublishAsync(topic, value);
        }

        public async Task SubscribeAsync(string topic)
        {
            var subscriber = _redis.GetSubscriber();
            await subscriber.SubscribeAsync(topic, (channel, value) =>
                {
                    MessageReceived?.Invoke(this, new MessageReceivedEventArgs() {Topic = topic, Value = value});
                });
        }

        public async Task UnsubscribeAsync(string topic)
        {
            var subscriber = _redis.GetSubscriber();
            await subscriber.UnsubscribeAsync(topic);
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        private string GetConnectionString(IDictionary<string, string> parameters)
        {
            var endpoint = parameters["Endpoint"];
            var connnectionStrBuilder= new StringBuilder();
            connnectionStrBuilder.Append(endpoint);
            if (parameters.ContainsKey("Key"))
            {
                var key = parameters["Key"];
                connnectionStrBuilder.Append(",password=" + key);
            }

            if (parameters.ContainsKey("UseSsl"))
            {
                var useSsl = bool.Parse(parameters["UseSsl"]);
                connnectionStrBuilder.Append(",ssl=" + useSsl);
            }

            var connectionStr = connnectionStrBuilder.ToString();
            return connectionStr;
        }
    }
}
