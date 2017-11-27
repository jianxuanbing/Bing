using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Encrypts
{
    /// <summary>
    /// 内部常量
    /// </summary>
    internal class Const
    {
        /// <summary>
        /// 公钥
        /// </summary>
        internal const string PublicKeyFormat =
            @"<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>";

        /// <summary>
        /// 私钥
        /// </summary>
        internal const string PrivateKeyFormat =
            @"<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>";
    }
}
