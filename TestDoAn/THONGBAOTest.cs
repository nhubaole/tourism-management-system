using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn
{
    [TestClass]
    public class THONGBAOTest
    {
        [TestMethod]
        public void TestMATBProperty()
        {
            var tb = new THONGBAO();
            string ma = "TB000001";
            tb.MATB = ma;

            Assert.AreEqual(ma, tb.MATB);
        }

        [TestMethod]
        public void TestTHONGBAO1Property()
        {
            var tb = new THONGBAO();
            string tb1 = "Thông báo lỗi";
            tb.THONGBAO1 = tb1;

            Assert.AreEqual(tb1, tb.THONGBAO1);
        }

        [TestMethod]
        public void TestTHOIGIANProperty()
        {
            var tb = new THONGBAO();
            DateTime tg = DateTime.Now;
            tb.THOIGIAN = tg;

            Assert.AreEqual(tg, tb.THOIGIAN);
        }

        [TestMethod]
        public void TestDADOCProperty()
        {
            var tb = new THONGBAO();
            bool dd = true;
            tb.DADOC = dd;

            Assert.AreEqual(dd, tb.DADOC);
        }
    }
}
