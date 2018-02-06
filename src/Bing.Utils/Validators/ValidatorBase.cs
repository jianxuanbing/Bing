using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bing.Utils.Validators
{
    /// <summary>
    /// 基础验证类，默认包含了空验证和正则验证，提供默认的错误信息描述
    /// </summary>
    /// <typeparam name="T">验证结果类型</typeparam>
    public abstract class ValidatorBase<T>:IValidator<T> where T:ValidationResult,new()
    {
        /// <summary>
        /// 用于判断输入内容是否正确的正则表达式，如果不设定，则不进行正则匹配
        /// </summary>
        protected virtual string RegexPattern { get; }

        /// <summary>
        /// 内容为空时的错误信息
        /// </summary>
        protected virtual string EmptyErrorMessage { get; } = "内容不可为空";

        /// <summary>
        /// 正则匹配失败时的错误信息
        /// </summary>
        protected virtual string RegexMatchFailMessage { get; } = "内容错误";

        /// <summary>
        /// 生成随机内容
        /// </summary>
        /// <returns></returns>
        public abstract string GenerateRandomContent();

        /// <summary>
        /// 验证内容是否正确
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public virtual T Validate(string content)
        {
            T result=new T()
            {
                Content = content
            };
            if (string.IsNullOrWhiteSpace(content))
            {
                result.AddErrorMessage(EmptyErrorMessage);
            }
            ValidWithPattern(content, result);
            return result;
        }

        /// <summary>
        /// 按正则验证内容是否正确
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="result">验证结果</param>
        /// <returns></returns>
        protected virtual bool ValidWithPattern(string content, T result)
        {
            bool valid = string.IsNullOrEmpty(this.RegexPattern) || Regex.IsMatch(content, this.RegexPattern);
            if (!valid)
            {
                result.AddErrorMessage(this.RegexMatchFailMessage);
            }
            return valid;
        }
    }
}
