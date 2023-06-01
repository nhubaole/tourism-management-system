using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    internal class TripDetailVM : BaseViewModel
    {
        public ObservableCollection<LOAICHUYEN> ListLoaiChuyen { get; set; }

        public ObservableCollection<KHACHHANG> ListKhach { get; set; }
        public ObservableCollection<HUONGDANVIEN> ListHDV { get; set; }

        private static CHUYEN _selectedChuyen;
        public static CHUYEN SelectedChuyen { get => _selectedChuyen; set 
            {
                _selectedChuyen = value;
            }
        }
        private CHUYEN _chuyen;
        public CHUYEN CHUYEN { get => _chuyen; set { _chuyen = value; OnPropertyChanged(); } }
        public TripDetailVM()
        {
            CHUYEN = SelectedChuyen;
            ListLoaiChuyen = new ObservableCollection<LOAICHUYEN>(DataProvider.Ins.DB.LOAICHUYENs);
            ListKhach = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(k => k.PHIEUDATCHOes.Any(p => p.CHUYEN.MACHUYEN.Equals(SelectedChuyen.MACHUYEN))));
            ListHDV = new ObservableCollection<HUONGDANVIEN>(DataProvider.Ins.DB.HUONGDANVIENs.Where(h => h.CHUYENs.Any(c=>c.MACHUYEN.Equals(SelectedChuyen.MACHUYEN))));
        }
    }
}
