using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TourismManagementSystem.Model;
using TourismManagementSystem.View;

namespace TourismManagementSystem.ViewModel
{
    internal class AddBookingVM : BaseViewModel
    {
        private PHIEUDATCHO _newBooking;
        public PHIEUDATCHO NewBooking { get => _newBooking; set { _newBooking = value; OnPropertyChanged(); } }
        public ObservableCollection<KHACHHANG> ListKhachHang { get; set; }
        private string _ToolTipText;


        private string _masoKH;
        public string MasoKH { get => _masoKH; set { _masoKH = value; OnPropertyChanged(); } }
        private static PHIEUDATCHO _editSelectedPhieu;
        public static PHIEUDATCHO EditSelectedPhieu{ get => _editSelectedPhieu; set { _editSelectedPhieu = value; } }
        private static int _IsEdit = 0;

        private int _isNew;
        public int IsNew { get => _isNew; set { _isNew = value; OnPropertyChanged(); } }

        private string _Title;
        private string _BtnText;

        private ObservableCollection<string> _TinhTrangCbItems;
        public ObservableCollection<string> TinhTrangCbItems { get => _TinhTrangCbItems; set => _TinhTrangCbItems = value; }
        private int _soLuong;
        public int SoLuong { get => _soLuong; set { _soLuong = value; OnPropertyChanged(); } }

        private ObservableCollection<HANHKHACH> _listHKOfPhieu;
        public ObservableCollection<HANHKHACH> ListHKOfPhieu
        {
            get { return _listHKOfPhieu; }
            set
            {
                _listHKOfPhieu = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Count));
            }
        }

        public int Count => ListHKOfPhieu?.Count ?? 0;
        public AddBookingVM()
        {
            IsNew = IsEdit;
            ToolTipText = "Chưa nhập đủ thông tin";
            ListKhachHang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
            if (IsEdit == 0)
            {
                Title = "Thêm mới phiếu đặt chỗ";
                _TinhTrangCbItems = new ObservableCollection<string>() { "Chưa thanh toán" };
                BtnText = "Thêm phiếu đặt chỗ";
                NewBooking = new PHIEUDATCHO();
                Random random = new Random();
                NewBooking.MAPHIEU = random.Next(0, 1000).ToString();

            }
            else
            {
                _TinhTrangCbItems = new ObservableCollection<string>() { "Chưa thanh toán", "Đã thanh toán", "Đã hủy" };
                Title = "Cập nhật phiếu đặt chỗ";
                BtnText = "Cập nhật phiếu đặt chỗ";
                NewBooking = EditSelectedPhieu;
            }


            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NewBooking.MAPHIEU) || NewBooking.HANHKHACHes.Count == 0 || NewBooking.MACHUYEN == null || NewBooking.MAKH == null || NewBooking.NGAYDAT == null || NewBooking.SLKHACH == null || NewBooking.TINHTRANG == null)
                {
                    ToolTipText = "Vui lòng nhập đủ các trường thông tin bắt buộc";
                    return false;
                }
                return true;
            }, (p) =>
            {
                var addPhieuWindow = p as Window;
                if (addPhieuWindow != null)
                {
                    if (IsEdit == 0)
                    {
                        NewBooking.SLKHACH = SoLuong;
                        NewBooking.TINHTRANG = "Chưa thanh toán";
                        DataProvider.Ins.DB.PHIEUDATCHOes.Add(NewBooking);
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Thêm mới phiếu thành công!");
                    }
                    else
                    {
                        NewBooking.SLKHACH = SoLuong;
                        var itemToUpdate = DataProvider.Ins.DB.PHIEUDATCHOes.FirstOrDefault(item => item.MAPHIEU == NewBooking.MAPHIEU);
                        if (itemToUpdate != null)
                        {
                            DataProvider.Ins.DB.SaveChanges();
                            MessageBox.Show("Cập nhật thành công!");
                        }
                    }
                    addPhieuWindow.Close();
                }
            });
            AddKhachCommand = new RelayCommand<object>((p)=> true, (p)=>
            {
            //    undone
            });
            DeleteKhachCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                HANHKHACH selectedHanhKhach = p as HANHKHACH;
                if (MessageBox.Show("Bạn có muốn xóa hành khách này?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    NewBooking.HANHKHACHes.Remove(selectedHanhKhach);
                    OnPropertyChanged();
                    MessageBox.Show("Xóa hành khách thành công");
                }

            });

        }

        public ICommand AddCommand { get; set; }

        public ICommand AddKhachCommand { get; set; }
        public ICommand DeleteKhachCommand { get; set; }

        public string ToolTipText { get => _ToolTipText; set { _ToolTipText = value; OnPropertyChanged(); } }
        public static int IsEdit { get => _IsEdit; set => _IsEdit = value; }
        public string Title { get => _Title; set => _Title = value; }
        public string BtnText { get => _BtnText; set => _BtnText = value; }
    }
}
