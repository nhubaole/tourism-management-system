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
    internal class PurchaseVM: BaseViewModel
    {
       
        private ObservableCollection<string> _status = new ObservableCollection<string>() { "Chưa thực hiện", "Thành công", "Thất bại","Chờ xử lý" };

        private ObservableCollection<string> _method = new ObservableCollection<string>() { "Tiền mặt", "Trả thẻ"};

        public ObservableCollection<THONGTINTHANHTOAN> THONGTINTHANHTOANs { get; set; }
        

        public ICommand AddDataCommand { get; set; }
      

        private THONGTINTHANHTOAN _selectedPurchase;
        public THONGTINTHANHTOAN SelectedPurchase
        {
            get => _selectedPurchase; set
            {
                _selectedPurchase = value;
                OnPropertyChanged(nameof(SelectedPurchase));
            }
        }


        private string MaTT;
        public string MATT
        {
            get => MaTT;
            set
            {
                MaTT = value;
                OnPropertyChanged(nameof(MATT));
            }
        }

        private string MaPHIEU;
        public string MAPHIEU
        {
            get => MaPHIEU;
            set
            {
                MaPHIEU = value;
                OnPropertyChanged(nameof(MAPHIEU));
            }
        }

        private string SoTIEN;
        public string SOTIEN
        {
            get => SoTIEN;
            set
            {

                SoTIEN = value;
                OnPropertyChanged(nameof(SOTIEN));
                
            }
        }

        private System.DateTime ThoiGIAN;
        
        public System.DateTime THOIGIAN
        {
            get => ThoiGIAN;
            set
            {
                ThoiGIAN = value;
                OnPropertyChanged(nameof(THOIGIAN));
                
            }
        }

        private string TrangTHAI;
        public string TRANGTHAI
        {
            get => TrangTHAI;
            set
            {
                TrangTHAI = value;
                OnPropertyChanged(nameof(TRANGTHAI));
            }
        }

        private string PhgTHUC;
        public string PHUONGTHUC
        {
            get => PhgTHUC;
            set
            {
                PhgTHUC = value;
                OnPropertyChanged(nameof(PHUONGTHUC));
            }
        }
        private string _selectedstatus;
        public string selectedstatus
        {
            get => _selectedstatus;
            set
            {
                _selectedstatus = value;
                OnPropertyChanged(nameof(selectedstatus));
            }
        }
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
        public ObservableCollection<string> status { get => _status; set { _status = value; OnPropertyChanged(nameof(status)); } }
        public ObservableCollection<string> method { get => _method; set { _method = value; OnPropertyChanged(nameof(method)); } }





        private ObservableCollection<THONGTINTHANHTOAN> _ListTHONGTINTHANHTOAN = new ObservableCollection<THONGTINTHANHTOAN>(DataProvider.Ins.DB.THONGTINTHANHTOANs);
        public ObservableCollection<THONGTINTHANHTOAN> ListTHONGTINTHANHTOAN { get => _ListTHONGTINTHANHTOAN; set { _ListTHONGTINTHANHTOAN = value; OnPropertyChanged(nameof(DataProvider.Ins.DB.THONGTINTHANHTOANs)); } }

        public PurchaseVM()
        {
            THOIGIAN = DateTime.Today;

            THONGTINTHANHTOANs = new ObservableCollection<THONGTINTHANHTOAN>();
            AddDataCommand = new RelayCommand<object>((p) => {
                if (PHUONGTHUC == null || TRANGTHAI == null ||!IsNumeric(SoTIEN)||SoTIEN==null)
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
                        var temp = new THONGTINTHANHTOAN()
                        {
                            MATT = GenerateCode(GetLastGeneratedMATT()),
                            MAPHIEU = MAPHIEU,
                            SOTIEN = int.Parse(SOTIEN),
                            THOIGIAN = THOIGIAN,
                            TRANGTHAI = TRANGTHAI,
                            PHUONGTHUC = PHUONGTHUC
                        };

                        DataProvider.Ins.DB.THONGTINTHANHTOANs.Add(temp);
                        DataProvider.Ins.DB.SaveChanges();
                        ListTHONGTINTHANHTOAN.Add(temp);

                        MessageBox.Show("Đã tạo thông tin thanh toán thành công MATT: " + temp.MATT);


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
        private void LoadDataGrid()
        {
            ListTHONGTINTHANHTOAN = new ObservableCollection<THONGTINTHANHTOAN>(DataProvider.Ins.DB.THONGTINTHANHTOANs);
        }





        private bool IsNumeric(string value)
        {
            int result;
            bool isNumeric = int.TryParse(value, out result);

            return isNumeric;
        }


        public static string GenerateCode(string previousCode)
        {
            int newNumber;

            if (previousCode == null)
            {
                newNumber = 1;
            }
            else
            {
                
                int previousNumber = int.Parse(previousCode.Substring(2));
                newNumber = previousNumber + 1;
            }

            string temp = "TT";

            string newNumberStr = newNumber.ToString().PadLeft(6, '0');
            string newCode = temp + newNumberStr;

            return newCode;
                
            
        }


      
        public string GetLastGeneratedMATT()
        {
            var lastPurchase = DataProvider.Ins.DB.THONGTINTHANHTOANs
                .OrderByDescending(p => p.MATT)
                .FirstOrDefault();

            if (lastPurchase != null)
            {
                return lastPurchase.MATT;
            }

            return null; 
        }
    }
}


