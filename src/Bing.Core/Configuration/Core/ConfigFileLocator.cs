using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Configuration.Abstractions;

namespace Bing.Configuration.Core
{
    /// <summary>
    /// 配置文件定位器
    /// </summary>
    public class ConfigFileLocator:IConfigFileLocator
    {
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private readonly IEnumerable<string> _paths;

        /// <summary>
        /// 配置文件扩展名
        /// </summary>
        private readonly IEnumerable<string> _fileExtensions;

        /// <summary>
        /// 初始化一个<see cref="ConfigFileLocator"/>类型的实例
        /// </summary>
        /// <param name="paths">配置文件路径</param>
        public ConfigFileLocator(params string[] paths)
        {
            this._paths = paths;
            this._fileExtensions = new[] {"config.json"};
        }

        /// <summary>
        /// 查找配置文件集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FileInfo> FindConfigFiles() => this._paths.SelectMany(FindConfigFilesInFolder).ToArray();

        /// <summary>
        /// 在指定文件夹查找配置文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        private IEnumerable<FileInfo> FindConfigFilesInFolder(string path) =>
            this._fileExtensions.SelectMany(ext => FindConfigFileByMask(new DirectoryInfo(path), ext));

        /// <summary>
        /// 通过模糊匹配查找配置文件
        /// </summary>
        /// <param name="dir">目录</param>
        /// <param name="fileExt">文件扩展名</param>
        /// <returns></returns>
        private static IEnumerable<FileInfo> FindConfigFileByMask(DirectoryInfo dir, string fileExt) =>
            dir.GetFiles("*." + fileExt, SearchOption.TopDirectoryOnly);

        /// <summary>
        /// 获取已支持文件的扩展名
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetSupportedFileExtensions() => this._fileExtensions;
    }
}
