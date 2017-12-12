using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Extensions;

namespace Bing.SqlBuilder.Conditions
{
    /// <summary>
    /// 条件生成器
    /// </summary>
    public class ConditionBuilder:ConditionBuilderBase
    {

        #region 构造函数

        /// <summary>
        /// 初始化一个<see cref="ConditionBuilder"/>类型的实例
        /// </summary>
        /// <param name="isExcludeEmpty">是否排除空或null值</param>
        /// <param name="parameterKey">参数键</param>
        /// <param name="isBuildParameterSql">否生成参数化Sql语句</param>
        public ConditionBuilder(bool isExcludeEmpty = true, string parameterKey = "P", bool isBuildParameterSql = true)
        {
            this.IsExcludeEmpty = isExcludeEmpty;
            this.ParameterKey = parameterKey;
            this.IsBuildParameterSql = isBuildParameterSql;
            this.Length = 0;
            this.ConditionAppendBuilder.Append(" WHERE ");
        }

        #endregion

        #region Append(添加条件)

        /// <summary>
        /// 添加条件
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="relationType">关联运算符</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="operator">操作符</param>
        /// <param name="fieldValue">字段值，注：1、不可谓数组;2、Between时，此字段必须填两个值</param>
        /// <returns></returns>
        public override IConditionBuilder Append<T>(RelationType relationType, string fieldName, SqlOperator @operator, params T[] fieldValue)
        {
            if (IsContinue(fieldValue))
            {
                return this;
            }
            if (!this.ConditionAppendBuilder.ToString().Trim()
                .EndsWith("WHERE", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ConditionAppendBuilder.Append(GetRelation(relationType));
            }
            this.Append(fieldName, @operator, fieldValue);
            return this;
        }

        /// <summary>
        /// 添加条件
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="fieldName">字段名</param>
        /// <param name="operator">操作符</param>
        /// <param name="fieldValue">字段值，注：1、不可谓数组;2、Between时，此字段必须填两个值</param>
        /// <returns></returns>
        public override IConditionBuilder Append<T>(string fieldName, SqlOperator @operator, params T[] fieldValue)
        {
            if (IsContinue(fieldValue))
            {
                return this;
            }
            switch (@operator)
            {
                case SqlOperator.Equal:
                case SqlOperator.NotEqual:
                case SqlOperator.GreaterThan:
                case SqlOperator.GreaterEqual:
                case SqlOperator.LessThan:
                case SqlOperator.LessEqual:
                    this.ConditionAppendBuilder.AppendFormat("{0}{1}{2}", GetFieldName(fieldName),
                        GetOperator(@operator), GetFieldValue(fieldValue[0]));
                    break;
                case SqlOperator.IsNull:
                case SqlOperator.IsNotNull:
                    this.ConditionAppendBuilder.AppendFormat("{0}{1}", GetFieldName(fieldName), GetOperator(@operator));
                    break;
                case SqlOperator.Contains:
                case SqlOperator.NotContains:
                    this.ConditionAppendBuilder.AppendFormat("{0}{1}{2}", GetFieldName(fieldName),
                        GetOperator(@operator), GetFieldValue(string.Format("%{0}%", fieldValue[0])));
                    break;
                case SqlOperator.In:
                case SqlOperator.NotIn:
                    this.ConditionAppendBuilder.AppendFormat("{0}{1}({2})", GetFieldName(fieldName),
                        GetOperator(@operator), GetFieldValue(fieldValue));
                    break;
                case SqlOperator.Starts:
                    this.ConditionAppendBuilder.AppendFormat("{0}{1}{2}", GetFieldName(fieldName),
                        GetOperator(@operator), GetFieldValue(string.Format("{0}%", fieldValue[0])));
                    break;
                case SqlOperator.Ends:
                    this.ConditionAppendBuilder.AppendFormat("{0}{1}{2}", GetFieldName(fieldName),
                        GetOperator(@operator), GetFieldValue(string.Format("%{0}", fieldValue[0])));
                    break;
                case SqlOperator.Between:
                    this.ConditionAppendBuilder.AppendFormat("{0}{1}{2} AND {3}", GetFieldName(fieldName),
                        GetOperator(@operator), GetFieldValue(fieldValue[0]), GetFieldValue(fieldValue[1]));
                    break;
                default:
                    throw new SqlBuilderException("条件未定义!");
            }
            this.Length++;
            return this;
        }

        /// <summary>
        /// 添加Sql语句条件
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns></returns>
        public override IConditionBuilder Append(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                return this;
            }
            this.ConditionAppendBuilder.Append(string.Format(" AND {0}", sql));
            return this;
        }
        #endregion

        #region Block(添加含有括号的条件)

        /// <summary>
        /// 添加含有括号的条件
        /// </summary>
        /// <param name="relationType">关联运算符</param>
        /// <param name="condition">条件生成器</param>
        /// <returns></returns>
        public override IConditionBuilder Block(RelationType relationType, IConditionBuilder condition)
        {
            if (condition == null)
            {
                return this;
            }
            var conditionBuilder = condition as ConditionBuilder;
            if (conditionBuilder == null || conditionBuilder.ConditionAppendBuilder.ToString().Trim().Length < 6)
            {
                return this;
            }
            if (!ConditionAppendBuilder.ToString().Trim()
                .EndsWith("WHERE", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ConditionAppendBuilder.Append(GetRelation(relationType));
            }
            this.ConditionAppendBuilder.AppendFormat(" ({0}) ",
                conditionBuilder.ConditionAppendBuilder.ToString().Remove(0, 6));
            if (conditionBuilder.ParamDictionary.Count > 0)
            {
                this.ParamDictionary.AddRange(conditionBuilder.ParamDictionary, true);
            }
            this.Length++;
            return this;
        }

        #endregion

        /// <summary>
        /// 转化成Sql条件语句（包含Where关键字）
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.ConditionAppendBuilder.Length < 8)
            {
                return string.Empty;
            }
            return this.ConditionAppendBuilder.ToString();
        }
    }
}
