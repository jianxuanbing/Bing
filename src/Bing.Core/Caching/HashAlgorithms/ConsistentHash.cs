using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Caching.HashAlgorithms
{
    /// <summary>
    /// 针对<see cref="T"/>哈希算法实现
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class ConsistentHash<T>
    {
        #region 字段

        /// <summary>
        /// 环形排序字典
        /// </summary>
        private readonly SortedDictionary<int,T> _ring=new SortedDictionary<int, T>();

        /// <summary>
        /// 环形节点
        /// </summary>
        private int[] _nodeKeysInRing = null;

        /// <summary>
        /// 哈希算法
        /// </summary>
        private readonly IHashAlgorithm _hashAlgorithm;

        #endregion

        #region 属性

        /// <summary>
        /// 复制哈希节点数
        /// </summary>
        public int VirtualNodeReplicationFactor { get; } = 1000;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化一个<see cref="ConsistentHash{T}"/>类型的实例
        /// </summary>
        /// <param name="hashAlgorithm">哈希算法</param>
        public ConsistentHash(IHashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
        }

        /// <summary>
        /// 初始化一个<see cref="ConsistentHash{T}"/>类型的实例
        /// </summary>
        /// <param name="hashAlgorithm">哈希算法</param>
        /// <param name="virtualNodeReplicationFactor">复制哈希节点数</param>
        public ConsistentHash(IHashAlgorithm hashAlgorithm, int virtualNodeReplicationFactor) : this(hashAlgorithm)
        {
            VirtualNodeReplicationFactor = virtualNodeReplicationFactor;
        }

        #endregion

        #region Initialize(初始化节点服务器)

        /// <summary>
        /// 初始化节点服务器
        /// </summary>
        /// <param name="nodes">节点</param>
        public void Initialize(IEnumerable<T> nodes)
        {
            foreach (var node in nodes)
            {
                AddNode(node);
            }

            _nodeKeysInRing = _ring.Keys.ToArray();
        }

        #endregion

        #region Add(添加节点)

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="node">节点</param>
        public void Add(T node)
        {
            AddNode(node);
            _nodeKeysInRing = _ring.Keys.ToArray();
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="node">节点</param>
        private void AddNode(T node)
        {
            for (var i = 0; i < VirtualNodeReplicationFactor; i++)
            {
                var hashOfVirtualNode =
                    _hashAlgorithm.Hash(node.GetHashCode().ToString(CultureInfo.InvariantCulture) + i);
                _ring[hashOfVirtualNode] = node;
            }
        }

        #endregion

        #region Remove(删除节点)

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="node">节点</param>
        public void Remove(T node)
        {
            RemoveNode(node);
            _nodeKeysInRing = _ring.Keys.ToArray();
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="node">节点</param>
        public void RemoveNode(T node)
        {
            for (var i = 0; i < VirtualNodeReplicationFactor; i++)
            {
                var hashOfVirtualNode =
                    _hashAlgorithm.Hash(node.GetHashCode().ToString(CultureInfo.InvariantCulture) + i);
                _ring.Remove(hashOfVirtualNode);
            }
        }

        #endregion

        #region GetItemNode(通过哈希算法计算出对应的节点)

        /// <summary>
        /// 通过哈希算法计算出对应的节点
        /// </summary>
        /// <param name="item">值</param>
        /// <returns></returns>
        public T GetItemNode(string item)
        {
            var hashOfItem = _hashAlgorithm.Hash(item);
            var nearestNodePosition = GetClockwiseNearestNode(_nodeKeysInRing, hashOfItem);
            return _ring[_nodeKeysInRing[nearestNodePosition]];
        }

        /// <summary>
        /// 顺时针查找对应哈希的位置
        /// </summary>
        /// <param name="keys">键集合数</param>
        /// <param name="hashOfItem">哈希值</param>
        /// <returns>哈希位置</returns>
        private int GetClockwiseNearestNode(int[] keys, int hashOfItem)
        {
            var begin = 0;
            var end = keys.Length - 1;
            if (keys[end] < hashOfItem || keys[0] > hashOfItem)
            {
                return 0;
            }

            while ((end-begin)>1)
            {
                var mid = (end + begin) / 2;
                if (keys[mid] >= hashOfItem)
                {
                    end = mid;
                }
                else
                {
                    begin = mid;
                }
            }

            return end;
        }

        #endregion
    }
}
