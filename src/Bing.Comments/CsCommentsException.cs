using System.Xml;

namespace Bing.Comments
{
    /// <summary>
    /// 注释异常
    /// </summary>
    public sealed class CsCommentsException
    {
        /// <summary>
        /// 节点在文档中的索引
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// 节点中的 cref 属性的值
        /// </summary>
        public string Cref { get; private set; }

        /// <summary>
        /// 节点中的内容
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// 初始化一个<see cref="CsCommentsException"/>类型的实例
        /// </summary>
        /// <param name="index">节点在文档中的索引</param>
        /// <param name="node">Xml节点</param>
        internal CsCommentsException(int index, XmlNode node)
        {
            Index = index;
            var attr = node.Attributes["cref"];
            if (attr != null)
            {
                Cref = attr.InnerText;
                Cref = Cref?.Remove(0, 2);
            }

            Text = node.InnerText.Trim();
        }

        /// <summary>
        /// 重写返回字符串。返回 Text 属性或空字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            
            return Text ?? "";
        }
    }
}
