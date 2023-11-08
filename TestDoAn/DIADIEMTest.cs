using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn
{
    [TestClass]
    public class DIADIEMTest
    {
        [TestMethod]
        public void TestMaDDProperty()
        {
            // Arrange
            var d = new DIADIEM();

            // Act
            d.MADD = "DD00000001";

            // Assert
            Assert.AreEqual("DD00000001", d.MADD);
        }
        [TestMethod]
        public void TestTENDDProperty()
        {
            var d = new DIADIEM();

            d.TENDD = "BMT";

            Assert.AreEqual("BMT", d.TENDD);
        }

        [TestMethod]
        public void TestDIACHIDDProperty()
        {
            var d = new DIADIEM();

            d.DIACHI = "01 Hồ Tùng Mậu";

            Assert.AreEqual("01 Hồ Tùng Mậu", d.DIACHI);

        }

        [TestMethod]
        public void TestMOTAProperty()
        {
            var d = new DIADIEM();

            d.MOTA = "Phù hợp 50 người";

            Assert.AreEqual("Phù hợp 50 người", d.MOTA);
        }

        [TestMethod]    
        public void TestLICHTRINHsProperty() 
        {
            var d = new DIADIEM();
            var lich = new LICHTRINH();
            d.LICHTRINHs.Add(lich);

            Assert.IsTrue(d.LICHTRINHs.Contains(lich));
        }

        [TestMethod]
        public void TestLICHTRINH1sProperty() 
        {
            var d = new DIADIEM();
            var lich1 = new LICHTRINH();
            d.LICHTRINHs1.Add(lich1);

            Assert.IsTrue(d.LICHTRINHs1.Contains(lich1));
        }

        [TestMethod]
        public void TestTUYENsValue()
        {
            var d = new DIADIEM();

            var tuyen = new TUYEN();    

            d.TUYENs.Add(tuyen);

            Assert.IsTrue(d.TUYENs.Contains(tuyen));

        }
    }
}
