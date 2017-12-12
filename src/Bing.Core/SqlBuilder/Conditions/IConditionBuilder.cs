using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.SqlBuilder.Conditions
{
    /// <summary>
    /// 条件生成器
    /// </summary>
    public interface IConditionBuilder
    {
        /// <summary>
        /// 清空条件
        /// </summary>
        /// <returns></returns>
        IConditionBuilder Clear();

        /// <summary>
        /// 添加条件
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="relationType">关联运算符</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="operator">操作符</param>
        /// <param name="fieldValue">字段值，注：1、不可谓数组;2、Between时，此字段必须填两个值</param>
        /// <returns></returns>
        IConditionBuilder Append<T>(RelationType relationType, string fieldName, SqlOperator @operator,
            params T[] fieldValue);

        /// <summary>
        /// 添加条件
        /// </summary>
        /// <typeparam name="T">字段值类型</typeparam>
        /// <param name="fieldName">字段名</param>
        /// <param name="operator">操作符</param>
        /// <param name="fieldValue">字段值，注：1、不可谓数组;2、Between时，此字段必须填两个值</param>
        /// <returns></returns>
        IConditionBuilder Append<T>(string fieldName, SqlOperator @operator, params T[] fieldValue);

        /// <summary>
        /// 添加Sql语句条件
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns></returns>
        IConditionBuilder Append(string sql);

        /// <summary>
        /// 添加含有括号的条件
        /// </summary>
        /// <param name="relationType">关联运算符</param>
        /// <param name="condition">条件生成器</param>
        /// <returns></returns>
        IConditionBuilder Block(RelationType relationType, IConditionBuilder condition);

    }
}
