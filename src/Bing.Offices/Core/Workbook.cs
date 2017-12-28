using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Offices.Abstractions;
using Bing.Offices.Internal;

namespace Bing.Offices.Core
{
    /// <summary>
    /// 工作簿
    /// </summary>
    public class Workbook:IExcelWorkbook
    {
        /// <summary>
        /// 工作表名称最大长度
        /// </summary>
        private static int MaxSensitveSheetNameLen = 31;

        /// <summary>
        /// 工作表列表
        /// </summary>
        public List<IExcelSheet> Sheets { get; set; }

        /// <summary>
        /// 获取工作表
        /// </summary>
        /// <param name="name">工作表名称</param>
        /// <returns></returns>
        public IExcelSheet GetSheet(string name)
        {
            foreach (var sheet in Sheets)
            {
                if (name.Equals(sheet.SheetName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return sheet;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取工作表
        /// </summary>
        /// <param name="index">工作表索引</param>
        /// <returns></returns>
        public IExcelSheet GetSheetAt(int index)
        {
            ValidateSheetIndex(index);
            return Sheets[index];
        }

        /// <summary>
        /// 创建工作表
        /// </summary>
        /// <returns></returns>
        public IExcelSheet CreateSheet()
        {
            string sheetName = "Sheet" + (Sheets.Count);
            int idx = 0;
            while (GetSheet(sheetName)!=null)
            {
                sheetName = "Sheet" + idx;
                idx++;
            }
            return CreateSheet(sheetName);
        }

        /// <summary>
        /// 创建工作表
        /// </summary>
        /// <param name="name">工作表名</param>
        /// <returns></returns>
        public IExcelSheet CreateSheet(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("sheetName must not null");
            }

            if (ContainsSheet(name, Sheets.Count))
            {
                throw new ArgumentException("The workbook already contains a sheet of this name");
            }

            if (name.Length > 31)
            {
                name = name.Substring(0, MaxSensitveSheetNameLen);
            }
            WorkbookUtil.ValidateSheetName(name);

            IExcelSheet sheet=new Table();
            sheet.SheetName = name;
            Sheets.Add(sheet);
            return sheet;
        }

        /// <summary>
        /// 验证工作表索引
        /// </summary>
        /// <param name="index">索引</param>
        private void ValidateSheetIndex(int index)
        {
            int lastSheetIndex = Sheets.Count - 1;
            if (index < 0 || index > lastSheetIndex)
            {
                throw new ArgumentException(string.Format("Sheet index ({0}) is out of range (0..{1})",index,lastSheetIndex));                
            }
        }

        /// <summary>
        /// 是否包含工作表
        /// </summary>
        /// <param name="name">工作表名</param>
        /// <param name="excludeSheetIdx">忽略索引</param>
        /// <returns></returns>
        private bool ContainsSheet(string name, int excludeSheetIdx)
        {
            if (name.Length > MaxSensitveSheetNameLen)
            {
                name = name.Substring(0, MaxSensitveSheetNameLen);
            }

            for (int i = 0; i < Sheets.Count; i++)
            {
                string sheetName = Sheets[i].SheetName;
                if (sheetName.Length > MaxSensitveSheetNameLen)
                {
                    sheetName = sheetName.Substring(0, MaxSensitveSheetNameLen);
                }

                if (excludeSheetIdx != i && name.Equals(sheetName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
