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

        public ObservableCollection<CHUYEN> ListChuyen { get; set; }

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
        public int Count { get => _count; set { _count = value; OnPropertyChanged(nameof(Count)); UpdateList();  } }


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
                    string formattedID;
                    do
                    {
                        int randomDigits = random.Next(0, 999999);
                        formattedID = string.Format("HK{0:D6}", randomDigits);
                    } while (ListHKOfPhieu.Any(t => t.MAHK == formattedID));
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
            ListChuyen = new ObservableCollection<CHUYEN>(DataProvider.Ins.DB.CHUYENs);
            ListHKOfPhieu = new ObservableCollection<HANHKHACH> { };
            IsNew = IsEdit;
            ToolTipText = "Chưa nhập đủ thông tin";
            ListKhachHang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
            if (IsEdit == 0)
            {
                Title = "Thêm mới phiếu đặt chỗ";
                _TinhTrangCbItems = new ObservableCollection<string>() { "Chưa thanh toán", "Đã thanh toán" };
                BtnText = "Thêm phiếu đặt chỗ";
                NewBooking = new PHIEUDATCHO();
                NewBooking.TINHTRANG = "Chưa thanh toán";
                string formattedID;
                ObservableCollection<PHIEUDATCHO> ListPhieu = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes);
                if (ListPhieu.Count() == 0)
                {
                    formattedID = string.Format("P{0:D7}", 1);
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
                if (string.IsNullOrEmpty(NewBooking.MAPHIEU) || Count == 0 || NewBooking.CHUYEN == null || NewBooking.KHACHHANG == null || NewBooking.NGAYDAT == null)
                {
                    ToolTipText = "Vui lòng nhập đủ các trường thông tin bắt buộc";
                    return false;
                }
                foreach (var Item in ListHKOfPhieu)
                {
                    if (string.IsNullOrEmpty(Item.HOTEN) || (string.IsNullOrEmpty(Item.NGSINH.ToString()) ) || (Item.GIOITINH == null))
                    {
                        ToolTipText = "Vui lòng nhập đầy đủ họ tên, giới tính và ngày sinh hành khách";
                        return false;
                    }
                }
                ToolTipText = "";
                return true;
            }, (p) =>
            {
                var addPhieuWindow = p as Window;
                if (addPhieuWindow != null)
                {
                    if (IsEdit == 0)
                    {
                        try
                        {
                            NewBooking.SLKHACH = Count;
                            NewBooking.HANHKHACHes = ListHKOfPhieu;
                            NewBooking.CHUYEN.SLTHUCTE += NewBooking.SLKHACH;

                            DataProvider.Ins.DB.PHIEUDATCHOes.Add(NewBooking);
                            DataProvider.Ins.DB.SaveChanges();
                            MessageBox.Show("Thêm mới phiếu thành công!");
                        }
                        catch
                        {
                            MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại!");
                        }
                    }
                    else
                    {
                        NewBooking.HANHKHACHes = ListHKOfPhieu;
                        if(NewBooking.SLKHACH < Count)
                        {
                            NewBooking.CHUYEN.SLTHUCTE += (Count - NewBooking.SLKHACH);
                        }
                        else if (NewBooking.SLKHACH > Count)
                        {
                            NewBooking.CHUYEN.SLTHUCTE -= (NewBooking.SLKHACH - Count);
                        }
                        NewBooking.SLKHACH = Count;

                        var itemToUpdate = DataProvider.Ins.DB.PHIEUDATCHOes.FirstOrDefault(item => item.MAPHIEU == NewBooking.MAPHIEU);
                        if (itemToUpdate != null)
                        {
                            DataProvider.Ins.DB.SaveChanges();
                            MessageBox.Show("Cập nhật phiếu đặt chỗ thành công!");
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
