using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TestDoAn
{
    [TestClass]

    public class CHUYENTest
    {
        [TestMethod]
        public void TestMACHUYENProperty()
        {
            // Arrange
            var chuyen = new CHUYEN();

            // Act
            chuyen.MACHUYEN = "CH000001";

            // Assert
            Assert.AreEqual("CH000001", chuyen.MACHUYEN);
        }

        [TestMethod]
        public void TestTGBATDAUProperty()
        {
            // Arrange
            var chuyen = new CHUYEN();

            // Act
            chuyen.TGBATDAU = DateTime.Now;

            // Assert
            Assert.IsNotNull(chuyen.TGBATDAU);
        }

        [TestMethod]
        public void TestTGKETTHUCProperty()
        {
            // Arrange
            var chuyen = new CHUYEN();

            // Act
            chuyen.TGKETTHUC = DateTime.Now;

            // Assert
            Assert.IsNotNull(chuyen.TGKETTHUC);
        }

        [TestMethod]
        public void TestSLTOITHIEUProperty()
        {
            // Arrange
            var chuyen = new CHUYEN();

            // Act
            chuyen.SLTOITHIEU = 10;

            // Assert
            Assert.AreEqual(10, chuyen.SLTOITHIEU);
        }

        [TestMethod]
        public void TestSLTHUCTEProperty()
        {
            // Arrange
            var chuyen = new CHUYEN();

            // Act
            chuyen.SLTHUCTE = 100;

            // Assert
            Assert.AreEqual(100, chuyen.SLTHUCTE);
        }



        [TestMethod]
        public void TestGIAVEProperty()
        {
            // Arrange
            var chuyen = new CHUYEN();

            // Act
            chuyen.GIAVE = 500;

            // Assert
            Assert.AreEqual(500, chuyen.GIAVE);
        }

        [TestMethod]
        public void TestMALOAIroperty()
        {
            // Arrange
            var chuyen = new CHUYEN();

            // Act
            chuyen.MALOAI = "Giảm giá";

            // Assert
            Assert.AreEqual("Giảm giá", chuyen.MALOAI);
        }

        [TestMethod]
        public void TestMATUYENroperty()
        {
            // Arrange
            var chuyen = new CHUYEN();

            // Act
            chuyen.MATUYEN = "T0000001";

            // Assert
            Assert.AreEqual("T0000001", chuyen.MATUYEN);
        }

        [TestMethod]
        public void TestTHANHCONGroperty()
        {
            // Arrange
            var chuyen = new CHUYEN();

            // Act
            chuyen.THANHCONG = true;

            // Assert
            Assert.AreEqual(true, chuyen.THANHCONG);
        }
    }
}
