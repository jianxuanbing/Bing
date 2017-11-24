using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using Bing.Aspects.Base;
using Bing.Utils.Helpers;

namespace Bing.Validations.Aspects
{
    /// <summary>
    /// 验证拦截器
    /// </summary>
    public class ValidAttribute : ParameterInterceptorBase
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            Validate(context.Parameter);
            await next(context);
        }

        /// <summary>
        /// 验证
        /// </summary>
        private void Validate(Parameter parameter)
        {
            if (Reflection.IsGenericCollection(parameter.RawType))
            {
                ValidateCollection(parameter);
                return;
            }
            IValidation validation = parameter.Value as IValidation;
            validation?.Validate();
        }

        /// <summary>
        /// 验证集合
        /// </summary>
        private void ValidateCollection(Parameter parameter)
        {
            var validations = parameter.Value as IEnumerable<IValidation>;
            if (validations == null)
            {
                return;
            }
            foreach (var validation in validations)
            {
                validation.Validate();
            }
        }
    }
}
