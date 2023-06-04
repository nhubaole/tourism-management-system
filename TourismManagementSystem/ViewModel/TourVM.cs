using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourismManagementSystem.Model;
using TourismManagementSystem.View;

namespace TourismManagementSystem.ViewModel
{
    internal class TourVM : BaseViewModel
    {
        private ObservableCollection<TUYEN> _ListTuyen;
        private string _SearchText;
        private ObservableCollection<string> _FilterCbItems = new ObservableCollection<string>() { "Mã tuyến", "Tên tuyến", "Loại tuyến" };
        private string selectedFilter;
        private bool _IsAdmin;
        private bool _IsDisplay;

        public TourVM()
        {
            IsDisplay = true;
            IsAdmin = MainVM.AdminRole;
            ListTuyen = new ObservableCollection<TUYEN>(DataProvider.Ins.DB.TUYENs);
            SelectedFilter = FilterCbItems.First();
            AddTourCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                AddTourVM.IsEdit = 0;
                AddTourWindow addTourWindow = new AddTourWindow();
                addTourWindow.ShowDialog();
                ListTuyen = new ObservableCollection<TUYEN>(DataProvider.Ins.DB.TUYENs);
            });
            DeleteTourCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                TUYEN selectedTuyen = p as TUYEN;
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa tuyến du lịch này?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var ListLichTrinhOfDelTour = DataProvider.Ins.DB.LICHTRINHs.Where(lt => lt.TUYEN.MATUYEN == selectedTuyen.MATUYEN);

                    foreach (var lichTrinh in ListLichTrinhOfDelTour)
                    {
                        lichTrinh.PHUONGTIENs.Clear();
                        lichTrinh.KHACHSANs.Clear();
                        lichTrinh.NHAHANGs.Clear();
                        lichTrinh.DICHVUKHACs.Clear();
                        DataProvider.Ins.DB.LICHTRINHs.Remove(lichTrinh);
                    }
                    DataProvider.Ins.DB.TUYENs.Remove(selectedTuyen);
                    DataProvider.Ins.DB.SaveChanges();
                    ListTuyen = new ObservableCollection<TUYEN>(DataProvider.Ins.DB.TUYENs);
                    MessageBox.Show("Xóa tuyến du lịch thành công");
                }
            });
            EditTourCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                TUYEN selectedTuyen = p as TUYEN;
                AddTourVM.EditSelectedTuyen = selectedTuyen;
                AddTourVM.IsEdit = 1;
                AddTourWindow addTourWindow = new AddTourWindow();
                addTourWindow.ShowDialog();
            });
            ViewTourCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                TUYEN selectedTuyen = p as TUYEN;
                TourDetailsVM.SelectedTour = selectedTuyen;
                TourDetailsWindow tourDetailsWindow = new TourDetailsWindow();
                tourDetailsWindow.ShowDialog();
            });
            RadioButtonCommand = new RelayCommand<UserControl>((p) => true, (p) =>
            {
                MainWindow mainWindow = Window.GetWindow(p) as MainWindow;
                if (mainWindow != null)
                {
                    MainVM mainVM = mainWindow.DataContext as MainVM;
                    if (mainVM != null)
                    {
                        mainVM.CurrentView = new TripVM();
                    }
                }
            });
        }

        public ICommand AddTourCommand { get; set; }
        public ICommand DeleteTourCommand { get; set; }
        public ICommand EditTourCommand { get; set; }
        public ICommand ViewTourCommand { get; set; }
        public ICommand RadioButtonCommand { get; set; }
        public ObservableCollection<TUYEN> ListTuyen { get => _ListTuyen; set { _ListTuyen = value; OnPropertyChanged(); } }

        public string SearchText { 
            get => _SearchText; 
            set { 
                _SearchText = value; 
                if(SelectedFilter == FilterCbItems[0])
                {
                    ListTuyen = new ObservableCollection<TUYEN>(DataProvider.Ins.DB.TUYENs.Where(t => t.MATUYEN.Contains(SearchText)));
                }
                else if (SelectedFilter == FilterCbItems[1])
                {
                    ListTuyen = new ObservableCollection<TUYEN>(DataProvider.Ins.DB.TUYENs.Where(t => t.TENTUYEN.Contains(SearchText)));
                }
                else if (SelectedFilter == FilterCbItems[2])
                {
                    ListTuyen = new ObservableCollection<TUYEN>(DataProvider.Ins.DB.TUYENs.Where(t => t.LOAITUYEN.TENLOAI.Contains(SearchText)));
                }
                OnPropertyChanged(); 
            } 
        }
        public ObservableCollection<string> FilterCbItems { get => _FilterCbItems; set => _FilterCbItems = value; }
        public string SelectedFilter { get => selectedFilter; set { selectedFilter = value; OnPropertyChanged(); } }

        public bool IsAdmin { get => _IsAdmin; set { _IsAdmin = value; OnPropertyChanged(); } }

        public bool IsDisplay { get => _IsDisplay; set { _IsDisplay = value; OnPropertyChanged(); } }
    }
}
