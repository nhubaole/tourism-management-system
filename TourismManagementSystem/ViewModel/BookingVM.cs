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
    internal class BookingVM : BaseViewModel
    {
        public ICommand AddBookingCommand { get; set; }
        public ICommand DeleteBookingCommand { get; set; }
        public ICommand EditBookingCommand { get; set; }
        public ICommand ViewBookingCommand { get; set; }

        private ObservableCollection<PHIEUDATCHO> _bookingList;
        public ObservableCollection<PHIEUDATCHO> BookingList { get => _bookingList; set { _bookingList = value; OnPropertyChanged(); } }
        private string _Search;
        public string Search
        {
            get => _Search;
            set
            {
                _Search = value;
                if (SelectedFilter == FilterCbItems[0])
                {
                    BookingList = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes.Where(t => t.MAPHIEU.Contains(Search)));
                }
                else if (SelectedFilter == FilterCbItems[1])
                {
                    BookingList = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes.Where(t => t.MACHUYEN.Contains(Search)));
                }
                else if (SelectedFilter == FilterCbItems[2])
                {
                    BookingList = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes.Where(t => t.KHACHHANG.HOTEN.Contains(Search)));
                }
                else if (SelectedFilter == FilterCbItems[3])
                {
                    BookingList = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes.Where(t => t.TINHTRANG.Contains(Search)));
                }
                OnPropertyChanged();
            }
        }
        private string selectedFilter;
        public string SelectedFilter { get => selectedFilter; set { selectedFilter = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _FilterCbItems = new ObservableCollection<string>() { "Mã phiếu", "Mã chuyến", "Họ tên", "Tình trạng"};
        public ObservableCollection<string> FilterCbItems { get => _FilterCbItems; set => _FilterCbItems = value; }

        private CHUYEN _selectedPhieu;
        public CHUYEN SelectedPhieu
        {
            get => _selectedPhieu;
            set
            {
                _selectedPhieu = value;
                OnPropertyChanged();
            }
        }

        public BookingVM()
        {
            SelectedFilter = FilterCbItems[0];
            BookingList = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes);
            DeleteBookingCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var values = (object[])p;
                MainWindow mainWindow = Window.GetWindow((DependencyObject)values[1]) as MainWindow;
                PHIEUDATCHO selectedPhieu = values[0] as PHIEUDATCHO;
                if(selectedPhieu.CHUYEN.TGKETTHUC > DateTime.Now && selectedPhieu.TINHTRANG == "Đã thanh toán")
                {
                    MessageBox.Show("Phiếu đặt chỗ đã thanh toán nhưng chuyến đi chưa diễn ra. Không thể xóa phiếu");
                }
                else
                {
                    if (MessageBox.Show("Bạn có muốn xóa phiếu này?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var hanhkhachToRemove = selectedPhieu.HANHKHACHes.ToList();
                        foreach (var item in hanhkhachToRemove)
                        {
                            DataProvider.Ins.DB.HANHKHACHes.Remove(item);
                        }
                        var veToRemove = selectedPhieu.VEs.ToList();
                        foreach (var item in veToRemove)
                        {
                            DataProvider.Ins.DB.VEs.Remove(item);
                        }

                        var ttToRemove = selectedPhieu.THONGTINTHANHTOANs.ToList();
                        foreach (var item in ttToRemove)
                        {
                            DataProvider.Ins.DB.THONGTINTHANHTOANs.Remove(item);
                        }

                        THONGBAO newTB = new THONGBAO();
                        string formattedID;
                        ObservableCollection<THONGBAO> ListTB = new ObservableCollection<THONGBAO>(DataProvider.Ins.DB.THONGBAOs);
                        if (ListTB.Count() == 0)
                        {
                            formattedID = string.Format("TB{0:D6}", 1);
                        }
                        else
                        {
                            string lastID = ListTB.Last().MATB;
                            int previousNumber = int.Parse(lastID.Substring(2));
                            int nextNumber = previousNumber + 1;
                            formattedID = string.Format("TB{0:D6}", nextNumber);
                        }
                        newTB.MATB = formattedID;
                        newTB.THONGBAO1 = "Phiếu đặt chỗ " + selectedPhieu.MAPHIEU + " đã được xóa";
                        newTB.THOIGIAN = DateTime.Now;
                        newTB.DADOC = false;
                        DataProvider.Ins.DB.THONGBAOs.Add(newTB);
                        DataProvider.Ins.DB.SaveChanges();
                        if (mainWindow != null)
                        {
                            MainVM mainVM = mainWindow.DataContext as MainVM;
                            if (mainVM != null)
                            {
                                mainVM.UnreadNotificationCount = DataProvider.Ins.DB.THONGBAOs.Where(t => t.DADOC == false).Count();
                            }
                        }

                        DataProvider.Ins.DB.PHIEUDATCHOes.Remove(selectedPhieu);

                        DataProvider.Ins.DB.SaveChanges();
                        BookingList = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes);
                        MessageBox.Show("Xóa phiếu thành công");
                    }
                }
            });
            AddBookingCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                AddBookingVM.IsEdit = 0;
                AddBookingWindow addBookingWindow = new AddBookingWindow();
                addBookingWindow.ShowDialog();
                BookingList = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes);
            });

            EditBookingCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                PHIEUDATCHO selectedPhieu = p as PHIEUDATCHO;
               if(selectedPhieu.TINHTRANG == "Đã xuất vé") {
                    MessageBox.Show("Không thể sửa thông tin phiếu đã xuất vé");
                }
                else
                {
                    AddBookingVM.EditSelectedPhieu = selectedPhieu;
                    AddBookingVM.IsEdit = 1;
                    AddBookingWindow addBookingWindow = new AddBookingWindow();
                    addBookingWindow.ShowDialog();
                    BookingList = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes);
                }
            });
            ViewBookingCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                PHIEUDATCHO selectedPhieu = p as PHIEUDATCHO;
                BookingDetailVM.SelectedPhieu = selectedPhieu;
                BookingDetailsWindow bookingDetailWindow = new BookingDetailsWindow();
                bookingDetailWindow.ShowDialog();
                BookingList = new ObservableCollection<PHIEUDATCHO>(DataProvider.Ins.DB.PHIEUDATCHOes);
            });
        }


    }
}
