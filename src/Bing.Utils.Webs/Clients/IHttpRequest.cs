using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Webs.Clients
{
    /// <summary>
    /// Http请求
    /// </summary>
    public interface IHttpRequest : IRequest<IHttpRequest>
    {
        /// <summary>
        /// 请求成功返回函数
        /// </summary>
        /// <param name="action">执行成功的回调函数，参数为响应结果</param>
        /// <returns></returns>
        IHttpRequest OnSuccess(Action<string> action);

        /// <summary>
        /// 请求成功返回函数
        /// </summary>
        /// <param name="action">执行成功的回调函数，第一参数为响应结果，第二个参数为状态码</param>
        /// <returns></returns>
        IHttpRequest OnSuccess(Action<string, HttpStatusCode> action);

        /// <summary>
        /// 获取Json结果
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        T ResultFromJson<T>();

        /// <summary>
        /// 获取Json结果
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        Task<T> ResultFromJsonAsync<T>();
    }

    /// <summary>
    /// Http请求
    /// </summary>
    /// <typeparam name="TResult">结果类型</typeparam>
    public interface IHttpRequest<TResult> : IRequest<IHttpRequest<TResult>> where TResult : class
    {
        /// <summary>
        /// 请求成功返回函数
        /// </summary>
        /// <param name="action">执行成功的回调函数，参数为响应结果</param>
        /// <param name="convertAction">将结果字符串转换为指定类型，当默认转换实现无法转换时使用</param>
        /// <returns></returns>
        IHttpRequest<TResult> OnSuccess(Action<TResult> action, Func<string, TResult> convertAction = null);

        /// <summary>
        /// 请求成功返回函数
        /// </summary>
        /// <param name="action">执行成功的回调函数，第一参数为响应结果，第二个参数为状态码</param>
        /// <param name="convertAction">将结果字符串转换为指定类型，当默认转换实现无法转换时使用</param>
        /// <returns></returns>
        IHttpRequest<TResult> OnSuccess(Action<TResult, HttpStatusCode> action,
            Func<string, TResult> convertAction = null);

        /// <summary>
        /// 获取Json结果
        /// </summary>
        /// <returns></returns>
        TResult ResultFromJson();

        /// <summary>
        /// 获取Json结果
        /// </summary>
        /// <returns></returns>
        Task<TResult> ResultFromJsonAsync();
    }
}
