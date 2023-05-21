using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    internal class AddScheduleVM : BaseViewModel
    {
        private static ObservableCollection<LICHTRINH> _CurrListLichTrinh;
        private DIADIEM _DDDi;
        private DIADIEM _DDDen;

        public ObservableCollection<DIADIEM> ListDiaDiem { get; set; }
        public ObservableCollection<PHUONGTIEN> ListPhuongTien;
        private ObservableCollection<ComboBox> _PhuongTienBoxes = new ObservableCollection<ComboBox>();

        public ObservableCollection<NHAHANG> ListNhaHang;
        private ObservableCollection<ComboBox> _NhaHangBoxes = new ObservableCollection<ComboBox>();

        public ObservableCollection<KHACHSAN> ListKhachSan;
        private ObservableCollection<ComboBox> _KhachSanBoxes = new ObservableCollection<ComboBox>();

        public ObservableCollection<DICHVUKHAC> ListDVKhac;
        private ObservableCollection<ComboBox> _DVKhacBoxes = new ObservableCollection<ComboBox>();


        public AddScheduleVM()
        {
            ListDiaDiem = new ObservableCollection<DIADIEM>(DataProvider.Ins.DB.DIADIEMs);
            ListPhuongTien = new ObservableCollection<PHUONGTIEN>(DataProvider.Ins.DB.PHUONGTIENs);
            ListNhaHang = new ObservableCollection<NHAHANG>(DataProvider.Ins.DB.NHAHANGs);
            ListKhachSan = new ObservableCollection<KHACHSAN>(DataProvider.Ins.DB.KHACHSANs);
            ListDVKhac = new ObservableCollection<DICHVUKHAC>(DataProvider.Ins.DB.DICHVUKHACs);

            AddPTBoxCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) => {
                ComboBox comboBox = new ComboBox();
                comboBox.ItemsSource = ListPhuongTien;
                comboBox.DisplayMemberPath = "TENPT";
                comboBox.Margin = new Thickness(comboBox.Margin.Left, comboBox.Margin.Top, comboBox.Margin.Right, 10);
                comboBox.FontSize = 16;
                PhuongTienBoxes.Add(comboBox);
            });

            AddNHBoxCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) => {
                ComboBox comboBox = new ComboBox();
                comboBox.ItemsSource = ListNhaHang;
                comboBox.DisplayMemberPath = "TENNH";
                comboBox.Margin = new Thickness(comboBox.Margin.Left, comboBox.Margin.Top, comboBox.Margin.Right, 10);
                comboBox.FontSize = 16;
                NhaHangBoxes.Add(comboBox);
            });

            AddKSBoxCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) => {
                ComboBox comboBox = new ComboBox();
                comboBox.ItemsSource = ListKhachSan;
                comboBox.DisplayMemberPath = "TENKS";
                comboBox.Margin = new Thickness(comboBox.Margin.Left, comboBox.Margin.Top, comboBox.Margin.Right, 10);
                comboBox.FontSize = 16;
                KhachSanBoxes.Add(comboBox);
            });

            AddDVBoxCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) => {
                ComboBox comboBox = new ComboBox();
                comboBox.ItemsSource = ListDVKhac;
                comboBox.DisplayMemberPath = "TENDV";
                comboBox.Margin = new Thickness(comboBox.Margin.Left, comboBox.Margin.Top, comboBox.Margin.Right, 10);
                comboBox.FontSize = 16;
                DVKhacBoxes.Add(comboBox);
            });

            AddScheduleCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                LICHTRINH newLichTrinh = new LICHTRINH();
                newLichTrinh.DIADIEM = DDDi;
                newLichTrinh.DIADIEM1 = DDDen;
                foreach(var item in PhuongTienBoxes)
                {
                    newLichTrinh.PHUONGTIENs.Add((PHUONGTIEN)item.SelectedItem);
                }
                foreach (var item in NhaHangBoxes)
                {
                    newLichTrinh.NHAHANGs.Add((NHAHANG)item.SelectedItem);
                }
                foreach (var item in KhachSanBoxes)
                {
                    newLichTrinh.KHACHSANs.Add((KHACHSAN)item.SelectedItem);
                }
                foreach (var item in DVKhacBoxes)
                {
                    newLichTrinh.DICHVUKHACs.Add((DICHVUKHAC)item.SelectedItem);
                }
                CurrListLichTrinh.Add(newLichTrinh);
                MessageBox.Show("Thêm mới lịch trình thành công!");
            });
        }

        public ICommand AddPTBoxCommand { get; set; }
        public ICommand AddNHBoxCommand { get; set; }
        public ICommand AddKSBoxCommand { get; set; }
        public ICommand AddDVBoxCommand { get; set; }
        public ICommand AddScheduleCommand { get; set; }

        public DIADIEM DDDi { get => _DDDi; set { _DDDi = value; OnPropertyChanged(); }  }
        public DIADIEM DDDen { get => _DDDen; set { _DDDen = value; OnPropertyChanged(); } }

        public ObservableCollection<ComboBox> PhuongTienBoxes { get => _PhuongTienBoxes; set { _PhuongTienBoxes = value; OnPropertyChanged(); }  }

        public ObservableCollection<ComboBox> NhaHangBoxes { get => _NhaHangBoxes; set { _NhaHangBoxes = value; OnPropertyChanged(); }  }
        public ObservableCollection<ComboBox> KhachSanBoxes { get => _KhachSanBoxes; set { _KhachSanBoxes = value; OnPropertyChanged(); }  }
        public ObservableCollection<ComboBox> DVKhacBoxes { get => _DVKhacBoxes; set { _DVKhacBoxes = value; OnPropertyChanged(); }  }

        public static ObservableCollection<LICHTRINH> CurrListLichTrinh { get => _CurrListLichTrinh; set { _CurrListLichTrinh = value; } }
    }
}
