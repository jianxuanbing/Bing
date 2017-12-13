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
        #region Between(添加范围条件过滤)

        /// <summary>
        /// 添加范围条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="beginValue">开始值</param>
        /// <param name="endValue">结束值</param>        
        /// <returns></returns>
        public static IConditionBuilder Between<T>(this IConditionBuilder builder, string fieldName, T beginValue, T endValue)
        {
            return Between<T>(builder, fieldName, beginValue, endValue, true);
        }

        /// <summary>
        /// 添加范围条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="beginValue">开始值</param>
        /// <param name="endValue">结束值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder Between<T>(this IConditionBuilder builder, string fieldName, T beginValue, T endValue, bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (beginValue == null && endValue == null)
            {
                return builder;
            }
            if (beginValue != null && endValue != null)
            {
                builder.Append(RelationType.And, fieldName, SqlOperator.Between, beginValue, endValue);
                return builder;
            }
            if (beginValue != null)
            {
                builder.GreaterEqual(fieldName, beginValue);
                return builder;
            }
            builder.LessEqual(fieldName, endValue);
            return builder;
        }

        #endregion

        #region OrBetween(添加范围条件过滤)

        /// <summary>
        /// 添加范围条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="beginValue">开始值</param>
        /// <param name="endValue">结束值</param>        
        /// <returns></returns>
        public static IConditionBuilder OrBetween<T>(this IConditionBuilder builder, string fieldName, T beginValue, T endValue)
        {
            return OrBetween<T>(builder, fieldName, beginValue, endValue, true);
        }

        /// <summary>
        /// 添加范围条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="beginValue">开始值</param>
        /// <param name="endValue">结束值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrBetween<T>(this IConditionBuilder builder, string fieldName, T beginValue, T endValue, bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (beginValue == null && endValue == null)
            {
                return builder;
            }
            if (beginValue != null && endValue != null)
            {
                builder.Append(RelationType.Or, fieldName, SqlOperator.Between, beginValue, endValue);
                return builder;
            }
            if (beginValue != null)
            {
                builder.OrGreaterEqual(fieldName, beginValue);
                return builder;
            }
            builder.OrLessEqual(fieldName, endValue);
            return builder;
        }

        #endregion

        #region Equal(添加相等条件过滤)

        /// <summary>
        /// 添加相等条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder Equal<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return Equal<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加相等条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder Equal<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.And, fieldName, SqlOperator.Equal, value);
            return builder;
        }

        /// <summary>
        /// 添加相等条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="conditionDict">条件字典</param>
        /// <returns></returns>
        public static IConditionBuilder Equal(this IConditionBuilder builder, Dictionary<string, object> conditionDict)
        {
            builder.Append(SqlOperator.Equal, conditionDict);
            return builder;
        }

        #endregion

        #region OrEqual(添加相等条件过滤)

        /// <summary>
        /// 添加相等条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder OrEqual<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return OrEqual<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加相等条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrEqual<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.Or, fieldName, SqlOperator.Equal, value);
            return builder;
        }

        #endregion

        #region NotEqual(添加不相等条件过滤)

        /// <summary>
        /// 添加不相等条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder NotEqual<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return NotEqual<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加不相等条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder NotEqual<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.And, fieldName, SqlOperator.NotEqual, value);
            return builder;
        }

        #endregion

        #region OrNotEqual(添加不相等条件过滤)

        /// <summary>
        /// 添加不相等条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder OrNotEqual<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return OrNotEqual<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加不相等条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrNotEqual<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.Or, fieldName, SqlOperator.NotEqual, value);
            return builder;
        }

        #endregion

        #region Greater(添加大于条件过滤)

        /// <summary>
        /// 添加大于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder Greater<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return Greater<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加大于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder Greater<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.And, fieldName, SqlOperator.GreaterThan, value);
            return builder;
        }

        #endregion

        #region OrGreater(添加大于条件过滤)

        /// <summary>
        /// 添加大于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder OrGreater<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return OrGreater<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加大于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrGreater<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.Or, fieldName, SqlOperator.GreaterThan, value);
            return builder;
        }

        #endregion

        #region GreaterEqual(添加大于等于条件过滤)

        /// <summary>
        /// 添加大于等于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder GreaterEqual<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return GreaterEqual<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加大于等于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder GreaterEqual<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.And, fieldName, SqlOperator.GreaterEqual, value);
            return builder;
        }

        #endregion

        #region OrGreaterEqual(添加大于等于条件过滤)

        /// <summary>
        /// 添加大于等于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder OrGreaterEqual<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return OrGreaterEqual<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加大于等于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrGreaterEqual<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.Or, fieldName, SqlOperator.GreaterEqual, value);
            return builder;
        }

        #endregion

        #region Less(添加小于条件过滤)

        /// <summary>
        /// 添加小于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder Less<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return Less<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加小于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder Less<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.And, fieldName, SqlOperator.LessThan, value);
            return builder;
        }

        #endregion

        #region OrLess(添加小于条件过滤)

        /// <summary>
        /// 添加小于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder OrLess<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return OrLess<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加小于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrLess<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.Or, fieldName, SqlOperator.LessThan, value);
            return builder;
        }

        #endregion

        #region LessEqual(添加小于等于条件过滤)

        /// <summary>
        /// 添加小于等于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder LessEqual<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return LessEqual<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加小于等于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder LessEqual<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.And, fieldName, SqlOperator.LessEqual, value);
            return builder;
        }

        #endregion

        #region OrLessEqual(添加小于等于条件过滤)

        /// <summary>
        /// 添加小于等于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder OrLessEqual<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return OrLessEqual<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加小于等于条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrLessEqual<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.Or, fieldName, SqlOperator.LessEqual, value);
            return builder;
        }

        #endregion

        #region Contains(添加头尾匹配条件过滤)

        /// <summary>
        /// 添加头尾匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder Contains<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return Contains<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加头尾匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder Contains<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.And, fieldName, SqlOperator.Contains, value);
            return builder;
        }

        #endregion

        #region OrContains(添加头尾匹配条件过滤)

        /// <summary>
        /// 添加头尾匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder OrContains<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return OrContains<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加头尾匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrContains<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.Or, fieldName, SqlOperator.Contains, value);
            return builder;
        }

        #endregion

        #region NotContains(添加头尾非匹配条件过滤)

        /// <summary>
        /// 添加头尾非匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder NotContains<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return NotContains<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加头尾非匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder NotContains<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.And, fieldName, SqlOperator.NotContains, value);
            return builder;
        }

        #endregion

        #region OrNotContains(添加头尾非匹配条件过滤)

        /// <summary>
        /// 添加头尾非匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder OrNotContains<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return OrNotContains<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加头尾非匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrNotContains<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.Or, fieldName, SqlOperator.NotContains, value);
            return builder;
        }

        #endregion

        #region Starts(添加头匹配条件过滤)

        /// <summary>
        /// 添加头匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder Starts<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return Starts<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加头匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder Starts<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.And, fieldName, SqlOperator.Starts, value);
            return builder;
        }

        #endregion

        #region OrStarts(添加头匹配条件过滤)

        /// <summary>
        /// 添加头匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder OrStarts<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return OrStarts<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加头匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrStarts<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.Or, fieldName, SqlOperator.Starts, value);
            return builder;
        }

        #endregion

        #region Ends(添加尾匹配条件过滤)

        /// <summary>
        /// 添加尾匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder Ends<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return Ends<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加尾匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder Ends<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.And, fieldName, SqlOperator.Ends, value);
            return builder;
        }

        #endregion

        #region OrEnds(添加尾匹配条件过滤)

        /// <summary>
        /// 添加尾匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder OrEnds<T>(this IConditionBuilder builder, string fieldName, T value)
        {
            return OrEnds<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加尾匹配条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrEnds<T>(this IConditionBuilder builder, string fieldName, T value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.Or, fieldName, SqlOperator.Ends, value);
            return builder;
        }

        #endregion

        #region In(添加In条件过滤)

        /// <summary>
        /// 添加In条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder In<T>(this IConditionBuilder builder, string fieldName, T[] value)
        {
            return In<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加In条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder In<T>(this IConditionBuilder builder, string fieldName, T[] value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.And, fieldName, SqlOperator.In, value);
            return builder;
        }

        /// <summary>
        /// 添加In条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="childQuery">子查询，例如：select Id from User</param>
        /// <returns></returns>
        public static IConditionBuilder In(this IConditionBuilder builder, string fieldName, string childQuery)
        {
            return In(builder, fieldName, childQuery, true);
        }

        /// <summary>
        /// 添加In条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="childQuery">子查询，例如：select Id from User</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder In(this IConditionBuilder builder, string fieldName, string childQuery,bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (string.IsNullOrWhiteSpace(childQuery))
            {
                return builder;
            }
            builder.AppendRaw(string.Format(" {0} IN ({1})", fieldName, childQuery));
            return builder;
        }
        #endregion

        #region OrIn(添加In条件过滤)

        /// <summary>
        /// 添加In条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder OrIn<T>(this IConditionBuilder builder, string fieldName, T[] value)
        {
            return OrIn<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加In条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrIn<T>(this IConditionBuilder builder, string fieldName, T[] value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.Or, fieldName, SqlOperator.In, value);
            return builder;
        }

        /// <summary>
        /// 添加In条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="childQuery">子查询，例如：select Id from User</param>
        /// <returns></returns>
        public static IConditionBuilder OrIn(this IConditionBuilder builder, string fieldName, string childQuery)
        {
            return OrIn(builder, fieldName, childQuery, true);
        }

        /// <summary>
        /// 添加In条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="childQuery">子查询，例如：select Id from User</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrIn(this IConditionBuilder builder, string fieldName, string childQuery, bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (string.IsNullOrWhiteSpace(childQuery))
            {
                return builder;
            }
            builder.AppendRaw(RelationType.Or, string.Format(" {0} IN ({1})", fieldName, childQuery));
            return builder;
        }
        #endregion

        #region NotIn(添加NotIn条件过滤)

        /// <summary>
        /// 添加NotIn条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder NotIn<T>(this IConditionBuilder builder, string fieldName, T[] value)
        {
            return NotIn<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加NotIn条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder NotIn<T>(this IConditionBuilder builder, string fieldName, T[] value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.And, fieldName, SqlOperator.NotIn, value);
            return builder;
        }

        /// <summary>
        /// 添加NotIn条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="childQuery">子查询，例如：select Id from User</param>
        /// <returns></returns>
        public static IConditionBuilder NotIn(this IConditionBuilder builder, string fieldName, string childQuery)
        {
            return NotIn(builder, fieldName, childQuery, true);
        }

        /// <summary>
        /// 添加NotIn条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="childQuery">子查询，例如：select Id from User</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder NotIn(this IConditionBuilder builder, string fieldName, string childQuery, bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (string.IsNullOrWhiteSpace(childQuery))
            {
                return builder;
            }
            builder.AppendRaw(string.Format(" {0} NOT IN ({1})", fieldName, childQuery));
            return builder;
        }
        #endregion

        #region OrNotIn(添加NotIn条件过滤)

        /// <summary>
        /// 添加NotIn条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public static IConditionBuilder OrNotIn<T>(this IConditionBuilder builder, string fieldName, T[] value)
        {
            return OrNotIn<T>(builder, fieldName, value, true);
        }

        /// <summary>
        /// 添加NotIn条件过滤
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrNotIn<T>(this IConditionBuilder builder, string fieldName, T[] value,
            bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (value == null)
            {
                return builder;
            }
            builder.Append(RelationType.Or, fieldName, SqlOperator.NotIn, value);
            return builder;
        }

        /// <summary>
        /// 添加NotIn条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="childQuery">子查询，例如：select Id from User</param>
        /// <returns></returns>
        public static IConditionBuilder OrNotIn(this IConditionBuilder builder, string fieldName, string childQuery)
        {
            return OrNotIn(builder, fieldName, childQuery, true);
        }

        /// <summary>
        /// 添加NotIn条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="childQuery">子查询，例如：select Id from User</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrNotIn(this IConditionBuilder builder, string fieldName, string childQuery, bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            if (string.IsNullOrWhiteSpace(childQuery))
            {
                return builder;
            }
            builder.AppendRaw(RelationType.Or,string.Format(" {0} NOT IN ({1})", fieldName, childQuery));
            return builder;
        }
        #endregion

        #region IsNull(添加IsNull条件过滤)

        /// <summary>
        /// 添加IsNull条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <returns></returns>
        public static IConditionBuilder IsNull(this IConditionBuilder builder, string fieldName)
        {
            return IsNull(builder, fieldName, true);
        }

        /// <summary>
        /// 添加IsNull条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder IsNull(this IConditionBuilder builder, string fieldName,bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            builder.Append<string>(RelationType.And, fieldName, SqlOperator.IsNull);
            return builder;
        }

        #endregion

        #region OrIsNull(添加IsNull条件过滤)

        /// <summary>
        /// 添加IsNull条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <returns></returns>
        public static IConditionBuilder OrIsNull(this IConditionBuilder builder, string fieldName)
        {
            return OrIsNull(builder, fieldName, true);
        }

        /// <summary>
        /// 添加IsNull条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrIsNull(this IConditionBuilder builder, string fieldName, bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            builder.Append<string>(RelationType.Or, fieldName, SqlOperator.IsNull);
            return builder;
        }

        #endregion

        #region IsNotNull(添加IsNotNull条件过滤)

        /// <summary>
        /// 添加IsNotNull条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <returns></returns>
        public static IConditionBuilder IsNotNull(this IConditionBuilder builder, string fieldName)
        {
            return IsNotNull(builder, fieldName, true);
        }

        /// <summary>
        /// 添加IsNotNull条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder IsNotNull(this IConditionBuilder builder, string fieldName, bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            builder.Append<string>(RelationType.And, fieldName, SqlOperator.IsNotNull);
            return builder;
        }

        #endregion

        #region OrIsNotNull(添加IsNotNull条件过滤)

        /// <summary>
        /// 添加IsNotNull条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <returns></returns>
        public static IConditionBuilder OrIsNotNull(this IConditionBuilder builder, string fieldName)
        {
            return OrIsNotNull(builder, fieldName, true);
        }

        /// <summary>
        /// 添加IsNotNull条件过滤
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrIsNotNull(this IConditionBuilder builder, string fieldName, bool appendCondition)
        {
            if (!appendCondition)
            {
                return builder;
            }
            builder.Append<string>(RelationType.Or, fieldName, SqlOperator.IsNotNull);
            return builder;
        }

        #endregion

        #region Or(Or条件拼接)

        /// <summary>
        /// Or 条件连接
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="condition">条件生成器操作</param>
        /// <returns></returns>
        public static IConditionBuilder Or(this IConditionBuilder builder, Action<IConditionBuilder> condition)
        {
            return Or(builder, condition, true);
        }

        /// <summary>
        /// Or 条件连接诶
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="condition">条件生成器操作</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder Or(this IConditionBuilder builder, Action<IConditionBuilder> condition,
            bool appendCondition)
        {
            if (appendCondition)
            {
                var child = builder.Clone();
                condition.Invoke(child);
                return builder.Block(RelationType.Or, child);
            }
            return builder;
        }

        #endregion

        #region And(And条件拼接)

        /// <summary>
        /// And 条件连接
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="condition">条件生成器</param>
        /// <returns></returns>
        public static IConditionBuilder And(this IConditionBuilder builder,
            Action<IConditionBuilder> condition)
        {
            return And(builder, condition, true);
        }

        /// <summary>
        /// And 条件连接
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="condition">条件生成器</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder And(this IConditionBuilder builder,
            Action<IConditionBuilder> condition, bool appendCondition)
        {
            if (appendCondition)
            {
                var child = builder.Clone();
                condition.Invoke(child);
                return builder.Block(RelationType.And, child);
            }
            return builder;
        }

        #endregion

        #region AppendRaw(添加Sql语句条件)

        /// <summary>
        /// 添加Sql语句条件，允许你写任何不支持上面的方法，所有它会给你最大的灵活性
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder AppendRaw(this IConditionBuilder builder,string sql, bool appendCondition)
        {
            if (appendCondition)
            {
                builder.AppendRaw(sql);
            }
            return builder;
        }

        #endregion

        #region OrAppendRaw(添加Sql语句条件)

        /// <summary>
        /// 添加Sql语句条件，默认Or连接，允许你写任何不支持上面的方法，所有它会给你最大的灵活性
        /// </summary>
        /// <param name="builder">生成器</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="appendCondition">是否拼接条件</param>
        /// <returns></returns>
        public static IConditionBuilder OrAppendRaw(this IConditionBuilder builder, string sql, bool appendCondition)
        {
            if (appendCondition)
            {
                builder.AppendRaw(RelationType.Or, sql);
            }
            return builder;
        }

        #endregion
    }
}
