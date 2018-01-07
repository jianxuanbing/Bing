using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.HashAlgorithms
{
    /// <summary>
    /// 一致性哈希算法
    /// </summary>
    public class HashAlgorithm:IHashAlgorithm
    {
        #region 常量

        private const uint m = 0x5bd1e995;
        private const int r = 24;

        #endregion

        #region Hash(获取哈希值)

        /// <summary>
        /// 获取哈希值
        /// </summary>
        /// <param name="item">字符串</param>
        /// <returns></returns>
        public int Hash(string item)
        {
            var hash = Hash(Encoding.ASCII.GetBytes(item));
            return (int)hash;
        }

        /// <summary>
        /// 获取哈希值
        /// </summary>
        /// <param name="data">需要哈希的数据</param>
        /// <param name="seed">种子数</param>
        /// <returns></returns>
        public static uint Hash(byte[] data, uint seed = 0xc58f1a7b)
        {
            var length = data.Length;
            if (length == 0)
            {
                return 0;
            }

            var h = seed ^ (uint)length;
            var c = 0;
            while (length >= 4)
            {
                var k = (uint)(data[c++] | data[c++] << 8 | data[c++] << 16 | data[c++] << 24);
                k *= m;
                k ^= k >> r;
                k *= m;
                h *= m;
                h ^= k;
                length -= 4;
            }

            switch (length)
            {
                case 3:
                    h ^= (ushort)(data[c++] | data[c++] << 8);
                    h ^= (uint)(data[c] << 16);
                    h *= m;
                    break;
                case 2:
                    h ^= (ushort)(data[c++] | data[c++] << 8);
                    h *= m;
                    break;
                case 1:
                    h ^= data[c];
                    h *= m;
                    break;
                default:
                    break;
            }

            h ^= h >> 13;
            h *= m;
            h ^= h >> 15;
            return h;
        }

        #endregion


    }
}
