using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;
namespace TestDoAn
{
    [TestClass]
    public class HUONGDANVIENTest
    {
        [TestMethod]

        public void TestMAHDVProperty()
        {
            var hdv = new HUONGDANVIEN();
            string ma = "HDV00001";
            hdv.MAHDV = ma;

            Assert.AreEqual(ma, hdv.MAHDV);

        }

        [TestMethod]

        public void TestHOTENProperty()
        {
            var hdv = new HUONGDANVIEN();
            string ten = "Nguyễn Văn Bá";
            hdv.HOTEN = ten;

            Assert.AreEqual(ten, hdv.HOTEN);
        }

        [TestMethod]

        public void TestGIOITINHProperty()
        {
            var hdv = new HUONGDANVIEN();
            string gt = "Nam";
            hdv.GIOITINH = gt;

            Assert.AreEqual(gt, hdv.GIOITINH);
        }
        [TestMethod]

        public void TestSDTProperty()
        {
            var hdv = new HUONGDANVIEN();
            string sdt = "09004343234";
            hdv.SDT = sdt;

            Assert.AreEqual (sdt, hdv.SDT);
        }
        [TestMethod]

        public void TestDIACHIProperty()
        {
            var hdv = new HUONGDANVIEN();
            string dc = "01 Nguyễn Văn BÁ";
            hdv.DIACHI = dc;    

            Assert.AreEqual (@dc, hdv.DIACHI);  
        }

        [TestMethod]

        public void TestNGSINHProperty()
        {
            var hdv = new HUONGDANVIEN();

            hdv.NGSINH = DateTime.Now;

            Assert.IsNotNull (hdv.NGSINH);  
        }

        [TestMethod]

        public void TestCHUYENsProperty()
        {

            var hdv = new HUONGDANVIEN();
            var chuyen = new CHUYEN();

            hdv.CHUYENs.Add (chuyen);

            Assert.IsTrue(hdv.CHUYENs.Contains (chuyen));

        }
    }
}
