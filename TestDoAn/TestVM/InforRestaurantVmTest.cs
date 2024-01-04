using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.ViewModel;

namespace TestDoAn.TestVM
{
    [TestClass]
    public class InforRestaurantVmTest
    {
        [TestMethod]
        public void Constructor_Should_Initialize_Properties_From_InforServiceVM()
        {
            // Arrange
            InforServiceVM.MaNH = "InitialMaNH";
            InforServiceVM.TenNH = "InitialTenNH";
            InforServiceVM.SDTNH = "InitialSDT";
            InforServiceVM.MoTaNH = "InitialMoTa";

            // Act
            var inforRestaurantVm = new InforRestaurantVm();

            // Assert
            Assert.AreEqual("InitialMaNH", inforRestaurantVm.maNH);
            Assert.AreEqual("InitialTenNH", inforRestaurantVm.tenNH);
            Assert.AreEqual("InitialSDT", inforRestaurantVm.sdt);
            Assert.AreEqual("InitialMoTa", inforRestaurantVm.moTa);
        } 

        [TestMethod]
        public void PropertyChanged_Event_Should_Update_InforServiceVM_Properties()
        {
            // Arrange
            var inforServiceVM = new InforServiceVM();
            var inforRestaurantVm = new InforRestaurantVm();

            inforRestaurantVm.maNH = "NewMaNH";
            inforRestaurantVm.tenNH = "NewTenNH";
            inforRestaurantVm.sdt = "NewSDT";
            inforRestaurantVm.moTa = "NewMoTa";

            // Assert
            Assert.AreEqual("NewMaNH", InforServiceVM.MaNH);
            Assert.AreEqual("NewTenNH", InforServiceVM.TenNH);
            Assert.AreEqual("NewSDT", InforServiceVM.SDTNH);
            Assert.AreEqual("NewMoTa", InforServiceVM.MoTaNH);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Update_InforServiceVM_Properties_When_Multiple_Instances()
        {
            // Arrange
            var inforServiceVM = new InforServiceVM();
            var inforRestaurantVm1 = new InforRestaurantVm();
            var inforRestaurantVm2 = new InforRestaurantVm();

            // Change properties in the first instance
            inforRestaurantVm1.maNH = "NewMaNH1";
            inforRestaurantVm1.tenNH = "NewTenNH1";
            inforRestaurantVm1.sdt = "NewSDT1";
            inforRestaurantVm1.moTa = "NewMoTa1";

            // Assert properties are updated in InforServiceVM
            Assert.AreEqual("NewMaNH1", InforServiceVM.MaNH);
            Assert.AreEqual("NewTenNH1", InforServiceVM.TenNH);
            Assert.AreEqual("NewSDT1", InforServiceVM.SDTNH);
            Assert.AreEqual("NewMoTa1", InforServiceVM.MoTaNH);

            // Change properties in the second instance
            inforRestaurantVm2.maNH = "NewMaNH2";
            inforRestaurantVm2.tenNH = "NewTenNH2";
            inforRestaurantVm2.sdt = "NewSDT2";
            inforRestaurantVm2.moTa = "NewMoTa2";

            // Assert properties are updated in InforServiceVM
            Assert.AreEqual("NewMaNH2", InforServiceVM.MaNH);
            Assert.AreEqual("NewTenNH2", InforServiceVM.TenNH);
            Assert.AreEqual("NewSDT2", InforServiceVM.SDTNH);
            Assert.AreEqual("NewMoTa2", InforServiceVM.MoTaNH);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Be_Subscribed_and_Unsubscribed_Correctly()
        {
            // Arrange
            var inforRestaurantVm = new InforRestaurantVm();
            var handlerWasCalled = false;

            // Act
            PropertyChangedEventHandler handler = (sender, args) => handlerWasCalled = true;
            inforRestaurantVm.PropertyChanged += handler;

            // Simulate property change to trigger the event
            inforRestaurantVm.maNH = "NewValue";

            // Assert
            Assert.IsTrue(handlerWasCalled, "PropertyChanged event should be subscribed and handler should be called");

            // Reset flag
            handlerWasCalled = false;

            // Act
            inforRestaurantVm.PropertyChanged -= handler;

            // Simulate property change to trigger the event
            inforRestaurantVm.tenNH = "AnotherValue";

            // Assert
            Assert.IsFalse(handlerWasCalled, "PropertyChanged event should be unsubscribed and handler should not be called");
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Be_Raised_For_maNH_Property()
        {
            // Arrange
            var inforRestaurantVm = new InforRestaurantVm();
            var propertyChangedRaised = false;

            inforRestaurantVm.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(InforRestaurantVm.maNH))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            inforRestaurantVm.maNH = "NewValue";

            // Assert
            Assert.IsTrue(propertyChangedRaised, "PropertyChanged event should be raised for maNH property");
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Be_Raised_For_tenNH_Property()
        {
            // Arrange
            var inforRestaurantVm = new InforRestaurantVm();
            var propertyChangedRaised = false;

            inforRestaurantVm.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(InforRestaurantVm.tenNH))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            inforRestaurantVm.tenNH = "NewValue";

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Be_Raised_For_sdt_Property()
        {
            // Arrange
            var inforRestaurantVm = new InforRestaurantVm();
            var propertyChangedRaised = false;

            inforRestaurantVm.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(InforRestaurantVm.sdt))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            inforRestaurantVm.sdt = "NewValue";

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Be_Raised_For_moTa_Property()
        {
            // Arrange
            var inforRestaurantVm = new InforRestaurantVm();
            var propertyChangedRaised = false;

            inforRestaurantVm.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(InforRestaurantVm.moTa))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            inforRestaurantVm.moTa = "NewValue";

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Update_InforServiceVM_For_maNH_Property()
        {
            // Arrange
            var inforRestaurantVm = new InforRestaurantVm();

            // Act
            inforRestaurantVm.maNH = "NewValue";

            // Assert
            Assert.AreEqual("NewValue", InforServiceVM.MaNH);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Update_InforServiceVM_For_tenNH_Property()
        {
            // Arrange
            var inforRestaurantVm = new InforRestaurantVm();

            // Act
            inforRestaurantVm.tenNH = "NewValue";

            // Assert
            Assert.AreEqual("NewValue", InforServiceVM.TenNH);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Update_InforServiceVM_For_sdt_Property()
        {
            // Arrange
            var inforRestaurantVm = new InforRestaurantVm();

            // Act
            inforRestaurantVm.sdt = "NewValue";

            // Assert
            Assert.AreEqual("NewValue", InforServiceVM.SDTNH);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Update_InforServiceVM_For_moTa_Property()
        {
            // Arrange
            var inforRestaurantVm = new InforRestaurantVm();

            // Act
            inforRestaurantVm.moTa = "NewValue";

            // Assert
            Assert.AreEqual("NewValue", InforServiceVM.MoTaNH);
        }
    }
}

