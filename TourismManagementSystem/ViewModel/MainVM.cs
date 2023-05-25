using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.View;

namespace TourismManagementSystem.ViewModel
{
    internal class MainVM : BaseViewModel
    {
        private object _currentView;
        public MainVM()
        {
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
            });
        }

        public bool IsLoaded = false;

        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
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


        private void Home(object obj) => CurrentView = new HomeVM();
        private void Booking(object obj) => CurrentView = new BookingVM();
        private void Customer(object obj) => CurrentView = new CustomerVM();
        private void Location(object obj) => CurrentView = new LocationVM();
        private void Notification(object obj) => CurrentView = new NotificationVM();
        private void RevenueStatistic(object obj) => CurrentView = new RevenueStatisticVM();
        private void Service(object obj) => CurrentView = new ServiceVM();
        private void Ticket(object obj) => CurrentView = new TicketVM();
        private void Tour(object obj) => CurrentView = new TourVM();
        private void Trip(object obj) => CurrentView = new TripVM();

    }
}
