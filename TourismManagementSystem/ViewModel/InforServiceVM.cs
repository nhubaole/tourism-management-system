using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.Model;
using TourismManagementSystem.UserControls;
using static System.Net.Mime.MediaTypeNames;

namespace TourismManagementSystem.ViewModel
{
   
    public class InforServiceVM :BaseViewModel
    {
        public static string filter;
        //Phương tiện
        public static String MaPT;
        public static String TenPT;
        public static int SLG;
        public static PHUONGTIEN newPT;

        //nhà hàng
        public static String MaNH;
        public static String TenNH;
        public static String SDT;
        public static String MoTa;
        public static NHAHANG newNH;


        //binding với .xaml
        private String _maPT;

        public String maPT { get { return _maPT; } set { _maPT = value; OnPropertyChanged(); } }

        private String _tenPT;
        public String tenPT { get { return _tenPT; } set { _tenPT = value; OnPropertyChanged(); } }

        private int _SLGhe;
        public int SLGhe { get { return _SLGhe; } set { _SLGhe = value; OnPropertyChanged(); } }


        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        public static bool IsNew; //xác định là tạo mới hay cập nhập

        private ObservableCollection<string> _FilterItems;
        public ObservableCollection<string> FilterItems
        {
            get { return _FilterItems; }
            set { _FilterItems = value; OnPropertyChanged(); }
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
        private string _Filter;
        public string Filter
        {
            get { return _Filter; }
            set
            {
                _Filter = value;

                OnPropertyChanged();
            }
        }
        public ICommand SaveServiceCommand { get; set; }
        public InforServiceVM() {
            FilterItems = new ObservableCollection<String>(new List<string> { "Phương tiện", "Khách sạn", "Nhà hàng", "Dịch vụ khác" });

            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Filter))
                {
                    switch (Filter)
                    {
                        case "Phương tiện":
                            filter = Filter;
                            PhuongTien = new ObservableCollection<PHUONGTIEN>(DataProvider.Ins.DB.PHUONGTIENs);
                            if (PhuongTien.Count() == 0)
                            {
                                MaPT = GenerateCode("PT");
                            }
                            else
                            {
                                MaPT = PhuongTien[PhuongTien.Count - 1].MAPT;
                                MaPT = GenerateCode("PT", MaPT);
                            }
                            TenPT = null;
                            SLG = 0;

                            Traffic(null);
                            
                            break;
                        case "Nhà hàng":
                            filter = Filter;
                            NhaHang = new ObservableCollection<NHAHANG>(DataProvider.Ins.DB.NHAHANGs);
                            if (NhaHang.Count() == 0)
                            {
                                MaNH = GenerateCode("NH");
                            }

                            else
                            {
                                MaNH = NhaHang[NhaHang.Count - 1].MANH;
                                MaNH = GenerateCode("NH", MaNH);
                            }
                            TenNH = null;
                            SDT = null;
                            MoTa = null;
                            Restaurant(null);
                            break;
                        case "Khách sạn":
                            Hotel(null);
                            break;
                        case "Dịch vụ khác":
                            OTherService(null);
                            break;
                        default:
                            // Xử lý cho trường hợp khác
                            break;
                    }
                }
            };
            SaveServiceCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) =>
            {
                if (IsNew)//tạo mới
                {

                    switch (Filter)
                    {
                        case "Phương tiện":
                            //cập nhập dữ liệu từ InforTRafficVm
                            if (TenPT == null ||  SLG == 0)
                            {
                                MessageBox.Show("Hãy điền các thông tin cần thiết.");
                            }
                            else
                            {
                                NewTraffic();
                            }
                           
                            break;
                        case "Nhà hàng":
                            if (TenNH == null || SDT == null || MoTa == null)
                            {
                                MessageBox.Show("Hãy điền các thông tin cần thiết.");
                            }
                            else
                            {
                                NewRestaurant();
                            }    

                            break;
                        case "Khách sạn":
                            Hotel(null);
                            break;
                        case "Dịch vụ khác":
                            OTherService(null);
                            break;
                        default:
                            // Xử lý cho trường hợp khác
                            break;
                    }
                }
                else//cập nhập
                {

                }

            });
        }

      
        private void Traffic(object obj) => CurrentView = new UCInforTrafffic();
        private void Restaurant(object obj) => CurrentView = new InforRestaurantVm();
        private void Hotel(object obj) => CurrentView = new InforHotelVM();
        private void OTherService(object obj) => CurrentView = new InforOTherServiceVM();
        public static string GenerateCode(string filter, string previousCode = null )
        {
            int newNumber;

            if (previousCode == null)
            {
                newNumber = 1;
            }
            else
            {
                // Lấy số từ mã trước đó
                int previousNumber = int.Parse(previousCode.Substring(2));
                newNumber = previousNumber + 1;
            }

            // Chuyển số mới thành chuỗi và thêm số không vào đầu nếu cần
            string newNumberStr = newNumber.ToString().PadLeft(6, '0');

            string newCode = filter + newNumberStr;

            return newCode;
        }
        private void NewTraffic()
        {
            var temp = new PHUONGTIEN {
                TENPT = TenPT,
                MAPT = MaPT,
                SLGHE = SLG
            };

            DataProvider.Ins.DB.PHUONGTIENs.Add(temp);
            DataProvider.Ins.DB.SaveChanges();

            MessageBox.Show("Đã tạo mới phương tiện thành công \nBạn có thể xem lại các thông tin");
            //sau cập nhập;
            //báo cho bên ServiceVm đã xong
            ServiceVM.IsDone = true;
            newPT = temp;
        }
        private void NewRestaurant()
        {
            var temp = new NHAHANG
            {
                MANH = MaNH,
                TENNH = TenNH,
                SDT = SDT,
                MOTA = MoTa,
            };

            DataProvider.Ins.DB.NHAHANGs.Add(temp);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Đã tạo mới nhà hàng thành công \nBạn có thể xem lại các thông tin");
            //sau cập nhập;
            //báo cho bên ServiceVm đã xong
            ServiceVM.IsDone = true;
            newNH = temp;
        }

    }
}
