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
    internal class ScheduleVM : BaseViewModel
    {
        private static TUYEN currTour;
        private string _SoNgayDem;
        private ObservableCollection<NgayTour> listNgay;

        public ScheduleVM()
        {
            SoNgayDem = CurrTour.SONGAY + " ngày " + CurrTour.SODEM + " đêm";
            listNgay = new ObservableCollection<NgayTour>();
            for(int i = 1; i <= CurrTour.SONGAY; i++)
            {
                NgayTour ngayTour = new NgayTour("Ngày " + i);
                ListNgay.Add(ngayTour);
            }
        }

        public static TUYEN CurrTour { 
            get => currTour;
            set
            {
                currTour = value;
            }
        }

        public string SoNgayDem { get => _SoNgayDem; set { _SoNgayDem = value; OnPropertyChanged(); }  }

        public ObservableCollection<NgayTour> ListNgay { get => listNgay; set { listNgay = value; OnPropertyChanged(); } }
    }

    class NgayTour : BaseViewModel
    {
        private string _NgayThu;
        private ObservableCollection<LICHTRINH> _ListLichTrinh;

        public NgayTour(string ngayThu = "")
        {
            NgayThu = ngayThu;
            ListLichTrinh = new ObservableCollection<LICHTRINH>();
            AddScheduleCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) => {
                AddScheduleVM.CurrListLichTrinh = ListLichTrinh;
                AddScheduleWindow addScheduleWindow = new AddScheduleWindow();
                addScheduleWindow.ShowDialog();
            });
        }
        public ICommand AddScheduleCommand { get; set; }

        public string NgayThu { get => _NgayThu; set { _NgayThu = value; OnPropertyChanged(); }  }
        public ObservableCollection<LICHTRINH> ListLichTrinh { get => _ListLichTrinh; set { _ListLichTrinh = value; OnPropertyChanged(); }  }
    }
}
