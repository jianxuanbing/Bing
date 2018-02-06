using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Validators
{
    /// <summary>
    /// 验证器，所有验证类均需实现的基础接口定义
    /// </summary>
    /// <typeparam name="T">验证结果类型</typeparam>
    public interface IValidator<out T> where T:ValidationResult,new()
    {
        /// <summary>
        /// 随机生成一个符合规则的内容
        /// </summary>
        /// <returns></returns>
        string GenerateRandomContent();

        /// <summary>
        /// 验证内容是否正确
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        T Validate(string content);
    }
}
