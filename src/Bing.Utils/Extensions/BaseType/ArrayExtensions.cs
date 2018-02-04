using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Extensions
{
    /// <summary>
    /// 数组（Array）扩展
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// 添加数据项
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="item">数据项</param>
        /// <returns></returns>
        public static T[] Add<T>(this T[] array, T item)
        {
            Array.Resize(ref array,array.Length+1);
            array[array.Length - 1] = item;
            return array;
        }

        /// <summary>
        /// 遍历数组
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="action">遍历操作</param>
        public static void ForEach(this Array array, Action<Array, int[]> action)
        {
            if (array.LongLength == 0)
            {
                return;
            }
            
            ArrayTraverse walker=new ArrayTraverse(array);

            do
            {
                action(array, walker.Position);
            } while (walker.Step());
        }

        /// <summary>
        /// 数组遍历
        /// </summary>
        private class ArrayTraverse
        {
            /// <summary>
            /// 位置数组
            /// </summary>
            public int[] Position;

            /// <summary>
            /// 最大长度集合
            /// </summary>
            private int[] _maxLengths;

            /// <summary>
            /// 初始化一个<see cref="ArrayTraverse"/>类型的实例
            /// </summary>
            /// <param name="array">数组</param>
            public ArrayTraverse(Array array)
            {
                Position=new int[array.Rank];
                _maxLengths=new int[array.Rank];

                for (int i = 0; i < array.Rank; i++)
                {
                    _maxLengths[i] = array.GetLength(i) - 1;
                }
            }

            /// <summary>
            /// 是否有下一步
            /// </summary>
            /// <returns></returns>
            public bool Step()
            {
                for (int i = 0; i < Position.Length; i++)
                {
                    if (Position[i] < _maxLengths[i])
                    {
                        Position[i]++;
                        for (int j = 0; j < i; j++)
                        {
                            Position[j] = 0;
                        }

                        return true;
                    }
                }

                return false;
            }
        }        
    }
}
