using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.SqlBuilder.Conditions;
using Bing.Utils.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.UnitTest.SqlBuilder.Conditions
{
    [TestClass]
    public class ConditionBuilderTest
    {
        [TestMethod]
        public void Test_Block()
        {
            Stopwatch stopwatch=new Stopwatch();
            stopwatch.Start();
            ConditionBuilder builder =new ConditionBuilder();
            builder.Append("A.CreateTime", SqlOperator.Equal, DateTime.Now);
            builder.Between("B.CreateTime", DateTime.Now, DateTime.Now.AddDays(1));
            
            //builder.Block(RelationType.And, childBuilder);
            builder.Or(child =>
            {                
                child.Equal("D.CreateTime",DateTime.Now);
                child.Equal("E.CreateTime",DateTime.Now);
                child.Equal( "E.CreateTime", DateTime.Now);
                child.And(c2 =>
                {
                    c2.Append("HH.CreateTime", SqlOperator.Equal, 5);
                });
            }).Or(child =>
            {
                child.Append(RelationType.And, "E.CreateTime", SqlOperator.Equal, DateTime.Now);
            });
            builder.Append(RelationType.And, "F.CreateTime", SqlOperator.Equal, DateTime.Now);
            builder.Append(RelationType.And, "A.ID", SqlOperator.In, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            
            builder.Append(RelationType.And, "B.ID", SqlOperator.In, 1,2,3,4,5,6,7,8,9,10);            
            builder.AppendRaw("E.CreateTime like '%123456%'");
            builder.Between("H.CreateTime", DateTime.Now, DateTime.Now.AddDays(5));
            
            var result = builder.ToString();
            stopwatch.Stop();

            var param = builder.GetParamDict().ToJson();
            
            Console.WriteLine(result);
            Console.WriteLine(param);

            Console.WriteLine(stopwatch.Elapsed.Milliseconds);
        }

        [TestMethod]
        public void Test_AppendRaw()
        {
            ConditionBuilder builder=new ConditionBuilder();
            builder.AppendRaw("A.CreateTime like '%123456%'").AppendRaw("A.CreateTime like '%123456%'"); 

            var result = builder.ToString();
            var param = builder.GetParamDict().ToJson();

            Console.WriteLine(result);
            Console.WriteLine(param);
        }

        [TestMethod]
        public void Test_Barcode()
        {
            ConditionBuilder builder=new ConditionBuilder();
            builder.Equal("A.MerchantID", Guid.NewGuid())
                .Contains("A.Barcode", "123456")
                .Contains("B.CustomBC", "111100")
                .Contains("C.Name", "Test")
                .Contains("A.Editor", "TT");

            var result = builder.ToString();
            var param = builder.GetParamDict().ToJson();

            Console.WriteLine(result);
            Console.WriteLine(param);
        }

        [TestMethod]
        public void Test_Or()
        {
            ConditionBuilder builder=new ConditionBuilder();
            builder.Equal("A.MerchantID", Guid.NewGuid())
                .OrEqual("B.MerchantID", Guid.NewGuid())
                .Equal("C.ID",Guid.Empty)
                .And(c =>
                {
                    c.Contains("B.Name", "测试用户")
                        .Contains("C.Name", "007");
                });

            var result = builder.ToString();
            var param = builder.GetParamDict().ToJson();

            Console.WriteLine(result);
            Console.WriteLine(param);
        }

        [TestMethod]
        public void Test_ChildIn()
        {
            ConditionBuilder builder=new ConditionBuilder();
            builder.In("A.ID", "select Id from User");
            builder.In("A.ID", new[] { 1, 2, 3, 4, 56, 7 });
            builder.And(x =>
            {
                x.Contains("A.Name", "测试");
            });
            var result = builder.ToString();
            var param = builder.GetParamDict().ToJson();

            Console.WriteLine(result);
            Console.WriteLine(param);
        }

        [TestMethod]
        public void Test_CustomParam()
        {
            ConditionBuilder builder=new ConditionBuilder();
            builder.AppendRawParam("(LEFT({0}, LEN({1})) = {1}",
                    builder.AddParameter("ParentID", "BB2A1B4A-5E9E-33D5-697B-39E37974ACC4"), "ParentID")
                .Equal("IsDeleted", 1).Equal("Status", 1);
            builder.AppendRaw("orders.flag=90", 1 == 0);
            var result = builder.ToString();
            var param = builder.GetParamDict().ToJson();

            Console.WriteLine(result);
            Console.WriteLine(param);
        }

        [TestMethod]
        public void Test_InternalOr()
        {
            ConditionBuilder builder = new ConditionBuilder();
            builder.And(x =>
            {
                x.Or(y =>
                    {
                        y.Equal("C.MemberId", "007")
                            .In("C.CheckFlag", new[] {3, 4})
                            .In("C.Flag", new[] {1, 2});
                    })
                    .Or(y =>
                    {
                        y.NotEqual("C.MemberId", "007")
                            .Equal("C.CheckFlag", 4)
                            .Equal("C.Flag", 2);
                    });
            });

            var result = builder.ToString();
            var param = builder.GetParamDict().ToJson();

            Console.WriteLine(result);
            Console.WriteLine(param);
        }

        [TestMethod]
        public void Test_IsNotNull()
        {
            ConditionBuilder builder = new ConditionBuilder();
            builder.IsNotNull("A.ClerkId").Equal("C.ClerkId",1);

            var result = builder.ToString();
            var param = builder.GetParamDict().ToJson();

            Console.WriteLine(result);
            Console.WriteLine(param);
        }
    }
}
