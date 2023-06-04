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

        public ObservableCollection<HANHKHACH> ListKhach { get; set; }

        private static CHUYEN _selectedChuyen;
        public static CHUYEN SelectedChuyen { get => _selectedChuyen; set 
            {
                _selectedChuyen = value;
            }
        }
        private CHUYEN _chuyen;
        public CHUYEN CHUYEN { get => _chuyen; set { _chuyen = value; OnPropertyChanged(); } }
        private bool _IsAdmin;
        public bool IsAdmin { get => _IsAdmin; set { _IsAdmin = value; OnPropertyChanged(); } }
        public TripDetailVM()
        {
            IsAdmin = MainVM.AdminRole;
            CHUYEN = SelectedChuyen;
            ListKhach = new ObservableCollection<HANHKHACH>(DataProvider.Ins.DB.HANHKHACHes.Where(k => k.PHIEUDATCHO.MACHUYEN.Equals(SelectedChuyen.MACHUYEN)));
        }
    }
}
