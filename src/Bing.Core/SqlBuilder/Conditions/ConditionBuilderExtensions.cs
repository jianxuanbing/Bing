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

    }
}
