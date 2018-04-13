using System;
using Bing.Utils.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var date = DateTime.Now.ToString("yyMMdd");
            Console.WriteLine(date);
        }

        [TestMethod]
        public void Test_GetType()
        {
            var logType = Type.GetType("Bing.Logs.ILog, Bing.Logs");
            Console.WriteLine(logType.Assembly);
        }

        [TestMethod]
        public void Test_Validate()
        {
            var result = Validate(Guid.Empty);
            Console.WriteLine(result);
        }

        private bool Validate<T>(params T[] fieldValue)
        {
            bool result =
                fieldValue==null||fieldValue.Length==0||
                (string.IsNullOrWhiteSpace(fieldValue[0] + "") || typeof(T) == typeof(Guid) &&
                 fieldValue[0].ToString().ToGuid().IsEmpty());
            return result;
        }
    }
}
