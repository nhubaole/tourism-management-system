using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    internal class InforRestaurantVm : BaseViewModel

    {

        public static String MaNH;
        public static String TenNH;
        public static String SDT;
        public static String MoTa;

        private String _maNH;

        public String maNH { get { return _maNH; } set { _maNH = value; OnPropertyChanged(); } }

        private String _tenNH;
        public String tenNH { get { return _tenNH; } set { _tenNH = value; OnPropertyChanged(); } }


        private String _sdt;
        public String sdt { get { return _sdt; } set { _sdt = value; OnPropertyChanged(); } }

        private String _moTa;
        public String moTa { get { return _moTa; } set { _moTa = value; OnPropertyChanged(); } }


        public InforRestaurantVm()
        {


            //nhận các thông tin từ InforService
            maNH = InforServiceVM.MaNH;
            tenNH = InforServiceVM.TenNH;
            sdt = InforServiceVM.SDT;
            moTa = InforServiceVM.MoTa;

            // Đăng ký sự kiện PropertyChanged
            PropertyChanged += InforRestaurantVm_PropertyChanged;

        }
        private void InforRestaurantVm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Kiểm tra xem thuộc tính nào đã thay đổi
            switch (e.PropertyName)
            {
                case nameof(maNH):
                    // Cập nhật giá trị MaPT khi maPT thay đổi
                    InforServiceVM.MaNH = maNH;
                    break;
                case nameof(tenNH):
                    // Cập nhật giá trị TenPT khi tenPT thay đổi
                    InforServiceVM.TenNH = tenNH;
                    break;
                case nameof(sdt):
                    // Cập nhật giá trị SLG khi SLGhe thay đổi
                    InforServiceVM.SDT = sdt;
                    break;
                case nameof(moTa):
                    // Cập nhật giá trị SLG khi SLGhe thay đổi
                    InforServiceVM.MoTa = moTa;
                    break;
                default:
                    // Xử lý cho các thuộc tính khác (nếu cần)
                    break;
            }
        }
    }
}
