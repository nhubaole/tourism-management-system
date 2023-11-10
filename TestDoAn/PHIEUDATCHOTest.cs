using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn
{
    [TestClass]
    public class PHIEUDATCHOTest
    {
        [TestMethod]
        public void TestMAPHIEUProperty()
        {
            var pdc = new PHIEUDATCHO();
            string maphieu = "PDC000001";
            pdc.MAPHIEU = maphieu;

            Assert.AreEqual(maphieu, pdc.MAPHIEU);
        }

        [TestMethod]
        public void TestNGAYDATProperty()
        {
            var pdc = new PHIEUDATCHO();
            DateTime nd = DateTime.Now;
            pdc.NGAYDAT = nd;

            Assert.AreEqual(nd, pdc.NGAYDAT);
        }

        [TestMethod]
        public void TestSLKHACHProperty()
        {
            var pdc = new PHIEUDATCHO();
            int slk = 20;
            pdc.SLKHACH = slk;

            Assert.AreEqual(slk, pdc.SLKHACH);
        }

        [TestMethod]
        public void TestTONGTIENProperty()
        {
            var pdc = new PHIEUDATCHO();
            int tt = 20000000;
            pdc.TONGTIEN = tt;

            Assert.AreEqual(tt, pdc.TONGTIEN);
        }

        [TestMethod]
        public void TestSOTIENDATHANHTOANProperty()
        {
            var pdc = new PHIEUDATCHO();
            int stdtt = 10000000;
            pdc.SOTIENDATHANHTOAN = stdtt;

            Assert.AreEqual(stdtt, pdc.SOTIENDATHANHTOAN);
        }

        [TestMethod]
        public void TestMACHUYENProperty()
        {
            var pdc = new PHIEUDATCHO();
            string machuyen = "CH000001";
            pdc.MACHUYEN = machuyen;

            Assert.AreEqual(machuyen, pdc.MACHUYEN);
        }

        [TestMethod]
        public void TestMAKHNProperty()
        {
            var pdc = new PHIEUDATCHO();
            string makh = "KH000001";
            pdc.MAKH = makh;

            Assert.AreEqual(makh, pdc.MAKH);
        }

        [TestMethod]
        public void TestCHUYENProperty()
        {
            var pdc = new PHIEUDATCHO();
            CHUYEN chuyen = new CHUYEN();
            pdc.CHUYEN = chuyen;

            Assert.AreEqual(chuyen, pdc.CHUYEN);
        }

        [TestMethod]
        public void TestHANHKHACHesProperty()
        {
            var pdc = new PHIEUDATCHO();
            HANHKHACH hk = new HANHKHACH();
            pdc.HANHKHACHes.Add(hk);

            Assert.IsTrue(pdc.HANHKHACHes.Contains(hk));
        }

        [TestMethod]
        public void TestKHACHHANGProperty()
        {
            var pdc = new PHIEUDATCHO();
            KHACHHANG kh = new KHACHHANG();
            pdc.KHACHHANG = kh;

            Assert.AreEqual(kh, pdc.KHACHHANG);
        }

        [TestMethod]
        public void TestTHONGTINTHANHTOANsProperty()
        {
            var pdc = new PHIEUDATCHO();
            THONGTINTHANHTOAN tttt = new THONGTINTHANHTOAN();
            pdc.THONGTINTHANHTOANs.Add(tttt);

            Assert.IsTrue(pdc.THONGTINTHANHTOANs.Contains(tttt));
        }

        [TestMethod]
        public void TestVEsProperty()
        {
            var pdc = new PHIEUDATCHO();
            VE ve = new VE();
            pdc.VEs.Add(ve);

            Assert.IsTrue(pdc.VEs.Contains(ve));
        }

        [TestMethod]
        public void TestTINHTRANGProperty()
        {
            var pdc = new PHIEUDATCHO();
            string tt = "BOOKED";
            pdc.TINHTRANG = tt;

            Assert.AreEqual(tt, pdc.TINHTRANG);
        }
    }
}
