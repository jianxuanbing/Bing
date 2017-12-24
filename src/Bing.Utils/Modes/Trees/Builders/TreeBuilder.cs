using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Modes.Trees.Contexts;

namespace Bing.Utils.Modes.Trees.Builders
{
    /// <summary>
    /// 树 生成器
    /// </summary>
    public class TreeBuilder
    {
        /// <summary>
        /// 生成
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="text">根节点名称</param>
        /// <returns></returns>
        public static ITreeContext<T> Build<T>(string text)
        {
            var root = new TreeNode(text);
            return new TreeContext<T>(root);
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="node">节点</param>
        /// <returns></returns>
        public static ITreeContext<T> Build<T>(ITreeNode node)
        {
            return new TreeContext<T>(node);
        }

        /// <summary>
        /// 生成节点
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="t">实体</param>
        /// <param name="textSelector">文本选择器</param>
        /// <returns></returns>
        internal static ITreeNode BuildNode<T>(T t, Func<T, string> textSelector = null)
        {
            var text = textSelector != null ? textSelector(t) : Convert.ToString(t);
            var node=new TreeNode<T>(text,t);
            return node;
        }
    }
}
