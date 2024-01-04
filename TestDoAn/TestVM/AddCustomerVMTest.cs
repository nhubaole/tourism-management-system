using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourismManagementSystem.Model;
using TourismManagementSystem.ViewModel;
using System.Windows;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Collections;
namespace TestDoAn.TestVM
{
    [TestClass]
    //thay đổi gì đó
    public class AddCustomerVMTest
    {
        ObservableCollection<KHACHHANG> customers = new ObservableCollection<KHACHHANG>
            {
                new KHACHHANG
                {
                    MAKH = "KH000001",
                    HOTEN = "Nguyen Van A",
                    CCCD = "123456789012",
                    SDT = "1234567890",
                    EMAIL = "nguyenvana@example.com",
                    DIACHI = "123 Street, City"
                },
                new KHACHHANG
                {
                    MAKH = "KH000002",
                    HOTEN = "Tran Thi B",
                    CCCD = "987654321098",
                    SDT = "0987654321",
                    EMAIL = "tranthib@example.com",
                    DIACHI = "456 Street, City"
                },
                new KHACHHANG
                {
                    MAKH = "KH000003",
                    HOTEN = "Le Van C",
                    CCCD = "111122223333",
                    SDT = "0123456789",
                    EMAIL = "levanc@example.com",
                    DIACHI = "789 Street, City"
                }
            };



        [TestMethod]
        public void TestMAKHProperty()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);

            addCustomerVM.MAKH = "KH000000001";


            Assert.AreEqual("KH000000001", addCustomerVM.MAKH);
        }

        [TestMethod]
        public void TestHOTENProperty()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);

            addCustomerVM.HOTEN = "KH000000001";


            Assert.AreEqual("KH000000001", addCustomerVM.HOTEN);
        }

        [TestMethod]
        public void TestCCCDProperty()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);

            addCustomerVM.CCCD = "KH000000001";


            Assert.AreEqual("KH000000001", addCustomerVM.CCCD);
        }

        [TestMethod]
        public void TestSDTProperty()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);

            addCustomerVM.SDT = "KH000000001";


            Assert.AreEqual("KH000000001", addCustomerVM.SDT);
        }
        [TestMethod]
        public void TestEMAILProperty()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);

            addCustomerVM.EMAIL = "KH000000001";


            Assert.AreEqual("KH000000001", addCustomerVM.EMAIL);
        }

        [TestMethod]
        public void TestDIACHIProperty()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);

            addCustomerVM.DIACHI = "KH000000001";


            Assert.AreEqual("KH000000001", addCustomerVM.DIACHI);
        }


        [TestMethod]
        public void TestValidateProperty_SDTNullOrEmpty_AddsError()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            addCustomerVM.SDT = null;

            // Act
            addCustomerVM.ValidateProperty(nameof(AddCustomerVM.SDT));

            // Assert
            CollectionAssert.Contains((ICollection)addCustomerVM.GetErrors(nameof(AddCustomerVM.SDT)), "Vui lòng nhập số điện thoại hợp lệ");
        }

        [TestMethod]
        public void TestValidateProperty_SDTNotNumber_AddsError()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            addCustomerVM.SDT = "abc123";

            // Act
            addCustomerVM.ValidateProperty(nameof(AddCustomerVM.SDT));

            // Assert
            CollectionAssert.Contains((ICollection)addCustomerVM.GetErrors(nameof(AddCustomerVM.SDT)), "Vui lòng nhập số điện thoại hợp lệ");
        }

        [TestMethod]
        public void TestValidateProperty_SDTInvalidLength_AddsError()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            addCustomerVM.SDT = "123456789";

            // Act
            addCustomerVM.ValidateProperty(nameof(AddCustomerVM.SDT));

            // Assert
            CollectionAssert.Contains((ICollection)addCustomerVM.GetErrors(nameof(AddCustomerVM.SDT)), "Vui lòng nhập số điện thoại hợp lệ");
        }

        [TestMethod]
        public void TestValidateProperty_NoErrors()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            addCustomerVM.SDT = "0123456789";

            ICollection errors = (ICollection)addCustomerVM.GetErrors(nameof(AddCustomerVM.SDT));
            
            Assert.IsNull(errors);
    
        }
        [TestMethod]
        public void TestValidateProperty_CCCDValid_NoErrors()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            addCustomerVM.CCCD = "123456789012"; 

            // Act
            addCustomerVM.ValidateProperty(nameof(AddCustomerVM.CCCD));

            // Assert
            ICollection errors = (ICollection)addCustomerVM.GetErrors(nameof(AddCustomerVM.CCCD));
            Assert.IsNull(errors);
        }

        [TestMethod]
        public void TestValidateProperty_CCCDInvalid_LengthError()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            addCustomerVM.CCCD = "12345";

            // Act
            addCustomerVM.ValidateProperty(nameof(AddCustomerVM.CCCD));

            // Assert
            ICollection errors = (ICollection)addCustomerVM.GetErrors(nameof(AddCustomerVM.CCCD));
            CollectionAssert.Contains(errors, "Vui lòng nhập số CCCD gồm 12 chữ số");
        }

        [TestMethod]
        public void TestValidateProperty_CCCDInvalid_FormatError()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            addCustomerVM.CCCD = "ABC123456789";

            // Act
            addCustomerVM.ValidateProperty(nameof(AddCustomerVM.CCCD));

            // Assert
            ICollection errors = (ICollection)addCustomerVM.GetErrors(nameof(AddCustomerVM.CCCD));
            CollectionAssert.Contains(errors, "Vui lòng nhập số CCCD gồm 12 chữ số");
        }

        [TestMethod]
        public void TestValidateProperty_HOTENValid_NoErrors()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            addCustomerVM.HOTEN = "Valid Name"; 

            // Act
            addCustomerVM.ValidateProperty(nameof(AddCustomerVM.HOTEN));

            // Assert
            ICollection errors = (ICollection)addCustomerVM.GetErrors(nameof(AddCustomerVM.HOTEN));
            Assert.IsNull(errors, "No errors expected for valid name");
        }

        [TestMethod]
        public void TestValidateProperty_HOTENInvalid_NullError()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            addCustomerVM.HOTEN = null;

            // Act
            addCustomerVM.ValidateProperty(nameof(AddCustomerVM.HOTEN));

            // Assert
            ICollection errors = (ICollection)addCustomerVM.GetErrors(nameof(AddCustomerVM.HOTEN));
            CollectionAssert.Contains(errors, "Vui lòng nhập họ và tên hợp lệ");
        }

        [TestMethod]
        public void TestValidateProperty_HOTENInvalid_InvalidCharactersError()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            addCustomerVM.HOTEN = "Invalid Name!"; 

            // Act
            addCustomerVM.ValidateProperty(nameof(AddCustomerVM.HOTEN));

            // Assert
            ICollection errors = (ICollection)addCustomerVM.GetErrors(nameof(AddCustomerVM.HOTEN));
            CollectionAssert.Contains(errors, "Vui lòng nhập họ và tên hợp lệ");
        }

        [TestMethod]
        public void TestIsNumber_ValidNumber_ReturnsTrue()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            var validNumber = "12345";

            // Act
            bool result = addCustomerVM.IsNumber(validNumber);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsNumber_InvalidNumber_ReturnsFalse()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            var invalidNumber = "abc";

            // Act
            bool result = addCustomerVM.IsNumber(invalidNumber);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsNameValid_ValidName_ReturnsTrue()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            var validName = "John Doe";

            // Act
            bool result = addCustomerVM.IsNameValid(validName);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsNameValid_InvalidName_ReturnsFalse()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            var invalidName = "John123";

            // Act
            bool result = addCustomerVM.IsNameValid(invalidName);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsNameValid_InvalidName_ReturnsFalse2()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            var invalidName = "John123. ";

            // Act
            bool result = addCustomerVM.IsNameValid(invalidName);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsEmailValid_ValidEmail_ReturnsTrue()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            var validEmail = "test@example.com";

            // Act
            bool result = addCustomerVM.IsEmailValid(validEmail);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsEmailValid_InvalidEmail_ReturnsFalse()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            var invalidEmail = "invalid-email";

            // Act
            bool result = addCustomerVM.IsEmailValid(invalidEmail);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsEmailValid_NullEmail_ReturnsFalse()
        {
            // Arrange
            var addCustomerVM = new AddCustomerVM(customers);
            string nullEmail = null;

            // Act
            bool result = addCustomerVM.IsEmailValid(nullEmail);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
