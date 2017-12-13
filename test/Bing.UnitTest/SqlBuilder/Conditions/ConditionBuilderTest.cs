using System;
using System.Collections.Generic;
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
            ConditionBuilder builder =new ConditionBuilder();
            builder.Append("A.CreateTime", SqlOperator.Equal, DateTime.Now);
            builder.Between("B.CreateTime", DateTime.Now, DateTime.Now.AddDays(1));
            
            //builder.Block(RelationType.And, childBuilder);
            builder.Or(child =>
            {                
                child.Append(RelationType.And,"D.CreateTime", SqlOperator.Equal, DateTime.Now);
                child.Append(RelationType.And, "E.CreateTime", SqlOperator.Equal, DateTime.Now);
                child.Append(RelationType.And, "E.CreateTime", SqlOperator.Equal, DateTime.Now);
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
            var param = builder.GetParamDict().ToJson();
            Console.WriteLine(result);
            Console.WriteLine(param);
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
    }
}
