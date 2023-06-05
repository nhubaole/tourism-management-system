using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.Model;
using TourismManagementSystem.View;
using TourismManagementSystem.UserControls;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Windows.Data;
using System.Text.RegularExpressions;

namespace TourismManagementSystem.ViewModel
{
    
    internal class CustomerVM : BaseViewModel,INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get { return _errors.Values.Any(list => list != null && list.Count > 0); }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_errors.ContainsKey(propertyName))
                return null;

            return _errors[propertyName];
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }


        private void ValidateProperty(string propertyName)
        {
            List<string> errors = new List<string>();

            if (propertyName == nameof(SDT))
            {
                if (string.IsNullOrEmpty(SDT))
                {
                    errors.Add("Please enter a valid phone number.");
                }
                else if (!IsNumber(SDT))
                {
                    errors.Add("Please enter a valid phone number.");

                }
                else if (SDT.Length!=10)
                {
                    errors.Add("Please enter a valid phone number.");

                }

            }
            
            if (propertyName == nameof(CCCD))
            {
                if (string.IsNullOrEmpty(CCCD))
                {
                    errors.Add("Please enter a valid 12-digit number.");
                }
                else if ( !IsNumber(CCCD))
                {
                    errors.Add("Please enter a valid 12-digit number.");
                }
                else if (cccd.Length != 12)
                {
                    errors.Add("Please enter a valid 12-digit number.");
                }
                else
                {
                    
                    var existingCustomer = DataProvider.Ins.DB.KHACHHANGs.FirstOrDefault(c => c.CCCD == CCCD);
                    if (existingCustomer != null && existingCustomer != SelectedCustomer)
                    {
                        errors.Add("This CCCD is already registered for another customer.");
                    }
                }

            }

            if (propertyName == nameof(HOTEN))
            {
                if (string.IsNullOrEmpty(HOTEN) || !IsNameValid(hoTen))
                {
                    errors.Add("Please enter a valid name.");
                }
                
                
            }
            if (propertyName == nameof(EMAIL))
            {
                if (string.IsNullOrEmpty(EMAIL) || !IsEmailValid(email))
                {
                    errors.Add("Please enter a valid email.");
                }


            }
            if (propertyName == nameof(DIACHI))
            {
                if (string.IsNullOrEmpty(DIACHI) )
                {
                    errors.Add("Please enter your address");
                }


            }

            if (errors.Count > 0)
            {
                _errors[propertyName] = errors;
            }
            else
            {
                _errors.Remove(propertyName);
            }

            OnErrorsChanged(propertyName);
        }

        private bool IsNumber(string value)
        {
            return double.TryParse(value, out _);
        }
        private bool IsNameValid(string name)
        {
            foreach (char c in name)
            {
                if (!char.IsLetter(c)&&c!=' ')
                {
                    return false;
                }
            }

            return true;
        }
        private bool IsEmailValid(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            // Regular expression pattern for email validation
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            return Regex.IsMatch(email, pattern);
        }

        private ObservableCollection<string> _filter = new ObservableCollection<string>() { "Mã khách hàng", "Tên khách hàng","Số điện thoại" };
        public ObservableCollection<KHACHHANG> KhachHangs { get; set; }
        public ICommand SwitchWindowCommand { get; set; }
        public ICommand AddDataCommand { get; set; }
        public ICommand DeleteDataCommand { get; set; }
        public ICommand UpdateDataCommand { get; set; }
        public ICommand BookingButtonCommand { get; set; }

        
       
        

        private KHACHHANG _selectedCustomer;
        public KHACHHANG SelectedCustomer { get => _selectedCustomer; set
            {
                _selectedCustomer = value;
                if (_selectedCustomer != null)
                {
                    HOTEN = _selectedCustomer.HOTEN;
                    ValidateProperty(nameof(HOTEN));
                    OnPropertyChanged(nameof(SelectedCustomer.HOTEN));
                    CCCD = _selectedCustomer.CCCD;
                    ValidateProperty(nameof(CCCD));
                    OnPropertyChanged(nameof(SelectedCustomer.CCCD));
                    SDT = _selectedCustomer.SDT;
                    ValidateProperty(nameof(SDT));
                    OnPropertyChanged(nameof(SelectedCustomer.SDT));
                    EMAIL = _selectedCustomer.EMAIL;
                    ValidateProperty(nameof(EMAIL));
                    OnPropertyChanged(nameof(_selectedCustomer.EMAIL));
                    DIACHI = _selectedCustomer.DIACHI;
                    ValidateProperty(nameof(DIACHI));
                    OnPropertyChanged(nameof(SelectedCustomer.DIACHI));
                   
                }
                OnPropertyChanged(nameof(SelectedCustomer));

            }
        }

        private string maKH;
        public string MAKH
        {
            get => maKH;
            set
            {
                maKH = value;
                OnPropertyChanged(nameof(MAKH));
            }
        }

        private string hoTen;
        public string HOTEN
        {
            get => hoTen;
            set
            {
                hoTen = value;
                OnPropertyChanged(nameof(HOTEN));
                ValidateProperty(nameof(HOTEN));
            }
        }

        private string cccd;
        public string CCCD
        {
            get => cccd;
            set
            {
                cccd = value;
                OnPropertyChanged(nameof(CCCD));
                ValidateProperty(nameof(CCCD));
            }
        }

        private string sdt;
        public string SDT
        {
            get => sdt;
            set
            {
                sdt = value;
                OnPropertyChanged(nameof(SDT));
                ValidateProperty(nameof(SDT));
            }
        }

        private string email;
        public string EMAIL
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(EMAIL));
                ValidateProperty(nameof(EMAIL));
            }
        }

        private string diaChi;
        public string DIACHI
        {
            get => diaChi;
            set
            {
                diaChi = value;
                OnPropertyChanged(nameof(DIACHI));
            }
        }
        
        private string _selectedFilter;
        public string selectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                OnPropertyChanged(nameof(selectedFilter));
            }
        }
        public ObservableCollection<string> filter { get => _filter; set { _filter = value; OnPropertyChanged(nameof(filter)); } }

        private string _SearchText;

        public string SearchText
        {
            get => _SearchText;
            set
            {
                _SearchText = value;
                if (selectedFilter == filter[0])
                {
                    ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(t => t.MAKH.Contains(SearchText)));
                }
                else if (selectedFilter == filter[1])
                {
                    ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(t => t.HOTEN.Contains(SearchText)));
                }
                else if (selectedFilter == filter[2])
                {
                    ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(t => t.SDT.Contains(SearchText)));
                }
              
                OnPropertyChanged();
            }
        }



        private ObservableCollection<KHACHHANG> _ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
        public ObservableCollection<KHACHHANG> ListKhachhang { get => _ListKhachhang; set { _ListKhachhang= value; OnPropertyChanged(nameof(ListKhachhang)); } }

        public CustomerVM()
        {
            selectedFilter = filter[0];
            SwitchWindowCommand = new RelayCommand<object>((p) => {
               
                  return true;
                
            }, (p) =>
            {SwitchWindow(p);});

            KhachHangs = new ObservableCollection<KHACHHANG>();
            AddDataCommand = new RelayCommand<object>((p) => { 
                if (HasErrors||DIACHI==null||HOTEN==null||EMAIL==null||SDT==null||CCCD==null)
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
                    string newCode = GenerateCode(GetLastGeneratedMAKH());
                    MAKH = newCode;
                    OnPropertyChanged(nameof(MAKH));
                    
                    var temp = new KHACHHANG()
                    {
                        MAKH = MAKH,
                        HOTEN = HOTEN,
                        CCCD = CCCD,
                        SDT = SDT,
                        EMAIL = EMAIL,
                        DIACHI = DIACHI
                    };
                        ListKhachhang.Add(temp);
                        DataProvider.Ins.DB.KHACHHANGs.Add(temp);
                        DataProvider.Ins.DB.SaveChanges();
                       
                        MessageBox.Show(temp.MAKH);
                        

                        MessageBox.Show("Đã tạo mới khách hàng thành công");
                    HOTEN = null;
                    CCCD = null;
                    SDT = null;
                    DIACHI=null;
                    EMAIL = null;
                        
                    //LoadDataGrid();



                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }


            });


            UpdateDataCommand = new RelayCommand<object>((p) =>
            {
                if (HasErrors||SelectedCustomer==null)
                { return false; }
                else
                return true;
            },
            (p) => { UpdateCustomer(); });
            DeleteDataCommand = new RelayCommand<object>((p) =>
            {
              
                return true;
            },
            (p) => { DeleteCustomer(); });

            BookingButtonCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                BookingDetailVM.SelectedPhieu = p as PHIEUDATCHO;
                BookingDetailsWindow bookingDetailsWindow = new BookingDetailsWindow();
                bookingDetailsWindow.ShowDialog();
            });


        }
        private void LoadDataGrid()
        {
            ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);

        }
        private void SwitchWindow(object parameter)
        {
            try
            {


                AddCustomerWindow addCustomerWindow = new AddCustomerWindow();
                // Assign the generated code to the property here
                string newCode = GenerateCode(GetLastGeneratedMAKH());
                MAKH = newCode;
                OnPropertyChanged(nameof(MAKH));
                addCustomerWindow.Show();
                //MessageBox.Show(newCode);
                
            }
            catch(Exception e)
            {
               
                MessageBox.Show(e.Message);

            }


        }

        private void UpdateCustomer()
        {
            if (SelectedCustomer != null)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn cập nhật khách hàng " + SelectedCustomer.HOTEN + " ?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var itemToUpdate = DataProvider.Ins.DB.KHACHHANGs.FirstOrDefault(item => item.MAKH == SelectedCustomer.MAKH);
                    if (itemToUpdate != null)
                    {
                        itemToUpdate = SelectedCustomer;
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Cập nhật thông tin khách hàng thành công!");
                    }
                }
            }
        }
        private void DeleteCustomer()
        {
            if(SelectedCustomer != null)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng " + SelectedCustomer.HOTEN + " ?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DataProvider.Ins.DB.KHACHHANGs.Remove(SelectedCustomer);
                    DataProvider.Ins.DB.SaveChanges();
                    ListKhachhang.Remove(SelectedCustomer);
                }
            }
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

            string temp = "KH";
            string newNumberStr = newNumber.ToString().PadLeft(6, '0'); 
            string newCode = temp + newNumberStr;

            return newCode;


        }
        public string GetLastGeneratedMAKH()
        {
            var lastPurchase = DataProvider.Ins.DB.KHACHHANGs
                .OrderByDescending(p => p.MAKH)
                .FirstOrDefault();

            if (lastPurchase != null)
            {
                return lastPurchase.MAKH;
            }

            return null;
        }




    }
}
