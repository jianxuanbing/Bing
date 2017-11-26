using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Samples.Domains.Models;

namespace Bing.Samples.Datas.Mappings
{
    /// <summary>
    /// 登录 映射配置
    /// </summary>
    public class LoginMap:Bing.Datas.EntityFramework.SqlServer.AggregateRootMap<Login>
    {
        /// <summary>
        /// 映射表
        /// </summary>
        /// <param name="builder"></param>
        protected override void MapTable(EntityTypeConfiguration<Login> builder)
        {
            builder.ToTable("Login");
        }

        /// <summary>
        /// 映射属性
        /// </summary>
        /// <param name="builder"></param>
        protected override void MapProperties(EntityTypeConfiguration<Login> builder)
        {
            base.MapProperties(builder);

            builder.Property(t => t.Id).HasColumnName("LoginID");
            
            builder.Property(t => t.CreatorId).HasColumnName("Creater");
            builder.Property(t => t.CreationTime).HasColumnName("CreateTime");
            builder.Property(t => t.LastModifierId).HasColumnName("Editor");
            builder.Property(t => t.LastModificationTime).HasColumnName("EditTime");

            builder.Ignore(t => t.Version);
        }

        /// <summary>
        /// 映射版本
        /// </summary>
        /// <param name="builder"></param>
        protected override void MapVersion(EntityTypeConfiguration<Login> builder)
        {
            
        }
    }
}
