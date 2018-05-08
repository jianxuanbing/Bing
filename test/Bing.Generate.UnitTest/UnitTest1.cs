using System;
using System.Net;
using Bing.DbGenerater;
using Bing.DbGenerater.Interface;
using Bing.DbGenerater.Realization.MySql.DbMaintenance;
using Bing.DbGenerater.Realization.SqlServer.DbMaintenance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.Generate.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Init()
        {
            Config.Instance.DbConnection = "Data Source=; Initial Catalog=Lxm_test; User ID=;Password=; Connect Timeout=120; MultipleActiveResultSets=True;";
            Config.Instance.ProviderName = "System.Data.SqlClient";
            Config.Instance.DbName = "Lxm_test";

            //Config.Instance.DbConnection = "server=;database=;user=;pwd=;allow user variables=true;SslMode=None";
            //Config.Instance.ProviderName = "MySql.Data.MySqlClient";
            //Config.Instance.DbName = "jitamin";
        }
        [TestMethod]
        public void Test_GetTableInfoList_SqlServer()
        {
            IDbMaintenance maintenance=new SqlServerDbMaintenance();
            var result=maintenance.GetTableInfoList();
            foreach (var info in result)
            {
                Console.WriteLine($"表名：{info.Name}，备注：{info.Description}");
            }
        }

        [TestMethod]
        public void Test_GetViewInfoList_SqlServer()
        {
            IDbMaintenance maintenance = new SqlServerDbMaintenance();
            var result = maintenance.GetViewInfoList();
            foreach (var info in result)
            {
                Console.WriteLine($"表名：{info.Name}，备注：{info.Description}");
            }
        }

        [TestMethod]
        public void Test_GetColumnInfosByTableName_SqlServer()
        {
            IDbMaintenance maintenance = new SqlServerDbMaintenance();
            var result = maintenance.GetTableInfoList();
            foreach (var info in result)
            {
                Console.WriteLine($"表名：{info.Name}，备注：{info.Description}");
                var columns = maintenance.GetColumnInfosByTableName(info.Name);
                foreach (var column in columns)
                {
                    Console.WriteLine($"    列名：{column.DbColumnName}，数据类型：{column.DataType}，长度：{column.Length}，默认值：{column.DefaultValue}，主键：{column.IsPrimaryKey}，可空：{column.IsNullable}，备注：{column.ColumnDescription}");
                }
            }
        }

        [TestMethod]
        public void Test_DnsHostName()
        {
            var ip = "120.239.67.239";            
            var result = Dns.GetHostEntry(IPAddress.Parse(ip));
            var hostName = result.HostName;
            Console.WriteLine(hostName);
        }

        [TestMethod]
        public void Test_DnsLocalHostName()
        {
            var hostname = Dns.GetHostName();
            Console.WriteLine(hostname);
        }

        [TestMethod]
        public void Test_GetTableInfoList_MySql()
        {
            IDbMaintenance maintenance = new MySqlDbMaintenance();
            var result = maintenance.GetTableInfoList();
            foreach (var info in result)
            {
                Console.WriteLine($"表名：{info.Name}，备注：{info.Description}");
            }
        }

        [TestMethod]
        public void Test_GetViewInfoList_MySql()
        {
            IDbMaintenance maintenance = new MySqlDbMaintenance();
            var result = maintenance.GetViewInfoList();
            foreach (var info in result)
            {
                Console.WriteLine($"表名：{info.Name}，备注：{info.Description}");
            }
        }

        [TestMethod]
        public void Test_GetColumnInfosByTableName_MySql()
        {
            IDbMaintenance maintenance = new MySqlDbMaintenance();
            var result = maintenance.GetTableInfoList();
            foreach (var info in result)
            {
                Console.WriteLine($"表名：{info.Name}，备注：{info.Description}");
                var columns=maintenance.GetColumnInfosByTableName(info.Name);
                foreach (var column in columns)
                {
                    Console.WriteLine($"    列名：{column.DbColumnName}，数据类型：{column.DataType}，长度：{column.Length}，默认值：{column.DefaultValue}，主键：{column.IsPrimaryKey}，可空：{column.IsNullable}，备注：{column.ColumnDescription}");
                }
            }
        }
    }
}
