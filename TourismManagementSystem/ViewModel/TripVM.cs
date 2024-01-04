using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.UserControls;
using TourismManagementSystem.Model;
using System.Windows.Input;
using TourismManagementSystem.View;
using System.Windows;
using System.Windows.Controls;

namespace TourismManagementSystem.ViewModel
{
    internal class TripVM : BaseViewModel
    {

        public ICommand AddTripCommand { get; set; }
        //public ICommand DeleteTripCommand { get; set; }
        //public ICommand EditTripCommand { get; set; }
        public ICommand ViewTripCommand { get; set; }
        public ICommand RadioButtonCommand { get; set; }

        private ObservableCollection<CHUYEN> _ChuyenList;
        public ObservableCollection<CHUYEN> ChuyenList { get => _ChuyenList; set { _ChuyenList = value; OnPropertyChanged(); } }
        private string _Search;
        public string Search
        {
            get => _Search;
            set
            {
                _Search = value;
                //if (SelectedFilter == FilterCbItems[0])
                //{
                //    ChuyenList = new ObservableCollection<CHUYEN>(DataProvider.Ins.DB.CHUYENs.Where(t => t.MACHUYEN.Contains(Search)));
                //}
                //else if (SelectedFilter == FilterCbItems[1])
                //{
                //    ChuyenList = new ObservableCollection<CHUYEN>(DataProvider.Ins.DB.CHUYENs.Where(t => t.MATUYEN.Contains(Search)));
                //}
                //else if (SelectedFilter == FilterCbItems[2])
                //{
                //    ChuyenList = new ObservableCollection<CHUYEN>(DataProvider.Ins.DB.CHUYENs.Where(t => t.TGBATDAU.Value.Day.Equals(Search)));
                //}
                //else if (SelectedFilter == FilterCbItems[3])
                //{
                //    ChuyenList = new ObservableCollection<CHUYEN>(DataProvider.Ins.DB.CHUYENs.Where(t => t.LOAICHUYEN.TENLOAI.Contains(Search)));
                //}

                OnPropertyChanged();
            }
        }
        private string selectedFilter;
        public string SelectedFilter { get => selectedFilter; set { selectedFilter = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _FilterCbItems = new ObservableCollection<string>() { "Mã chuyến", "Mã tuyến", "Thời gian bắt đầu", "Loại chuyến" };
        public ObservableCollection<string> FilterCbItems { get => _FilterCbItems; set => _FilterCbItems = value; }

        private CHUYEN _selectedChuyen;
        public CHUYEN SelectedChuyen
        {
            get => _selectedChuyen;
            set
            {
                _selectedChuyen = value;
                OnPropertyChanged();
            }
        }
        private bool _IsDisplay;

        public bool IsDisplay { get => _IsDisplay; set { _IsDisplay = value; OnPropertyChanged(); } }
        private bool _IsAdmin;
        public bool IsAdmin { get => _IsAdmin; set { _IsAdmin = value; OnPropertyChanged(); } }


        public TripVM()
        {
            SelectedFilter = FilterCbItems[0];
            IsDisplay = true;
            IsAdmin = MainVM.AdminRole;
            ChuyenList = new ObservableCollection<CHUYEN>(DataProvider.Ins.DB.CHUYENs);
            AddTripCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                AddTripVM.IsEdit = 0;
                AddTripWindow addTourWindow = new AddTripWindow();              
                addTourWindow.ShowDialog();
                ChuyenList = new ObservableCollection<CHUYEN>(DataProvider.Ins.DB.CHUYENs);
            });
            //DeleteTripCommand = new RelayCommand<object>((p) => true, (p) =>
            //{
            //    var values = (object[])p;
            //    MainWindow mainWindow = Window.GetWindow((DependencyObject)values[1]) as MainWindow;
            //    CHUYEN selectedChuyen = values[0] as CHUYEN;
            //    if(selectedChuyen.PHIEUDATCHOes.Count() > 0)
            //    {
            //        MessageBox.Show("Chuyến du lịch đã có phiếu đặt chỗ. Không thể xóa chuyến");
            //    }
            //    else
            //    {
            //        if (MessageBox.Show("Bạn có muốn xóa chuyến du lịch này?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //        {
            //            foreach (var item in DataProvider.Ins.DB.HUONGDANVIENs)
            //            {
            //                foreach (var tripItem in selectedChuyen.HUONGDANVIENs)
            //                {
            //                    if (item.MAHDV.Equals(tripItem.MAHDV))
            //                        item.CHUYENs.Remove(selectedChuyen);

            //                }
            //            }
            //            THONGBAO newTB = new THONGBAO();
            //            string formattedID;
            //            ObservableCollection<THONGBAO> ListTB = new ObservableCollection<THONGBAO>(DataProvider.Ins.DB.THONGBAOs);
            //            if (ListTB.Count() == 0)
            //            {
            //                formattedID = string.Format("TB{0:D6}", 1);
            //            }
            //            else
            //            {
            //                string lastID = ListTB.Last().MATB;
            //                int previousNumber = int.Parse(lastID.Substring(2));
            //                int nextNumber = previousNumber + 1;
            //                formattedID = string.Format("TB{0:D6}", nextNumber);
            //            }
            //            newTB.MATB = formattedID;
            //            newTB.THONGBAO1 = "Chuyến du lịch \"" + selectedChuyen.TUYEN.TENTUYEN + "\" diễn ra vào ngày " + ((DateTime)selectedChuyen.TGBATDAU).ToString("MM/dd/yyyy") + " đã được xóa \nMã chuyến: " + selectedChuyen.MACHUYEN;
            //            newTB.THOIGIAN = DateTime.Now;
            //            newTB.DADOC = false;
            //            DataProvider.Ins.DB.THONGBAOs.Add(newTB);
            //            DataProvider.Ins.DB.SaveChanges();
            //            if (mainWindow != null)
            //            {
            //                MainVM mainVM = mainWindow.DataContext as MainVM;
            //                if (mainVM != null)
            //                {
            //                    mainVM.UnreadNotificationCount = DataProvider.Ins.DB.THONGBAOs.Where(t => t.DADOC == false).Count();
            //                }
            //            }

            //            DataProvider.Ins.DB.CHUYENs.Remove(selectedChuyen);

            //            DataProvider.Ins.DB.SaveChanges();
            //            ChuyenList = new ObservableCollection<CHUYEN>(DataProvider.Ins.DB.CHUYENs);
            //            MessageBox.Show("Xóa chuyến du lịch thành công");
            //        }
            //    }
            //});
            //EditTripCommand = new RelayCommand<object>((p) => true, (p) =>
            //{
            //    CHUYEN selectedChuyen = p as CHUYEN;
            //    AddTripVM.EditSelectedChuyen = selectedChuyen;
            //    AddTripVM.IsEdit = 1;
            //    AddTripWindow addTripWindow = new AddTripWindow();
            //    addTripWindow.ShowDialog();
            //});
            ViewTripCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                CHUYEN selectedChuyen = p as CHUYEN;
                TripDetailVM.SelectedChuyen = SelectedChuyen;
                TripDetailsWindow tripDetailWindow = new TripDetailsWindow();
                tripDetailWindow.ShowDialog();
            });
            RadioButtonCommand = new RelayCommand<UserControl>((p) => true, (p) =>
            {
                MainWindow mainWindow = Window.GetWindow(p) as MainWindow;
                if (mainWindow != null)
                {
                    MainVM mainVM = mainWindow.DataContext as MainVM;
                    if (mainVM != null)
                    {
                        mainVM.CurrentView = new TourVM();
                        mainVM.PageTitle = "Quản lý tuyến du lịch";
                    }
                }
            });
        }

    }
}
