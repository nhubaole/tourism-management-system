using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn
{
    [TestClass]
    public class KHACHHANGTest
    {
        [TestMethod]
        public void TestMAKHProperty()
        {
            var kh = new KHACHHANG();
            string ma = "KH000001";
            kh.MAKH = ma;

            Assert.AreEqual(ma, kh.MAKH);
        }

        [TestMethod]
        public void TestHOTENProperty()
        {
            var kh = new KHACHHANG();
            string ten = "Nguyễn Văn A";
            kh.HOTEN = ten;

            Assert.AreEqual(ten, kh.HOTEN);
        }

        [TestMethod]
        public void TestCCCDProperty()
        {
            var kh = new KHACHHANG();
            string cccd = "32343424343";
            kh.CCCD = cccd;

            Assert.AreEqual(cccd, kh.CCCD);
        }
        [TestMethod]
        public void TestSDTProperty()
        {
            var kh = new KHACHHANG();
            string sdt = "034223423423";

            kh.SDT = sdt;

            Assert.AreEqual(sdt, kh.SDT);
        }

        [TestMethod]
        public void TestEMAILProperty()
        {
            var kh = new KHACHHANG();
            string email = "abinh@gmail.com";
            kh.EMAIL = email;   

            Assert.AreEqual(email, kh.EMAIL);   
        }

        [TestMethod]
        public void TestDIACHIProperty()
        {
            var kh = new KHACHHANG();
            string dc = "01 Dương Văn Nga";

            kh.DIACHI = dc; 

            Assert.AreEqual(dc, kh.DIACHI);
        }


        [TestMethod]
        public void TestPHIEUDATCHOsProperty()
        {
            var kh = new KHACHHANG();
            var phieu = new PHIEUDATCHO();  

            kh.PHIEUDATCHOes.Add(phieu);

            Assert.IsTrue(kh.PHIEUDATCHOes.Contains(phieu));    
        }
    }

    
}
