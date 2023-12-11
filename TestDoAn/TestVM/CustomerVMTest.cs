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

    public class CustomerVMTest
    {
        


        public ObservableCollection<KHACHHANG> listKH = new ObservableCollection<KHACHHANG>() {
             new KHACHHANG
            {
                MAKH = "KH001",
                HOTEN = "John Doe",
                CCCD = "123456789",
                SDT = "123-456-7890",
                EMAIL = "john.doe@example.com",
                DIACHI = "123 Main St, City",

            },

        // Example Customer 2
             new KHACHHANG
            {
                MAKH = "KH002",
                HOTEN = "Jane Smith",
                CCCD = "987654321",
                SDT = "987-654-3210",
                EMAIL = "jane.smith@example.com",
                DIACHI = "456 Oak St, Town"
            }

        };

        [TestMethod]
        public void TestFilterIsUpdate ()
        {
            var customerVM = new CustomerVM(listKH);

            ObservableCollection<string> expect = new ObservableCollection<string>() { "Mã khách hàng", "Tên khách hàng", "Số điện thoại" };

            bool areEqual = expect.SequenceEqual(customerVM.filter);

            Assert.IsTrue(areEqual);

        }

        [TestMethod]
        public void TestSelected_Cus()
        {
            var customerVM = new CustomerVM(listKH);
            customerVM.SelectedCustomer = listKH[0];
            Assert.AreEqual(customerVM.SelectedCustomer, listKH[0]);
        }

        //[TestMethod]
        //public void TestUpdateDataCommand_WithValidData()
        //{


        //    CustomerVM viewModel = new CustomerVM(listKH);
        //    KHACHHANG selectedCustomer = listKH.First(); // Chọn một khách hàng từ danh sách test

        //    // Set up ViewModel
        //    viewModel.SelectedCustomer = selectedCustomer;
        //    viewModel.HOTEN = "UpdatedName";
        //    viewModel.CCCD = "987654321098";
        //    viewModel.SDT = "1234567899";
        //    viewModel.DIACHI = "UpdatedAddress";
        //    viewModel.EMAIL = "updatedemail@example.com";

        //    // Act
        //    bool canExecute = viewModel.UpdateDataCommand.CanExecute(null);

        //    // Assert
        //    Assert.IsTrue(canExecute);
        //}


        [TestMethod]
    

        public void TestValidateProperty_SDT_Valid()
        {
            // Arrange
            var customerVM = new CustomerVM(listKH);
            customerVM.SDT = "1234567890";

            // Act
            customerVM.ValidateProperty(nameof(customerVM.SDT));

            // Assert
            Assert.IsFalse(customerVM.HasErrors);
        }

        [TestMethod]
        public void TestValidateProperty_SDT_Invalid_Null()
        {
            // Arrange
            var customerVM = new CustomerVM(listKH);
            customerVM.SDT = null;

            // Act
            customerVM.ValidateProperty(nameof(customerVM.SDT));

            // Assert
            Assert.IsTrue(customerVM.HasErrors);
            CollectionAssert.Contains(customerVM.GetErrors(nameof(customerVM.SDT)).Cast<string>().ToList(), "Vui lòng nhập số điện thoại hợp lệ");
        }

        [TestMethod]
        public void TestValidateProperty_SDT_Invalid_NonNumeric()
        {
            // Arrange
            var customerVM = new CustomerVM(listKH);
            customerVM.SDT = "abc123";

            // Act
            customerVM.ValidateProperty(nameof(customerVM.SDT));

            // Assert
            Assert.IsTrue(customerVM.HasErrors);
            CollectionAssert.Contains(customerVM.GetErrors(nameof(customerVM.SDT)).Cast<string>().ToList(), "Vui lòng nhập số điện thoại hợp lệ");
        }

        [TestMethod]
        public void TestValidateProperty_SDT_Invalid_Length()
        {
            // Arrange
            var customerVM = new CustomerVM(listKH);
            customerVM.SDT = "123456789";

            // Act
            customerVM.ValidateProperty(nameof(customerVM.SDT));

            // Assert
            Assert.IsTrue(customerVM.HasErrors);
            CollectionAssert.Contains(customerVM.GetErrors(nameof(customerVM.SDT)).Cast<string>().ToList(), "Vui lòng nhập số điện thoại hợp lệ");
        }

        // Tương tự, viết các unit test cho CCCD, HOTEN, EMAIL, DIACHI

        [TestMethod]
        [DataRow("            ", false)]
        [DataRow("", false)]
        [DataRow("abc", false)]
        [DataRow("12345", true)]
        public void TestIsNumber(string data, bool expect)
        {
            // Arrange
            var customerVM = new CustomerVM(listKH);

            // Act
            var result = customerVM.IsNumber(data);

            // Assert
            Assert.AreEqual(result, expect);
        }

  

        [TestMethod]
        [DataRow("            ", false)]
        [DataRow("", false)]
        [DataRow("john", true)]
        [DataRow("1233", false)]
        public void TestIsNameValid(string data, bool expect)
        {
            // Arrange
            var customerVM = new CustomerVM(listKH);

            // Act
            var result = customerVM.IsNameValid(data);

            // Assert
            Assert.AreEqual(result, expect);
        }

        
        [TestMethod]
        public void TestIsEmailValid_Valid()
        {
            // Arrange
            var customerVM = new CustomerVM(listKH);

            // Act
            var result = customerVM.IsEmailValid("john.doe@example.com");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsEmailValid_Invalid()
        {
            // Arrange
            var customerVM = new CustomerVM(listKH);

            // Act
            var result = customerVM.IsEmailValid("john.doe@");

            // Assert
            Assert.IsFalse(result);
        }
    }
}
