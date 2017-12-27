using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Offices.Imports
{
    /// <summary>
    /// 导入器
    /// </summary>
    public abstract class ImportBase:IImport
    {
        /// <summary>
        /// 文件绝对路径
        /// </summary>
        protected string Path { get; set; }

        /// <summary>
        /// 初始化一个<see cref="ImportBase"/>类型的实例
        /// </summary>
        /// <param name="path">文件的绝对路径</param>
        protected ImportBase(string path)
        {
            Path = path;
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public List<T> GetResult<T>()
        {
            using (var stream=new FileStream(Path,FileMode.Open,FileAccess.Read))
            {
                return GetResult<T>(stream);
            }
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        protected abstract List<T> GetResult<T>(Stream stream);
    }
}
