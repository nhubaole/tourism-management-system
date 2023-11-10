using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn
{
    [TestClass]
    public class NHAHANGTest
    {
        [TestMethod]
        public void TestMANHAHANGProperty()
        {
            var nh = new NHAHANG();
            string ma = "NH000001";
            nh.MANH = ma;

            Assert.AreEqual(ma, nh.MANH);
        }

        [TestMethod]
        public void TestTENNHAHANGProperty()
        {
            var nh = new NHAHANG();
            string ten = "Nhà hàng Vịnh Hạ Long";
            nh.TENNH = ten;

            Assert.AreEqual(ten, nh.TENNH);
        }

        [TestMethod]
        public void TestSDTNHAHANGProperty()
        {
            var nh = new NHAHANG();
            string sdt = "0344004890";
            nh.SDT = sdt;

            Assert.AreEqual(sdt, nh.SDT);
        }

        [TestMethod]
        public void TestMOTANHAHANGProperty()
        {
            var nh = new NHAHANG();
            string mt = "5 sao";
            nh.MOTA = mt;

            Assert.AreEqual(mt, nh.MOTA);
        }

        [TestMethod]
        public void TestLICHTRINHsProperty()
        {
            var nh = new NHAHANG();
            var lich = new LICHTRINH();

            nh.LICHTRINHs.Add(lich);

            Assert.IsTrue(nh.LICHTRINHs.Contains(lich));

        }
    }
}
