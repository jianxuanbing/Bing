using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bing.Utils.Modes.IdCards
{
    /// <summary>
    /// 身份证
    /// </summary>
    public class IdCard
    {
        #region Field(字段)
        /// <summary>
        /// 身份证验证器
        /// </summary>
        private static IdCardVerifier verifier = new IdCardVerifier();
        /// <summary>
        /// 生日日期格式
        /// </summary>
        private const string BIRTH_DATE_FORMAT = "yyyyMMdd";
        /// <summary>
        /// 18位身份证长度
        /// </summary>
        private const int CARD_NUMBER_LENGTH = 18;
        /// <summary>
        /// 15位身份证长度
        /// </summary>
        private const int OLD_CARD_NUMBER_LENGTH = 15;
        /// <summary>
        /// 身份证格式正则
        /// </summary>
        private static readonly Regex SocialNumberPattern = new Regex(@"^[0-9]{17}[0-9X]$");
        #endregion

        #region Property(属性)
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string CardNumber { get; }
        /// <summary>
        /// 地址编码
        /// </summary>
        public string AddressCode { get; private set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BirthDate { get; private set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Sex Gender { get; private set; }
        #endregion

        #region Constructor(构造函数)
        /// <summary>
        /// 初始化一个<see cref="IdCard"/>类型的实例
        /// </summary>
        /// <param name="cardNumber">身份证号码</param>
        public IdCard(string cardNumber)
        {
            if (cardNumber.Length == OLD_CARD_NUMBER_LENGTH)
            {
                cardNumber = verifier.ToNewCardNumber(cardNumber);
            }
            Validate(cardNumber);
            CardNumber = cardNumber;
            Extract();
        }
        #endregion

        #region ExtractBirthDate(抽取生日信息)
        /// <summary>
        /// 抽取生日信息
        /// </summary>
        /// <returns></returns>
        public DateTime ExtractBirthDate()
        {
            try
            {
                return DateTime.ParseExact(CardNumber.Substring(6, 8), BIRTH_DATE_FORMAT, null);
            }
            catch (Exception)
            {
                throw new ApplicationException("身份证的出生日期无效");
            }
        }
        #endregion

        #region Private Methods(私有方法)
        /// <summary>
        /// 验证身份证号码
        /// </summary>
        /// <param name="cardNumber">身份证号码</param>
        private void Validate(string cardNumber)
        {
            if (!SocialNumberPattern.IsMatch(cardNumber))
            {
                throw new ApplicationException("Card Number has wrong charactor(s).");
            }
            if (cardNumber[CARD_NUMBER_LENGTH - 1] != verifier.CalculateVerifyCode(cardNumber))
            {
                throw new ApplicationException("Card Number Verified code is not match");
            }
        }

        /// <summary>
        /// 抽取信息
        /// </summary>
        private void Extract()
        {
            AddressCode = CardNumber.Substring(0, 6);
            Gender = ((int)CardNumber[CARD_NUMBER_LENGTH - 2]) % 2 == 0 ? Sex.Female : Sex.Male;
            BirthDate = ExtractBirthDate();
        }
        #endregion        

    }
}
