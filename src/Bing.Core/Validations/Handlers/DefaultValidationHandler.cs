using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Validations.Handlers
{
    /// <summary>
    /// 默认验证处理器，直接抛出异常
    /// </summary>
    public class DefaultValidationHandler : IValidationHandler
    {
        /// <summary>
        /// 处理验证错误
        /// </summary>
        /// <param name="results">验证结果集合</param>
        public void Handle(ValidationResultCollection results)
        {
            if (results.IsValid)
            {
                return;
            }
            throw new ValidationException(results.First().ErrorMessage);
        }
    }
}
