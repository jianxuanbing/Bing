using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.Comments.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_GetComments()
        {
            var method = typeof(CsCommentReader).GetMethod("Create", BindingFlags.Default);

            var comment = CsCommentReader.Create(method);
            Console.WriteLine("summary:"+comment.Summary);

        }
    }
}
