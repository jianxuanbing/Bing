using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Encrypts.Hash
{
    /// <summary>
    /// Hmac 加密
    /// </summary>
    public class HmacCryptor : HashCryptorBase
    {
        /// <summary>
        /// 获取字符串的 HMAC_SHA1 哈希值，默认编码为<see cref="Encoding.UTF8"/>
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string HmacSha1(string value, string key, Encoding encoding = null)
        {
            return Encrypt<HMACSHA1>(value, key, encoding);
        }

        /// <summary>
        /// 获取字符串的 HMAC_SHA256 哈希值，默认编码为<see cref="Encoding.UTF8"/>
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string HmacSha256(string value, string key, Encoding encoding = null)
        {
            return Encrypt<HMACSHA256>(value, key, encoding);
        }

        /// <summary>
        /// 获取字符串的 HMAC_SHA384 哈希值，默认编码为<see cref="Encoding.UTF8"/>
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string HmacSha384(string value, string key, Encoding encoding = null)
        {
            return Encrypt<HMACSHA384>(value, key, encoding);
        }

        /// <summary>
        /// 获取字符串的 HMAC_SHA512 哈希值，默认编码为<see cref="Encoding.UTF8"/>
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string HmacSha512(string value, string key, Encoding encoding = null)
        {
            return Encrypt<HMACSHA512>(value, key, encoding);
        }

        /// <summary>
        /// 获取字符串的 HMAC_MD5 哈希值，默认编码为<see cref="Encoding.UTF8"/>
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string HmacMd5(string value, string key, Encoding encoding = null)
        {
            return Encrypt<HMACMD5>(value, key, encoding);
        }
    }
}
