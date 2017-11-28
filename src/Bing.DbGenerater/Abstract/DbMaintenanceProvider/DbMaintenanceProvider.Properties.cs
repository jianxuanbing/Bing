using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.DbGenerater.Interface;

namespace Bing.DbGenerater.Abstract
{
    /// <summary>
    /// 数据库维护中心 提供程序 - 属性
    /// </summary>
    public abstract partial class DbMaintenanceProvider:IDbMaintenance
    {
        /// <summary>
        /// 获取 数据库连接对象
        /// </summary>
        public IDbConnection Context
        {
            get
            {
                IDbConnection conn = null;
                try
                {
                    conn = CreateConnection();
                    conn.Open();
                }
                catch (Exception ex)
                {
                    if (null != conn)
                    {
                        conn.Dispose();
                        conn = null;
                    }
                    throw new Exception("创建数据库连接对象出错!", ex);
                }
                return conn;
            } 
        }

        /// <summary>
        /// 获取视图信息列表 Sql语句
        /// </summary>
        protected abstract string GetViewInfoListSql { get; }

        /// <summary>
        /// 获取数据表信息列表 Sql语句
        /// </summary>
        protected abstract string GetTableInfoListSql { get; }

        /// <summary>
        /// 根据数据表名获取列信息 Sql语句
        /// </summary>
        protected abstract string GetColumnInfosByTableNameSql { get; }

        /// <summary>
        /// 创建数据库连接对象
        /// </summary>
        /// <returns></returns>
        private IDbConnection CreateConnection()
        {
            var factory = DbProviderFactories.GetFactory(Config.Instance.ProviderName);
            IDbConnection connection = factory.CreateConnection();
            if (connection == null)
            {
                throw new Exception("no DbConnection.");
            }
            connection.ConnectionString = Config.Instance.DbConnection;
            return connection;
        }
    }
}
