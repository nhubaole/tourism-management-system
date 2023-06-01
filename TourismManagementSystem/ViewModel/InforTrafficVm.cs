using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    internal class InforTrafficVm: BaseViewModel
    {
        public static bool IsNew; //xác định là tạo mới hay cập nhập
        public static PHUONGTIEN d = null;


        private String _maPT;

        public String maPT { get { return _maPT; } set { _maPT = value; OnPropertyChanged(); } }

        private String _tenPT;
        public String tenPT { get { return _tenPT; } set { _tenPT = value; OnPropertyChanged(); } }


        private String _dcPT;
        public String dcPT { get { return _dcPT; } set { _dcPT = value; OnPropertyChanged(); } }
         
        private int _SLGhe;
        public int SLGhe { get { return _SLGhe; } set { _SLGhe = value; OnPropertyChanged(); } }


        public InforTrafficVm()
        {


            //lấy thông tin từ Inforservice

            maPT = InforServiceVM.MaPT;
            tenPT = InforServiceVM.TenPT;
            SLGhe = InforServiceVM.SLG;

            // Đăng ký sự kiện PropertyChanged
            PropertyChanged += InforTrafficVm_PropertyChanged;

        }
        private void InforTrafficVm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Kiểm tra xem thuộc tính nào đã thay đổi
            switch (e.PropertyName)
            {
                case nameof(maPT):
                    // Cập nhật giá trị MaPT khi maPT thay đổi
                    InforServiceVM.MaPT = maPT;
                    break;
                case nameof(tenPT):
                    // Cập nhật giá trị TenPT khi tenPT thay đổi
                    InforServiceVM.TenPT = tenPT;
                    break;
                case nameof(SLGhe):
                    // Cập nhật giá trị SLG khi SLGhe thay đổi
                    InforServiceVM.SLG = SLGhe;
                    break;
                default:
                    // Xử lý cho các thuộc tính khác (nếu cần)
                    break;
            }
        }
    }
}
