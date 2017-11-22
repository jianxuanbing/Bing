using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Core.Reflections
{
    /// <summary>
    /// Web应用类型查找器
    /// </summary>
    public class WebAppTypeFinder:AppDomainTypeFinder
    {
        /// <summary>
        /// 获取程序集列表
        /// </summary>
        /// <returns></returns>
        public override List<Assembly> GetAssemblies()
        {
            LoadAssemblies(GetBinDirectory());
            return base.GetAssemblies();
        }

        /// <summary>
        /// 获取bin目录的物理磁盘路径
        /// </summary>
        /// <returns></returns>
        public virtual string GetBinDirectory()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            return path == Environment.CurrentDirectory + "\\" ? path : Path.Combine(path, "bin");
        }
    }
}
