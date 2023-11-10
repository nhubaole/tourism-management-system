using TourismManagementSystem.Model;
using TourismManagementSystem.ViewModel;
namespace TestDoAn
{
    [TestClass]
    public class InforLocationVMTest
    {

        [TestMethod]
        public void TestMaDDProperty()
        {
            // Arrange
            var d = new InforLocationVM();

            // Act
            d.maDD = "DD00000001";

            // Assert
            Assert.AreEqual("DD00000001", d.maDD);
        }
        [TestMethod]
        public void TestTENDDProperty()
        {
            var d = new InforLocationVM();

            d.tenDD = "BMT";

            Assert.AreEqual("BMT", d.tenDD);
        }

        [TestMethod]
        public void TestDIACHIDDProperty()
        {
            var d = new InforLocationVM();

            d.dcDD = "01 Hồ Tùng Mậu";

            Assert.AreEqual("01 Hồ Tùng Mậu", d.dcDD);

        }

        [TestMethod]
        public void TestMOTAProperty()
        {
            var d = new InforLocationVM();

            d.mtDD = "Phù hợp 50 người";

            Assert.AreEqual("Phù hợp 50 người", d.mtDD);
        }


        [TestMethod]
        [DataRow(null, "DD000001")]
        [DataRow("DD000005", "DD000006")]
        [DataRow("DD000009", "DD000010")]
        [DataRow("DD000010", "DD000011")]
        [DataRow("DD000099", "DD000100")]
        public void TestGenerateCode(string previousCode, string expectedCode)
        {
            // Act
            string generatedCode = InforLocationVM.GenerateCode(previousCode);

            // Assert
            Assert.AreEqual(expectedCode, generatedCode);
        }

        [TestMethod]
        [DataRow("            ", true)]
        [DataRow("", true)]
        [DataRow("Không phải là cách ", false)]
        [DataRow(" Không phải cách ", false)]
        [DataRow("adasdasdas ", false)]
        [DataRow(null, true)]
        [DataRow(" Không phải cách ", false)]



        public void TestIsAllWhiteSpace(string s, bool expect)
        {
            bool actual = InforLocationVM.IsAllWhitespace(s);
            Assert.AreEqual(expect, actual);

        }
    }


}