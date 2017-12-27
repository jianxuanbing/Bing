using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Modes.Trees.Contexts
{
    /// <summary>
    /// 树节点上下文
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <typeparam name="TOuter">输出实体类型</typeparam>
    public interface ITreeContext<T,TOuter>
    {
        /// <summary>
        /// 数据源
        /// </summary>
        List<ITreeNode<TOuter>> Data { get; set; }

        /// <summary>
        /// 当前节点
        /// </summary>
        ITreeNode<TOuter> Current { get; set; }

        /// <summary>
        /// 分隔符
        /// </summary>
        string Separator { get; set; }

        /// <summary>
        /// 设置树节点列表数据源
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <param name="collection">数据源</param>
        /// <param name="textSelector">显示文本选择器</param>
        /// <param name="idSelector">ID选择器</param>
        /// <param name="parentIdSelector">父ID选择器</param>
        /// <returns></returns>
        ITreeContext<T, TOuter> SetItems<TKey>(List<T> collection, Func<T, string> textSelector,
            Func<T, TKey> idSelector, Func<T, TKey> parentIdSelector);

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
        ITreeContext<T,TOuter> SetItems<TKey>(List<T> collection, Func<T, string> textSelector,
            Func<T, TKey> idSelector, Func<T, TKey> parentIdSelector, Func<T, TOuter> convertSelector);

        /// <summary>
        /// 获取当前树节点
        /// </summary>
        /// <returns></returns>
        ITreeNode<TOuter> GetTreeNode();

        /// <summary>
        /// 获取树节点
        /// </summary>
        /// <returns></returns>
        List<ITreeNode<TOuter>> GetTrees();
    }

    /// <summary>
    /// 树节点上下文
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public interface ITreeContext<T> : ITreeContext<T, T>
    {        
    }
}
