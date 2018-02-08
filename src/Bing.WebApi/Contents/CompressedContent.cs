using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bing.WebApi.Contents
{
    /// <summary>
    /// 压缩内容
    /// </summary>
    public class CompressedContent:HttpContent
    {
        /// <summary>
        /// 来源内容
        /// </summary>
        private readonly HttpContent _originalContent;

        /// <summary>
        /// 编码类型
        /// </summary>
        private readonly string _encodingType;

        /// <summary>
        /// 初始化一个<see cref="CompressedContent"/>类型的实例
        /// </summary>
        /// <param name="content">Http内容</param>
        /// <param name="encodingType">编码类型</param>
        public CompressedContent(HttpContent content, string encodingType)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (encodingType == null)
            {
                throw new ArgumentNullException(nameof(encodingType));
            }

            this._originalContent = content;
            this._encodingType = encodingType.ToLowerInvariant();

            if (this._encodingType != "gzip" && this._encodingType != "deflate")
            {
                throw new InvalidOperationException($"Encoding '{this._encodingType}' is not supported. Only supports gzip or deflate encoding.");
            }

            foreach (var header in this._originalContent.Headers)
            {
                Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
            Headers.ContentEncoding.Add(encodingType);
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            Stream compressedStream = null;

            if (this._encodingType == "gzip")
            {
                compressedStream = new GZipStream(stream, CompressionMode.Compress, leaveOpen: true);
            }
            else if (this._encodingType == "deflate")
            {
                compressedStream = new DeflateStream(stream, CompressionMode.Compress, leaveOpen: true);
            }

            return this._originalContent.CopyToAsync(compressedStream).ContinueWith(tsk =>
            {
                if (compressedStream != null)
                {
                    compressedStream.Dispose();
                }
            });
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }
    }
}
