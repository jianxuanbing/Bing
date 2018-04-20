using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.SqlBuilder.Conditions;
using Bing.Utils.Json;

namespace Bing.SqlBuilder.Logics
{
    /// <summary>
    /// 逻辑Sql语句生成器
    /// </summary>
    public class LogicSqlBuilder:ILogicSqlBuilder
    {
        /// <summary>
        /// 当前参数字典
        /// </summary>
        private Dictionary<string,object> _currentParamDict=new Dictionary<string, object>();

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
        public string GetParentsByCode(string tableName, string fieldName, string fieldValue, string order, bool idOnly = false,
            Action<IConditionBuilder> conditionBuilder = null)
        {
            StringBuilder sb=new StringBuilder();
            if (idOnly)
            {
                sb.AppendFormat("select {0} ", SqlBusinessLogicOptions.FieldId);
            }
            else
            {
                sb.Append("select * ");
            }
            sb.AppendFormat(" from {0} ", tableName);

            ConditionBuilder builder=new ConditionBuilder();
            builder.AppendRawParam("(LEFT({0}, LEN({1})) = {1}) ", builder.AddParameter(fieldName, fieldValue), fieldName);
            conditionBuilder?.Invoke(builder);

            sb.Append(builder.ToString());

            _currentParamDict = builder.GetParamDict();

            sb.AppendFormat(" order by {0}", order);

            return sb.ToString();
        }

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
        public string GetChildrens(string tableName, string pkFieldName, string pkFieldValue, string parentFieldName, string order,
            bool idOnly = false, Action<IConditionBuilder> conditionBuilder = null)
        {
            return GetChildrens(tableName, pkFieldName, new string[] {pkFieldValue}, parentFieldName, order, idOnly,
                conditionBuilder);
        }

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
        public string GetChildrens(string tableName, string pkFieldName, string[] pkFieldValues, string parentFieldName, string order,
            bool idOnly = false, Action<IConditionBuilder> conditionBuilder = null)
        {
            StringBuilder sb = new StringBuilder();

            ConditionBuilder builder = new ConditionBuilder();
            builder.In(pkFieldName, pkFieldValues);
            conditionBuilder?.Invoke(builder);

            sb.AppendFormat(
                @"with Tree as (select {0} from {1} {2} union all select ResourceTree.{0} from {1} as ResourceTree inner join Tree as A on A.{3} = ResourceTree.{4}) select {0} from Tree",
                idOnly ? pkFieldName : "*", tableName, builder.ToString(), pkFieldName, parentFieldName);

            _currentParamDict = builder.GetParamDict();

            sb.AppendFormat(" order by {0} ", order);

            return sb.ToString();
        }

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
        public string GetChildrensByCode(string tableName, string fieldName, string fieldValue, string order, bool idOnly = false,
            Action<IConditionBuilder> conditionBuilder = null)
        {
            throw new NotImplementedException();
        }

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
        public string GetParentChildrensByCode(string tableName, string fieldName, string fieldValue, string order,
            bool idOnly = false, Action<IConditionBuilder> conditionBuilder = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取父节点Sql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkFieldName">主键字段名</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="conditionBuilder">条件生成器</param>
        /// <returns></returns>
        public string GetParentIdByCode(string tableName, string pkFieldName, string fieldName, string fieldValue,
            Action<IConditionBuilder> conditionBuilder = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取参数字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetParamDict()
        {
            return _currentParamDict;
        }
    }
}
