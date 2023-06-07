using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using TourismManagementSystem.Model;
using TourismManagementSystem.View;
using System.Data.Entity.Validation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Globalization;
using System.Collections;

namespace TourismManagementSystem.ViewModel
{
    internal class PurchaseVM : BaseViewModel
    {
        private static PHIEUDATCHO _selectedPhieu;
        public static PHIEUDATCHO SelectedPhieu { get => _selectedPhieu; set { _selectedPhieu = value; } }

        private ObservableCollection<string> _method = new ObservableCollection<string>() { "Tiền mặt", "Chuyển khoản", "Thẻ tín dụng", "Thẻ ghi nợ", "Ví điện tử MoMo" };

        public ObservableCollection<THONGTINTHANHTOAN> THONGTINTHANHTOANs { get; set; }

        private THONGTINTHANHTOAN _NewPurchase = new THONGTINTHANHTOAN();
        public ICommand AddDataCommand { get; set; }
      
        private string _selectedmethod;
        public string selectedmethod
        {
            get => _selectedmethod;
            set
            {
                _selectedmethod = value;
                OnPropertyChanged(nameof(selectedmethod));
            }
        }
        public ObservableCollection<string> method { get => _method; set { _method = value; OnPropertyChanged(nameof(method)); } }

        public THONGTINTHANHTOAN NewPurchase { get => _NewPurchase; set { _NewPurchase = value; OnPropertyChanged(); } }

        public PurchaseVM()
        {
            string formattedID;
            ObservableCollection<THONGTINTHANHTOAN> ListTT = new ObservableCollection<THONGTINTHANHTOAN>(DataProvider.Ins.DB.THONGTINTHANHTOANs);
            if (ListTT.Count() == 0)
            {
                formattedID = string.Format("TT{0:D6}", 1);
            }
            else
            {
                string lastID = ListTT.Last().MATT;
                int previousNumber = int.Parse(lastID.Substring(2));
                int nextNumber = previousNumber + 1;
                formattedID = string.Format("TT{0:D6}", nextNumber);
            }
            NewPurchase.MATT = formattedID;
            NewPurchase.THOIGIAN = DateTime.Today;
            NewPurchase.SOTIEN = SelectedPhieu.CHUYEN.GIAVE * SelectedPhieu.SLKHACH;
            NewPurchase.PHIEUDATCHO = SelectedPhieu;
            AddDataCommand = new RelayCommand<object>((p) => {
                if (NewPurchase.PHUONGTHUC == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) =>
            {
                    try
                    {
                        Window w = p as Window;
                        if(w != null)
                    {
                        SelectedPhieu.THONGTINTHANHTOANs.Add(NewPurchase);
                        SelectedPhieu.TINHTRANG = "Đã thanh toán";
                        var itemToUpdate = DataProvider.Ins.DB.PHIEUDATCHOes.FirstOrDefault(item => item.MAPHIEU == SelectedPhieu.MAPHIEU);
                        if (itemToUpdate != null)
                        {
                            itemToUpdate = SelectedPhieu;
                            DataProvider.Ins.DB.SaveChanges();
                            MessageBox.Show("Đã tạo thông tin thanh toán thành công");
                        }
                        w.Close();
                    }
                    }
                    catch (Exception ex)
                    {
                        Exception innerException = ex.InnerException;
                        while (innerException != null)
                        {
                            MessageBox.Show("Inner Exception: " + innerException.Message);
                            innerException = innerException.InnerException;
                        }
                    }
            }); 

        }
    }
}


