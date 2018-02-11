using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Extensions;
using Bing.Utils.Helpers;
using Bing.Utils.Modes.Trees.Builders;

namespace Bing.Utils.Modes.Trees.Contexts
{
    /// <summary>
    /// 树节点上下文
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <typeparam name="TOuter">输出类型</typeparam>
    public class TreeContext<T,TOuter>:ITreeContext<T,TOuter>
    {
        /// <summary>
        /// 数据源
        /// </summary>
        public List<TreeNode<TOuter>> Data { get; set; }

        /// <summary>
        /// 当前节点
        /// </summary>
        public TreeNode<TOuter> Current { get; set; }

        /// <summary>
        /// 分隔符
        /// </summary>
        public string Separator { get; set; } = ".";

        /// <summary>
        /// 初始化一个<see cref="TreeContext{T,TOuter}"/>类型的实例
        /// </summary>
        public TreeContext():this(null)
        {            
        }

        /// <summary>
        /// 初始化一个<see cref="TreeContext{T,TOuter}"/>类型的实例
        /// </summary>
        /// <param name="node">节点</param>
        public TreeContext(TreeNode<TOuter> node)
        {
            Current = node;
            Data = new List<TreeNode<TOuter>>();
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
        public ITreeContext<T,TOuter> SetItems<TKey>(List<T> collection, Func<T, string> textSelector,
            Func<T, TKey> idSelector, Func<T, TKey> parentIdSelector)
        {
            Data = GenerateTree<TKey>(collection, textSelector, idSelector, parentIdSelector, Current).ToList();
            if (Current != null)
            {
                Current.Children = Data;
            }
            return this;
        }

        /// <summary>
        /// 设置树节点列表数据源
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <param name="collection">数据源</param>
        /// <param name="textSelector">显示文本选择器</param>
        /// <param name="idSelector">ID选择器</param>
        /// <param name="parentIdSelector">父ID选择器</param>
        /// <param name="convertSelector">转换选择器</param>
        /// <returns></returns>
        public ITreeContext<T, TOuter> SetItems<TKey>(List<T> collection, Func<T, string> textSelector,
            Func<T, TKey> idSelector, Func<T, TKey> parentIdSelector, Func<T, TOuter> convertSelector)
        {
            Data = GenerateTree<TKey>(collection, textSelector, idSelector, parentIdSelector, Current, convertSelector).ToList();
            if (Current != null)
            {
                Current.Children = Data;
            }
            return this;
        }

        /// <summary>
        /// 获取当前树节点
        /// </summary>
        /// <returns></returns>
        public TreeNode<TOuter> GetTreeNode()
        {
            return Current;
        }

        /// <summary>
        /// 获取树节点
        /// </summary>
        /// <returns></returns>
        public List<TreeNode<TOuter>> GetTrees()
        {
            return Data;
        }

        /// <summary>
        /// 生成树
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <param name="collection">数据源</param>
        /// <param name="textSelector">显示文本选择器</param>
        /// <param name="idSelector">ID选择器</param>
        /// <param name="parentIdSelector">父ID选择器</param>
        /// <param name="parentNode">父节点</param>
        /// <param name="convertSelector">转换选择器</param>
        /// <returns></returns>
        internal IEnumerable<TreeNode<TOuter>> GenerateTree<TKey>(List<T> collection, Func<T, string> textSelector,
            Func<T, TKey> idSelector, Func<T, TKey> parentIdSelector, TreeNode<TOuter> parentNode,
            Func<T, TOuter> convertSelector=null)
        {
            var level = parentNode == null || parentNode.Level == 0 ? 1 : parentNode.Level + 1;
            foreach (var c in collection.Where(u =>
            {
                var selector = parentIdSelector(u);
                return ((parentNode == null || parentNode.Id == null) && selector == null) ||
                       (parentNode?.Id != null && parentNode.Id.Equals(selector));
            }).ToList())
            {
                TreeNode<TOuter> node = convertSelector == null
                    ? new TreeNode<TOuter>(textSelector(c), Conv.To<TOuter>(c))
                    : new TreeNode<TOuter>(textSelector(c), convertSelector(c));
                node.Id = idSelector(c);
                node.ParentId = parentIdSelector(c);
                node.Level = level;
                node.Path = parentNode == null||parentNode.Level==0
                    ? node.Text
                    : $"{parentNode.Path}{Separator}{node.Text}";
                node.Children = GenerateTree<TKey>(collection, textSelector, idSelector, parentIdSelector, node,
                    convertSelector).ToList();
                yield return node;
            }
        }
    }

    /// <summary>
    /// 树节点上下文
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class TreeContext<T> : TreeContext<T, T>, ITreeContext<T>
    {
        /// <summary>
        /// 初始化一个<see cref="TreeContext{T}"/>类型的实例
        /// </summary>

        public TreeContext():base()
        {
            
        }

        /// <summary>
        /// 初始化一个<see cref="TreeContext{T}"/>类型的实例
        /// </summary>
        /// <param name="rootNode">根节点</param>
        public TreeContext(TreeNode<T> rootNode):base(rootNode)
        {
        }
    }
}
