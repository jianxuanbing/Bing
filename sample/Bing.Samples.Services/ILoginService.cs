using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Aspects;
using Bing.Caching.Abstractions;
using Bing.Dependency;
using Bing.Samples.Domains.Models;
using Bing.Samples.Domains.Request.Act;

namespace Bing.Samples.Services
{
    /// <summary>
    /// 登录 服务
    /// </summary>
    public interface ILoginService: IScopeDependency, ICaching
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="act">注册操作</param>
        
        Guid Register(RegisterAct act);

        /// <summary>
        /// 获取所有登录信息
        /// </summary>
        /// <returns></returns>
        List<Login> GetAllLogin();

        List<Login> GetListByName(string name);
    }
}
