using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Encrypts.Symmetric
{
    /// <summary>
    /// DES 加密
    /// </summary>
    public class DESCryptor : XESCryptorBase
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="key">秘钥，必须使用英文字符，区分大小写，且字符数量是8个，不能多也不能少</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key)
        {
            return Encrypt(data, key, Encoding.UTF8);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="key">秘钥，必须使用英文字符，区分大小写，且字符数量是8个，不能多也不能少</param>
        /// <param name="encoding">编码方式，默认：UTF-8</param>
        /// <param name="iv">加密偏移量，<see cref="CipherMode.ECB"/>模式不可用</param>
        /// <param name="keySize">秘钥大小，只能为：64</param>
        /// <param name="blockSize">块大小，只能为：64</param>
        /// <param name="cipherMode">对称算法加密模式，默认：<see cref="CipherMode.ECB"/></param>
        /// <param name="paddingMode">填充模式，默认：<see cref="PaddingMode.Zeros"/></param>
        /// <param name="outType">输出类型，默认：Base64</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key, Encoding encoding,
            string iv = null, int keySize = 64, int blockSize = 64,
            CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.Zeros,
            OutType outType = OutType.Base64)
        {
            if (key.Length >= 8)
            {
                key = key.Substring(0, 8);
            }
            if (keySize != 64)
            {
                throw new ArgumentOutOfRangeException(nameof(keySize), "keySize 只能为 64.");
            }
            if (blockSize != 64)
            {
                throw new ArgumentOutOfRangeException(nameof(blockSize), "blockSize 只能为 64.");
            }
            return Encrypt<DESCryptoServiceProvider>(data, key, encoding, outType, iv, keySize, blockSize, cipherMode,
                paddingMode);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="key">秘钥，必须使用英文字符，区分大小写，且字符数量是8个，不能多也不能少</param>
        /// <returns></returns>
        public static string Decrypt(string data, string key)
        {
            return Decrypt(data, key, Encoding.UTF8);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="key">秘钥，必须使用英文字符，区分大小写，且字符数量是8个，不能多也不能少</param>
        /// <param name="encoding">编码方式，默认：UTF-8</param>
        /// <param name="iv">加密偏移量，<see cref="CipherMode.ECB"/>模式不可用</param>
        /// <param name="keySize">秘钥大小，只能为：64</param>
        /// <param name="blockSize">块大小，只能为：64</param>
        /// <param name="cipherMode">对称算法加密模式，默认：<see cref="CipherMode.ECB"/></param>
        /// <param name="paddingMode">填充模式，默认：<see cref="PaddingMode.Zeros"/></param>
        /// <param name="outType">输出类型，默认：Base64</param>
        /// <returns></returns>
        public static string Decrypt(string data, string key, Encoding encoding,
            string iv = null, int keySize = 64, int blockSize = 64,
            CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.Zeros,
            OutType outType = OutType.Base64)
        {
            if (key.Length >= 8)
            {
                key = key.Substring(0, 8);
            }
            if (keySize != 64)
            {
                throw new ArgumentOutOfRangeException(nameof(keySize), "keySize 只能为 64.");
            }
            if (blockSize != 64)
            {
                throw new ArgumentOutOfRangeException(nameof(blockSize), "blockSize 只能为 64.");
            }
            return Decrypt<DESCryptoServiceProvider>(data, key, encoding, outType, iv, keySize, blockSize, cipherMode,
                paddingMode).Replace("\0", "");
        }
    }
}
