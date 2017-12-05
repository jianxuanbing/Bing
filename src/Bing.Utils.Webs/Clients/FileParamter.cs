using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Webs.Clients
{
    /// <summary>
    /// 文件参数
    /// </summary>
    public struct FileParamter
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 读写操作流，返回的是写入的字节流长度
        /// </summary>
        public Action<Stream> Writer;

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName;

        /// <summary>
        /// 文件类型
        /// </summary>
        public string ContentType;

        /// <summary>
        /// 初始化一个<see cref="FileParamter"/>类型的实例
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="fileStream">文件流，调用方会自动释放</param>
        /// <param name="fileName">文件名</param>
        /// <param name="contentType">文件类型</param>
        public FileParamter(string name, Stream fileStream, string fileName, string contentType)
        {
            this.Writer = x =>
            {
                var buffer = new byte[1024];
                using (fileStream)
                {
                    int readCount = 0;
                    while ((readCount = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        x.Write(buffer, 0, readCount);
                    }
                }
            };
            this.FileName = fileName;
            this.ContentType = contentType;
            this.Name = name;
        }
    }
}
