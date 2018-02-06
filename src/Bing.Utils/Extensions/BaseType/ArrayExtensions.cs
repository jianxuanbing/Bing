using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Helpers;

namespace Bing.Utils.Extensions
{
    /// <summary>
    /// 数组（Array）扩展
    /// </summary>
    public static class ArrayExtensions
    {
        #region Add(添加数据项)

        /// <summary>
        /// 添加数据项
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="item">数据项</param>
        /// <returns></returns>
        public static T[] Add<T>(this T[] array, T item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
            return array;
        }

        #endregion

        #region ForEach(遍历数组)

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

            ArrayTraverse walker = new ArrayTraverse(array);

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
                Position = new int[array.Rank];
                _maxLengths = new int[array.Rank];

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
        #endregion

        #region Swap(交换数组中两索引的值)

        /// <summary>
        /// 交换数组中两索引的值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="index1">索引1</param>
        /// <param name="index2">索引2</param>
        public static void Swap<T>(this T[] array, int index1, int index2)
        {
            Common.Swap(ref array[index1], ref array[index2]);
        }



        #endregion

        #region SwapRow(交换数组中两行的值)

        /// <summary>
        /// 交换数组中两行的值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="array">二维数组</param>
        /// <param name="row1">行索引1</param>
        /// <param name="row2">行索引2</param>
        public static void SwapRow<T>(this T[,] array, int row1, int row2)
        {
            for (int i = array.GetLength(1) - 1; i >= 0; --i)
            {
                Common.Swap(ref array[row1, i], ref array[row2, i]);
            }
        }

        #endregion

    }
}
