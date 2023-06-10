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
        private string _Title;
        private string _BtnText;
        private static ObservableCollection<LICHTRINH> _CurrListLichTrinh;
        private static string _CurrNgay;
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

        private static LICHTRINH _EditSelectedLichTrinh;
        private static int _IsEdit = 0;
        public AddScheduleVM()
        {
            ListDiaDiem = new ObservableCollection<DIADIEM>(DataProvider.Ins.DB.DIADIEMs);
            ListPhuongTien = new ObservableCollection<PHUONGTIEN>(DataProvider.Ins.DB.PHUONGTIENs);
            ListNhaHang = new ObservableCollection<NHAHANG>(DataProvider.Ins.DB.NHAHANGs);
            ListKhachSan = new ObservableCollection<KHACHSAN>(DataProvider.Ins.DB.KHACHSANs);
            ListDVKhac = new ObservableCollection<DICHVUKHAC>(DataProvider.Ins.DB.DICHVUKHACs);

            if(IsEdit == 1)
            {
                Title = "Cập nhật lịch trình";
                BtnText = "Cập nhật";
                DDDi = EditSelectedLichTrinh.DIADIEM;
                DDDen = EditSelectedLichTrinh.DIADIEM1;
                foreach(var item in EditSelectedLichTrinh.PHUONGTIENs)
                {
                    ComboBox comboBox = new ComboBox();
                    comboBox.ItemsSource = ListPhuongTien;
                    comboBox.DisplayMemberPath = "TENPT";
                    comboBox.Margin = new Thickness(comboBox.Margin.Left, comboBox.Margin.Top, comboBox.Margin.Right, 10);
                    comboBox.FontSize = 16;
                    comboBox.SelectedItem = item;
                    PhuongTienBoxes.Add(comboBox);
                }
                foreach (var item in EditSelectedLichTrinh.NHAHANGs)
                {
                    ComboBox comboBox = new ComboBox();
                    comboBox.ItemsSource = ListNhaHang;
                    comboBox.DisplayMemberPath = "TENNH";
                    comboBox.Margin = new Thickness(comboBox.Margin.Left, comboBox.Margin.Top, comboBox.Margin.Right, 10);
                    comboBox.FontSize = 16;
                    comboBox.SelectedItem = item;
                    NhaHangBoxes.Add(comboBox);
                }
                foreach (var item in EditSelectedLichTrinh.KHACHSANs)
                {
                    ComboBox comboBox = new ComboBox();
                    comboBox.ItemsSource = ListKhachSan;
                    comboBox.DisplayMemberPath = "TENKS";
                    comboBox.Margin = new Thickness(comboBox.Margin.Left, comboBox.Margin.Top, comboBox.Margin.Right, 10);
                    comboBox.FontSize = 16;
                    comboBox.SelectedItem = item;
                    KhachSanBoxes.Add(comboBox);
                }
                foreach (var item in EditSelectedLichTrinh.DICHVUKHACs)
                {
                    ComboBox comboBox = new ComboBox();
                    comboBox.ItemsSource = ListDVKhac;
                    comboBox.DisplayMemberPath = "TENDV";
                    comboBox.Margin = new Thickness(comboBox.Margin.Left, comboBox.Margin.Top, comboBox.Margin.Right, 10);
                    comboBox.FontSize = 16;
                    comboBox.SelectedItem = item;
                    DVKhacBoxes.Add(comboBox);
                }
            }
            else
            {
                Title = "Thêm mới lịch trình";
                BtnText = "Thêm";
            }

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

            AddScheduleCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                var addScheduleWindow = p as Window;
                if (addScheduleWindow != null)
                {
                    if (DDDi == DDDen)
                    {
                        MessageBox.Show("Địa điểm đến không được trùng địa điểm đi!");
                        return;
                    }
                    if (IsEdit == 0)
                    {
                        LICHTRINH newLichTrinh = new LICHTRINH();
                        Random random = new Random();
                        int randomDigits = random.Next(0, 999999);
                        string formattedID = string.Format("LT{0:D6}", randomDigits);
                        newLichTrinh.MALT = formattedID;
                        newLichTrinh.DIADIEM = DDDi;
                        newLichTrinh.DIADIEM1 = DDDen;
                        foreach (var item in PhuongTienBoxes)
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
                        newLichTrinh.NGAYTHU = int.Parse(CurrNgay.Substring(CurrNgay.Length - 1, 1));
                        CurrListLichTrinh.Add(newLichTrinh);
                        MessageBox.Show("Thêm mới lịch trình thành công!");
                        addScheduleWindow.Close();
                    }
                    else
                    {
                        var itemToUpdate = CurrListLichTrinh.FirstOrDefault(item => item.MALT == EditSelectedLichTrinh.MALT);
                        if (itemToUpdate != null)
                        {
                            itemToUpdate.DIADIEM = DDDi;
                            itemToUpdate.DIADIEM1 = DDDen;
                            itemToUpdate.PHUONGTIENs.Clear();
                            itemToUpdate.NHAHANGs.Clear();
                            itemToUpdate.KHACHSANs.Clear();
                            itemToUpdate.DICHVUKHACs.Clear();
                            foreach (var item in PhuongTienBoxes)
                            {
                                itemToUpdate.PHUONGTIENs.Add((PHUONGTIEN)item.SelectedItem);
                            }
                            foreach (var item in NhaHangBoxes)
                            {
                                itemToUpdate.NHAHANGs.Add((NHAHANG)item.SelectedItem);
                            }
                            foreach (var item in KhachSanBoxes)
                            {
                                itemToUpdate.KHACHSANs.Add((KHACHSAN)item.SelectedItem);
                            }
                            foreach (var item in DVKhacBoxes)
                            {
                                itemToUpdate.DICHVUKHACs.Add((DICHVUKHAC)item.SelectedItem);
                            }
                            MessageBox.Show("Cập nhật lịch trình thành công!");
                            addScheduleWindow.Close();
                        }
                    }
                }
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

        public static string CurrNgay { get => _CurrNgay; set => _CurrNgay = value; }
        public static LICHTRINH EditSelectedLichTrinh { get => _EditSelectedLichTrinh; set => _EditSelectedLichTrinh = value; }
        public static int IsEdit { get => _IsEdit; set => _IsEdit = value; }
        public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); }  }

        public string BtnText { get => _BtnText; set { _BtnText = value; OnPropertyChanged(); }  }
    }
}
