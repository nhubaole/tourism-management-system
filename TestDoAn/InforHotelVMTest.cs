using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.ViewModel;

namespace TestDoAn
{
    [TestClass]
    public class InforHotelVMTest
    {
        [TestMethod]
        public void PropertyChanged_MaKS_UpdateInforServiceVM()
        {
            // Arrange
            var viewModel = new InforHotelVM();
            string maKS = "KS0000001";

            // Act
            viewModel.maKS = maKS;

            // Assert
            Assert.AreEqual(maKS, InforServiceVM.MaKS);
        }

        [TestMethod]
        public void PropertyChanged_TenKS_UpdateInforServiceVM()
        {
            // Arrange
            var viewModel = new InforHotelVM();
            string tenKS = "KS Bạch Mã";

            // Act
            viewModel.tenKS = tenKS;

            // Assert
            Assert.AreEqual(tenKS, InforServiceVM.TenKS);
        }


        [TestMethod]

        public void PropertyChanged_DiaCHi_UpdateInforServiceVM()
        {
            // Arrange
            var viewModel = new InforHotelVM();
            string dc = "01 Đặng Văn Bi";
            // Act
            viewModel.dcKS = dc;

            // Assert
            Assert.AreEqual(dc, InforServiceVM.DcKS);
        }


        [TestMethod]

        public void PropertyChanged_SoSao_UpdateInforServiceVM()
        {
            // Arrange
            var viewModel = new InforHotelVM();
            int soSao = 5;

            // Act
            viewModel.soSao = soSao;

            // Assert
            Assert.AreEqual(soSao, InforServiceVM.SoSaoKS);
        }


        [TestMethod]

        public void PropertyChanged_SDT_UpdateInforServiceVM()
        {
            // Arrange
            var viewModel = new InforHotelVM();
            string sdT = "01231241432";
            // Act
            viewModel.sdt = sdT;

            // Assert
            Assert.AreEqual(sdT, InforServiceVM.SDTKS);
        }

        [TestMethod]

        public void PropertyChanged_SucChua_UpdateInforServiceVM()
        {
            // Arrange
            var viewModel = new InforHotelVM();
            int sucChua = 100;

            // Act
            viewModel.sucChua = sucChua;

            // Assert
            Assert.AreEqual(sucChua, InforServiceVM.SucChuaKS);
        }
    }
}
