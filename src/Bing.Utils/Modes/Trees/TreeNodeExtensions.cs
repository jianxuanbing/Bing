using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Modes.Trees
{
    /// <summary>
    /// 树节点扩展
    /// </summary>
    public static class TreeNodeExtensions
    {
        /// <summary>
        /// 获取当前节点的所有子节点
        /// </summary>
        /// <param name="treeNode">当前节点</param>
        /// <returns></returns>
        public static IEnumerable<TreeNode> GetLeafNodes(this TreeNode treeNode)
        {
            foreach (TreeNode child in treeNode.Children)
            {
                if (child.Children.Any())
                {
                    foreach (var descendant in GetLeafNodes(child))
                    {
                        yield return descendant;
                    }
                }
                else
                {
                    yield return child;
                }
            }
        }
    }
}
