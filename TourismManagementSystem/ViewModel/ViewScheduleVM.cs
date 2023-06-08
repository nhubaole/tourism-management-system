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
    internal class ViewScheduleVM : BaseViewModel
    {
        private static TUYEN currTour;
        private string _SoNgayDem;
        private ObservableCollection<NgayTour2> listNgay;

        public ViewScheduleVM()
        {
            SoNgayDem = CurrTour.SONGAY + " ngày " + CurrTour.SODEM + " đêm";
            listNgay = new ObservableCollection<NgayTour2>();
            for (int i = 1; i <= CurrTour.SONGAY; i++)
            {
                NgayTour2 ngayTour = new NgayTour2("Ngày " + i);
                ListNgay.Add(ngayTour);
            }

            CloseCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                var viewScheduleWindow = p as Window;
                if (viewScheduleWindow != null)
                {
                    viewScheduleWindow.Close();
                }
            });
        }

        public ICommand CloseCommand { get; set; }

        public static TUYEN CurrTour
        {
            get => currTour;
            set
            {
                currTour = value;
            }
        }

        public string SoNgayDem { get => _SoNgayDem; set { _SoNgayDem = value; OnPropertyChanged(); } }

        public ObservableCollection<NgayTour2> ListNgay { get => listNgay; set { listNgay = value; OnPropertyChanged(); } }
    }

    class NgayTour2 : BaseViewModel
    {
        private string _NgayThu;
        private ObservableCollection<LICHTRINH> _ListLichTrinh;

        public NgayTour2(string ngayThu = "")
        {
            NgayThu = ngayThu;
            ListLichTrinh = new ObservableCollection<LICHTRINH>();
            foreach (var item in ViewScheduleVM.CurrTour.LICHTRINHs)
            {
                if (item.NGAYTHU == int.Parse(ngayThu.Substring(ngayThu.Length - 1, 1)))
                {
                    ListLichTrinh.Add(item);
                }
            }

            ViewScheduleCommand = new RelayCommand<object>((p) => {
                return true;
            },
            (p) => {
                LICHTRINH selectedLichTrinh = p as LICHTRINH;
                ScheduleDetailsVM.SelectedLichTrinh = selectedLichTrinh;
                ScheduleDetailsWindow scheduleDetailsWindow = new ScheduleDetailsWindow();
                scheduleDetailsWindow.ShowDialog();
            });
        }
        public ICommand ViewScheduleCommand { get; set; }


        public string NgayThu { get => _NgayThu; set { _NgayThu = value; OnPropertyChanged(); } }
        public ObservableCollection<LICHTRINH> ListLichTrinh { get => _ListLichTrinh; set { _ListLichTrinh = value; OnPropertyChanged(); } }
    }
}
