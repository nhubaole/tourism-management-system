using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;
using TourismManagementSystem.ViewModel;

namespace TestDoAn.TestVM
{
    [TestClass]
    public class BookDetailVMTest
    {

        [TestMethod]
        public void Phieu_SetProperty_TriggerOnPropertyChanged()
        {
            // Arrange
            var viewModel = new BookingDetailVM();
            bool propertyChanged = false;
            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Phieu")
                {
                    propertyChanged = true;
                }
            };

            // Act
            viewModel.Phieu = new PHIEUDATCHO();

            // Assert
            Assert.IsTrue(propertyChanged);
        }

        [TestMethod]
        public void Count_NoHanhKhach_ReturnsZero()
        {
            // Arrange
            BookingDetailVM.SelectedPhieu = new PHIEUDATCHO { HANHKHACHes = null };
            var viewModel = new BookingDetailVM();

            // Act
            int count = viewModel.Count;

            // Assert
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void Count_WithHanhKhach_ReturnsCorrectCount()
        {
            // Arrange
            BookingDetailVM.SelectedPhieu = new PHIEUDATCHO
            {
                HANHKHACHes = new ObservableCollection<HANHKHACH>
                {
                    new HANHKHACH(),
                    new HANHKHACH(),
                    new HANHKHACH()
                }
            };
            var viewModel = new BookingDetailVM();

            // Act
            int count = viewModel.Count;

            // Assert
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void ThanhToanCommand_CanExecute_WhenTinhTrangIsChuaThanhToan()
        {
            // Arrange
            var viewModel = new BookingDetailVM();
            BookingDetailVM.SelectedPhieu = new PHIEUDATCHO { TINHTRANG = "Chưa thanh toán" };

            // Act
            bool canExecute = viewModel.ThanhToanCommand.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute);
        }

        [TestMethod]
        public void ThanhToanCommand_CannotExecute_WhenTinhTrangIsDaThanhToan()
        {
            // Arrange
            BookingDetailVM.SelectedPhieu = new PHIEUDATCHO { TINHTRANG = "Đã thanh toán" };
            var viewModel = new BookingDetailVM();

            // Act
            bool canExecute = viewModel.ThanhToanCommand.CanExecute(null);

            // Assert
            Assert.IsFalse(canExecute);
        }

        [TestMethod]
        public void ThanhToanCommand_CannotExecute_WhenTinhTrangIsDẫutVe()
        {
            // Arrange
            BookingDetailVM.SelectedPhieu = new PHIEUDATCHO { TINHTRANG = "Đã xuất vé" };
            var viewModel = new BookingDetailVM();
            
            // Act
            bool canExecute = viewModel.ThanhToanCommand.CanExecute(null);

            // Assert
            Assert.IsFalse(canExecute);
        }

        [TestMethod]
        public void XuatVeCommand_CannotExecute_WhenTinhTrangIsChuaThanhToan()
        {
            // Arrange
            BookingDetailVM.SelectedPhieu = new PHIEUDATCHO { TINHTRANG = "Chưa thanh toán" };
            var viewModel = new BookingDetailVM();
            
            // Act
            bool canExecute = viewModel.XuatVeCommand.CanExecute(null);

            // Assert
            Assert.IsFalse(canExecute);
        }

        [TestMethod]
        public void XuatVeCommand_CannotExecute_WhenTinhTrangIsDaXuatVe()
        {
            // Arrange
            var viewModel = new BookingDetailVM();
            BookingDetailVM.SelectedPhieu = new PHIEUDATCHO { TINHTRANG = "Đã xuất vé" };

            // Act
            bool canExecute = viewModel.XuatVeCommand.CanExecute(null);

            // Assert
            Assert.IsFalse(canExecute);
        }


        [TestMethod]
        public void XuatVeCommand_CanExecute_WhenTinhTrangIsDaThanhToan()
        {
            // Arrange
            BookingDetailVM.SelectedPhieu = new PHIEUDATCHO { TINHTRANG = "Đã thanh toán" };
            var viewModel = new BookingDetailVM();

            // Act
            bool canExecute = viewModel.XuatVeCommand.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute);
        }
    }
}
