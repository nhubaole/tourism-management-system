using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                AddTourWindow addTourWindow = new AddTourWindow();
                addTourWindow.ShowDialog();
                ListTuyen = new ObservableCollection<TUYEN>(DataProvider.Ins.DB.TUYENs);
            });
        }

        public ICommand AddTourCommand { get; set; }
        public ObservableCollection<TUYEN> ListTuyen { get => _ListTuyen; set { _ListTuyen = value; OnPropertyChanged(); } }
    }
}
