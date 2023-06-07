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
        public ObservableCollection<string> ListMaKhachHang { get; set; }
        private string _ToolTipText;

        public ObservableCollection<string> ListChuyen { get; set; }

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
                Count = ListHKOfPhieu.Count;
                OnPropertyChanged(nameof(ListHKOfPhieu));
            }
        }
        private int _count;
        public int Count { get => _count; set { _count = value; DtgHeight= 50 + value*50 ; OnPropertyChanged(nameof(Count)); UpdateList();  } }

        private int _dtgHeight;
        public int DtgHeight { get => _dtgHeight; set { _dtgHeight = value; OnPropertyChanged(nameof(_dtgHeight)); } }

        private void UpdateList()
        {
            int currentRowCount = ListHKOfPhieu.Count;
            if (Count < currentRowCount)
            {
                Count = ListHKOfPhieu.Count;
            }
            else if (Count > currentRowCount)
            {
                // Tăng số hàng
                while (ListHKOfPhieu.Count < Count)
                {
                    Random random = new Random();
                    int randomDigits = random.Next(0, 999999);
                    string formattedID = string.Format("LT{0:D6}", randomDigits);
                    HANHKHACH newHK = new HANHKHACH();
                    newHK.MAHK = formattedID;
                    ListHKOfPhieu.Add(newHK);
                }
            }


            //if (Count > currentRowCount)
            //{
            //    int itemsToAdd = Count - currentRowCount;

            //    for (int i = 0; i < itemsToAdd; i++)
            //    {
            //        HANHKHACH n = new HANHKHACH();
            //        Random random = new Random();
            //        n.MAHK = random.Next(0,1000).ToString();
            //        ListHKOfPhieu.Add(new HANHKHACH());
            //    }
            //}
            //else if (Count < currentRowCount)
            //{
            //    int itemsToRemove = currentRowCount - Count;

            //    for (int i = 0; i < itemsToRemove; i++)
            //    {
            //        ListHKOfPhieu.RemoveAt(ListHKOfPhieu.Count - 1);
            //    }
            //}
        }
        public AddBookingVM()
        {
            ListMaKhachHang = new ObservableCollection<string>(DataProvider.Ins.DB.KHACHHANGs.Select(t => t.MAKH));
            ListChuyen = new ObservableCollection<string>(DataProvider.Ins.DB.CHUYENs.Select(t => t.MACHUYEN));
            ListHKOfPhieu = new ObservableCollection<HANHKHACH> { };
            IsNew = IsEdit;
            ToolTipText = "Chưa nhập đủ thông tin";
            ListKhachHang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
            if (IsEdit == 0)
            {
                Count = 1;
                Title = "Thêm mới phiếu đặt chỗ";
                _TinhTrangCbItems = new ObservableCollection<string>() { "Chưa thanh toán", "Đã thanh toán" };
                BtnText = "Thêm phiếu đặt chỗ";
                NewBooking = new PHIEUDATCHO();
                NewBooking.TINHTRANG = "Chưa thanh toán";
                string formattedID;
                ObservableCollection<PHIEUDATCHO> ListPhieu = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes);
                if (ListPhieu.Count() == 0)
                {
                    formattedID = string.Format("T{0:D7}", 1);
                }
                else
                {
                    string lastID = ListPhieu.Last().MAPHIEU;
                    int previousNumber = int.Parse(lastID.Substring(1));
                    int nextNumber = previousNumber + 1;
                    formattedID = string.Format("P{0:D7}", nextNumber);
                }
                NewBooking.MAPHIEU = formattedID;
            }
            else
            {
                ListHKOfPhieu = new ObservableCollection<HANHKHACH>(EditSelectedPhieu.HANHKHACHes);
                Count = EditSelectedPhieu.HANHKHACHes.Count;
                _TinhTrangCbItems = new ObservableCollection<string>() { "Chưa thanh toán", "Đã thanh toán", "Đã hủy" };
                Title = "Cập nhật phiếu đặt chỗ";
                BtnText = "Cập nhật phiếu đặt chỗ";
                NewBooking = EditSelectedPhieu;
            }


            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NewBooking.MAPHIEU) || ListHKOfPhieu.Count == 0 || NewBooking.MACHUYEN == null || NewBooking.MAKH == null || NewBooking.NGAYDAT == null )
                {
                    ToolTipText = "Vui lòng nhập đủ các trường thông tin bắt buộc";
                    return false;
                }
                foreach (var Item in ListHKOfPhieu)
                {
                    if (string.IsNullOrEmpty(Item.MAHK) || string.IsNullOrEmpty(Item.HOTEN) || (string.IsNullOrEmpty(Item.GIOITINH) || string.IsNullOrEmpty(Item.DIACHI) || string.IsNullOrEmpty(Item.NGSINH.ToString()) || string.IsNullOrEmpty(Item.CCCD) || string.IsNullOrEmpty(Item.NGAYHETHANVISA.ToString()) || string.IsNullOrEmpty(Item.NGAYHETHANPASSPORT.ToString()) || string.IsNullOrEmpty(Item.SDT) || string.IsNullOrEmpty(Item.PASSPORT)))
                    {
                        ToolTipText = "Vui lòng nhập đủ các trường thông tin bắt buộc";
                        return false;
                    }
                }
                return true;
            }, (p) =>
            {
                var addPhieuWindow = p as Window;
                if (addPhieuWindow != null)
                {
                    if (IsEdit == 0)
                    {
                        NewBooking.SLKHACH = Count;
                        NewBooking.TINHTRANG = "Chưa thanh toán";
                        NewBooking.HANHKHACHes = ListHKOfPhieu;
                        foreach (var t in NewBooking.HANHKHACHes)
                        {
                            t.PHIEUDATCHO = NewBooking;
                            t.MAPHIEU = NewBooking.MAPHIEU;
                        }
                        NewBooking.CHUYEN = DataProvider.Ins.DB.CHUYENs.FirstOrDefault(t => t.MACHUYEN == NewBooking.MACHUYEN);
                        DataProvider.Ins.DB.PHIEUDATCHOes.Add(NewBooking);
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Thêm mới phiếu thành công!");
                    }
                    else
                    {
                        NewBooking.SLKHACH = Count;
                        NewBooking.HANHKHACHes = ListHKOfPhieu;
                        foreach (var t in NewBooking.HANHKHACHes)
                        {
                            t.MAPHIEU = NewBooking.MAPHIEU;
                            t.PHIEUDATCHO = NewBooking;
                        }
                        NewBooking.CHUYEN = DataProvider.Ins.DB.CHUYENs.FirstOrDefault(t => t.MACHUYEN == NewBooking.MACHUYEN);
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
            DeleteKhachCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                HANHKHACH selectedHanhKhach = p as HANHKHACH;
                if (MessageBox.Show("Bạn có muốn xóa hành khách này?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ListHKOfPhieu.Remove(selectedHanhKhach);
                    Count -= 1;
                    OnPropertyChanged();
                    MessageBox.Show("Xóa hành khách thành công");
                }
            });

        }

        public ICommand AddCommand { get; set; }

        public ICommand DeleteKhachCommand { get; set; }

        public string ToolTipText { get => _ToolTipText; set { _ToolTipText = value; OnPropertyChanged(); } }
        public static int IsEdit { get => _IsEdit; set => _IsEdit = value; }
        public string Title { get => _Title; set => _Title = value; }
        public string BtnText { get => _BtnText; set => _BtnText = value; }
    }
}
