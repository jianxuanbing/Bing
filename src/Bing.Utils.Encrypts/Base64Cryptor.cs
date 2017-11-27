using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Extensions;

namespace Bing.Utils.Encrypts
{
    /// <summary>
    /// Base64 加密
    /// </summary>
    public class Base64Cryptor
    {
        #region Encrypt(加密)

        /// <summary>
        /// 加密，返回Base64字符串
        /// </summary>
        /// <param name="data">明文</param>
        /// <returns></returns>
        public static string Encrypt(string data)
        {
            return Encrypt(data, Encoding.UTF8);
        }

        /// <summary>
        /// 加密，返回Base64字符串
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string Encrypt(string data, Encoding encoding)
        {
            data.CheckNotNullOrEmpty(nameof(data));
            return Convert.ToBase64String(encoding.GetBytes(data));
        }

        #endregion

        #region Decrypt(解密)

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <returns></returns>
        public static string Decrypt(string data)
        {
            return Decrypt(data, Encoding.UTF8);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string Decrypt(string data, Encoding encoding)
        {
            data.CheckNotNullOrEmpty(nameof(data));
            return encoding.GetString(Convert.FromBase64String(data));
        }

        #endregion
    }
}
