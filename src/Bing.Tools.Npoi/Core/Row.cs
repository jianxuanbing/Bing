using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Tools.Npoi.Core
{
    /// <summary>
    /// 单元行
    /// </summary>
    public class Row
    {
        #region 字段

        /// <summary>
        /// 索引管理器
        /// </summary>
        private readonly IndexManager _indexManager;

        #endregion

        #region 属性

        /// <summary>
        /// 单元格列表
        /// </summary>
        public List<Cell> Cells { get; set; }
       
        /// <summary>
        /// 单元格
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public Cell this[int index] => Cells[index];

        /// <summary>
        /// 行索引
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        /// 列数
        /// </summary>
        public int ColumnNumber => Cells.Sum(x => x.ColumnSpan);


        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化一个<see cref="Row"/>类型的实例
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        public Row(int rowIndex)
        {
            Cells=new List<Cell>();
            RowIndex = rowIndex;
            _indexManager=new IndexManager();
        }

        #endregion

        /// <summary>
        /// 清空内容
        /// </summary>
        /// <returns></returns>
        public Row ClearContent()
        {
            foreach (var cell in Cells)
            {
                cell.SetValue(string.Empty);
            }
            return this;
        }
    }
}
