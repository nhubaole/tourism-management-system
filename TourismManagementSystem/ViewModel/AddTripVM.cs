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

        public ObservableCollection<HUONGDANVIEN> ListOfHDV;
        private ObservableCollection<ComboBox> _HDVBoxes = new ObservableCollection<ComboBox>();
        public ObservableCollection<ComboBox> HDVBoxes { get => _HDVBoxes; set { _HDVBoxes = value; OnPropertyChanged(); } }


        public ObservableCollection<TUYEN> ListTuyen { get; set; }
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
            ListTuyen = new ObservableCollection<TUYEN>(DataProvider.Ins.DB.TUYENs);
            ListOfHDV = new ObservableCollection<HUONGDANVIEN>(DataProvider.Ins.DB.HUONGDANVIENs);
            if (IsEdit == 0)
            {
                Title = "Thêm mới chuyến du lịch";
                BtnText = "Thêm chuyến du lịch";
                NewTrip = new CHUYEN();
                NewTrip.SLTHUCTE = 0;
                string formattedID;
                ObservableCollection<CHUYEN> ListChuyen = new ObservableCollection<CHUYEN>(DataProvider.Ins.DB.CHUYENs);
                if (ListChuyen.Count() == 0)
                {
                    formattedID = string.Format("C{0:D7}", 1);
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
                    comboBox.DisplayMemberPath = "HOTEN";
                    comboBox.Margin = new Thickness(comboBox.Margin.Left, comboBox.Margin.Top, comboBox.Margin.Right, 10);
                    comboBox.FontSize = 16;
                    comboBox.SelectedItem = item;
                    HDVBoxes.Add(comboBox);
                }
            }


            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NewTrip.MACHUYEN) || NewTrip.TUYEN == null || NewTrip.LOAICHUYEN == null || NewTrip.TGBATDAU == null )
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
                    if (HDVBoxes.Count() != 0 && !AreComboBoxValuesUnique())
                    {
                        MessageBox.Show("Huấn luyện viên không được trùng nhau");
                        return;
                    }
                    if (IsEdit == 0)
                    {
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
                    comboBox.DisplayMemberPath = "HOTEN";
                    comboBox.Margin = new Thickness(comboBox.Margin.Left, comboBox.Margin.Top, comboBox.Margin.Right, 10);
                    comboBox.FontSize = 16;
                    HDVBoxes.Add(comboBox);               
            });

            SelectLoaiCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                if(NewTrip.TUYEN != null)
                {
                    if (NewTrip.LOAICHUYEN.TENLOAI == "Khuyến mãi")
                    {
                        NewTrip.GIAVE = (int?)(NewTrip.TUYEN.GIADUKIEN * 0.8);
                    }
                    else
                    {
                        NewTrip.GIAVE = NewTrip.TUYEN.GIADUKIEN;
                    }
                    OnPropertyChanged(nameof(NewTrip.GIAVE));
                }
            });
            SelectTuyenCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                if (NewTrip.LOAICHUYEN != null)
                {
                    if (NewTrip.LOAICHUYEN.TENLOAI == "Khuyến mãi")
                    {
                        NewTrip.GIAVE = (int?)(NewTrip.TUYEN.GIADUKIEN * 0.8);
                    }
                    else
                    {
                        NewTrip.GIAVE = NewTrip.TUYEN.GIADUKIEN;
                    }
                    OnPropertyChanged(nameof(NewTrip.GIAVE));
                }
            });
        }

        public bool AreComboBoxValuesUnique()
        {
            if (HDVBoxes == null || HDVBoxes.Count == 0)
            {
                return false;
            }

            HashSet<string> uniqueValues = new HashSet<string>();

            foreach (ComboBox item in HDVBoxes)
            {
                string value = ((HUONGDANVIEN)item.SelectedItem).MAHDV;
                if (uniqueValues.Contains(value))
                {
                    return false;
                }

                uniqueValues.Add(value);
            }

            return true;
        }

        private string _selectedHDVMaSo;
        public string SelectedHDVMaSo
        {
            get { return _selectedHDVMaSo; }
            set
            {
                _selectedHDVMaSo = value;
                OnPropertyChanged(); 
            }
        }
        public ICommand AddCommand { get; set; }
        public ICommand SelectLoaiCommand { get; set; }
        public ICommand SelectTuyenCommand { get; set; }

        public ICommand AddHDV { get; set; }
        public ICommand RemoveHDV { get; set; }

        public string ToolTipText { get => _ToolTipText; set { _ToolTipText = value; OnPropertyChanged(); } }
        public static int IsEdit { get => _IsEdit; set => _IsEdit = value; }
        public string Title { get => _Title; set => _Title = value; }
        public string BtnText { get => _BtnText; set => _BtnText = value; }
    }
}
