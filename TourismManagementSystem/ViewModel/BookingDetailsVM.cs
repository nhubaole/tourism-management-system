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
            ThanhToanCommand = new RelayCommand<object>((p)=>
            {
                if (Phieu.TINHTRANG == "Đã thanh toán" || Phieu.TINHTRANG == "Đã xuất vé")
                    return false;
                return true;
            }, (p) =>
            {
                //chuyen sang man hinh thanh toan
                PurchaseVM.SelectedPhieu = _selectedPhieu;
                PurchaseWindow purchaseWindow= new PurchaseWindow();
                purchaseWindow.ShowDialog();
                Phieu = PurchaseVM.SelectedPhieu;
            });
            XuatVeCommand = new RelayCommand<object>((p) =>
            {
                if (Phieu.TINHTRANG == "Chưa thanh toán" || Phieu.TINHTRANG == "Đã xuất vé")
                    return false;
                return true;
            }, (p) =>
            {
                //chuyen sang man hinh xuat ve
                foreach (var item in Phieu.HANHKHACHes)
                {
                    VE ve = new VE();
                    ve.NGAYBAN = DateTime.Today;
                    ve.TRANGTHAI = "Chưa sử dụng";
                    ve.MAHK = item.MAHK;
                    ve.HANHKHACH = item;
                    ve.MAPHIEU = Phieu.MAPHIEU;
                    ve.PHIEUDATCHO = Phieu;
                    ve.GIAVE = Phieu.CHUYEN.GIAVE;
                    string formattedID;
                    ObservableCollection<VE> ListVe = new ObservableCollection<VE>(DataProvider.Ins.DB.VEs);
                    if (ListVe.Count() == 0)
                    {
                        formattedID = string.Format("V{0:D7}", 1);
                    }
                    else
                    {
                        string lastID = ListVe.Last().MAVE;
                        int previousNumber = int.Parse(lastID.Substring(1));
                        int nextNumber = previousNumber + 1;
                        formattedID = string.Format("V{0:D7}", nextNumber);
                    }
                    ve.MAVE = formattedID;
                    DataProvider.Ins.DB.VEs.Add(ve);
                    DataProvider.Ins.DB.SaveChanges();
                }
                Phieu.TINHTRANG = "Đã xuất vé";
                OnPropertyChanged(nameof(Phieu.TINHTRANG));
                var itemToUpdate = DataProvider.Ins.DB.PHIEUDATCHOes.FirstOrDefault(item => item.MAPHIEU == Phieu.MAPHIEU);
                if (itemToUpdate != null)
                {
                    itemToUpdate = Phieu;
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Tạo vé thành công!");
                }
            });

        }

        public ICommand ThanhToanCommand { get; set; }

        public ICommand XuatVeCommand { get; set; }
    }
}
