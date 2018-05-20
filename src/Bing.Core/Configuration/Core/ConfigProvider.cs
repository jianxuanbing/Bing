using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Configuration.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Bing.Configuration.Core
{
    /// <summary>
    /// 配置 提供程序
    /// </summary>
    public class ConfigProvider:IConfigProvider
    {
        /// <summary>
        /// 默认配置目录
        /// </summary>
        private const string DEFAULT_CONFIG_DIR = "config";

        /// <summary>
        /// 配置类名后缀
        /// </summary>
        private const string CONFIG_CLASS_NAME_SUFFIX = "configuration";

        /// <summary>
        /// 配置字典
        /// </summary>
        private readonly ConcurrentDictionary<Type, object> _configurations;

        /// <summary>
        /// 配置元数据字典
        /// </summary>
        private readonly ConcurrentDictionary<string, ConfigFileMetadata> _metadatas;

        /// <summary>
        /// 是否已加载配置
        /// </summary>
        private volatile bool _configLoaded;

        /// <summary>
        /// 对象锁
        /// </summary>
        private readonly object _lock=new object();

        /// <summary>
        /// Json序列化设置
        /// </summary>
        private static readonly JsonSerializerSettings JsonSerializerSettings;

        /// <summary>
        /// 配置文件读取器
        /// </summary>
        private readonly IConfigFileReader _configFileReader;

        /// <summary>
        /// 配置文件定位器
        /// </summary>
        private readonly IConfigFileLocator _configFileLocator;

        private readonly IConfigFileWriter _configFileWriter;

        /// <summary>
        /// 默认配置
        /// </summary>
        public static IConfigProvider Default { get; } = new ConfigProvider();

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ConfigProvider() => JsonSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            MissingMemberHandling = MissingMemberHandling.Error,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateParseHandling = DateParseHandling.None,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            Formatting = Formatting.Indented
        };

        /// <summary>
        /// 初始化一个<see cref="ConfigProvider"/>类型的实例
        /// </summary>
        public ConfigProvider() : this(DEFAULT_CONFIG_DIR)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="ConfigProvider"/>类型的实例
        /// </summary>
        /// <param name="configBaseDir">配置目录基路径</param>
        /// <param name="configFileReader">配置文件读取器</param>
        /// <param name="configFileLocator">配置文件定位器</param>
        /// <param name="configFileWriter">配置文件写入器</param>
        public ConfigProvider(string configBaseDir,
            IConfigFileReader configFileReader = null, IConfigFileLocator configFileLocator = null,IConfigFileWriter configFileWriter=null)
        {
            var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configBaseDir);
            _configurations=new ConcurrentDictionary<Type, object>();
            _metadatas=new ConcurrentDictionary<string, ConfigFileMetadata>();
            _configLoaded = false;
            this._configFileLocator = configFileLocator ?? new ConfigFileLocator(baseDir);
            this._configFileReader = configFileReader ?? new ConfigFileReader();
            this._configFileWriter = configFileWriter ?? new ConfigFileWriter();
        }

        /// <summary>
        /// 获取配置对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        public T GetConfiguration<T>() where T : class, new()
        {
            CheckLoadConfiguration();

            T config;
            if ((config = TryGetCacheConfiguration<T>()) == null)
            {
                config = TryGetTypedConfiguration<T>();
                _configurations[typeof(T)] = config ?? throw new ArgumentNullException(
                                                $"Unable to get configuration of type {typeof(T).Name}! " +
                                                $"Missing {TypeToSectionName(typeof(T))}." +
                                                $"[{string.Join("|", _configFileLocator.GetSupportedFileExtensions())}]?");
            }

            return config;
        }

        /// <summary>
        /// 设置配置对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="entity">对象</param>
        public void SetConfiguration<T>(T entity) where T : class, new()
        {
            var configName = TypeToSectionName(typeof(T));
            var metadata = _metadatas[configName];
            if (metadata == null)
            {
                throw new ArgumentNullException($"{configName} 配置文件的元数据不存在");
            }

            var content = JsonConvert.SerializeObject(entity, JsonSerializerSettings);
            this._configFileWriter.Write(metadata.ConfigFile, content);
        }

        /// <summary>
        /// 尝试通过缓存获取配置
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        private T TryGetCacheConfiguration<T>()
        {
            _configurations.TryGetValue(typeof(T), out var tmpConfig);
            return (T) tmpConfig;
        }

        /// <summary>
        /// 尝试获取类型配置
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        private T TryGetTypedConfiguration<T>() where T : class,new()
        {
            var configData = _metadatas.Values
                .FirstOrDefault(v => v.ConfigName == TypeToSectionName(typeof(T)));

            if (string.IsNullOrWhiteSpace(configData?.Content))
            {
                return new T();
            }

            var config = JsonConvert.DeserializeObject<T>(configData.Content, JsonSerializerSettings) ?? new T();

            return config;
        }

        /// <summary>
        /// 类型转换成节点名称
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private static string TypeToSectionName(Type type) =>
            type.Name.ToLower().Replace(CONFIG_CLASS_NAME_SUFFIX, string.Empty);

        /// <summary>
        /// 加载配置
        /// </summary>
        private void LoadConfiguration()
        {
            var configFiles = this._configFileLocator.FindConfigFiles();
            foreach (var configFile in configFiles)
            {
                var metadata = this._configFileReader.Parse(configFile);                
                _metadatas[metadata.ConfigName] = metadata;
            }
        }

        /// <summary>
        /// 检查加载配置
        /// </summary>
        private void CheckLoadConfiguration()
        {
            if (!_configLoaded)
            {
                lock (_lock)
                {
                    if (!_configLoaded)
                    {
                        LoadConfiguration();
                        _configLoaded = true;
                    }
                }
            }
        }

        /// <summary>
        /// 刷新配置
        /// </summary>
        public void RefreshAll()
        {
            _configLoaded = false;
            _configurations.Clear();
            _metadatas.Clear();
        }
    }
}
