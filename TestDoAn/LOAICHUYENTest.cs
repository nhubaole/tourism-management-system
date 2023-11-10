using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn
{
    [TestClass]
    public class LOAICHUYENTest
    {
        [TestMethod]
        public void TestMALOAIProperty ()
        {
            var lc = new LOAICHUYEN();
            string ma = "LC0000001";

            lc.MALOAI = ma;

            Assert.AreEqual (ma, lc.MALOAI);    

        }

        [TestMethod]
        public void TestTENLOAIProperty()
        {
            var lc = new LOAICHUYEN();
            string ten = "Khuyến mãi";
            lc.TENLOAI = ten;

            Assert.AreEqual(ten, lc.TENLOAI);

        }

        [TestMethod]
        public void TestTILEHOANTRAProperty()
        {
            var lc = new LOAICHUYEN();
            int tile = 40;
            lc.TILEHOANTRA = tile;

            Assert.AreEqual(tile, lc.TILEHOANTRA);

        }

        [TestMethod]
        public void TestCHUYENsProperty()
        {
            var lc = new LOAICHUYEN();
            var chuyen = new CHUYEN();

            lc.CHUYENs.Add(chuyen);

            Assert.IsTrue(lc.CHUYENs.Contains(chuyen));

        }
    }
}
