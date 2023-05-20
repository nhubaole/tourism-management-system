using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.View;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    internal class AddTourVM : BaseViewModel
    {
        private string _MaTuyen;
        private string _TenTuyen;
        private string _DDXuatPhat;
        private int _SoNgay;
        private int _SoDem;
        private int _GiaDuKien;
        private string _MaLoai;
        private TUYEN newTour = new TUYEN();
        public AddTourVM()
        {
            ScheduleOptionCommand = new RelayCommand<Window>((p) => { 
                if (p == null)
                {
                    return false;
                }

                if (SoNgay == 0 || SoDem == 0)
                {
                    return false;
                }

                return true;
            }, (p) => {
                var addTourWindow = p as Window;
                if (addTourWindow != null)
                {
                    ScheduleVM.CurrTour = newTour;
                    ScheduleWindow scheduleWindow = new ScheduleWindow();
                    scheduleWindow.ShowDialog();
                }
            });

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                DataProvider.Ins.DB.TUYENs.Add(newTour);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Thêm mới tuyến thành công!");
            });
        }

        public ICommand ScheduleOptionCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public string MaTuyen { get => _MaTuyen; set { _MaTuyen = value; OnPropertyChanged(); newTour.MATUYEN = MaTuyen; }  }
        public string TenTuyen { get => _TenTuyen; set { _TenTuyen = value; OnPropertyChanged(); newTour.TENTUYEN = TenTuyen; }  }
        public string DDXuatPhat { get => _DDXuatPhat; set { _DDXuatPhat = value; OnPropertyChanged(); newTour.DDXUATPHAT = DDXuatPhat; } }
        public int SoNgay { get => _SoNgay; set { _SoNgay = value; OnPropertyChanged(); newTour.SONGAY = SoNgay; } }
        public int SoDem { get => _SoDem; set { _SoDem = value; OnPropertyChanged(); newTour.SODEM = SoDem; } }
        public int GiaDuKien { get => _GiaDuKien; set { _GiaDuKien = value; OnPropertyChanged(); newTour.GIADUKIEN = GiaDuKien; } }
        public string MaLoai { get => _MaLoai; set { _MaLoai = value; OnPropertyChanged(); newTour.MALOAI = MaLoai; } }

    }
}
