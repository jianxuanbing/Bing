using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bing.Utils.Encrypts.Asymmetric
{
    /// <summary>
    /// RSA 加密扩展
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class RSAExtensions
    {
        /// <summary>
        /// 获取RSA Xml序列化
        /// </summary>
        /// <param name="rsa">RSA实例</param>
        /// <param name="includePrivateParameters">是否包含私钥</param>
        /// <returns></returns>
        public static string ToExtXmlString(this RSA rsa, bool includePrivateParameters)
        {
            RSAParameters parameters = rsa.ExportParameters(includePrivateParameters);

            if (includePrivateParameters)
            {
                return string.Format(Const.PrivateKeyFormat,
                    parameters.Modulus != null ? Convert.ToBase64String(parameters.Modulus) : null,
                    parameters.Exponent != null ? Convert.ToBase64String(parameters.Exponent) : null,
                    parameters.P != null ? Convert.ToBase64String(parameters.P) : null,
                    parameters.Q != null ? Convert.ToBase64String(parameters.Q) : null,
                    parameters.DP != null ? Convert.ToBase64String(parameters.DP) : null,
                    parameters.DQ != null ? Convert.ToBase64String(parameters.DQ) : null,
                    parameters.InverseQ != null ? Convert.ToBase64String(parameters.InverseQ) : null,
                    parameters.D != null ? Convert.ToBase64String(parameters.D) : null);
            }
            return string.Format(Const.PublicKeyFormat,
                parameters.Modulus != null ? Convert.ToBase64String(parameters.Modulus) : null,
                parameters.Exponent != null ? Convert.ToBase64String(parameters.Exponent) : null);
        }

        /// <summary>
        /// 从Xml字符串中获取RSA
        /// </summary>
        /// <param name="rsa">RSA实例</param>
        /// <param name="xmlString">Xml字符串</param>
        public static void FromExtXmlString(this RSA rsa, string xmlString)
        {
            RSAParameters parameters = new RSAParameters();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            if (doc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus":
                            parameters.Modulus = Convert.FromBase64String(node.InnerText);
                            break;
                        case "Exponent":
                            parameters.Exponent = Convert.FromBase64String(node.InnerText);
                            break;
                        case "P":
                            parameters.P = Convert.FromBase64String(node.InnerText);
                            break;
                        case "Q":
                            parameters.Q = Convert.FromBase64String(node.InnerText);
                            break;
                        case "DP":
                            parameters.DP = Convert.FromBase64String(node.InnerText);
                            break;
                        case "DQ":
                            parameters.DQ = Convert.FromBase64String(node.InnerText);
                            break;
                        case "InverseQ":
                            parameters.InverseQ = Convert.FromBase64String(node.InnerText);
                            break;
                        case "D":
                            parameters.D = Convert.FromBase64String(node.InnerText);
                            break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid Xml RSA Key");
            }
            rsa.ImportParameters(parameters);
        }
    }
}
