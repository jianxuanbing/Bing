using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bing.Comments
{
    /// <summary>
    /// Xml注释缓存，对于注释Xml的缓存
    /// </summary>
    public static class CsCommentsXmlCache
    {
        /// <summary>
        /// 缓存项。Key:Assembly.FullName   Value:XmlDocument
        /// </summary>
        private static readonly Dictionary<string,XmlDocument> _cache=new Dictionary<string, XmlDocument>();

        /// <summary>
        /// 缓存项。Key:XmlPath     Value:XmlDocument
        /// </summary>
        private static readonly Dictionary<string,XmlDocument> _cacheByPath=new Dictionary<string, XmlDocument>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <param name="member">成员信息</param>
        /// <returns></returns>
        public static XmlDocument Get(MemberInfo member)
        {
            var key = member.Module.Assembly.FullName;
            XmlDocument xml;

            if (_cache.TryGetValue(key, out xml))
            {
                return xml;
            }

            lock (_cache)
            {
                if (_cache.TryGetValue(key, out xml))
                {
                    return xml;
                }

                var xmlFile = GetXmlPath(member.Module.FullyQualifiedName);
                if (xmlFile == null)
                {
                    return null;
                }

                var xmlText = File.ReadAllText(xmlFile);
                xml=new XmlDocument();
                xml.LoadXml(xmlText);
                _cache.Add(key,xml);
                _cacheByPath[xmlFile] = xml;
                return xml;
            }
        }

        /// <summary>
        /// 设置缓存项
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="xmlPath">Xml路径</param>
        internal static void Set(Assembly assembly, string xmlPath)
        {
            var key = assembly.FullName;
            lock (_cache)
            {
                if (xmlPath == null || File.Exists(xmlPath) == false)
                {
                    _cacheByPath[xmlPath] = _cache[key] = new XmlDocument();
                }
                else
                {
                    var xmlText = File.ReadAllText(xmlPath);
                    var xml=new XmlDocument();

                    xml.LoadXml(xmlText);
                    _cache[key] = xml;
                    _cacheByPath[xmlPath] = xml;
                }
            }
        }

        /// <summary>
        /// 刷新缓存项
        /// </summary>
        /// <param name="xmlPath">xml路径</param>
        internal static void Reset(string xmlPath)
        {
            if (xmlPath == null)
            {
                return;
            }

            lock (_cache)
            {
                XmlDocument xml;
                if (_cacheByPath.TryGetValue(xmlPath, out xml) == false)
                {
                    return;
                }

                var xmlText = File.ReadAllText(xmlPath);
                xml.LoadXml(xmlText);
            }
        }

        /// <summary>
        /// 获取指定dll的注释xml的路径，如果不存在则返回null
        /// </summary>
        /// <param name="dllPath">dll路径</param>
        /// <returns></returns>
        internal static string GetXmlPath(string dllPath)
        {
            if (string.IsNullOrEmpty(dllPath) || dllPath[0] == '<')
            {
                return null;
            }

            var xmlFile = dllPath.Remove(dllPath.Length - Path.GetExtension(dllPath).Length) + ".xml";
            if (File.Exists(xmlFile) == false)
            {
                xmlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin",
                    Path.GetFileNameWithoutExtension(dllPath) + ".xml");
                if (File.Exists(xmlFile) == false)
                {
                    return null;
                }
            }

            return xmlFile;
        }
    }
}
