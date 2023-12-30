using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn.TestModel
{
    [TestClass]
    public class KHACHSANTest
    {
        [TestMethod]
        public void TestMAKSProperty()
        {
            var ks = new KHACHSAN();
            string ma = "KS000001";

            ks.MAKS = ma;

            Assert.AreEqual(ma, ks.MAKS);
        }

        [TestMethod]
        public void TestTenKSProperty()
        {
            var ks = new KHACHSAN();
            string ten = "Khách sạn ánh dưog";

            ks.TENKS = ten;

            Assert.AreEqual(ten, ks.TENKS);

        }

        [TestMethod]
        public void TestSDTKSProperty()
        {
            var ks = new KHACHSAN();
            string sdt = "04534353123";

            ks.SDT = sdt;

            Assert.AreEqual(sdt, ks.SDT);

        }

        [TestMethod]
        public void TestDIACHIProperty()
        {
            var ks = new KHACHSAN();
            string dc = "01 dương văn nga";

            ks.DIACHI = dc;

            Assert.AreEqual(dc, ks.DIACHI);
        }

        [TestMethod]
        public void TestSOSAOProperty()
        {
            var ks = new KHACHSAN();
            int sao = 3;
            ks.SOSAO = sao;

            Assert.AreEqual(ks.SOSAO, sao);
        }

        [TestMethod]
        public void TestSUCCHUAProperty()
        {
            var ks = new KHACHSAN();
            int sc = 3;
            ks.SOSAO = sc;

            Assert.AreEqual(ks.SOSAO, sc);

        }

        [TestMethod]
        public void TestLICHTRINHsProperty()
        {
            var ks = new KHACHSAN();
            var lich = new LICHTRINH();

            ks.LICHTRINHs.Add(lich);

            Assert.IsTrue(ks.LICHTRINHs.Contains(lich));

        }


    }
}
