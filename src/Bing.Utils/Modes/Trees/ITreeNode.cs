using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Modes.Trees
{
    /// <summary>
    /// 树节点
    /// </summary>
    public interface ITreeNode
    {
        /// <summary>
        /// 标识
        /// </summary>
        object Id { get; set; }

        /// <summary>
        /// 父标识
        /// </summary>
        object ParentId { get; set; }       

        /// <summary>
        /// 文本
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// 级数
        /// </summary>
        int? Level { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        List<ITreeNode> Children { get; set; }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="childNode">子节点</param>
        void Add(ITreeNode childNode);

        /// <summary>
        /// 移除子节点
        /// </summary>
        /// <param name="childNode">子节点</param>
        void Remove(ITreeNode childNode);
    }
}
