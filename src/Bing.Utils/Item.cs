using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils
{
    /// <summary>
    /// 列表项
    /// </summary>
    public class Item : IComparable<Item>
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// 初始化一个<see cref="Item"/>类型的实例
        /// </summary>
        public Item()
        {
        }

        /// <summary>
        /// 初始化一个<see cref="Item"/>类型的实例
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        public Item(string text, string value) : this(text, value, 0)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="Item"/>类型的实例
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="value">值</param>
        /// <param name="sortId">排序号</param>
        public Item(string text, string value, int sortId)
        {
            Text = text;
            Value = value;
            SortId = sortId;
        }

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="other">其他列表项</param>
        /// <returns></returns>
        public int CompareTo(Item other)
        {
            return string.Compare(Text, other.Text, StringComparison.CurrentCulture);
        }
    }
}
