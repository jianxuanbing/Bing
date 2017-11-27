using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Extensions;

namespace Bing.Utils.Encrypts.Hash
{
    /// <summary>
    /// MD5 加密
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class MD5Cryptor
    {
        #region Encrypt16(16位加密)

        /// <summary>
        /// 加密，返回16位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <returns></returns>
        public static string Encrypt16(string text)
        {
            return Encrypt16(text, Encoding.UTF8);
        }

        /// <summary>
        /// 加密，返回16位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string Encrypt16(string text, Encoding encoding)
        {
            var result = Encrypt(text, encoding);
            return BitConverter.ToString(result, 4, 8).Replace("-", "");
        }

        #endregion

        #region Encrypt32(32位加密)

        /// <summary>
        /// 加密，返回32位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <returns></returns>
        public static string Encrypt32(string text)
        {
            return Encrypt32(text, Encoding.UTF8);
        }

        /// <summary>
        /// 加密，返回32位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string Encrypt32(string text, Encoding encoding)
        {
            var result = Encrypt(text, encoding);
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < result.Length; i++)
            //{
            //    sb.Append(result[i].ToString("X2"));
            //}
            //return sb.ToString();
            return BitConverter.ToString(result).Replace("-", "");
        }

        #endregion

        #region Encrypt64(64加密)

        /// <summary>
        /// 加密，返回Base64结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <returns></returns>
        public static string Encrypt64(string text)
        {
            return Encrypt64(text, Encoding.UTF8);
        }

        /// <summary>
        /// 加密，返回Base64结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string Encrypt64(string text, Encoding encoding)
        {
            var result = Encrypt(text, encoding);
            return Convert.ToBase64String(result);
        }

        #endregion

        #region Encrypt(加密)

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string Encrypt(string text, Encoding encoding, int? startIndex, int? length)
        {
            var result = Encrypt(text, encoding);
            var encrypted = startIndex == null
                ? BitConverter.ToString(result)
                : BitConverter.ToString(result, startIndex.SafeValue(), length.SafeValue());
            return encrypted.Replace("-", "");
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">明文</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        private static byte[] Encrypt(string text, Encoding encoding)
        {
            byte[] result = null;
            text.CheckNotNullOrEmpty(nameof(text));
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            try
            {
                result = md5.ComputeHash(encoding.GetBytes(text));
            }
            finally
            {
                md5.Clear();
            }
            return result;
        }

        #endregion

    }
}
