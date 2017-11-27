using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Encrypts.Symmetric
{
    /// <summary>
    /// AES 加密
    /// </summary>
    public class AESCryptor : XESCryptorBase
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key)
        {
            return Encrypt(data, key, Encoding.UTF8);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="key">秘钥</param>
        /// <param name="encoding">编码方式，默认：UTF-8</param>
        /// <param name="iv">加密偏移量，<see cref="CipherMode.ECB"/>模式不可用</param>
        /// <param name="keySize">秘钥大小，只能为：128、192、256</param>
        /// <param name="blockSize">块大小，只能为：128、192、256</param>
        /// <param name="cipherMode">对称算法加密模式，默认：<see cref="CipherMode.ECB"/></param>
        /// <param name="paddingMode">填充模式，默认：<see cref="PaddingMode.PKCS7"/></param>
        /// <param name="outType">输出类型，默认：Base64</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key, Encoding encoding,
            string iv = null, int keySize = 256, int blockSize = 128,
            CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.PKCS7,
            OutType outType = OutType.Base64)
        {
            if (!(keySize == 256 || keySize == 192 || keySize == 128))
            {
                throw new ArgumentOutOfRangeException(nameof(keySize), "keySize 只能为 128、192、256.");
            }
            if (!(blockSize == 256 || blockSize == 192 || blockSize == 128))
            {
                throw new ArgumentOutOfRangeException(nameof(blockSize), "blockSize 只能为 128、192、256.");
            }
            return Encrypt<RijndaelManaged>(data, key, encoding, outType, iv, keySize, blockSize, cipherMode,
                paddingMode);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string Decrypt(string data, string key)
        {
            return Decrypt(data, key, Encoding.UTF8);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="key">秘钥</param>
        /// <param name="encoding">编码方式，默认：UTF-8</param>
        /// <param name="iv">加密偏移量，<see cref="CipherMode.ECB"/>模式不可用</param>
        /// <param name="keySize">秘钥大小，只能为：128、192、256</param>
        /// <param name="blockSize">块大小，只能为：128、192、256</param>
        /// <param name="cipherMode">对称算法加密模式，默认：<see cref="CipherMode.ECB"/></param>
        /// <param name="paddingMode">填充模式，默认：<see cref="PaddingMode.PKCS7"/></param>
        /// <param name="outType">输出类型，默认：Base64</param>
        /// <returns></returns>
        public static string Decrypt(string data, string key, Encoding encoding,
            string iv = null, int keySize = 256, int blockSize = 128,
            CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.PKCS7,
            OutType outType = OutType.Base64)
        {
            if (!(keySize == 256 || keySize == 192 || keySize == 128))
            {
                throw new ArgumentOutOfRangeException(nameof(keySize), "keySize 只能为 128、192、256.");
            }
            if (!(blockSize == 256 || blockSize == 192 || blockSize == 128))
            {
                throw new ArgumentOutOfRangeException(nameof(blockSize), "blockSize 只能为 128、192、256.");
            }
            return Decrypt<RijndaelManaged>(data, key, encoding, outType, iv, keySize, blockSize, cipherMode,
                paddingMode);
        }
    }
}
