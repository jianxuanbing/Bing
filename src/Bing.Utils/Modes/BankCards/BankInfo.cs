using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bing.Utils.Modes.BankCards
{
    /// <summary>
    /// 银行卡
    /// </summary>
    public class BankInfo
    {
        /// <summary>
        /// 银行名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 银行卡类型
        /// </summary>
        public CardType Type { get; set; }

        /// <summary>
        /// 银行卡编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 初始化一个<see cref="BankInfo"/>类型的实例
        /// </summary>
        /// <param name="card"></param>
        public BankInfo(string card)
        {
            CardNumber = card;
        }        
    }
}
