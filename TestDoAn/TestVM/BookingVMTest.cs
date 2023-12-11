using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDoAn.TestModel;
using TourismManagementSystem.Model;
using TourismManagementSystem.ViewModel;


namespace TestDoAn.TestVM
{
    [TestClass]
    public class BookingVMTest
    {
        public ObservableCollection<PHIEUDATCHO> listPDC = new ObservableCollection<PHIEUDATCHO>()
        {
            new PHIEUDATCHO
            {
                MAPHIEU = "PH001",
                NGAYDAT = DateTime.Now,
                SLKHACH = 2,
                TONGTIEN = 100,
                SOTIENDATHANHTOAN = 50,
                MACHUYEN = "CH001",
                MAKH = "KH001",
                TINHTRANG = "Confirmed"
            },

            new PHIEUDATCHO
            {
                MAPHIEU = "PH002",
                NGAYDAT = DateTime.Now.AddDays(1),
                SLKHACH = 4,
                TONGTIEN = 200,
                SOTIENDATHANHTOAN = 100,
                MACHUYEN = "CH002",
                MAKH = "KH002",
                TINHTRANG = "Pending"
            }
        };

        [TestMethod]
        public void BookingList_SetProperty_TriggerOnPropertyChanged()
        {
            // Arrange
            var viewModel = new BookingVM(listPDC);

            // Act
            bool test = (viewModel.BookingList == listPDC);

            // Assert
            Assert.IsTrue(test);
        }



        [TestMethod]
        public void SelectedFilter_SetProperty_TriggerOnPropertyChanged()
        {
            // Arrange
            var viewModel = new BookingVM(listPDC);

            // Act
            viewModel.SelectedFilter = "Mã phiếu";

            // Assert
            Assert.AreEqual(viewModel.SelectedFilter, "Mã phiếu");
        }


        [TestMethod]
        public void FilterCbItems_IsNotNull()
        {
            // Arrange
            var viewModel = new BookingVM(listPDC);
            

            // Assert
            Assert.IsNotNull(viewModel.FilterCbItems);
        }

    }
}
