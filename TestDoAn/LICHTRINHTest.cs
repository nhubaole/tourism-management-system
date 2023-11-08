using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn
{
    [TestClass]
    public class LICHTRINHTest
    {
        [TestMethod]
        public void TestMALTProperty()
        {
            var lt = new LICHTRINH();
            string ma = "LT0000001";

            lt.MALT = ma;

            Assert.AreEqual(ma, lt.MALT);       

        }

        [TestMethod]
        public void TestMATUYENProperty()
        {
            var lt = new LICHTRINH();
            string tuyen = "T0000001";
            lt.MATUYEN = tuyen;

            Assert.AreEqual(tuyen, lt.MATUYEN);
        }

        [TestMethod]
        public void TestDDBATDAUProperty()
        {
            var lt = new LICHTRINH();
            string dd = "An Giang";
            lt.DDBATDAU = dd;

            Assert.AreEqual(dd, lt.DDBATDAU);
        }

        [TestMethod]
        public void TestDDKETTHUCProperty()
        {
            var lt = new LICHTRINH();
            string dd = "Phú Yên";

            lt.DDKETTHUC = dd;

            Assert.AreEqual(dd, lt.DDKETTHUC);

        }

        [TestMethod]
        public void TestNGAYTHUProperty()
        {
            var lt = new LICHTRINH();
            int ngayThu = 2;

            lt.NGAYTHU = ngayThu;

            Assert.AreEqual(ngayThu, lt.NGAYTHU);

        }

        [TestMethod]
        public void TestDIADIEMProperty()
        {
            var lt = new LICHTRINH();
            var dd = new DIADIEM();

            lt.DIADIEM = dd;

            Assert.AreEqual(lt.DIADIEM, dd);

        }

        [TestMethod]
        public void TestDIADIEM1Property()
        {
            var lt = new LICHTRINH();
            var dd1 = new DIADIEM();

            lt.DIADIEM1 = dd1;

            Assert.AreEqual(lt.DIADIEM1, dd1);

        }

        [TestMethod]
        public void TestTUYENProperty()
        {
            var lt = new LICHTRINH();
            var tuyen = new TUYEN();

            lt.TUYEN = tuyen;

            Assert.AreEqual(tuyen, lt.TUYEN);

        }

        [TestMethod]
        public void TestDICHVUKHACsProperty()
        {
            var lt = new LICHTRINH();
            var dvk = new DICHVUKHAC();

            lt.DICHVUKHACs.Add(dvk);

            Assert.IsTrue(lt.DICHVUKHACs.Contains(dvk));
        }

        [TestMethod]
        public void TestKHACHSANsProperty()
        {
            var lt = new LICHTRINH();
            var ks = new KHACHSAN();   

            lt.KHACHSANs.Add(ks);

            Assert.IsTrue(lt.KHACHSANs.Contains(ks));   

        }

        [TestMethod]
        public void TestNHAHANGsProperty()
        {
            var lt = new LICHTRINH();
            var nh = new NHAHANG();

            lt.NHAHANGs.Add(nh);

            Assert.IsTrue(lt.NHAHANGs.Contains(nh));    

        }

        [TestMethod]
        public void TestPHUONGTIENsProperty()
        {
            var lt = new LICHTRINH();
            var pt = new PHUONGTIEN();

            lt.PHUONGTIENs.Add(pt); 

            Assert.IsTrue(lt.PHUONGTIENs.Contains(pt));

        }
    }
}
