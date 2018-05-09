using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Applications.Dtos
{
    /// <summary>
    /// Api请求数据结果
    /// </summary>
    [Serializable]
    public class ApiResult
    {
        #region Property(属性)
        /// <summary>
        /// 全局返回码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="ApiResult"/>类型的实例
        /// </summary>
        public ApiResult()
        {
            OperationTime = DateTime.Now;
        }
        #endregion
    }

    /// <summary>
    /// Api请求数据结果
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    [Serializable]
    public class ApiResult<T> : ApiResult
    {
        /// <summary>
        /// 数据结果
        /// </summary>
        public T Data { get; set; }
    }
}
