using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.SqlBuilder.Conditions
{
    /// <summary>
    /// 条件生成器 扩展
    /// </summary>
    public static class ConditionBuilderExtensions
    {
        /// <summary>
        /// 添加范围条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="conditionBuilder">条件生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="beginValue">开始值</param>
        /// <param name="endValue">结束值</param>        
        /// <returns></returns>
        public static IConditionBuilder Between<T>(this IConditionBuilder conditionBuilder, string fieldName, T beginValue, T endValue)
        {
            
            return Between<T>(conditionBuilder, fieldName, beginValue, endValue, true);
        }

        /// <summary>
        /// 添加范围条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="conditionBuilder">条件生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="beginValue">开始值</param>
        /// <param name="endValue">结束值</param>
        /// <param name="condition">拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder Between<T>(this IConditionBuilder conditionBuilder,string fieldName, T beginValue, T endValue, bool condition)
        {
            if (beginValue == null && endValue == null)
            {
                return conditionBuilder;
            }
            conditionBuilder.Append(RelationType.And, fieldName, SqlOperator.Between, beginValue, endValue);
            return conditionBuilder;
        }
    }
}
