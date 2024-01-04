
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;
using TourismManagementSystem.ViewModel;
using System.Collections;

namespace TestDoAn.TestVM
{
    [TestClass]
    public class AddTourVMTest
    {
        ObservableCollection<DIADIEM> diadiemList = new ObservableCollection<DIADIEM>()
        {
            new DIADIEM
            {
                MADD = "1",
                TENDD = "Đại điểm 1",
                DIACHI = "Địa chỉ 1",
                MOTA = "Mô tả 1"
            },

            new DIADIEM
            {
                MADD = "2",
                TENDD = "Đại điểm 2",
                DIACHI = "Địa chỉ 2",
                MOTA = "Mô tả 2"
            },

            new DIADIEM
            {
                MADD = "3",
                TENDD = "Đại điểm 3",
                DIACHI = "Địa chỉ 3",
                MOTA = "Mô tả 3"
            }
        };
        ObservableCollection<LOAITUYEN> loaiTuyen = new ObservableCollection<LOAITUYEN>()
        {
            new LOAITUYEN
            {
                MALOAI = "LT001",

                TENLOAI = "Loại tuyến A",
                TGMUATRUOCKHOIHANH = 30,  
                TGHOANVEMIENPHI = 60,     
                LEPHIHOANVETRE = 50000    
                },

             new LOAITUYEN
                {
                    MALOAI = "LT002",
                    TENLOAI = "Loại tuyến B",
                    TGMUATRUOCKHOIHANH = 15,  
                    TGHOANVEMIENPHI = 45,     
                    LEPHIHOANVETRE = 30000    
                }
        };
        ObservableCollection<TUYEN> tuyen = new ObservableCollection<TUYEN>();
        [TestMethod]
        public void TestToolTipText()
        {
            // Arrange
            AddTourVM addTourVM = new AddTourVM(diadiemList, loaiTuyen, tuyen);

            // Act
            addTourVM.ToolTipText = "New ToolTipText";

            // Assert
            Assert.AreEqual("New ToolTipText", addTourVM.ToolTipText);
        }

        [TestMethod]
        public void TestListDiaDiemInitialization()
        {
            AddTourVM addTourVM = new AddTourVM(diadiemList, loaiTuyen, tuyen);

            // Assert
            Assert.IsNotNull(addTourVM.ListDiaDiem);
        }

        [TestMethod]
        public void TestListLoaiTuyenInitialization()
        {
            AddTourVM addTourVM = new AddTourVM(diadiemList, loaiTuyen, tuyen);
            // Assert
            Assert.IsNotNull(addTourVM.ListLoaiTuyen);
        }

        [TestMethod]
        public void TestEditSelectedTuyen()
        {
            // Arrange
            AddTourVM addTourVM = new AddTourVM(diadiemList, loaiTuyen, tuyen);

            var tuyenq = new TUYEN();
            // Act
            AddTourVM.EditSelectedTuyen = tuyenq;

            // Assert
            Assert.AreEqual(tuyenq, AddTourVM.EditSelectedTuyen);
        }

        [TestMethod]
        public void TestIsEdit()
        {
            // Arrange
            AddTourVM addTourVM = new AddTourVM(diadiemList, loaiTuyen, tuyen);
            // Act
            AddTourVM.IsEdit = 1;

            // Assert
            Assert.AreEqual(1, AddTourVM.IsEdit);
        }

        [TestMethod]
        public void TestTitle()
        {
            // Arrange
            AddTourVM addTourVM = new AddTourVM(diadiemList, loaiTuyen, tuyen);
            // Act
            addTourVM.Title = "New Title";

            // Assert
            Assert.AreEqual("New Title", addTourVM.Title);
        }

        [TestMethod]
        public void TestBtnText()
        {
            // Arrange
            AddTourVM addTourVM = new AddTourVM(diadiemList, loaiTuyen, tuyen);

            // Act
            addTourVM.BtnText = "New BtnText";

            // Assert
            Assert.AreEqual("New BtnText", addTourVM.BtnText);
        }
    }
}
