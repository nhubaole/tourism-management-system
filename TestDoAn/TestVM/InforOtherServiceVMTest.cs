using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.ViewModel;

namespace TestDoAn.TestModel
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

        [TestMethod]
        public void Constructor_Should_Initialize_Properties_From_InforServiceVM()
        {
            // Arrange
            InforServiceVM.MaDVK = "InitialMaDVK";
            InforServiceVM.TenDVK = "InitialTenDVK";
            InforServiceVM.MoTaDVK = "InitialMOTA";

            // Act
            var inforOTherServiceVM = new InforOTherServiceVM();

            // Assert
            Assert.AreEqual("InitialMaDVK", inforOTherServiceVM.maDVK);
            Assert.AreEqual("InitialTenDVK", inforOTherServiceVM.tenDVK);
            Assert.AreEqual("InitialMOTA", inforOTherServiceVM.moTaDVK);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Update_InforServiceVM_Properties()
        {
            // Arrange
            var inforOTherServiceVM = new InforOTherServiceVM();

            inforOTherServiceVM.maDVK = "NewMaDVK";
            inforOTherServiceVM.tenDVK = "NewTenDVK";
            inforOTherServiceVM.moTaDVK = "NewMOTA";

            // Assert
            Assert.AreEqual("NewMaDVK", InforServiceVM.MaDVK);
            Assert.AreEqual("NewTenDVK", InforServiceVM.TenDVK);
            Assert.AreEqual("NewMOTA", InforServiceVM.MoTaDVK);

        }
        [TestMethod]
        public void PropertyChanged_Event_Should_Be_Raised_For_maDVK_Property()
        {
            // Arrange
            var inforOTherServiceVm = new InforOTherServiceVM();
            var propertyChangedRaised = false;

            inforOTherServiceVm.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(InforOTherServiceVM.maDVK))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            inforOTherServiceVm.maDVK = "NewValue";

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Be_Raised_For_tenDVK_Property()
        {
            // Arrange
            var inforOTherServiceVm = new InforOTherServiceVM();
            var propertyChangedRaised = false;

            inforOTherServiceVm.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(InforOTherServiceVM.tenDVK))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            inforOTherServiceVm.tenDVK = "NewValue";

            // Assert
            Assert.IsTrue(propertyChangedRaised, "PropertyChanged event should be raised for tenDVK property");
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Be_Raised_For_moTaDVK_Property()
        {
            // Arrange
            var inforOTherServiceVm = new InforOTherServiceVM();
            var propertyChangedRaised = false;

            inforOTherServiceVm.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(InforOTherServiceVM.moTaDVK))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            inforOTherServiceVm.moTaDVK = "NewValue";

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Update_InforServiceVM_For_maDVK_Property()
        {
            // Arrange
            var inforOTherServiceVm = new InforOTherServiceVM();

            // Act
            inforOTherServiceVm.maDVK = "NewValue";

            // Assert
            Assert.AreEqual("NewValue", InforServiceVM.MaDVK);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Update_InforServiceVM_For_tenDVK_Property()
        {
            // Arrange
            var inforOTherServiceVm = new InforOTherServiceVM();

            // Act
            inforOTherServiceVm.tenDVK = "NewValue";

            // Assert
            Assert.AreEqual("NewValue", InforServiceVM.TenDVK);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Update_InforServiceVM_For_moTaDVK_Property()
        {
            // Arrange
            var inforOTherServiceVm = new InforOTherServiceVM();

            // Act
            inforOTherServiceVm.moTaDVK = "NewValue";

            // Assert
            Assert.AreEqual("NewValue", InforServiceVM.MoTaDVK);
        }

    }
}
