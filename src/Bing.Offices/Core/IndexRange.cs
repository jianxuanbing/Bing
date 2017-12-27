using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Offices.Core
{
    /// <summary>
    /// 索引范围
    /// </summary>
    public class IndexRange
    {
        #region 属性

        /// <summary>
        /// 当前索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 结束索引
        /// </summary>
        public int EndIndex { get; set; }

        /// <summary>
        /// 是否已结束
        /// </summary>
        public bool IsEnd => Index >= EndIndex;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化一个<see cref="IndexRange"/>类型的实例
        /// </summary>
        /// <param name="index">当前索引</param>
        /// <param name="endIndex">结束索引</param>
        public IndexRange(int index, int endIndex)
        {
            Index = index;
            EndIndex = endIndex;
        }

        #endregion

        #region GetIndex(获取索引)

        /// <summary>
        /// 获取索引
        /// </summary>
        /// <param name="span">跨度</param>
        /// <returns></returns>
        public int GetIndex(int span = 1)
        {
            var result = Index;
            Index = Index + span;
            return result;
        }

        #endregion

        #region Contains(是否包含指定索引)

        /// <summary>
        /// 是否包含指定索引
        /// </summary>
        /// <param name="index">索引值</param>
        /// <returns></returns>
        public bool Contains(int index)
        {
            return index >= Index && index <= EndIndex;
        }

        #endregion

        #region Split(分割索引范围)

        /// <summary>
        /// 分割索引范围
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="span">跨度</param>
        /// <returns></returns>
        public IndexRange Split(int index, int span)
        {
            if (index == Index)
            {
                Index = index + span;
                return null;
            }
            var result=new IndexRange(index+span,EndIndex);
            EndIndex = index - 1;
            return result;
        }

        #endregion
    }
}
