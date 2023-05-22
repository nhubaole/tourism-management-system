using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    internal class ScheduleDetailsVM : BaseViewModel
    {
        private static LICHTRINH _SelectedLichTrinh;
        private LICHTRINH lichTrinh;

        public ScheduleDetailsVM()
        {
            lichTrinh = SelectedLichTrinh;
            CloseCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                var scheduleDetailsWindow = p as Window;
                if(scheduleDetailsWindow != null)
                {
                    scheduleDetailsWindow.Close();
                }
            });
        }

        public ICommand CloseCommand { get; set; }

        public static LICHTRINH SelectedLichTrinh { get => _SelectedLichTrinh; set => _SelectedLichTrinh = value; }
        public LICHTRINH LichTrinh { get => lichTrinh; set { lichTrinh = value; OnPropertyChanged(); } }
    }
}
