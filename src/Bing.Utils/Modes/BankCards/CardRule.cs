using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bing.Utils.Modes.BankCards
{
    /// <summary>
    /// 银行卡规则
    /// </summary>
    public class CardRule
    {
        /// <summary>
        /// 银行名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 银行编码
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// 当前银行所有验证规则
        /// </summary>
        public List<string> AllRules { get; }=new List<string>();

        /// <summary>
        /// 借记卡规则
        /// </summary>
        internal List<string> DebitCardRules { get; }=new List<string>();

        /// <summary>
        /// 信用卡规则
        /// </summary>
        internal List<string> CreditCardRules { get; }=new List<string>();

        /// <summary>
        /// 准贷记卡规则
        /// </summary>
        internal List<string> QuasiCreditCardRules { get;}=new List<string>();

        /// <summary>
        /// 预付费卡规则
        /// </summary>
        internal List<string> PrepaidCardRules { get; }=new List<string>();

        /// <summary>
        /// 初始化一个<see cref="CardRule"/>类型的实例
        /// </summary>
        /// <param name="name">银行名称</param>
        /// <param name="code">银行编码</param>
        public CardRule(string name, string code)
        {
            Name = name;
            Code = code;
        }

        /// <summary>
        /// 获取银行卡类型
        /// </summary>
        /// <param name="cardNumber">银行卡号</param>
        /// <returns></returns>
        public CardType GetCardType(string cardNumber)
        {
            if (DebitCardRules.Any(debitCardRule => Regex.IsMatch(cardNumber,debitCardRule)))
            {
                return CardType.DebitCard;
            }
            if (CreditCardRules.Any(creditCardRule => Regex.IsMatch(cardNumber, creditCardRule)))
            {
                return CardType.CreditCard;
            }
            if (QuasiCreditCardRules.Any(quasiCreditCardRule => Regex.IsMatch(cardNumber, quasiCreditCardRule)))
            {
                return CardType.QuasiCreditCard;
            }
            if (PrepaidCardRules.Any(prepaidCardRule => Regex.IsMatch(cardNumber, prepaidCardRule)))
            {
                return CardType.PrepaidCard;
            }

            return CardType.Unknown;
        }

        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="cardType">银行卡类型</param>
        /// <param name="rule">银行卡正则表达式规则</param>
        /// <returns></returns>
        public CardRule AddRule(CardType cardType,string rule)
        {
            AllRules.Add(rule);
            switch (cardType)
            {
                case CardType.DebitCard:
                    DebitCardRules.Add(rule);                    
                    break;
                case CardType.CreditCard:
                    CreditCardRules.Add(rule);
                    break;
                case CardType.QuasiCreditCard:
                    QuasiCreditCardRules.Add(rule);
                    break;
                case CardType.PrepaidCard:
                    PrepaidCardRules.Add(rule);
                    break;
            }
            return this;
        }
    }
}