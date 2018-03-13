//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using EntityFramework.Mapping;

//namespace Bing.Datas.EntityFramework
//{
//    /// <summary>
//    /// 数据操作
//    /// </summary>
//    public static class Helper
//    {
//        public static DataTable ToDataTable<T>(List<T> entities, EntityMap map)
//        {

//        }


//        private static DataTable CreateTable<T>(EntityMap map)
//        {

//        }

//        private static void AddColumn(DataTable table, Type type, PropertyMap propertyMap)
//        {

//        }

//        private static void AddColumn(DataTable table, Type propertyType, string columnName)
//        {

//        }

//        private static void FillData<T>(DataTable table, IEnumerable<T> entities, EntityMap map)
//        {
//            foreach (var entity in entities)
//            {
                
//            }
//        }

//        private static DataRow CreateRow<T>(DataTable table, T entity, EntityMap map)
//        {

//        }

//        private static void AddRow<T>(DataRow row, Type type, T entity, PropertyMap propertyMap)
//        {

//        }

//        private static void AddComplexRow(DataRow row, object complex, PropertyMap propertyMap)
//        {
//            foreach (var VARIABLE in propertyMap.GetCh)
//            {
                
//            }
//        }

//        /// <summary>
//        /// 添加行
//        /// </summary>
//        /// <param name="row"></param>
//        /// <param name="columnName"></param>
//        /// <param name="value"></param>
//        private static void AddRow(DataRow row, string columnName, object value)
//        {
//            row[columnName] = value ?? DBNull.Value;
//        }
//    }
//}
