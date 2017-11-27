using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Utils.Encrypts.Asymmetric
{
    /// <summary>
    /// Rsa 算法类型
    /// </summary>
    public enum RsaType
    {
        /// <summary>
        /// SHA1
        /// </summary>
        [Description("SHA1")]
        Rsa,
        /// <summary>
        /// RSA2 密钥长度至少为2048，SHA256
        /// </summary>
        [Description("SHA256")]
        Rsa2
    }
}
