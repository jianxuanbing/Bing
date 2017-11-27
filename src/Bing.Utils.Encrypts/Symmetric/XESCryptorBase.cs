using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Extensions;

namespace Bing.Utils.Encrypts.Symmetric
{
    /// <summary>
    /// 对称加密基类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public abstract class XESCryptorBase
    {
        /// <summary>
        /// 加密 - 默认加密
        /// </summary>
        /// <typeparam name="T">对称加密算法类型</typeparam>
        /// <param name="data">明文</param>
        /// <param name="key">秘钥</param>
        /// <param name="encoding">编码方式，默认：UTF-8</param>
        /// <param name="outType">输出类型，默认：Base64</param>
        /// <param name="iv">加密偏移量，<see cref="CipherMode.ECB"/>模式不可用</param>
        /// <param name="keySize">秘钥大小</param>
        /// <param name="blockSize">块大小</param>
        /// <param name="cipherMode">对称算法加密模式，默认：<see cref="CipherMode.ECB"/></param>
        /// <param name="paddingMode">填充模式，默认：<see cref="PaddingMode.PKCS7"/></param>
        /// <returns></returns>
        internal static string Encrypt<T>(string data, string key, Encoding encoding = null,
            OutType outType = OutType.Base64, string iv = null,
            int keySize = 128, int blockSize = 128, CipherMode cipherMode = CipherMode.ECB,
            PaddingMode paddingMode = PaddingMode.PKCS7) where T : SymmetricAlgorithm, new()
        {
            data.CheckNotNullOrEmpty(nameof(data));
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            byte[] plainBytes = encoding.GetBytes(data);

            byte[] encrypted;
            using (T cipher = new T())
            {
                cipher.KeySize = keySize;
                cipher.BlockSize = blockSize;
                cipher.Mode = cipherMode;
                cipher.Padding = paddingMode;
                cipher.Key = encoding.GetBytes(key);
                if (iv != null)
                {
                    int ivsLength = blockSize / 8;
                    byte[] ivs = new byte[ivsLength];
                    Array.Copy(encoding.GetBytes(iv.PadRight(ivsLength)), ivs, ivsLength);
                    cipher.IV = ivs;
                }

                using (ICryptoTransform encryptor = cipher.CreateEncryptor())
                {
                    encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                }

                cipher.Clear();
            }
            return GetEncryptResult(encrypted, outType);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <typeparam name="T">对称加密算法类型</typeparam>
        /// <param name="data">明文</param>
        /// <param name="key">秘钥</param>
        /// <param name="encoding">编码方式，默认：UTF-8</param>
        /// <param name="cipherMode">对称算法加密模式</param>
        /// <param name="hashName">哈希算法名</param>
        /// <param name="keySize">秘钥大小</param>
        /// <param name="blockSize">块大小</param>
        /// <param name="salt">加盐，用于对秘钥进行加密</param>
        /// <param name="iv">加密偏移量，<see cref="CipherMode.ECB"/>模式不可用</param>
        /// <param name="iterations">迭代数</param>
        /// <returns></returns>
        internal static string Encrypt<T>(string data, string key, Encoding encoding = null,
            CipherMode cipherMode = CipherMode.ECB, string hashName = "SHA1", int keySize = 128, int blockSize = 128,
            string salt = null, string iv = null, int iterations = 1000) where T : SymmetricAlgorithm, new()
        {
            data.CheckNotNullOrEmpty(nameof(data));
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            byte[] plainBytes = encoding.GetBytes(data);

            byte[] encrypted;
            using (T cipher = new T())
            {
                cipher.KeySize = keySize;
                cipher.BlockSize = blockSize;
                cipher.Mode = cipherMode;

                int keyLength = keySize / 8;

                byte[] saltBytes = salt == null ? encoding.GetBytes(key) : encoding.GetBytes(salt);
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, saltBytes, hashName, iterations);
                byte[] keys = pdb.GetBytes(keyLength);
                //Rfc2898DeriveBytes rdb = new Rfc2898DeriveBytes(key, saltBytes, iterations);
                //byte[] keys = rdb.GetBytes(keyLength);

                cipher.Key = keys;

                if (iv != null)
                {
                    int ivLength = blockSize / 8;
                    byte[] ivs = new byte[ivLength];
                    Array.Copy(encoding.GetBytes(iv.PadRight(ivLength)), ivs, ivLength);
                    cipher.IV = ivs;
                }

                using (ICryptoTransform encryptor = cipher.CreateEncryptor())
                {
                    encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                }

                cipher.Clear();
            }
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// 解密 - 默认解密
        /// </summary>
        /// <typeparam name="T">对称加密算法类型</typeparam>
        /// <param name="data">明文</param>
        /// <param name="key">秘钥</param>
        /// <param name="encoding">编码方式，默认：UTF-8</param>
        /// <param name="outType">输出类型，默认：Base64</param>
        /// <param name="iv">加密偏移量，<see cref="CipherMode.ECB"/>模式不可用</param>
        /// <param name="keySize">秘钥大小</param>
        /// <param name="blockSize">块大小</param>
        /// <param name="cipherMode">对称算法加密模式，默认：<see cref="CipherMode.ECB"/></param>
        /// <param name="paddingMode">填充模式，默认：<see cref="PaddingMode.PKCS7"/></param>
        /// <returns></returns>
        internal static string Decrypt<T>(string data, string key, Encoding encoding = null,
            OutType outType = OutType.Base64, string iv = null,
            int keySize = 128, int blockSize = 128, CipherMode cipherMode = CipherMode.ECB,
            PaddingMode paddingMode = PaddingMode.PKCS7) where T : SymmetricAlgorithm, new()
        {
            data.CheckNotNullOrEmpty(nameof(data));
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            byte[] encryptedBytes = GetEncryptBytes(data, outType);

            byte[] decrypted;
            using (T cipher = new T())
            {
                cipher.KeySize = keySize;
                cipher.BlockSize = blockSize;
                cipher.Mode = cipherMode;
                cipher.Padding = paddingMode;
                cipher.Key = encoding.GetBytes(key);
                if (iv != null)
                {
                    int ivsLength = blockSize / 8;
                    byte[] ivs = new byte[ivsLength];
                    Array.Copy(encoding.GetBytes(iv.PadRight(ivsLength)), ivs, ivsLength);
                    cipher.IV = ivs;
                }

                using (ICryptoTransform encryptor = cipher.CreateDecryptor())
                {
                    decrypted = encryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                }

                cipher.Clear();
            }
            return encoding.GetString(decrypted);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <typeparam name="T">对称加密算法类型</typeparam>
        /// <param name="data">明文</param>
        /// <param name="key">秘钥</param>
        /// <param name="encoding">编码方式，默认：UTF-8</param>
        /// <param name="cipherMode">对称算法加密模式</param>
        /// <param name="hashName">哈希算法名</param>
        /// <param name="keySize">秘钥大小</param>
        /// <param name="blockSize">块大小</param>
        /// <param name="salt">加盐，用于对秘钥进行加密</param>
        /// <param name="iv">加密偏移量，<see cref="CipherMode.ECB"/>模式不可用</param>
        /// <param name="iterations">迭代数</param>
        /// <returns></returns>
        internal static string Decrypt<T>(string data, string key, Encoding encoding = null,
            CipherMode cipherMode = CipherMode.ECB, string hashName = "SHA1", int keySize = 128, int blockSize = 128,
            string salt = null, string iv = null, int iterations = 1000) where T : SymmetricAlgorithm, new()
        {
            data.CheckNotNullOrEmpty(nameof(data));
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            byte[] encryptedBytes = Convert.FromBase64String(data);

            byte[] decrypted;
            using (T cipher = new T())
            {
                cipher.KeySize = keySize;
                cipher.BlockSize = blockSize;
                cipher.Mode = cipherMode;

                int keyLength = keySize / 8;


                byte[] saltBytes = salt == null ? encoding.GetBytes(key) : encoding.GetBytes(salt);
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, saltBytes, hashName, iterations);
                byte[] keys = pdb.GetBytes(keyLength);
                //Rfc2898DeriveBytes rdb=new Rfc2898DeriveBytes(key,saltBytes,iterations);
                //byte[] keys = rdb.GetBytes(keyLength);

                cipher.Key = keys;
                if (iv != null)
                {
                    int ivLength = blockSize / 8;
                    byte[] ivs = new byte[ivLength];
                    Array.Copy(encoding.GetBytes(iv.PadRight(ivLength)), ivs, ivLength);
                    cipher.IV = ivs;
                }

                using (ICryptoTransform encryptor = cipher.CreateDecryptor())
                {
                    decrypted = encryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                }
                cipher.Clear();
            }
            return encoding.GetString(decrypted);
        }

        #region 辅助方法

        /// <summary>
        /// 获取加密结果
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="outType">输出类型</param>
        /// <returns></returns>
        protected static string GetEncryptResult(byte[] data, OutType outType)
        {
            switch (outType)
            {
                case OutType.Base64:
                    return Convert.ToBase64String(data);
                case OutType.Hex:
                    return Byte2HexString(data);
                default:
                    return Convert.ToBase64String(data);
            }
        }

        /// <summary>
        /// 获取加密数组
        /// </summary>
        /// <param name="data">加密文本</param>
        /// <param name="outType">输出类型</param>
        /// <returns></returns>
        protected static byte[] GetEncryptBytes(string data, OutType outType)
        {
            switch (outType)
            {
                case OutType.Base64:
                    return Convert.FromBase64String(data);
                case OutType.Hex:
                    return HexString2Byte(data);
                default:
                    return Convert.FromBase64String(data);
            }
        }

        /// <summary>
        /// Byte[]转16进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        protected static string Byte2HexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 16进制字符串转Byte[]
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <returns></returns>
        protected static byte[] HexString2Byte(string hex)
        {
            int len = hex.Length / 2;
            byte[] bytes = new byte[len];
            for (int i = 0; i < len; i++)
            {
                bytes[i] = (byte)(Convert.ToInt32(hex.Substring(i * 2, 2), 16));
            }
            return bytes;
        }

        #endregion
    }
}
