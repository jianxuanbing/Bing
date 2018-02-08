using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.SqlBuilder.Conditions;
using Bing.SqlBuilder.Logics;
using Bing.Utils.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.UnitTest.SqlBuilder.Logics
{
    [TestClass]
    public class LogicSqlBuilderTest
    {
        [TestMethod]
        public void Test_GetParentsByCode()
        {
            ILogicSqlBuilder sqlBuilder=new LogicSqlBuilder();
            var sql=sqlBuilder.GetParentsByCode("SysRegion", "ParentID", "00000000-0000-0000-0000-000000010397", "CreateTime");
            var param = sqlBuilder.GetParamDict();
            Console.WriteLine(sql);
            Console.WriteLine(param.ToJson());
        }

        [TestMethod]
        public void Test_GetChildrens()
        {
            ILogicSqlBuilder sqlBuilder = new LogicSqlBuilder();
            var sql = sqlBuilder.GetChildrens("SysRegion", "ID", "00000000-0000-0000-0000-000000010397", "ParentID",
                "CreateTime");
            var param = sqlBuilder.GetParamDict();
            Console.WriteLine(sql);
            Console.WriteLine(param.ToJson());
        }
    }
}
