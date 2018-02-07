using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bing.Comments
{
    /// <summary>
    /// 用于描述注释中的 param 或者 typeparam 节点
    /// </summary>
    public sealed class CsCommentsParam
    {
        /// <summary>
        /// 节点中的 name 属性的值
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 节点中的内容
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// 空对象
        /// </summary>
        public static readonly CsCommentsParam Empty=new CsCommentsParam();

        /// <summary>
        /// 初始化一个<see cref="CsCommentsParam"/>类型的实例
        /// </summary>
        private CsCommentsParam() { }

        /// <summary>
        /// 初始化一个<see cref="CsCommentsParam"/>类型的实例
        /// </summary>
        /// <param name="node">Xml节点</param>
        internal CsCommentsParam(XmlNode node)
        {
            var attr = node.Attributes["name"];
            if (attr != null)
            {
                Name = attr.InnerText.Trim();
            }

            Text = node.InnerText;
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
