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

        /// <summary>
        /// 设置树节点列表数据源
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <param name="collection">数据源</param>
        /// <param name="textSelector">显示文本选择器</param>
        /// <param name="idSelector">ID选择器</param>
        /// <param name="parentIdSelector">父ID选择器</param>
        /// <returns></returns>
        public ITreeContext<T> SetItems<TKey>(List<T> collection, Func<T, string> textSelector,
            Func<T, TKey> idSelector, Func<T, TKey> parentIdSelector)
        {
            var result = GenerateTree<TKey,T>(collection, textSelector, idSelector, parentIdSelector, Current).ToList();
            Current.Children = result;
            return this;
        }

        /// <summary>
        /// 设置树节点列表数据源
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TOther">输出类型</typeparam>
        /// <param name="collection">数据源</param>
        /// <param name="textSelector">显示文本选择器</param>
        /// <param name="idSelector">ID选择器</param>
        /// <param name="parentIdSelector">父ID选择器</param>
        /// <param name="convertSelector">转换选择器</param>
        /// <returns></returns>
        public ITreeContext<T> SetItems<TKey,TOther>(List<T> collection, Func<T, string> textSelector,
            Func<T, TKey> idSelector, Func<T, TKey> parentIdSelector, Func<T, TOther> convertSelector)
        {
            var result = GenerateTree(collection, textSelector, idSelector, parentIdSelector, Current, convertSelector).ToList();
            Current.Children = result;
            return this;
        }

        /// <summary>
        /// 生成树
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TOther">输出类型</typeparam>
        /// <param name="collection">数据源</param>
        /// <param name="textSelector">显示文本选择器</param>
        /// <param name="idSelector">ID选择器</param>
        /// <param name="parentIdSelector">父ID选择器</param>
        /// <param name="parentNode">父节点</param>
        /// <param name="convertSelector">转换选择器</param>
        /// <returns></returns>
        internal IEnumerable<ITreeNode> GenerateTree<TKey, TOther>(List<T> collection, Func<T, string> textSelector,
            Func<T, TKey> idSelector, Func<T, TKey> parentIdSelector, ITreeNode parentNode,
            Func<T, TOther> convertSelector=null)
        {
            var level = (parentNode.Level ?? 0) + 1;
            foreach (var c in collection.Where(u =>
            {
                var selector = parentIdSelector(u);
                return (parentNode.Id == null && selector == null) || (parentNode.Id != null && parentNode.Id.Equals(selector));
            }).ToList())
            {
                var node = convertSelector == null
                    ? (ITreeNode) new TreeNode<T>(textSelector(c), c)
                    : new TreeNode<TOther>(textSelector(c), convertSelector(c));

                node.Id = idSelector(c);
                node.ParentId = parentIdSelector(c);
                node.Level = level;
                node.Path = parentNode.Level == 0 ? node.Text : string.Format("{0}.{1}", parentNode.Path, node.Text);
                node.Children = GenerateTree<TKey, TOther>(collection, textSelector, idSelector, parentIdSelector, node,
                    convertSelector).ToList();
                yield return node;
            }
        }
    }
}
