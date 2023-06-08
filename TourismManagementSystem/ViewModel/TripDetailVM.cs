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

        private ObservableCollection<HANHKHACH> _ListKhach;

        private string _SearchText;
        private ObservableCollection<string> _FilterCbItems = new ObservableCollection<string>() { "CCCD", "Họ tên", "Giới tính" };
        private string selectedFilter;
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

        public string SearchText { 
            get => _SearchText; 
            set {
                _SearchText = value;
                if (SelectedFilter == FilterCbItems[0])
                {
                    ListKhach = new ObservableCollection<HANHKHACH>(DataProvider.Ins.DB.HANHKHACHes.Where(k => k.PHIEUDATCHO.MACHUYEN.Equals(SelectedChuyen.MACHUYEN) && k.CCCD.Contains(SearchText)));
                }
                else if (SelectedFilter == FilterCbItems[1])
                {
                    ListKhach = new ObservableCollection<HANHKHACH>(DataProvider.Ins.DB.HANHKHACHes.Where(k => k.PHIEUDATCHO.MACHUYEN.Equals(SelectedChuyen.MACHUYEN) && k.HOTEN.Contains(SearchText)));

                }
                else if (SelectedFilter == FilterCbItems[2])
                {
                    ListKhach = new ObservableCollection<HANHKHACH>(DataProvider.Ins.DB.HANHKHACHes.Where(k => k.PHIEUDATCHO.MACHUYEN.Equals(SelectedChuyen.MACHUYEN) && k.GIOITINH.Contains(SearchText)));

                }
                OnPropertyChanged();
            } 
        }
        public ObservableCollection<string> FilterCbItems { get => _FilterCbItems; set => _FilterCbItems = value; }
        public string SelectedFilter { get => selectedFilter; set { selectedFilter = value; OnPropertyChanged(); } }

        public ObservableCollection<HANHKHACH> ListKhach { get => _ListKhach; set { _ListKhach = value; OnPropertyChanged(); } }

        public TripDetailVM()
        {
            SelectedFilter = FilterCbItems.First();
            IsAdmin = MainVM.AdminRole;
            CHUYEN = SelectedChuyen;
            ListKhach = new ObservableCollection<HANHKHACH>(DataProvider.Ins.DB.HANHKHACHes.Where(k => k.PHIEUDATCHO.MACHUYEN.Equals(SelectedChuyen.MACHUYEN)));
        }
    }
}
