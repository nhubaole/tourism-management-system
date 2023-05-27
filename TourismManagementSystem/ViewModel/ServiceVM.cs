using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    internal class ServiceVM : BaseViewModel
    {
        private String _SearchTerm;
        public String SearchTerm { get { return _SearchTerm; } set { _SearchTerm = value; OnPropertyChanged(); } }

        private string _Filter1;
        public string Filter1
        {
            get { return _Filter1; }
            set
            {
                _Filter1 = value;
                
                UpdateFilter2();
            }
        }

        private string _Filter2;
        public string Filter2
        {
            get { return _Filter2; }
            set
            {
                _Filter2 = value;
;
            }
        }
        private ObservableCollection<string> _FilterItems1;
        public ObservableCollection<string> FilterItems1
        {
            get { return _FilterItems1; }
            set { _FilterItems1 = value; }
        }

        private ObservableCollection<string> _FilterItems2;
        public ObservableCollection<string> FilterItems2
        {
            get { return _FilterItems2; }
            set { _FilterItems2 = value; }
        }

        private ObservableCollection<PHUONGTIEN> _PhuongTien;
        public ObservableCollection<PHUONGTIEN> PhuongTien
        {

            get => _PhuongTien;
            set { _PhuongTien = value; OnPropertyChanged(); }
        }
        private ObservableCollection<NHAHANG> _NhaHang;
        public ObservableCollection<NHAHANG> NhaHang
        {

            get => _NhaHang;
            set { _NhaHang = value; OnPropertyChanged(); }
        }
        private ObservableCollection<KHACHSAN> _KhachSan;
        public ObservableCollection<KHACHSAN> KhachSan
        {

            get => _KhachSan;
            set { _KhachSan = value; OnPropertyChanged(); }
        }
        private ObservableCollection<DICHVUKHAC> _DVKhac;
        public ObservableCollection<DICHVUKHAC> DVKhac
        {

            get => _DVKhac;
            set { _DVKhac = value; OnPropertyChanged(); }
        }

        //Danh sách để binding vô datagrid (tính cả tìm kiếm)

        private ObservableCollection<PHUONGTIEN> _SearchResultPT;
        public ObservableCollection<PHUONGTIEN> SearchResultPT
        {

            get => _SearchResultPT;
            set { _SearchResultPT = value; OnPropertyChanged(); }
        }
        private ObservableCollection<NHAHANG> _SearchResultNH;
        public ObservableCollection<NHAHANG> SearchResultNH
        {

            get => _SearchResultNH;
            set { _SearchResultNH = value; OnPropertyChanged(); }
        }
       
        private ObservableCollection<KHACHSAN> _SearchResultKS;
        public ObservableCollection<KHACHSAN> SearchResultKS
        {

            get => _SearchResultKS;
            set { _SearchResultKS = value; OnPropertyChanged(); }
        }

       
        private ObservableCollection<DICHVUKHAC> _SearchResultDVK;
        public ObservableCollection<DICHVUKHAC> SearchResultDVK
        {

            get => _SearchResultDVK;
            set { _SearchResultDVK = value; OnPropertyChanged(); }
        }

        // các dịch vụ được chọn


        private PHUONGTIEN _selectedItemPT;

        public PHUONGTIEN selectedItemPT { get { return _selectedItemPT; } set { _selectedItemPT = value; OnPropertyChanged(); } }

        private NHAHANG _selectedItemNH;

        public NHAHANG selectedItemNH { get { return _selectedItemNH; } set { _selectedItemNH = value; OnPropertyChanged(); } }

        private KHACHSAN _selectedItemKS;

        public KHACHSAN selectedItemKS { get { return _selectedItemKS; } set { _selectedItemKS = value; OnPropertyChanged(); } }

        private DICHVUKHAC _selectedItemDVK;

        public DICHVUKHAC selectedItemDVK { get { return _selectedItemDVK; } set { _selectedItemDVK = value; OnPropertyChanged(); } }

        public ICommand ToAddServiceCommand { get; set; }


        public ICommand DeleteTrafficCommand { get; set; }

        public ICommand ToEditTrafficCommand { get; set; }

        public ICommand DeleteRestaurantCommand { get; set; }
        public ICommand ToEditRestaurantCommand { get; set; }

        public ICommand DeleteHotelCommand { get; set; }
        public ICommand ToEditHotelCommand { get; set; }

        public ICommand DeleteOtherServiceCommand { get; set; }
        public ICommand ToEditOtherServiceCommand { get; set; }


        public ServiceVM() {
            FilterItems1 = new ObservableCollection<String>(new List<string> { "Phương tiện", "Nhà hàng", "Khách sạn", "Dịch vụ khác" });


            PhuongTien = new ObservableCollection<PHUONGTIEN>(DataProvider.Ins.DB.PHUONGTIENs);
            SearchResultPT = new ObservableCollection<PHUONGTIEN>(DataProvider.Ins.DB.PHUONGTIENs);

            NhaHang = new ObservableCollection<NHAHANG>(DataProvider.Ins.DB.NHAHANGs);
            SearchResultNH = new ObservableCollection<NHAHANG>(DataProvider.Ins.DB.NHAHANGs);

            KhachSan = new ObservableCollection<KHACHSAN>(DataProvider.Ins.DB.KHACHSANs);
            SearchResultKS = new ObservableCollection<KHACHSAN>(DataProvider.Ins.DB.KHACHSANs);

            DVKhac = new ObservableCollection<DICHVUKHAC>(DataProvider.Ins.DB.DICHVUKHACs);
            SearchResultDVK = new ObservableCollection<DICHVUKHAC>(DataProvider.Ins.DB.DICHVUKHACs);


        }
        private void UpdateFilter2()
        {
            // Cập nhật items trong Filter2
            if (Filter1 == "Phương tiện")
            {
                FilterItems2 = new ObservableCollection<string> { "Mã phương tiện", "Tên phương tiện", "Số lượng ghế" };
                
            }
            else if (Filter1 == "Nhà hàng")
            {
                FilterItems2 = new ObservableCollection<string> { "Mã khách sạn", "Tên khách sạn", "Địa chỉ", "Số điện thoại", "Số sao", "Sức chứa" };
            }
            else if (Filter1 == "Khách sạn")
            {
                FilterItems2 = new ObservableCollection<string> { "Mã nhà hàng", "Tên nhà hàng", "Số điện thoại", "Mô tả" };
            }
            else if (Filter1 == "Dịch vụ khác")
            {
                FilterItems2 = new ObservableCollection<string> { "Mã dịch vụ", "Tên dịch vụ", "Mô tả" };
            }
            OnPropertyChanged(nameof(FilterItems2));
        }
    }
}
