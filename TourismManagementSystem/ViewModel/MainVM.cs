using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.Model;
using TourismManagementSystem.View;

namespace TourismManagementSystem.ViewModel
{
    internal class MainVM : BaseViewModel
    {
        public static bool AdminRole;
        private object _currentView;
        private string _PageTitle;
        private string _AccountTitle;
        private bool _IsDisplay;
        private int _UnreadNotificationCount;
       
        public MainVM()
        {
            UnreadNotificationCount = DataProvider.Ins.DB.THONGBAOs.Where(t => t.DADOC == false).Count();
            HomeCommand = new RelayCommand<object>((p) => { return true; }, Home);
            BookingCommand = new RelayCommand<object>((p) => { return true; }, Booking);
            CustomerCommand = new RelayCommand<object>((p) => { return true; }, Customer);
            LocationCommand = new RelayCommand<object>((p) => { return true; }, Location);
            NotificationCommand = new RelayCommand<object>((p) => { return true; }, Notification);
            RevenueStatisticCommand = new RelayCommand<object>((p) => { return true; }, RevenueStatistic);
            ServiceCommand = new RelayCommand<object>((p) => { return true; }, Service);
            TicketCommand = new RelayCommand<object>((p) => { return true; }, Ticket);
            TourCommand = new RelayCommand<object>((p) => { return true; }, Tour);
            TripCommand = new RelayCommand<object>((p) => { return true; }, Trip);

            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                try
                {
                    IsLoaded = true;
                    if (p == null) return;
                    p.Hide();

                    LoginWindow login = new LoginWindow();
                    login.ShowDialog();

                    if (login.DataContext == null) return;
                    var loginVM = login.DataContext as LoginVM;
                    if (loginVM.IsLogin == true)
                    {
                        CurrentView = new HomeVM();
                        PageTitle = "Trang chủ";
                        AccountTitle = "Quản trị viên";
                        IsDisplay = true;
                        AdminRole = true;
                        p.Show();
                    }
                    else if (loginVM.IsGuest == true)
                    {
                        CurrentView = new HomeVM();
                        PageTitle = "Trang chủ";
                        AccountTitle = "Khách";
                        IsDisplay = false;
                        AdminRole = false;
                        p.Show();
                    }
                    else
                    {
                        p.Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            LogOutCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                if(AdminRole == true)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var w = p as Window;
                        if (w != null)
                        {
                            MainWindow main = new MainWindow();
                            w.Close();
                            main.Show();
                        }
                    }
                }
                else
                {
                    var w = p as Window;
                    if (w != null)
                    {
                        MainWindow main = new MainWindow();
                        w.Close();
                        main.Show();
                    }
                }
            });

            WarnTrip();

        }

        public bool IsLoaded = false;

        public object CurrentView
        {
            get => _currentView;
            set {
                if (_currentView != null && _currentView.GetType() == typeof(TourismManagementSystem.ViewModel.NotificationVM))
                {
                    foreach (var thongBao in DataProvider.Ins.DB.THONGBAOs)
                    {
                        thongBao.DADOC = true;
                    }

                    DataProvider.Ins.DB.SaveChanges();
                    UnreadNotificationCount = 0;
                }
                _currentView = value; OnPropertyChanged();
            }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand BookingCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand LocationCommand { get; set; }
        public ICommand NotificationCommand { get; set; }
        public ICommand RevenueStatisticCommand { get; set; }
        public ICommand ServiceCommand { get; set; }
        public ICommand TicketCommand { get; set; }
        public ICommand TourCommand { get; set; }
        public ICommand TripCommand { get; set; }

        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public string PageTitle { get => _PageTitle; set { _PageTitle = value; OnPropertyChanged(); } }

        public string AccountTitle { get => _AccountTitle; set { _AccountTitle = value; OnPropertyChanged(); } }

        public bool IsDisplay { get => _IsDisplay; set { _IsDisplay = value; OnPropertyChanged(); } }
        public int UnreadNotificationCount
        {
            get { return _UnreadNotificationCount; }
            set
            {
                _UnreadNotificationCount = value;
                OnPropertyChanged();
            }
        }

        //public object ObsevableCollection { get; private set; }

        public void WarnTrip()
        {
            ObservableCollection<CHUYEN> ListChuyen = new ObservableCollection<CHUYEN>(DataProvider.Ins.DB.CHUYENs);

            foreach(var item in ListChuyen)
            {
                TimeSpan timeSpan = item.TGBATDAU.Value - DateTime.Now;

                int numberOfDays = timeSpan.Days;
                if (timeSpan <= TimeSpan.FromDays(30) && DateTime.Now.Date < item.TGBATDAU.Value.Date && item.SLTHUCTE < item.SLTOITHIEU)
                {
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
                    newTB.THONGBAO1 = "Chuyến đi " + item.MACHUYEN + " sẽ bắt đầu trong " + timeSpan.Days + " ngày nữa nhưng chưa đạt số lượng tối thiểu!";
                    newTB.THOIGIAN = DateTime.Now;
                    newTB.DADOC = false;
                    DataProvider.Ins.DB.THONGBAOs.Add(newTB);
                    DataProvider.Ins.DB.SaveChanges();
                }
            }
            UnreadNotificationCount = DataProvider.Ins.DB.THONGBAOs.Where(t => t.DADOC == false).Count();

        }
        private void Home(object obj)
        {
            PageTitle = "Trang chủ";
            CurrentView = new HomeVM();
        }
        private void Booking(object obj)
        {
            PageTitle = "Quản lý phiếu đặt chỗ";
            if (AdminRole)
            {
                CurrentView = new BookingVM();
            }
            else
            {
                CurrentView = new RequireLoginVM();
            }
        }
        private void Customer(object obj)
        {
            PageTitle = "Quản lý khách hàng";
            if (AdminRole)
            {
                CurrentView = new CustomerVM();
            }
            else
            {
                CurrentView = new RequireLoginVM();
            }
        }
        private void Location(object obj)
        {
            PageTitle = "Quản lý địa điểm du lịch";
            if (AdminRole)
            {
                CurrentView = new LocationVM();
            }
            else
            {
                CurrentView = new RequireLoginVM();
            }
        }
        private void RevenueStatistic(object obj)
        {
            PageTitle = "Báo cáo thống kê";
            if (AdminRole)
            {
                CurrentView = new RevenueStatisticVM();
            }
            else
            {
                CurrentView = new RequireLoginVM();
            }
        }
        private void Service(object obj)
        {
            PageTitle = "Quản lý dịch vụ du lịch";
            if (AdminRole)
            {
                CurrentView = new ServiceVM();
            }
            else
            {
                CurrentView = new RequireLoginVM();
            }
        }
        private void Notification(object obj)
        {
            PageTitle = "Thông báo";
            if (AdminRole)
            {
                CurrentView = new NotificationVM();
            }
            else
            {
                CurrentView = new RequireLoginVM();
            }
        }
        private void Ticket(object obj)
        {
            PageTitle = "Quản lý vé";
            if (AdminRole)
            {
                CurrentView = new TicketVM();
            }
            else
            {
                CurrentView = new RequireLoginVM();
            }
        }
        private void Tour(object obj)
        {
            CurrentView = new TourVM();
            PageTitle = "Quản lý tuyến du lịch";
        }
        private void Trip(object obj)
        {
            CurrentView = new TripVM();
            PageTitle = "Quản lý chuyến du lịch";
        }

    }
}
