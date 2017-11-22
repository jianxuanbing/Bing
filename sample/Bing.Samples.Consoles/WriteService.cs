using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bing.Samples.Consoles
{
    public class WriteService:IWriteService
    {
        #region Property(属性)            
        #endregion

        #region Constructor(构造函数)
        #endregion

        public void WriteContent(string content)
        {
            Console.WriteLine(content);
        }
    }
}
