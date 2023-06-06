using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TourismManagementSystem.Model;
using System.Windows.Controls;
using TourismManagementSystem.View;

namespace TourismManagementSystem.ViewModel
{
    internal class AddTripVM: BaseViewModel
    {
        private CHUYEN _newTrip;
        public CHUYEN NewTrip { get => _newTrip; set { _newTrip= value; OnPropertyChanged(); } }
        public ObservableCollection<LOAICHUYEN> ListLoaiChuyen { get; set; }
        private string _ToolTipText;

        public ObservableCollection<PHUONGTIEN> ListOfHDV;
        private ObservableCollection<ComboBox> _HDVBoxes = new ObservableCollection<ComboBox>();
        public ObservableCollection<ComboBox> HDVBoxes { get => _HDVBoxes; set { _HDVBoxes = value; OnPropertyChanged(); } }


        public ObservableCollection<string> ListTuyen { get; set; }
        public ObservableCollection<string> ListHDV { get; set; }

        private static CHUYEN _editSelectedChuyen;
        public static CHUYEN EditSelectedChuyen { get => _editSelectedChuyen; set { _editSelectedChuyen = value; } }
        private static int _IsEdit = 0;

        private string _Title;
        private string _BtnText;

        public AddTripVM()
        {
            ToolTipText = "Chưa nhập đủ thông tin";
            ListLoaiChuyen = new ObservableCollection<LOAICHUYEN>(DataProvider.Ins.DB.LOAICHUYENs);
            ListTuyen = new ObservableCollection<string>(DataProvider.Ins.DB.TUYENs.Select(t => t.MATUYEN));
            ListHDV = new ObservableCollection<string>(DataProvider.Ins.DB.HUONGDANVIENs.Select(t => t.MAHDV));
            if (IsEdit == 0)
            {
                Title = "Thêm mới cuyến du lịch";
                BtnText = "Thêm chuyến du lịch";
                NewTrip = new CHUYEN();
                string formattedID;
                ObservableCollection<CHUYEN> ListChuyen = new ObservableCollection<CHUYEN>(DataProvider.Ins.DB.CHUYENs);
                if (ListTuyen.Count() == 0)
                {
                    formattedID = string.Format("T{0:D7}", 1);
                }
                else
                {
                    string lastID = ListChuyen.Last().MACHUYEN;
                    int previousNumber = int.Parse(lastID.Substring(1));
                    int nextNumber = previousNumber + 1;
                    formattedID = string.Format("C{0:D7}", nextNumber);
                }
                NewTrip.MACHUYEN = formattedID;
            }
            else
            {
                Title = "Cập nhật chuyến du lịch";
                BtnText = "Cập nhật chuyến du lịch";
                NewTrip = EditSelectedChuyen;
                foreach (var item in NewTrip.HUONGDANVIENs)
                {
                    ComboBox comboBox = new ComboBox();
                    comboBox.ItemsSource = ListOfHDV; ;
                    comboBox.DisplayMemberPath = "TENKS";
                    comboBox.Margin = new Thickness(comboBox.Margin.Left, comboBox.Margin.Top, comboBox.Margin.Right, 10);
                    comboBox.FontSize = 16;
                    comboBox.SelectedItem = item;
                    HDVBoxes.Add(comboBox);
                }
            }


            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NewTrip.MACHUYEN) || NewTrip.MATUYEN == null || NewTrip.LOAICHUYEN == null || NewTrip.HUONGDANVIENs.Count == 0 || NewTrip.TGBATDAU == null || NewTrip.GIAVE == null || NewTrip.TGKETTHUC == null || NewTrip.SLTOITHIEU == null || NewTrip.SLTHUCTE == null)
                {
                    ToolTipText = "Vui lòng nhập đủ các trường thông tin bắt buộc";
                    return false;
                }
                return true;
            }, (p) =>
            {
                var addTripWindow = p as Window;
                if (addTripWindow != null)
                {
                    if (NewTrip.TGBATDAU > NewTrip.TGKETTHUC)
                    {
                        MessageBox.Show("Thời gian không hợp lệ");
                        return;
                    }
                    if (IsEdit == 0)
                    {
                        NewTrip.MALOAI = DataProvider.Ins.DB.LOAICHUYENs.Where(t => t.TENLOAI.Equals(NewTrip.LOAICHUYEN)).ToString();
                        foreach (var t in HDVBoxes)
                        {
                            NewTrip.HUONGDANVIENs.Add((HUONGDANVIEN)t.SelectedItem);
                        }
                        foreach (var item in DataProvider.Ins.DB.HUONGDANVIENs)
                        {
                            foreach (var tripItem in NewTrip.HUONGDANVIENs)
                            {
                                if (item.MAHDV.Equals(tripItem.MAHDV))
                                    item.CHUYENs.Add(NewTrip);

                            }
                        }
                        DataProvider.Ins.DB.CHUYENs.Add(NewTrip);
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Thêm mới chuyến thành công!");
                    }
                    else
                    {
                        var itemToUpdate = DataProvider.Ins.DB.CHUYENs.FirstOrDefault(item => item.MACHUYEN == NewTrip.MACHUYEN);
                        if (itemToUpdate != null)
                        {
                            NewTrip.MALOAI = DataProvider.Ins.DB.LOAICHUYENs.Where(t => t.TENLOAI.Equals(NewTrip.LOAICHUYEN)).ToString();
                            itemToUpdate = NewTrip;
                            foreach (var t in HDVBoxes)
                            {
                                itemToUpdate.HUONGDANVIENs.Add((HUONGDANVIEN)t.SelectedItem);
                            }

                            foreach (var item in DataProvider.Ins.DB.HUONGDANVIENs)
                            {
                                foreach (var tripItem in itemToUpdate.HUONGDANVIENs)
                                {
                                    if (item.MAHDV.Equals(tripItem.MAHDV))
                                        item.CHUYENs.Add(itemToUpdate);
                                }
                            }
                            DataProvider.Ins.DB.SaveChanges();
                            MessageBox.Show("Cập nhật thành công!");
                        }
                    }
                    addTripWindow.Close();
                }
            });
            AddHDV = new RelayCommand<object>((p) => {
                return true;
            }, (p) => {
                ComboBox comboBox = new ComboBox();
                    comboBox.ItemsSource = ListOfHDV; ;
                    comboBox.DisplayMemberPath = "TENHDV";
                    comboBox.Margin = new Thickness(comboBox.Margin.Left, comboBox.Margin.Top, comboBox.Margin.Right, 10);
                    comboBox.FontSize = 16;
                    HDVBoxes.Add(comboBox);
                
            });
        }

        private string _selectedHDVMaSo;
        public string SelectedHDVMaSo
        {
            get { return _selectedHDVMaSo; }
            set
            {
                _selectedHDVMaSo = value;
                OnPropertyChanged(); // Implement INotifyPropertyChanged to notify the UI of property changes
            }
        }
        public ICommand AddCommand { get; set; }

        public ICommand AddHDV { get; set; }
        public ICommand RemoveHDV { get; set; }

        public string ToolTipText { get => _ToolTipText; set { _ToolTipText = value; OnPropertyChanged(); } }
        public static int IsEdit { get => _IsEdit; set => _IsEdit = value; }
        public string Title { get => _Title; set => _Title = value; }
        public string BtnText { get => _BtnText; set => _BtnText = value; }
    }
}
