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
    public interface ITreeContext<T>
    {
        /// <summary>
        /// 当前树节点
        /// </summary>
        ITreeNode Current { get; set; }

        /// <summary>
        /// 设置树节点列表数据源
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <param name="collection">数据源</param>
        /// <param name="textSelector">显示文本选择器</param>
        /// <param name="idSelector">ID选择器</param>
        /// <param name="parentIdSelector">父ID选择器</param>
        /// <returns></returns>
        ITreeContext<T> SetItems<TKey>(List<T> collection, Func<T, string> textSelector,
            Func<T, TKey> idSelector, Func<T, TKey> parentIdSelector);

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
        ITreeContext<T> SetItems<TKey, TOther>(List<T> collection, Func<T, string> textSelector,
            Func<T, TKey> idSelector, Func<T, TKey> parentIdSelector, Func<T, TOther> convertSelector);
    }
}
