using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Extensions;

namespace Bing.Utils.Modes
{
    /// <summary>
    /// 通用单例模式
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class Singleton<T>
    {
        static Dictionary<Type, object> _lockers = new Dictionary<Type, object>();
        static T _instance;

        /// <summary>
        /// 获取对象实例
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static T GetInstance(params object[] parameters)
        {
            if (_instance == null)
            {
                Type type = typeof(T);
                var locker = _lockers.GetOrDefault(type);
                if (locker == null)
                {
                    lock (_lockers)
                    {
                        locker = _lockers.GetOrAdd(type, x => new object());
                    }
                }
                lock (locker)
                {
                    if (_instance == null)
                    {
                        var cons =
                            type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                                .FirstOrDefault();
                        _instance = (T)cons.Invoke(parameters);
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// 获取对象实例
        /// </summary>
        /// <param name="action">获取对象方法</param>
        /// <returns></returns>
        public static T GetInstance(Func<T> action)
        {
            if (_instance == null)
            {
                Type type = typeof(T);
                var locker = _lockers.GetOrDefault(type);
                if (locker == null)
                {
                    lock (_lockers)
                    {
                        locker = _lockers.GetOrAdd(type, x => new object());
                    }
                }
                lock (locker)
                {
                    if (_instance == null)
                    {                        
                        _instance = action.Invoke();
                    }
                }
            }
            return _instance;
        }
    }
}
