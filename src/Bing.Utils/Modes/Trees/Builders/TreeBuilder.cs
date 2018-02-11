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
        /// 获取树节点上下文
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TOuter">输出类型</typeparam>
        /// <returns></returns>
        public static ITreeContext<T, TOuter> Build<T, TOuter>()
        {
            return new TreeContext<T, TOuter>();
        }

        /// <summary>
        /// 获取树节点上下文
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TOuter">输出类型</typeparam>
        /// <param name="text">顶级节点显示文本</param>
        /// <returns></returns>
        public static ITreeContext<T, TOuter> Build<T, TOuter>(string text)
        {
            var root=new TreeNode<TOuter>(text);
            return Build<T, TOuter>(root);
        }

        /// <summary>
        /// 获取树节点上下文
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <typeparam name="TOuter">输出类型</typeparam>
        /// <param name="rootNode">顶级节点</param>
        /// <returns></returns>
        public static ITreeContext<T, TOuter> Build<T, TOuter>(TreeNode<TOuter> rootNode)
        {
            return new TreeContext<T, TOuter>(rootNode);
        }

        /// <summary>
        /// 获取树节点上下文 
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public static ITreeContext<T> Build<T>()
        {
            return new TreeContext<T>();
        }

        /// <summary>
        /// 获取树节点上下文
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="text">顶级节点显示文本</param>
        /// <returns></returns>
        public static ITreeContext<T> Build<T>(string text)
        {
            var root = new TreeNode<T>(text);
            return Build<T>(root);
        }

        /// <summary>
        /// 获取树节点上下文
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="rootNode">顶级节点</param>
        /// <returns></returns>
        public static ITreeContext<T> Build<T>(TreeNode<T> rootNode)
        {
            return new TreeContext<T>(rootNode);
        }
    }
}
