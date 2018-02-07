using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Comments
{
    /// <summary>
    /// 用于描述注释中的 param 或者 typeparam 节点集合
    /// </summary>
    public sealed class CsCommentsParamCollection:Dictionary<string,CsCommentsParam>
    {
        /// <summary>
        /// 根据节点的 name 属性获取注释，如果没有，则返回null
        /// </summary>
        /// <param name="name">name 属性</param>
        /// <returns></returns>
        public new CsCommentsParam this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                {
                    return null;
                }

                if (name[0] == '@')
                {
                    name = name.Remove(0, 1);
                }

                CsCommentsParam value;
                if (base.TryGetValue(name, out value))
                {
                    return value;
                }

                return null;
            }
            set
            {
                if (!string.IsNullOrEmpty(name) && name[0] == '@')
                {
                    name = name.Remove(0, 1);
                }

                base[name] = value;
            }
        }
    }
}
