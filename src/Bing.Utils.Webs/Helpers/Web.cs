namespace Bing.Utils.Webs.Helpers
{
    /// <summary>
    /// Web操作
    /// </summary>
    public static partial class Web
    {
        #region Client(客户端)
        /// <summary>
        /// Web客户端，用于发送Http请求
        /// </summary>
        /// <returns></returns>
        public static Bing.Utils.Webs.Clients.WebClient Client()
        {
            return new Bing.Utils.Webs.Clients.WebClient();
        }

        /// <summary>
        /// Web客户端，用于发送Http请求
        /// </summary>
        /// <typeparam name="TResult">返回的结果类型</typeparam>
        /// <returns></returns>
        public static Bing.Utils.Webs.Clients.WebClient<TResult> Client<TResult>() where TResult : class
        {
            return new Bing.Utils.Webs.Clients.WebClient<TResult>();
        }

        #endregion
    }
}
