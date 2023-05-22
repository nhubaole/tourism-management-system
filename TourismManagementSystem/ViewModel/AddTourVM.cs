using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.View;
using TourismManagementSystem.Model;
using System.Collections.ObjectModel;

namespace TourismManagementSystem.ViewModel
{
    internal class AddTourVM : BaseViewModel
    {
        private TUYEN newTour = new TUYEN();
        public ObservableCollection<DIADIEM> ListDiaDiem { get; set; }
        public ObservableCollection<LOAITUYEN> ListLoaiTuyen { get; set; }

        public AddTourVM()
        {
            ListDiaDiem = new ObservableCollection<DIADIEM>(DataProvider.Ins.DB.DIADIEMs);
            ListLoaiTuyen = new ObservableCollection<LOAITUYEN>(DataProvider.Ins.DB.LOAITUYENs);
            Random random = new Random();
            NewTour.MATUYEN = random.Next(0, 1000).ToString();
            ScheduleOptionCommand = new RelayCommand<Window>((p) => { 
                if (p == null)
                {
                    return false;
                }

                if (NewTour.SONGAY == 0 || NewTour.SODEM == 0)
                {
                    return false;
                }

                return true;
            }, (p) => {
                var addTourWindow = p as Window;
                if (addTourWindow != null)
                {
                    ScheduleVM.CurrTour = NewTour;
                    ScheduleWindow scheduleWindow = new ScheduleWindow();
                    scheduleWindow.ShowDialog();
                }
            });

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                DataProvider.Ins.DB.TUYENs.Add(NewTour);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Thêm mới tuyến thành công!");
            });
        }

        public ICommand ScheduleOptionCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public TUYEN NewTour { get => newTour; set { newTour = value; OnPropertyChanged(); } }
    }
}
