using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Bing.Caching.Redis
{
    /// <summary>
    /// Redis 对象管理
    /// </summary>
    public class RedisManager
    {
        /// <summary>
        /// 系统自定义Key前缀
        /// </summary>
        public static string SysCustomKey { get; set; }

        /// <summary>
        /// Redis 连接字符串
        /// </summary>
        internal static readonly string RedisConnectionString;

        /// <summary>
        /// 对象锁
        /// </summary>
        private static readonly object _locked=new object();

        /// <summary>
        /// Redis 连接对象实例
        /// </summary>
        private static ConnectionMultiplexer _instance;

        /// <summary>
        /// Redis 连接缓存字典
        /// </summary>
        private static readonly ConcurrentDictionary<string,ConnectionMultiplexer> ConnectionCache=new ConcurrentDictionary<string, ConnectionMultiplexer>();

        /// <summary>
        /// 单例获取
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locked)
                    {
                        if (_instance == null || !_instance.IsConnected)
                        {
                            _instance = BuildConnectionMultiplexer();
                        }
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// 获取 ConnectionMultiplexer 对象
        /// </summary>
        /// <param name="connectionStr">连接字符串</param>
        /// <returns></returns>
        public static ConnectionMultiplexer GetConnectionMultiplexer(string connectionStr)
        {
            if (!ConnectionCache.ContainsKey(connectionStr))
            {
                ConnectionCache[connectionStr] = BuildConnectionMultiplexer(connectionStr);
            }

            return ConnectionCache[connectionStr];
        }

        /// <summary>
        /// 生成 ConnectionMultiplexer 对象
        /// </summary>
        /// <param name="connectionStr">连接字符串</param>
        /// <returns></returns>
        private static ConnectionMultiplexer BuildConnectionMultiplexer(string connectionStr=null)
        {
            connectionStr = connectionStr ?? RedisConnectionString;
            var connect = ConnectionMultiplexer.Connect(connectionStr);
            // 注册事件如下
            connect.ConnectionFailed += MuxerConnectionFailed;
            connect.ConnectionRestored += MuxerConnectionRestored;
            connect.ErrorMessage += MuxerErrorMessage;
            connect.ConfigurationChanged += MuxerConfigurationChanged;
            connect.HashSlotMoved += MuxerHashSlotMoved;
            connect.InternalError += MuxerInternalError;

            return connect;
        }

        /// <summary>
        /// 配置更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Console.WriteLine("Configuration changed:"+e.EndPoint);
        }

        /// <summary>
        /// 发生错误事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            Console.WriteLine("ErrorMessage:"+e.Message);
        }

        /// <summary>
        /// 重新建立连接之前的错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("ConnectionRestored:"+e.EndPoint);
        }

        /// <summary>
        /// 连接失败，如果重新连接成功你奖不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("重新连接:EndPoint failed:" + e.EndPoint + ", " + e.FailureType +
                              (e.Exception == null ? "" : "," + e.Exception.Message));
        }

        /// <summary>
        /// 更改集群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            Console.WriteLine("HashSlotMoved:NewEndPoint"+e.NewEndPoint+", OldEndPoint"+e.OldEndPoint);
        }

        /// <summary>
        /// Redis 类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            Console.WriteLine("InternalError:Message"+e.Exception.Message);
        }
    }
}
