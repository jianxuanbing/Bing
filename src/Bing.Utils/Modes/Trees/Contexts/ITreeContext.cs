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

        ITreeContext<T> SetItems<TKey>(List<T> items, Func<T, string> textSelector, Func<T, TKey> idSelector,
            Func<T, TKey> parentIdSelector, TKey rootId = default(TKey));
    }
}
