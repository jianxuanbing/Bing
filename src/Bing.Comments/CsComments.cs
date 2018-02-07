using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bing.Comments
{
    /// <summary>
    /// 用于描述一个成员的注释信息
    /// </summary>
    public sealed class CsComments
    {
        /// <summary>
        /// 文档注释中的 summary 或 value 节点，用于表示注释的主体说明
        /// </summary>
        public string Summary { get; private set; }

        /// <summary>
        /// 文档注释中的 remarks 节点，用于表示备注信息
        /// </summary>
        public string Remarks { get; private set; }

        /// <summary>
        /// 文档注释中的 returns 节点，用于表示返回值信息
        /// </summary>
        public string Returns { get; private set; }

        /// <summary>
        /// 文档注释中的 param 节点，用于表示参数信息
        /// </summary>
        public CsCommentsParamCollection Param { get; private set; }

        /// <summary>
        /// 文档注释中的 typeparam 节点，用于表示泛型参数信息
        /// </summary>
        public CsCommentsParamCollection TypeParam { get; private set; }

        /// <summary>
        /// 文档注释中的 exception 节点
        /// </summary>
        public CsCommentsException[] Exception { get; private set; }
        
        /// <summary>
        /// 空注释
        /// </summary>
        public static readonly CsComments Empty=new CsComments();

        /// <summary>
        /// 初始化一个<see cref="CsComments"/>类型的实例
        /// </summary>
        private CsComments()
        {
            Param = new CsCommentsParamCollection();
            TypeParam = new CsCommentsParamCollection();
            Exception = new CsCommentsException[0];
        }

        internal CsComments(XmlNode node)
        {
            var summary = node["summary"] ?? node["value"];
            if (summary != null)
            {
                Summary = summary.InnerText.Trim();
            }

            var remarks = node["remarks"];
            if (remarks != null)
            {
                Remarks = remarks.InnerText.Trim();
            }

            var returns = node["returns"];
            if (returns != null)
            {
                Returns = returns.InnerText.Trim();
            }

            var param = node.SelectNodes("param");
            Param=new CsCommentsParamCollection();
            foreach (XmlNode item in param)
            {
                var p=new CsCommentsParam(item);
                Param[p.Name] = p;
            }

            var typeparam = node.SelectNodes("typeparam");
            TypeParam=new CsCommentsParamCollection();
            foreach (XmlNode item in typeparam)
            {
                var p=new CsCommentsParam(item);
                TypeParam[p.Name] = p;
            }

            var exception = node.SelectNodes("exception");
            var index = 0;
            Exception=new CsCommentsException[exception.Count];
            foreach (XmlNode item in exception)
            {
                Exception[index]=new CsCommentsException(index,item);
                index++;
            }
        }

        /// <summary>
        /// 重写返回字符串。返回 Smmary 属性或空字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Summary ?? "";
        }
    }
}
