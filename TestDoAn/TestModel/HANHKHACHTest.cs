using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn.TestModel
{
    [TestClass]
    public class HANHKHACHTest
    {
        [TestMethod]
        public void TestMAProperty()
        {
            var kh = new HANHKHACH();
            string ma = "HK0000001";
            kh.MAHK = ma;

            Assert.AreEqual(ma, kh.MAHK);
        }

        [TestMethod]
        public void TestHOTENProperty()
        {
            var hk = new HANHKHACH();
            string ten = "Nguyễn Văn Bá";
            hk.HOTEN = ten;

            Assert.AreEqual(ten, hk.HOTEN);
        }

        [TestMethod]
        public void TestNGAYSINHProperty()
        {
            var hk = new HANHKHACH();

            hk.NGSINH = DateTime.Now;

            Assert.IsNotNull(hk.NGSINH);

        }

        [TestMethod]
        public void TestGIOITINHProperty()
        {
            var hk = new HANHKHACH();

            string gt = "Nam";

            hk.GIOITINH = gt;

            Assert.AreEqual(gt, hk.GIOITINH);

        }

        [TestMethod]
        public void TestDIACHIProperty()
        {
            var hk = new HANHKHACH();
            string dc = "01 Nguyễn Văn Bá";
            hk.DIACHI = dc;
            Assert.AreEqual(dc, hk.DIACHI);
        }

        [TestMethod]
        public void TestSDTProperty()
        {
            var hk = new HANHKHACH();
            string sdt = "002312312312312";
            hk.SDT = sdt;
            Assert.AreEqual(sdt, hk.SDT);
        }

        [TestMethod]
        public void TestCCCDProperty()
        {

            var hk = new HANHKHACH();
            string cccd = "002312312312312";
            hk.CCCD = cccd;
            Assert.AreEqual(cccd, hk.CCCD);
        }

        [TestMethod]
        public void TestPASSPORTProperty()
        {
            var hk = new HANHKHACH();
            string passport = "002312312312312";
            hk.PASSPORT = passport;
            Assert.AreEqual(passport, hk.PASSPORT);
        }

        [TestMethod]
        public void TestNGAYHETHANPASSPORTProperty()
        {
            var hk = new HANHKHACH();
            hk.NGAYHETHANPASSPORT = DateTime.Now;
            Assert.IsNotNull(hk.NGAYHETHANPASSPORT);
        }

        [TestMethod]
        public void TestNGAYHETHANVISAProperty()
        {
            var hk = new HANHKHACH();
            hk.NGAYHETHANVISA = DateTime.Now;
            Assert.IsNotNull(hk.NGAYHETHANVISA);
        }

        [TestMethod]
        public void TestMAPHIEUProperty()
        {
            var hk = new HANHKHACH();
            string phieu = "P0000001";
            hk.MAPHIEU = phieu;

            Assert.AreEqual(phieu, hk.MAPHIEU);
        }

        [TestMethod]
        public void TestPHIEUDATCHOProperty()
        {
            var hk = new HANHKHACH();
            var dat = new PHIEUDATCHO();

            hk.PHIEUDATCHO = dat;

            Assert.AreEqual(dat, hk.PHIEUDATCHO);
        }

        [TestMethod]
        public void TestVEsProperty()
        {
            var hk = new HANHKHACH();
            var vr = new VE();

            hk.VEs.Add(vr);

            Assert.IsTrue(hk.VEs.Contains(vr));
        }
    }
}
