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
    internal class TourVM : BaseViewModel
    {
        private ObservableCollection<TUYEN> _ListTuyen;
        public TourVM()
        {
            ListTuyen = new ObservableCollection<TUYEN>(DataProvider.Ins.DB.TUYENs);
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
        }

        public ICommand AddTourCommand { get; set; }
        public ICommand DeleteTourCommand { get; set; }
        public ICommand EditTourCommand { get; set; }
        public ObservableCollection<TUYEN> ListTuyen { get => _ListTuyen; set { _ListTuyen = value; OnPropertyChanged(); } }
    }
}
