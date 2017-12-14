using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Utils.Helpers;

namespace Bing.Utils.Modes.IdCards
{
    /// <summary>
    /// 身份证验证器
    /// </summary>
    public class IdCardVerifier
    {
        /// <summary>
        /// 18位身份证中最后一位校验码
        /// </summary>
        private static readonly char[] VERIFY_CODE = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };

        /// <summary>
        /// 18位身份证中，各个数字的生成校验码时的权值
        /// </summary>
        private static readonly int[] VERIFY_CODE_WEIGHT = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };

        /// <summary>
        /// 18位身份证长度
        /// </summary>
        private const int CARD_NUMBER_LENGTH = 18;

        /// <summary>
        /// 计算校验码
        /// </summary>
        /// <param name="source">身份证号码</param>
        /// <returns></returns>
        public char CalculateVerifyCode(string source)
        {
            /**
             * <li>校验码（第十八位数）：<br/>
             * <ul>
             * <li>十七位数字本体码加权求和公式 S = Sum(Ai * Wi), i = 0...16 ，先对前17位数字的权求和；
             * Ai:表示第i位置上的身份证号码数字值 Wi:表示第i位置上的加权因子 Wi: 7 9 10 5 8 4 2 1 6 3 7 9 10 5 8 4
             * 2；</li>
             * <li>计算模 Y = mod(S, 11)</li>
             * <li>通过模得到对应的校验码 Y: 0 1 2 3 4 5 6 7 8 9 10 校验码: 1 0 X 9 8 7 6 5 4 3 2</li>
             * </ul>
             */
            int sum = 0;
            for (int i = 0; i < CARD_NUMBER_LENGTH - 1; i++)
            {
                char ch = source[i];
                sum += ((int)(ch - '0')) * VERIFY_CODE_WEIGHT[i];
            }
            return VERIFY_CODE[sum % 11];
        }

        /// <summary>
        /// 将15位身份证号码转换成18位身份证号码<br/>
        /// 15为身份证与18位身份证号码的区别为：<br/>
        /// 1、15位身份证号码中，“出生年份”字段是2位，转换时需要补入“19”，表示20世纪<br/>
        /// 2、15位身份证无最后一位校验码。18位身份证中，校验码根据前17位生成
        /// </summary>
        /// <param name="oldCardNumber">15位身份证号码</param>
        /// <returns></returns>
        public string ToNewCardNumber(string oldCardNumber)
        {
            Str str = new Str(CARD_NUMBER_LENGTH);
            str.Append(oldCardNumber.Substring(0, 6));
            str.Append("19");
            str.Append(oldCardNumber.Substring(6));
            str.Append(CalculateVerifyCode(str.ToString()));
            return str.ToString();
        }
    }
}
