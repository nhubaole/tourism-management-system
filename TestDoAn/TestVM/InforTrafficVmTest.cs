using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.ViewModel;

namespace TestDoAn.TestVM
{
    [TestClass]
    public class InforTrafficVmTest
    {
        [TestMethod]
        public void Constructor_Should_Initialize_Properties_From_InforServiceVM()
        {
            // Arrange
            InforServiceVM.MaPT = "InitialMaPT";
            InforServiceVM.TenPT = "InitialTenPT";
            InforServiceVM.SLG = 40;

            // Act
            var inforTrafficVm = new InforTrafficVm();

            // Assert
            Assert.AreEqual("InitialMaPT", inforTrafficVm.maPT);
            Assert.AreEqual("InitialTenPT", inforTrafficVm.tenPT);
            Assert.AreEqual(40, inforTrafficVm.SLGhe);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Update_InforServiceVM_Properties()
        {
            // Arrange
            var inforServiceVM = new InforServiceVM();
            var inforTrafficVM = new InforTrafficVm();

            inforTrafficVM.maPT = "NewMaPT";
            inforTrafficVM.tenPT = "NewTenPT";
            inforTrafficVM.SLGhe = 42;

            // Assert
            Assert.AreEqual("NewMaPT", InforServiceVM.MaPT);
            Assert.AreEqual("NewTenPT", InforServiceVM.TenPT);
            Assert.AreEqual(42, InforServiceVM.SLG);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Be_Raised_For_maPT_Property()
        {
            // Arrange
            var inforTrafficVm = new InforTrafficVm();
            var propertyChangedRaised = false;

            inforTrafficVm.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(InforTrafficVm.maPT))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            inforTrafficVm.maPT = "NewValue";

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Be_Raised_For_tenPT_Property()
        {
            // Arrange
            var inforTrafficVm = new InforTrafficVm();
            var propertyChangedRaised = false;

            inforTrafficVm.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(InforTrafficVm.tenPT))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            inforTrafficVm.tenPT = "NewValue";

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Be_Raised_For_SLGhe_Property()
        {
            // Arrange
            var inforTrafficVm = new InforTrafficVm();
            var propertyChangedRaised = false;

            inforTrafficVm.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(InforTrafficVm.SLGhe))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            inforTrafficVm.SLGhe = 42;

            // Assert
            Assert.IsTrue(propertyChangedRaised);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Update_InforServiceVM_For_maPT_Property()
        {
            // Arrange
            var inforTrafficVm = new InforTrafficVm();

            // Act
            inforTrafficVm.maPT = "NewValue";

            // Assert
            Assert.AreEqual("NewValue", InforServiceVM.MaPT);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Update_InforServiceVM_For_tenPT_Property()
        {
            // Arrange
            var inforTrafficVm = new InforTrafficVm();

            // Act
            inforTrafficVm.tenPT = "NewValue";

            // Assert
            Assert.AreEqual("NewValue", InforServiceVM.TenPT);
        }

        [TestMethod]
        public void PropertyChanged_Event_Should_Update_InforServiceVM_For_SLGhe_Property()
        {
            // Arrange
            var inforTrafficVm = new InforTrafficVm();

            // Act
            inforTrafficVm.SLGhe = 42;

            // Assert
            Assert.AreEqual(42, InforServiceVM.SLG);
        }
    }
}
