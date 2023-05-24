using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.Model;
using TourismManagementSystem.View;

namespace TourismManagementSystem.ViewModel
{
    internal class TourDetailsVM : BaseViewModel
    {
        private static TUYEN _SelectedTour;
        private TUYEN _Tour;
        private string numDayNight;

        public TourDetailsVM()
        {
            Tour = SelectedTour;
            numDayNight = Tour.SONGAY + " ngày " + Tour.SODEM + " đêm";
            CloseCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                var tourDetailsWindow = p as Window;
                if (tourDetailsWindow != null)
                {
                    tourDetailsWindow.Close();
                }
            });
            ScheduleOptionCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) => {
                ViewScheduleVM.CurrTour = Tour;
                ViewScheduleWindow viewScheduleWindow = new ViewScheduleWindow();
                viewScheduleWindow.ShowDialog();
            });
        }

        public ICommand CloseCommand { get; set; }
        public ICommand ScheduleOptionCommand { get; set; }
        public static TUYEN SelectedTour { get => _SelectedTour; set => _SelectedTour = value; }
        public TUYEN Tour { get => _Tour; set { _Tour = value; OnPropertyChanged(); }  }

        public string NumDayNight { get => numDayNight; set { numDayNight = value; OnPropertyChanged(); } }
    }
}
