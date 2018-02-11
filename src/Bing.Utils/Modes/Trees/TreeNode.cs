using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Modes.Trees
{
    /// <summary>
    /// 泛型树节点
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class TreeNode<T>
    {
        /// <summary>
        /// 标识
        /// </summary>
        public object Id { get; set; }

        /// <summary>
        /// 父标识
        /// </summary>
        public object ParentId { get; set; }
        
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 级数
        /// </summary>
        public int? Level { get; set; }

        /// <summary>
        /// 子树节点集合
        /// </summary>
        public List<TreeNode<T>> Children { get; set; }

        /// <summary>
        /// 初始化一个<see cref="TreeNode{T}"/>类型的实例
        /// </summary>
        public TreeNode()
        {
            
        }

        /// <summary>
        /// 初始化一个<see cref="TreeNode{T}"/>类型的实例
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        public TreeNode(string text, T value = default(T))
        {
            this.Text = text;
            this.Value = value;
            this.Level = 0;
            this.Children=new List<TreeNode<T>>();
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="childNode">子节点</param>
        public void Add(TreeNode<T> childNode)
        {
            Children.Add(childNode);
            childNode.Level += 1;
        }        

        /// <summary>
        /// 移除子节点
        /// </summary>
        /// <param name="childNode">子节点</param>
        public void Remove(TreeNode<T> childNode)
        {
            Children.Remove(childNode);
        }

        /// <summary>
        /// 返回树节点为文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }
    }

    /// <summary>
    /// 树节点
    /// </summary>
    public class TreeNode: TreeNode<object>
    {
        /// <summary>
        /// 初始化一个<see cref="TreeNode{T}"/>类型的实例
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        public TreeNode(string text,object value=null):base(text,value)
        {            
        }        
    }
}
