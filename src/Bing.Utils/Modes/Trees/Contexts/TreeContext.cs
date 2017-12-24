using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Extensions;
using Bing.Utils.Modes.Trees.Builders;

namespace Bing.Utils.Modes.Trees.Contexts
{
    /// <summary>
    /// 树节点上下文
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class TreeContext<T>:ITreeContext<T>
    {
        /// <summary>
        /// 当前树节点
        /// </summary>
        public ITreeNode Current { get; set; }

        /// <summary>
        /// 初始化一个<see cref="TreeContext{T}"/>类型的实例
        /// </summary>
        /// <param name="treeNode">树节点</param>
        public TreeContext(ITreeNode treeNode)
        {
            this.Current = treeNode;
        }

        public ITreeContext<T> SetItems<TKey>(List<T> items, Func<T,string> textSelector, Func<T, TKey> idSelector, Func<T, TKey> parentIdSelector, TKey rootId = default(TKey))
        {
            var result = GenerateTree(items, textSelector, idSelector, parentIdSelector, rootId,Current.Level).ToList();            
            Current.Children = result;
            return this;
        }

        /// <summary>
        /// 生成树
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="collection"></param>
        /// <param name="textSelector"></param>
        /// <param name="idSelector"></param>
        /// <param name="parentIdSelector"></param>
        /// <param name="rootId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        internal IEnumerable<ITreeNode> GenerateTree<TKey>(List<T> collection, Func<T, string> textSelector,
            Func<T, TKey> idSelector, Func<T, TKey> parentIdSelector, TKey rootId = default(TKey),
            int? level = default(int))
        {
            level += 1;
            foreach (var c in collection.Where(u =>
            {
                var selector = parentIdSelector(u);
                return (rootId == null && selector == null) || (rootId != null && rootId.Equals(selector));
            }).ToList())
            {
                var node = new TreeNode<T>(textSelector(c), c);
                node.Id = idSelector(c);
                node.ParentId = parentIdSelector(c);
                node.Level = level;
                node.Children = GenerateTree<TKey>(collection, textSelector, idSelector, parentIdSelector,
                    idSelector(c), level).ToList();
                yield return node;
            }
        }
    }
}
