using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.AutoMapper.UnitTest
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid? GroupId { get; set; }
    }

    /// <summary>
    /// 用户信息 数据传输对象
    /// </summary>
    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid? GroupId { get; set; }
    }
}
