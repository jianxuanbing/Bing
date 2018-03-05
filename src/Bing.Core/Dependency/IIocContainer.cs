using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Dependency
{
    /// <summary>
    /// 容器
    /// </summary>
    public interface IIocContainer
    {

        /// <summary>
        /// 设置容器
        /// </summary>
        /// <param name="container">容器</param>
        void SetContainer(object container);

        /// <summary>
        /// 获取容器
        /// </summary>
        /// <typeparam name="T">容器类型</typeparam>
        /// <returns></returns>
        T GetContainer<T>();

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        T Create<T>(string name = null);

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        object Create(Type type, string name = null);

        /// <summary>
        /// 创建集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        List<T> CreateList<T>(string name = null);

        /// <summary>
        /// 创建集合
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        object CreateList(Type type, string name = null);

        /// <summary>
        /// 该类型是否已注册
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        bool IsRegistered(Type type, string name = null);

        /// <summary>
        /// 该类型是否已注册
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        bool IsRegistered<T>(string name=null);
    }
}
