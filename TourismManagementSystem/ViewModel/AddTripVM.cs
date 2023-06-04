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
    internal class AddTripVM: BaseViewModel
    {
        private CHUYEN _newTrip;
        public CHUYEN NewTrip { get => _newTrip; set { _newTrip= value; OnPropertyChanged(); } }
        public ObservableCollection<LOAICHUYEN> ListLoaiChuyen { get; set; }
        private string _ToolTipText;

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
                Random random = new Random();
                NewTrip.MACHUYEN = random.Next(0, 1000).ToString();
            }
            else
            {
                Title = "Cập nhật chuyến du lịch";
                BtnText = "Cập nhật chuyến du lịch";
                NewTrip = EditSelectedChuyen;
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
                            foreach (var item in DataProvider.Ins.DB.HUONGDANVIENs)
                            {
                                foreach (var tripItem in NewTrip.HUONGDANVIENs)
                                {
                                    if (item.MAHDV.Equals(tripItem.MAHDV))
                                        item.CHUYENs.Add(NewTrip);

                                }
                            }
                            DataProvider.Ins.DB.SaveChanges();
                            MessageBox.Show("Cập nhật thành công!");
                        }
                    }
                    addTripWindow.Close();
                }
            });
            AddHDV = new RelayCommand<object>((p) => true, (p) => {
                if (SelectedHDVMaSo != null)
                {
                    string HDV = SelectedHDVMaSo;
                    //NewTrip.HUONGDANVIENs.Add((HUONGDANVIEN)DataProvider.Ins.DB.HUONGDANVIENs.Where(t => t.MAHDV.Equals(HDV)));
                    NewTrip.HUONGDANVIENs.Add(DataProvider.Ins.DB.HUONGDANVIENs.FirstOrDefault(t => t.MAHDV.Equals(HDV)));

                }
            });
            RemoveHDV = new RelayCommand<object>((p) => true, (p) => {
                HUONGDANVIEN hdv = p as HUONGDANVIEN;
                if (hdv != null)
                {
                    NewTrip.HUONGDANVIENs.Remove(hdv);
                    OnPropertyChanged();
                }
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
