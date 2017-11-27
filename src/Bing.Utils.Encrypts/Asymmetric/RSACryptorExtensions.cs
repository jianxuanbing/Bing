using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Bing.Utils.Encrypts.Asymmetric
{
    /// <summary>
    /// RSA 加密器 扩展
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class RSACryptorExtensions
    {
        /// <summary>
        /// RSA 私钥格式转换，.net => java
        /// </summary>
        /// <param name="privateKey">.net生成的私钥</param>
        /// <returns></returns>
        public static string RsaPrivateKeyDotNet2Java(this string privateKey)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(privateKey);
            BigInteger modulus = doc.GetBigInteger("Modulus");
            BigInteger publicExponent = doc.GetBigInteger("Exponent");
            BigInteger privateExponent = doc.GetBigInteger("D");
            BigInteger p = doc.GetBigInteger("P");
            BigInteger q = doc.GetBigInteger("Q");
            BigInteger dp = doc.GetBigInteger("DP");
            BigInteger dq = doc.GetBigInteger("DQ");
            BigInteger qinv = doc.GetBigInteger("InverseQ");

            RsaPrivateCrtKeyParameters privateKeyParam =
                new RsaPrivateCrtKeyParameters(modulus, publicExponent, privateExponent, p, q, dp, dq, qinv);
            PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKeyParam);
            byte[] serializedPrivateBytes = privateKeyInfo.ToAsn1Object().GetEncoded();
            return Convert.ToBase64String(serializedPrivateBytes);
        }

        /// <summary>
        /// RSA 公钥格式转换，.net => java
        /// </summary>
        /// <param name="publicKey">.net生成的公钥</param>
        /// <returns></returns>
        public static string RsaPublicKeyDotNet2Java(this string publicKey)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(publicKey);
            BigInteger modulus = doc.GetBigInteger("Modulus");
            BigInteger publicExponent = doc.GetBigInteger("Exponent");

            RsaKeyParameters publicKeyParam = new RsaKeyParameters(false, modulus, publicExponent);
            SubjectPublicKeyInfo publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKeyParam);
            byte[] serializePublicBytes = publicKeyInfo.ToAsn1Object().GetDerEncoded();
            return Convert.ToBase64String(serializePublicBytes);
        }

        /// <summary>
        /// RSA 私钥格式转换，java => .net
        /// </summary>
        /// <param name="privateKey">java生成的RSA私钥</param>
        /// <returns></returns>
        public static string RsaPrivateKeyJava2DotNet(this string privateKey)
        {
            RsaPrivateCrtKeyParameters param =
                (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));
            return string.Format(Const.PrivateKeyFormat,
                param.Modulus.GetBase64String(),
                param.PublicExponent.GetBase64String(),
                param.P.GetBase64String(),
                param.Q.GetBase64String(),
                param.DP.GetBase64String(),
                param.DQ.GetBase64String(),
                param.QInv.GetBase64String(),
                param.Exponent.GetBase64String());
        }

        /// <summary>
        /// RSA 公钥格式转换，java => .net
        /// </summary>
        /// <param name="publicKey">java生成的RSA公钥</param>
        /// <returns></returns>
        public static string RsaPublicKeyJava2DotNet(this string publicKey)
        {
            RsaKeyParameters param = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
            return string.Format(Const.PublicKeyFormat, param.Modulus.GetBase64String(),
                param.Exponent.GetBase64String());
        }

        /// <summary>
        /// 获取超大数，用于获取Java秘钥格式
        /// </summary>
        /// <param name="document">Xml文档</param>
        /// <param name="tagName">标签名</param>
        /// <returns></returns>
        private static BigInteger GetBigInteger(this XmlDocument document, string tagName)
        {
            return new BigInteger(1,
                Convert.FromBase64String(document.DocumentElement.GetElementsByTagName(tagName)[0].InnerText));
        }

        /// <summary>
        /// 获取Base64字符串
        /// </summary>
        /// <param name="integer">超大数</param>
        /// <returns></returns>
        public static string GetBase64String(this BigInteger integer)
        {
            return Convert.ToBase64String(integer.ToByteArrayUnsigned());
        }
    }
}
