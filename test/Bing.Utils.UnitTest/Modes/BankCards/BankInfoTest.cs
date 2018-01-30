using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Json;
using Bing.Utils.Modes.BankCards;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.Utils.UnitTest.Modes.BankCards
{
    [TestClass]
    public class BankInfoTest
    {
        [TestMethod]
        public void Test_BankInfo()
        {
            var card = "6217996620000156427";
            var result = BankCardRuleBuilder.GetBankInfo(card);
            Console.WriteLine(result.ToJson());
        }
    }
}
