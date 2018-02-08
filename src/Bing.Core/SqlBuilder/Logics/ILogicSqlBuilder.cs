using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.SqlBuilder.Conditions;

namespace Bing.SqlBuilder.Logics
{
    /// <summary>
    /// 逻辑Sql语句生成器
    /// </summary>
    public interface ILogicSqlBuilder
    {
        /// <summary>
        /// 获取父节点列表Sql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="order">排序</param>
        /// <param name="idOnly">是否只需要主键</param>
        /// <param name="conditionBuilder">条件生成器</param>
        /// <returns></returns>
        string GetParentsByCode(string tableName, string fieldName, string fieldValue, string order,
            bool idOnly = false, Action<IConditionBuilder> conditionBuilder = null);

        /// <summary>
        /// 获取子节点列表Sql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkFieldName">主键字段名</param>
        /// <param name="pkFieldValue">主键字段值</param>
        /// <param name="parentFieldName">父节点字段名</param>
        /// <param name="order">排序</param>
        /// <param name="idOnly">是否只需要主键</param>
        /// <param name="conditionBuilder">条件生成器</param>
        /// <returns></returns>
        string GetChildrens(string tableName, string pkFieldName, string pkFieldValue,string parentFieldName, string order,
            bool idOnly = false, Action<IConditionBuilder> conditionBuilder = null);

        /// <summary>
        /// 获取子节点列表Sql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkFieldName">主键字段名</param>
        /// <param name="pkFieldValues">主键字段值数组</param>
        /// <param name="parentFieldName">父节点字段名</param>
        /// <param name="order">排序</param>
        /// <param name="idOnly">是否只需要主键</param>
        /// <param name="conditionBuilder">条件生成器</param>
        /// <returns></returns>
        string GetChildrens(string tableName, string pkFieldName, string[] pkFieldValues, string parentFieldName, string order,
            bool idOnly = false, Action<IConditionBuilder> conditionBuilder = null);

        /// <summary>
        /// 获取子节点列表Sql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="order">排序</param>
        /// <param name="idOnly">是否只需要主键</param>
        /// <param name="conditionBuilder">条件生成器</param>
        /// <returns></returns>
        string GetChildrensByCode(string tableName, string fieldName, string fieldValue, string order,
            bool idOnly = false, Action<IConditionBuilder> conditionBuilder = null);

        /// <summary>
        /// 获取父子节点列表Sql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="order">排序</param>
        /// <param name="idOnly">是否只需要主键</param>
        /// <param name="conditionBuilder">条件生成器</param>
        /// <returns></returns>
        string GetParentChildrensByCode(string tableName, string fieldName, string fieldValue, string order,
            bool idOnly = false, Action<IConditionBuilder> conditionBuilder = null);

        /// <summary>
        /// 获取父节点Sql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkFieldName">主键字段名</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="conditionBuilder">条件生成器</param>
        /// <returns></returns>
        string GetParentIdByCode(string tableName, string pkFieldName, string fieldName, string fieldValue,
            Action<IConditionBuilder> conditionBuilder = null);

        /// <summary>
        /// 获取参数字典
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> GetParamDict();
    }
}
