using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Validators
{
    /// <summary>
    /// 验证结果类
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// 验证结果是否通过
        /// </summary>
        public bool IsValid { get; internal set; } = true;

        /// <summary>
        /// 如果验证不通过，这里包含验证失败的原因
        /// </summary>
        public IList<string> Errors { get; internal set; }=new List<string>();

        /// <summary>
        /// 当前验证的内容
        /// </summary>
        public string Content { get; internal set; }

        /// <summary>
        /// 添加错误消息
        /// </summary>
        /// <param name="errorMsg">错误消息</param>
        /// <param name="parameters">参数</param>
        internal void AddErrorMessage(string errorMsg, params object[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
            {
                errorMsg = string.Format(errorMsg, parameters);
            }
            this.Errors.Add(errorMsg);
            this.IsValid = false;
        }
    }
}
