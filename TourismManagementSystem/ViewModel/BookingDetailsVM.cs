using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TourismManagementSystem.Model;
using TourismManagementSystem.View;

namespace TourismManagementSystem.ViewModel
{
    internal class BookingDetailVM : BaseViewModel
    {
        private static PHIEUDATCHO _selectedPhieu;
        public static PHIEUDATCHO SelectedPhieu { get => _selectedPhieu; set { _selectedPhieu = value; } }
        private PHIEUDATCHO _phieu;
        public  PHIEUDATCHO Phieu { get => _phieu; set { _phieu = value; OnPropertyChanged(); } }

        public int Count => SelectedPhieu.HANHKHACHes?.Count ?? 0;
        public BookingDetailVM()
        {
            Phieu = _selectedPhieu;
            ThanhToanCommand = new RelayCommand<object>((p)=> true, (p) =>
            {
                //chuyen sang man hinh thanh toan
            });
            XuatVeCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                //chuyen sang man hinh xuat ve
            });

        }

        public ICommand ThanhToanCommand { get; set; }

        public ICommand XuatVeCommand { get; set; }
    }
}
