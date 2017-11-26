using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Domains.Entities;
using Bing.Domains.Entities.Auditing;

namespace Bing.Samples.Domains.Models
{
    /// <summary>
    /// 登录
    /// </summary>
    public partial class Login: AggregateRoot<Login>,IAudited<string>
    {
        public Login() : this(Guid.Empty)
        {
            
        }
        public Login(Guid id) : base(id)
        {
        }

        /// <summary>
        /// 状态
        /// </summary>

        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>

        public string Note { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>

        public Guid? RoleID { get; set; }

        /// <summary>
        /// 账号
        /// </summary>

        public string LoginName { get; set; }

        /// <summary>
        /// 名字
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        /// 手机
        /// </summary>

        public string Mobile { get; set; }

        /// <summary>
        /// 密码
        /// </summary>

        public string PassWord { get; set; }

        public DateTime? CreationTime { get; set; }
        public string CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string LastModifierId { get; set; }
    }
}
