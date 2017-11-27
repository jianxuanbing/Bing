using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.IdGenerators;

namespace Bing.Utils.Helpers
{
    /// <summary>
    /// Id生成器
    /// </summary>
    public static class Id
    {
        /// <summary>
        /// Id
        /// </summary>
        private static string _id;

        /// <summary>
        /// 设置Id
        /// </summary>
        /// <param name="id">Id</param>
        public static void SetId(string id)
        {
            _id = id;
        }

        /// <summary>
        /// 重置Id
        /// </summary>
        public static void Reset()
        {
            _id = null;
        }

        /// <summary>
        /// 创建Id
        /// </summary>
        /// <returns></returns>
        public static string ObjectId()
        {
            return string.IsNullOrWhiteSpace(_id) ? Bing.Utils.Helpers.Internal.ObjectId.GenerateNewStringId() : _id;
        }

        /// <summary>
        /// 用Guid创建Id，去掉分隔符
        /// </summary>
        /// <returns></returns>
        public static string Guid()
        {
            return string.IsNullOrWhiteSpace(_id) ? System.Guid.NewGuid().ToString("N") : _id;
        }

        /// <summary>
        /// 创建有序的Guid
        /// </summary>
        /// <returns></returns>
        public static Guid Sequence()
        {
            return SequentialGuidGenerator.Instance.Create();
        }
    }
}
