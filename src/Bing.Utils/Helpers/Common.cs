using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Helpers
{
    /// <summary>
    /// 常用公共操作
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public static Type GetType<T>()
        {
            var type = typeof(T);
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        /// <summary>
        /// 换行符
        /// </summary>
        public static string Line => Environment.NewLine;

        /// <summary>
        /// 交换值。交换两个提供的变量中的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="a">变量A</param>
        /// <param name="b">变量B</param>
        public static void Swap<T>(ref T a, ref T b)
        {
            T swap = a;
            a = b;
            b = swap;
        }
    }
}
