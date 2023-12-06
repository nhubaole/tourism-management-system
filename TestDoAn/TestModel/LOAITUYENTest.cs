using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn.TestModel
{
    [TestClass]
    public class LOAITUYENTest
    {
        [TestMethod]
        public void TestMALOAIProperty()
        {
            var lt = new LOAITUYEN();
            string ma = "LT0000001";
            lt.MALOAI = ma;

            Assert.AreEqual(ma, lt.MALOAI);
        }

        [TestMethod]
        public void TestTENLOAIProperty()
        {
            var lt = new LOAITUYEN();
            string ten = "Khuyến mãi";
            lt.TENLOAI = ten;

            Assert.AreEqual(ten, lt.TENLOAI);
        }

        [TestMethod]
        public void TestTGMUATRUOCKHOIHANHProperty()
        {
            var lt = new LOAITUYEN();
            int tgmuatruockhoihanh = 24;
            lt.TGMUATRUOCKHOIHANH = tgmuatruockhoihanh;

            Assert.AreEqual(tgmuatruockhoihanh, lt.TGMUATRUOCKHOIHANH);
        }

        [TestMethod]
        public void TestTGHOANVEMIENPHIProperty()
        {
            var lt = new LOAITUYEN();
            int tghoanvemienphi = 24;
            lt.TGHOANVEMIENPHI = tghoanvemienphi;

            Assert.AreEqual(tghoanvemienphi, lt.TGHOANVEMIENPHI);
        }

        [TestMethod]
        public void TestLEPHIHOANVETREProperty()
        {
            var lt = new LOAITUYEN();
            int lephi = 500000;
            lt.LEPHIHOANVETRE = lephi;

            Assert.AreEqual(lephi, lt.LEPHIHOANVETRE);
        }

        [TestMethod]
        public void TestTUYENsProperty()
        {
            var lt = new LOAITUYEN();
            var tuyen = new TUYEN();

            lt.TUYENs.Add(tuyen);

            Assert.IsTrue(lt.TUYENs.Contains(tuyen));

        }
    }
}
