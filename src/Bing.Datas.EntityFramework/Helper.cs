using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Mapping;

namespace Bing.Datas.EntityFramework
{
    /// <summary>
    /// 数据操作
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// 将实体集合转换为DataTable
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体集合</param>
        /// <param name="map">实体映射配置</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(List<T> entities, EntityMap map)
        {
            var result = CreateTable<T>(map);
            FillData(result,entities,map);
            return result;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="map">实体映射配置</param>
        /// <returns></returns>
        private static DataTable CreateTable<T>(EntityMap map)
        {
            var result=new DataTable();
            var type = typeof(T);
            foreach (var each in map.PropertyMaps)
            {
                AddColumn(result,type,each);
            }

            return result;
        }

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="type">类型</param>
        /// <param name="propertyMap">属性映射配置</param>
        private static void AddColumn(DataTable table, Type type, PropertyMap propertyMap)
        {
            var propertyType = type.GetProperty(propertyMap.PropertyName)?.PropertyType;
            AddColumn(table,propertyType,propertyMap.ColumnName);
        }

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="propertyType">属性类型</param>
        /// <param name="columnName">列名</param>
        private static void AddColumn(DataTable table, Type propertyType, string columnName)
        {
            if ((propertyType.IsGenericType) && (propertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                propertyType = propertyType.GetGenericArguments()[0];
            }

            table.Columns.Add(columnName, propertyType);
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据表</param>
        /// <param name="entities">实体集合</param>
        /// <param name="map">实体映射配置</param>
        private static void FillData<T>(DataTable table, IEnumerable<T> entities, EntityMap map)
        {
            foreach (var entity in entities)
            {
                var row = CreateRow(table, entity, map);
                if (row != null)
                {
                    table.Rows.Add(row);
                }
            }
        }

        /// <summary>
        /// 创建行
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据表</param>
        /// <param name="entity">实体</param>
        /// <param name="map">实体映射配置</param>
        /// <returns></returns>
        private static DataRow CreateRow<T>(DataTable table, T entity, EntityMap map)
        {
            DataRow row = table.NewRow();
            var type = typeof(T);
            foreach (var propertyMap in map.PropertyMaps)
            {
                AddRow(row,type,entity,propertyMap);
            }

            return row;
        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="row">数据行</param>
        /// <param name="type">类型</param>
        /// <param name="entity">实体</param>
        /// <param name="propertyMap">属性映射配置</param>
        private static void AddRow<T>(DataRow row, Type type, T entity, PropertyMap propertyMap)
        {
            AddRow(row,propertyMap.ColumnName,type.GetProperty(propertyMap.PropertyName)?.GetValue(entity));
        }

        /// <summary>
        /// 添加复杂属性
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="complex">复杂类型对象</param>
        /// <param name="propertyMap">属性映射</param>
        private static void AddComplexRow(DataRow row, object complex, PropertyMap propertyMap)
        {            
        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="columnName">列名</param>
        /// <param name="value">值</param>
        private static void AddRow(DataRow row, string columnName, object value)
        {
            row[columnName] = value ?? DBNull.Value;
        }
    }
}
