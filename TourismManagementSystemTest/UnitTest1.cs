using TourismManagementSystem.ViewModel;

namespace TourismManagementSystemTest
{
    [TestClass]
    public class InforLocationVMTest
    {
        [TestMethod]
        public void TestGenerateCode_WithoutPreviousCode()
        {
            // Arrange

            // Act
            string generatedCode = InforLocationVM.GenerateCode();

            // Assert
            // In this case, since there is no previous code, the generated code should be "DD000001".
            Assert.AreEqual("DD000001", generatedCode);
        }

        [TestMethod]
        public void TestGenerateCode_WithPreviousCode()
        {
            // Arrange
            string previousCode = "DD000005";

            // Act
            string generatedCode = InforLocationVM.GenerateCode(previousCode);

            // Assert
            // In this case, the generated code should be "DD000006" because it increments the number.
            Assert.AreEqual("DD000006", generatedCode);
        }
    }
}