using System;
using System.Net;
using Bing.DbGenerater;
using Bing.DbGenerater.Interface;
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
            Config.Instance.DbConnection = "Data Source=.oicp.net,1433; Initial Catalog=Lxm_test; User ID=login;Password=disneyatyongjun; Connect Timeout=120; MultipleActiveResultSets=True;";
            Config.Instance.ProviderName = "System.Data.SqlClient";
            Config.Instance.DbName = "Lxm_test";
        }
        [TestMethod]
        public void Test_GetTableInfoList()
        {
            IDbMaintenance maintenance=new SqlServerDbMaintenance();
            var result=maintenance.GetTableInfoList();
            foreach (var info in result)
            {
                Console.WriteLine($"表名：{info.Name}，备注：{info.Description}");
            }
        }

        [TestMethod]
        public void Test_GetViewInfoList()
        {
            IDbMaintenance maintenance = new SqlServerDbMaintenance();
            var result = maintenance.GetViewInfoList();
            foreach (var info in result)
            {
                Console.WriteLine($"表名：{info.Name}，备注：{info.Description}");
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
    }
}
