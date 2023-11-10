using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn
{
    [TestClass]
    public class TUYENTest
    {
        [TestMethod]
        public void TestMATUYENProperty()
        {
            var t = new TUYEN();
            string ma = "T000001";
            t.MATUYEN = ma;

            Assert.AreEqual(ma, t.MATUYEN);
        }

        [TestMethod]
        public void TestTENTUYENProperty()
        {
            var t = new TUYEN();
            string ten= "Nha Trang - Đà Lạt";
            t.TENTUYEN = ten;

            Assert.AreEqual(ten, t.TENTUYEN);
        }

        [TestMethod]
        public void TestDDXUATPHATProperty()
        {
            var t = new TUYEN();
            string ddxp = "15 Hoàng Kế Viêm, phường 12, quận Tân Bình";
            t.DDXUATPHAT = ddxp;

            Assert.AreEqual(ddxp, t.DDXUATPHAT);
        }

        [TestMethod]
        public void TestDIADUKIENProperty()
        {
            var t = new TUYEN();
            int gdk = 5000000;
            t.GIADUKIEN = gdk;

            Assert.AreEqual(gdk, t.GIADUKIEN);
        }

        [TestMethod]
        public void TestSONGAYProperty()
        {
            var t = new TUYEN();
            int sn = 4;
            t.SONGAY = sn ;

            Assert.AreEqual(sn, t.SONGAY);
        }

        [TestMethod]
        public void TestSODEMProperty()
        {
            var t = new TUYEN();
            int sd = 3;
            t.SODEM = sd;

            Assert.AreEqual(sd, t.SODEM);
        }

        [TestMethod]
        public void TestMALOAIProperty()
        {
            var t = new TUYEN();
            string maloai = "LT000001";
            t.MALOAI = maloai;

            Assert.AreEqual(maloai, t.MALOAI);
        }

        [TestMethod]
        public void TestCHUYENsProperty()
        {
            var t = new TUYEN();
            CHUYEN chuyen = new CHUYEN();
            t.CHUYENs.Add(chuyen);

            Assert.IsTrue(t.CHUYENs.Contains(chuyen));
        }

        [TestMethod]
        public void TestDIADIEMProperty()
        {
            var t = new TUYEN();
            DIADIEM dd= new DIADIEM();
            t.DIADIEM = dd;

            Assert.AreEqual(dd, t.DIADIEM);
        }

        [TestMethod]
        public void TestLICHTRINHsProperty()
        {
            var t = new TUYEN();
            LICHTRINH lich = new LICHTRINH();
            t.LICHTRINHs.Add(lich);

            Assert.IsTrue(t.LICHTRINHs.Contains(lich));
        }

        [TestMethod]
        public void TestLOAITUYENProperty()
        {
            var t = new TUYEN();
            LOAITUYEN lt = new LOAITUYEN();
            t.LOAITUYEN = lt;

            Assert.AreEqual(lt, t.LOAITUYEN);
        }
    }
}
