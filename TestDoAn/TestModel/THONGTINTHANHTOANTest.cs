using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn.TestModel
{
    [TestClass]
    public class THONGTINTHANHTOANTest
    {
        [TestMethod]
        public void TestMATTProperty()
        {
            var tttt = new THONGTINTHANHTOAN();
            string ma = "TT000001";
            tttt.MATT = ma;

            Assert.AreEqual(ma, tttt.MATT);
        }

        [TestMethod]
        public void TestMAPHIEUProperty()
        {
            var tttt = new THONGTINTHANHTOAN();
            string maphieu = "PH000001";
            tttt.MAPHIEU = maphieu;

            Assert.AreEqual(maphieu, tttt.MAPHIEU);
        }

        [TestMethod]
        public void TestSOTIENProperty()
        {
            var tttt = new THONGTINTHANHTOAN();
            int st = 4000000;
            tttt.SOTIEN = st;

            Assert.AreEqual(st, tttt.SOTIEN);
        }

        [TestMethod]
        public void TestTHOIGIANProperty()
        {
            var tttt = new THONGTINTHANHTOAN();
            DateTime tg = DateTime.Now;
            tttt.THOIGIAN = tg;

            Assert.AreEqual(tg, tttt.THOIGIAN);
        }

        [TestMethod]
        public void TestTRANGTHAIProperty()
        {
            var tttt = new THONGTINTHANHTOAN();
            string tt = "SOLD";
            tttt.TRANGTHAI = tt;

            Assert.AreEqual(tt, tttt.TRANGTHAI);
        }

        [TestMethod]
        public void TestPHUONGTHUCProperty()
        {
            var tttt = new THONGTINTHANHTOAN();
            string pt = "Tiền mặt";
            tttt.PHUONGTHUC = pt;

            Assert.AreEqual(pt, tttt.PHUONGTHUC);
        }

        [TestMethod]
        public void TestPHIEUDATCHOProperty()
        {
            var tttt = new THONGTINTHANHTOAN();
            PHIEUDATCHO pdc = new PHIEUDATCHO();
            tttt.PHIEUDATCHO = pdc;

            Assert.AreEqual(pdc, tttt.PHIEUDATCHO);
        }
    }
}
