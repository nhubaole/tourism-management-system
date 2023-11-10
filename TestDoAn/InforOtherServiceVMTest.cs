using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.ViewModel;

namespace TestDoAn
{
    [TestClass]
    public class InforOtherServiceVMTest
    {

        [TestMethod]
        public void PropertyChanged_MaDVK_UpdateInforServiceVM()
        {
            // Arrange
            var viewModel = new InforOTherServiceVM();
            string maDV = "DV00000002";

            // Act
            viewModel.maDVK = maDV;

            // Assert
            Assert.AreEqual(maDV, InforServiceVM.MaDVK);
        }

        [TestMethod]
        public void PropertyChanged_TenDVK_UpdateInforServiceVM()
        {
            // Arrange
            var viewModel = new InforOTherServiceVM();
            string tenDVK = "Mát xa thư giãn";
            // Act
            viewModel.tenDVK = tenDVK;

            // Assert
            Assert.AreEqual(tenDVK, InforServiceVM.TenDVK);
        }

        [TestMethod]
        public void PropertyChanged_MoTaDVK_UpdateInforServiceVM()
        {
            // Arrange
            var viewModel = new InforOTherServiceVM();
            string moTaDVK = "5tr một người";

            // Act
            viewModel.moTaDVK = moTaDVK;

            // Assert
            Assert.AreEqual(moTaDVK, InforServiceVM.MoTaDVK);
        }

    }
}
