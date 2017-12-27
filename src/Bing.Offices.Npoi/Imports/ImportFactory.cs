using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Offices.Imports;

namespace Bing.Offices.Npoi.Imports
{
    public class ImportFactory:IImportFactory
    {
        /// <summary>
        /// 文件绝对路径
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// 工作表名称
        /// </summary>
        private readonly string _sheetName;

        public ImportFactory(string path, string sheetName = "")
        {
            _path = path;
            _sheetName = sheetName;
        }

        public IImport Create(ImportFormat format)
        {
            switch (format)
            {
                case ImportFormat.Xlsx:
                    return CreateExcel2007Import(_path, _sheetName);
                case ImportFormat.Xls:
                    return CreateExcel2003Import(_path, _sheetName);
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// 创建Npoi Excel2003 导入器
        /// </summary>
        /// <param name="path">文件的绝对路径</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns></returns>
        public static IImport CreateExcel2003Import(string path, string sheetName = "")
        {
            return new Excel2003Import(path, sheetName);
        }
        /// <summary>
        /// 创建Npoi Excel2007 导入器
        /// </summary>
        /// <param name="path">文件的绝对路径</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns></returns>
        public static IImport CreateExcel2007Import(string path, string sheetName = "")
        {
            return new Excel2007Import(path, sheetName);
        }
    }
}
