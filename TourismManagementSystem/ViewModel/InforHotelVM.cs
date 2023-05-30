using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismManagementSystem.ViewModel
{
    internal class InforHotelVM : BaseViewModel
    {
        private String _maKS;

        public String maKS { get { return _maKS; } set { _maKS = value; OnPropertyChanged(); } }

        private String _tenKS;
        public String tenKS { get { return _tenKS; } set { _tenKS = value; OnPropertyChanged(); } }


        private String _dcKS;
        public String dcKS { get { return _dcKS; } set { _dcKS = value; OnPropertyChanged(); } }

        private int _soSao;
        public int soSao { get { return _soSao; } set { _soSao = value; OnPropertyChanged(); } }


        private String _sdt;
        public String sdt { get { return _sdt; } set { _sdt = value; OnPropertyChanged(); } }

        private int _sucChua;
        public int sucChua { get { return _sucChua; } set { _sucChua = value; OnPropertyChanged(); } }
        public InforHotelVM()
        {

            maKS = InforServiceVM.MaKS;
            tenKS = InforServiceVM.TenKS;
            dcKS = InforServiceVM.DcKS;
            sdt = InforServiceVM.SDTKS;
            soSao = InforServiceVM.SoSaoKS;
            sucChua = InforServiceVM.SucChuaKS;
            

            PropertyChanged += InforHotelVM_PropertyChanged;
        }

        private void InforHotelVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Kiểm tra xem thuộc tính nào đã thay đổi
            switch (e.PropertyName)
            {
                case nameof(maKS):
                    // Cập nhật giá trị MaPT khi maPT thay đổi
                    InforServiceVM.MaKS = maKS;
                    break;
                case nameof(tenKS):
                    // Cập nhật giá trị TenPT khi tenPT thay đổi
                    InforServiceVM.TenKS = tenKS;
                    break;
                case nameof(dcKS):
                    // Cập nhật giá trị SLG khi SLGhe thay đổi
                    InforServiceVM.DcKS = dcKS;
                    break;
                case nameof(sdt):
                    // Cập nhật giá trị SLG khi SLGhe thay đổi
                    InforServiceVM.SDTKS = sdt;
                    break;
                case nameof(soSao):
                    // Cập nhật giá trị SLG khi SLGhe thay đổi
                    InforServiceVM.SoSaoKS = soSao;
                    break;
                case nameof(sucChua):
                    // Cập nhật giá trị SLG khi SLGhe thay đổi
                    InforServiceVM.SucChuaKS = sucChua;
                    break;
                default:
                    // Xử lý cho các thuộc tính khác (nếu cần)
                    break;
            }
        }
    }
}
