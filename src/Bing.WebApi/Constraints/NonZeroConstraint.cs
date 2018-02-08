using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace Bing.WebApi.Constraints
{
    /// <summary>
    /// 非零约束
    /// </summary>
    public class NonZeroConstraint:IHttpRouteConstraint
    {
        /// <summary>
        /// 确定此实例是否等于指定的路由。
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="route">要比较的路由</param>
        /// <param name="parameterName">参数名</param>
        /// <param name="values">参数值的列表</param>
        /// <param name="routeDirection">路由方向</param>
        /// <returns>如果此实例等于指定的路由，则为 True；否则为 false。</returns>
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                long longValue;
                if (value is long)
                {
                    longValue = (long) value;
                    return longValue != 0;
                }

                string valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
                if (long.TryParse(valueString, NumberStyles.Integer, CultureInfo.InvariantCulture, out longValue))
                {
                    return longValue != 0;
                }
            }

            return false;
        }
    }
}
