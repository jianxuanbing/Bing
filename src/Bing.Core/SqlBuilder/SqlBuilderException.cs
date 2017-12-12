using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Exceptions;

namespace Bing.SqlBuilder
{
    /// <summary>
    /// Sql生成器异常
    /// </summary>
    public class SqlBuilderException:Exception
    {
        /// <summary>
        /// 初始化一个<see cref="SqlBuilderException"/>类型的实例
        /// </summary>
        /// <param name="message"></param>
        public SqlBuilderException(string message) : base(message)
        {            
        }
    }
}
