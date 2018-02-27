using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.Tools.QrCode.ZXing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bing.Tools.QrCode.UnitTest
{
    [TestClass]
    public class ZXingQrCodeServiceTest
    {
        [TestMethod]
        public void Test_SaveBase64()
        {
            ZXingQrCodeService service=new ZXingQrCodeService();
            var result=service.SaveBase64("13610142496");
            Console.WriteLine(result);
        }

        [TestMethod]
        public void Test_Barcode_SaveBase64()
        {
            ZXingBarcodeService service=new ZXingBarcodeService();
            service.Size(200, 100);
            service.Correction(ErrorCorrectionLevel.H);
            var result = service.SaveBase64("13610142496ABCD");
            Console.WriteLine(result);
        }
    }
}
