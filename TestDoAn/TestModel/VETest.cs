using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn.TestModel
{
    [TestClass]
    public class VETest
    {
        [TestMethod]
        public void TestMAVEProperty()
        {
            var ve = new VE();
            string ma = "VE000001";
            ve.MAVE = ma;

            Assert.AreEqual(ma, ve.MAVE);
        }

        [TestMethod]
        public void TestMAHKProperty()
        {
            var ve = new VE();
            string mahk = "HK000001";
            ve.MAHK = mahk;

            Assert.AreEqual(mahk, ve.MAHK);
        }

        [TestMethod]
        public void TestMAPHIEUProperty()
        {
            var ve = new VE();
            string maphieu = "PH000001";
            ve.MAPHIEU = maphieu;

            Assert.AreEqual(maphieu, ve.MAPHIEU);
        }

        [TestMethod]
        public void TestTRANGTHAIProperty()
        {
            var ve = new VE();
            string tt = "SOLD";
            ve.TRANGTHAI = tt;

            Assert.AreEqual(tt, ve.TRANGTHAI);
        }

        [TestMethod]
        public void TestNGAYBANProperty()
        {
            var ve = new VE();
            DateTime nb = DateTime.Now;
            ve.NGAYBAN = nb;

            Assert.AreEqual(nb, ve.NGAYBAN);
        }

        [TestMethod]
        public void TestGIAVEProperty()
        {
            var ve = new VE();
            int gv = 3000000;
            ve.GIAVE = gv;

            Assert.AreEqual(gv, ve.GIAVE);
        }

        [TestMethod]
        public void TestHANHKHACHProperty()
        {
            var ve = new VE();
            HANHKHACH hk = new HANHKHACH();
            ve.HANHKHACH = hk;

            Assert.AreEqual(hk, ve.HANHKHACH);
        }

        [TestMethod]
        public void TestPHIEUDATCHOProperty()
        {
            var ve = new VE();
            PHIEUDATCHO pdc = new PHIEUDATCHO();
            ve.PHIEUDATCHO = pdc;

            Assert.AreEqual(pdc, ve.PHIEUDATCHO);
        }
    }
}
