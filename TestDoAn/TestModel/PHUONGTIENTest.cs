using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn.TestModel
{
    [TestClass]
    public class PHUONGTIENTest
    {
        [TestMethod]
        public void TestMAPTProperty()
        {
            var pt = new PHUONGTIEN();
            string ma = "PT000001";
            pt.MAPT = ma;

            Assert.AreEqual(ma, pt.MAPT);
        }

        [TestMethod]
        public void TestTENPTProperty()
        {
            var pt = new PHUONGTIEN();
            string ten = "Máy bay";
            pt.TENPT = ten;

            Assert.AreEqual(ten, pt.TENPT);
        }

        [TestMethod]
        public void TestSLGHEProperty()
        {
            var pt = new PHUONGTIEN();
            int slg = 15;
            pt.SLGHE = slg;

            Assert.AreEqual(slg, pt.SLGHE);
        }

        [TestMethod]
        public void TestLICHTRINHsProperty()
        {
            var pt = new PHUONGTIEN();
            LICHTRINH lich = new LICHTRINH();
            pt.LICHTRINHs.Add(lich);

            Assert.IsTrue(pt.LICHTRINHs.Contains(lich));
        }
    }
}
