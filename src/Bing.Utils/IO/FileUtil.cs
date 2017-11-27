using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Helpers;

namespace Bing.Utils.IO
{
    /// <summary>
    /// 文件操作辅助类
    /// </summary>
    public class FileUtil
    {
        #region CreateIfNotExists(创建文件，如果文件不存在)

        /// <summary>
        /// 创建文件，如果文件不存在
        /// </summary>
        /// <param name="fileName">文件名，绝对路径</param>
        public static void CreateIfNotExists(string fileName)
        {
            if (File.Exists(fileName))
            {
                return;
            }
            File.Create(fileName);
        }

        #endregion

        #region DeleteIfExists(删除指定文件)

        /// <summary>
        /// 删除指定文件，如果文件存在
        /// </summary>
        /// <param name="fileName">文件名，绝对路径</param>
        public static void DeleteIfExists(string fileName)
        {
            if (File.Exists(fileName))
            {
                return;
            }
            File.Delete(fileName);
        }

        #endregion

        #region SetAttribute(设置文件属性)

        /// <summary>
        /// 设置文件属性
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="attribute">文件属性</param>
        /// <param name="isSet">是否为设置属性,true:设置,false:取消</param>
        public static void SetAttribute(string fileName, FileAttributes attribute, bool isSet)
        {
            FileInfo fi = new FileInfo(fileName);
            if (!fi.Exists)
            {
                throw new FileNotFoundException("要设置属性的文件不存在。", fileName);
            }
            if (isSet)
            {
                fi.Attributes = fi.Attributes | attribute;
            }
            else
            {
                fi.Attributes = fi.Attributes & ~attribute;
            }
        }

        #endregion

        #region GetVersion(获取文件版本号)

        /// <summary>
        /// 获取文件版本号
        /// </summary>
        /// <param name="fileName">完整文件名</param>
        /// <returns></returns>
        public static string GetVersion(string fileName)
        {
            if (File.Exists(fileName))
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(fileName);
                return fvi.FileVersion;
            }
            return null;
        }

        #endregion

        #region GetFileMd5(获取文件的MD5值)

        /// <summary>
        /// 获取文件的MD5值
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string GetFileMd5(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            const int bufferSize = 1024 * 1024;
            byte[] buffer = new byte[bufferSize];

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.Initialize();

            long offset = 0;
            while (offset < fs.Length)
            {
                long readSize = bufferSize;
                if (offset + readSize > fs.Length)
                {
                    readSize = fs.Length - offset;
                }
                fs.Read(buffer, 0, (int)readSize);
                if (offset + readSize < fs.Length)
                {
                    md5.TransformBlock(buffer, 0, (int)readSize, buffer, 0);
                }
                else
                {
                    md5.TransformFinalBlock(buffer, 0, (int)readSize);
                }
                offset += bufferSize;
            }
            fs.Close();
            byte[] result = md5.Hash;
            md5.Clear();
            StringBuilder sb = new StringBuilder();
            foreach (var b in result)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        #endregion

        #region GetContentType(根据扩展名获取文件内容类型)
        /// <summary>
        /// 根据扩展名获取文件内容类型
        /// </summary>
        /// <param name="ext">扩展名</param>
        /// <returns></returns>
        public static string GetContentType(string ext)
        {
            string contentType = "";
            var dict = Const.FileExtensionDict;
            ext = ext.ToLower();
            if (!ext.StartsWith("."))
            {
                ext = "." + ext;
            }
            dict.TryGetValue(ext, out contentType);
            return contentType;
        }

        #endregion
    }
}
