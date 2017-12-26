using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Develops;
using Bing.Utils.Json;
using Bing.Utils.Modes.Trees.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.Utils.UnitTest.Modes.Trees
{
    [TestClass]
    public class TreeBuilderTest
    {
        [TestMethod]
        public void Test_Build()
        {
            List<Product> list=new List<Product>();
            for (int i = 1; i < 10; i++)
            {
                list.Add(new Product
                {
                    Id = i,
                    Name = "测试"+i.ToString("D"),
                    ParentId = null
                });
            }
            for (int i = 1; i < 10; i++)
            {
                list.Add(new Product
                {
                    Id = i+9,
                    Name = "测试" + (i+9).ToString("D"),
                    ParentId = i
                });
            }
            for (int i = 1; i < 10; i++)
            {
                list.Add(new Product
                {
                    Id = i + 18,
                    Name = "测试" + (i+18).ToString("D"),
                    ParentId = i+9
                });
            }
            CodeTimer.CodeExecuteTime(() =>
            {
                var tree = TreeBuilder.Build<Product>("产品")
                    .SetItems(list, t => t.Name, i => i.Id, p => p.ParentId)                    
                    .Current;
                var result = tree.ToJson();
                Console.WriteLine(result);
            });
            
        }

        [TestMethod]
        public void Test_Build_Other()
        {
            List<Product> list = new List<Product>();
            for (int i = 1; i < 10; i++)
            {
                list.Add(new Product
                {
                    Id = i,
                    Name = "测试" + i.ToString("D"),
                    ParentId = null
                });
            }
            for (int i = 1; i < 10; i++)
            {
                list.Add(new Product
                {
                    Id = i + 9,
                    Name = "测试" + (i + 9).ToString("D"),
                    ParentId = i
                });
            }
            for (int i = 1; i < 10; i++)
            {
                list.Add(new Product
                {
                    Id = i + 18,
                    Name = "测试" + (i + 18).ToString("D"),
                    ParentId = i + 9
                });
            }
            CodeTimer.CodeExecuteTime(() =>
            {
                var tree = TreeBuilder.Build<Product>("产品")
                    .SetItems(list, t => t.Name, i => i.Id, p => p.ParentId,x=>x.Id)
                    .Current;
                var result = tree.ToJson();
                Console.WriteLine(result);
            });
        }
    }

    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? ParentId { get; set; }
    }
}
