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

namespace TourismManagementSystem.ViewModel
{
    internal class PurchaseVM:BaseViewModel
    {

        private ObservableCollection<string> _status = new ObservableCollection<string>() { "Chưa thực hiện", "Thành công", "Thất bại","Chờ xử lý" };

        private ObservableCollection<string> _method = new ObservableCollection<string>() { "Tiền mặt", "Trả thẻ"};

        public ObservableCollection<THONGTINTHANHTOAN> THONGTINTHANHTOANs { get; set; }
        

        public ICommand OnlyNumericCommand { get; private set; }

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

        private int SoTIEN;
        public int SOTIEN
        {
            get => SoTIEN;
            set
            {
                SoTIEN = value;
                OnPropertyChanged(nameof(SOTIEN));
            }
        }

        private DateTime ThoiGIAN;
        public DateTime THOIGIAN
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
           
            THONGTINTHANHTOANs = new ObservableCollection<THONGTINTHANHTOAN>();
            AddDataCommand = new RelayCommand<object>((p) => {
                if (PHUONGTHUC==null||TRANGTHAI==null)
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
                        MATT = MATT,
                        MAPHIEU = MAPHIEU,
                        SOTIEN = SOTIEN,
                        THOIGIAN = THOIGIAN,
                        TRANGTHAI = TRANGTHAI,
                        PHUONGTHUC = PHUONGTHUC
                    };

                    DataProvider.Ins.DB.THONGTINTHANHTOANs.Add(temp);
                    DataProvider.Ins.DB.SaveChanges();
                    ListTHONGTINTHANHTOAN.Add(temp);
                    LoadDataGrid();



                    MessageBox.Show("Đã tạo thông tin thanh toán thành công");


                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            MessageBox.Show($"Property: {validationError.PropertyName}");
                            MessageBox.Show($"Error Message: {validationError.ErrorMessage}");
                        }
                    }
                }


            });


        
            OnlyNumericCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
           (p) => { OnlyNumericExecute(); });

        }
        private void LoadDataGrid()
        {
            ListTHONGTINTHANHTOAN = new ObservableCollection<THONGTINTHANHTOAN>(DataProvider.Ins.DB.THONGTINTHANHTOANs);
        }
       
     
      
     
        private void OnlyNumericExecute()
        {


            int result;
            bool isNumeric = int.TryParse(SOTIEN.ToString(), out result);
            if (!isNumeric)
            {
                // Reset the SOTIEN property to 0 or any other default value
                SOTIEN = 0;
                MessageBox.Show("Please enter a numeric value for SOTIEN.");
            }
        }
    }
}


