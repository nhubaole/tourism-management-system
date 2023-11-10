using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn
{
    [TestClass]
    public class DICHVUKHACTest
    {
        [TestMethod]
        public void TestMADVProperty()
        {
            var dv = new DICHVUKHAC();
            string ma = "DV000001";

            dv.MADV = ma;

            Assert.AreEqual(ma, dv.MADV);
        }

        [TestMethod]
        public void TestTENDVProperty()
        {
            var dv = new DICHVUKHAC();
            string ten = "xe khach";

            dv.TENDV = ten;

            Assert.AreEqual(ten, dv.TENDV);
        }

        [TestMethod]
        public void TestMOTAProperty() 
        {
            var dv = new DICHVUKHAC();
            string mota = "tối đa 40 người";
            dv.MOTA = mota;

           Assert.AreEqual(mota, dv.MOTA);

        }

        [TestMethod]
        public void TestLICHTRINHSProperty()
        {
            var dv = new DICHVUKHAC();
            var lich = new LICHTRINH();

            dv.LICHTRINHs.Add(lich);

            Assert.IsTrue(dv.LICHTRINHs.Contains(lich));
        }
    }
}
