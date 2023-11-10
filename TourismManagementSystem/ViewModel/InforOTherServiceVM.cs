using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismManagementSystem.ViewModel
{
    public class InforOTherServiceVM: BaseViewModel
    {
        private String _maDVK;

        public String maDVK { get { return _maDVK; } set { _maDVK = value; OnPropertyChanged(); } }

        private String _tenDVK;
        public String tenDVK { get { return _tenDVK; } set { _tenDVK = value; OnPropertyChanged(); } }

        private String _moTaDVK;
        public String moTaDVK { get { return _moTaDVK; } set { _moTaDVK = value; OnPropertyChanged(); } }

      
        public InforOTherServiceVM() {

            maDVK = InforServiceVM.MaDVK;
            tenDVK = InforServiceVM.TenDVK;
            moTaDVK = InforServiceVM.MoTaDVK;

            PropertyChanged += InforOTherServiceVM_PropertyChanged;
        }

        private void InforOTherServiceVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Kiểm tra xem thuộc tính nào đã thay đổi
            switch (e.PropertyName)
            {
                case nameof(maDVK):
                    // Cập nhật giá trị MaPT khi maPT thay đổi
                    InforServiceVM.MaDVK = maDVK;
                    break;
                case nameof(tenDVK):
                    // Cập nhật giá trị TenPT khi tenPT thay đổi
                    InforServiceVM.TenDVK = tenDVK;
                    break;
                case nameof(moTaDVK):
                    // Cập nhật giá trị TenPT khi tenPT thay đổi
                    InforServiceVM.MoTaDVK = moTaDVK;
                    break;
                default:
                    // Xử lý cho các thuộc tính khác (nếu cần)
                    break;
            }
        }
    }
}
