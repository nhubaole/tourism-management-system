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
                if (!string.IsNullOrEmpty(SDT) && !IsNumber(SDT))
                {
                    errors.Add("Please enter a valid number.");
                }
            }
            if (propertyName == nameof(CCCD))
            {
                if (!string.IsNullOrEmpty(CCCD) && !IsNumber(CCCD))
                {
                    errors.Add("Please enter a valid number.");
                }
            }

            if (propertyName == nameof(HOTEN))
            {
                if (!string.IsNullOrEmpty(HOTEN) && IsNumber(HOTEN))
                {
                    errors.Add("Please enter a valid name.");
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


        private ObservableCollection<string> _filter = new ObservableCollection<string>() { "Mã khách hàng", "Tên khách hàng" };
        public ObservableCollection<KHACHHANG> KhachHangs { get; set; }
        public ICommand SwitchWindowCommand { get; set; }
        public ICommand AddDataCommand { get; set; }
        public ICommand DeleteDataCommand { get; set; }
        public ICommand UpdateDataCommand { get; set; }

        private KHACHHANG _selectedCustomer;
        public KHACHHANG SelectedCustomer { get => _selectedCustomer; set
            {
                _selectedCustomer = value;
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
      




        private ObservableCollection<KHACHHANG> _ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
        public ObservableCollection<KHACHHANG> ListKhachhang { get => _ListKhachhang; set { _ListKhachhang= value; OnPropertyChanged(nameof(ListKhachhang)); } }

        public CustomerVM()
        {
            SwitchWindowCommand = new RelayCommand<object>((p) => {
               
                  return true;
                
            }, (p) =>
            {SwitchWindow(p);});

            KhachHangs = new ObservableCollection<KHACHHANG>();
            AddDataCommand = new RelayCommand<object>((p) => { 
                if (!CanAddData())
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
                    var temp = new KHACHHANG()
                    {
                        MAKH = MAKH,
                        HOTEN = HOTEN,
                        CCCD = CCCD,
                        SDT = SDT,
                        EMAIL = EMAIL,
                        DIACHI = DIACHI
                    };

                    DataProvider.Ins.DB.KHACHHANGs.Add(temp);
                    DataProvider.Ins.DB.SaveChanges();
                    ListKhachhang.Add(temp);



                    MessageBox.Show("Đã tạo mới khách hàng thành công");

                    MAKH = null;
                    HOTEN = null;
                    CCCD = null;
                    SDT = null;
                    EMAIL = null;
                    DIACHI = null;

                    CollectionViewSource.GetDefaultView(ListKhachhang).Refresh();
                    //LoadDataGrid();



                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                }


            });


            UpdateDataCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) => { UpdateCustomer(); });
            DeleteDataCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) => { DeleteCustomer(); });

          

        }
        private void LoadDataGrid()
        {
            ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);

        }
        private void SwitchWindow(object parameter)
        {
            AddCustomerWindow addCustomerWindow = new AddCustomerWindow();
            addCustomerWindow.Show();
           
        }
        private bool CanAddData()
        {
            return !string.IsNullOrEmpty(MAKH)&& !string.IsNullOrEmpty(DIACHI)&& !string.IsNullOrEmpty(EMAIL) && !HasErrors;
        }
        private void UpdateCustomer()
        {


            SelectedCustomer.HOTEN = SelectedCustomer.HOTEN;
            SelectedCustomer.CCCD = SelectedCustomer.CCCD;
            SelectedCustomer.SDT = SelectedCustomer.SDT;
            SelectedCustomer.EMAIL = SelectedCustomer.EMAIL;
            SelectedCustomer.DIACHI = SelectedCustomer.DIACHI;
           

            MessageBox.Show("Bạn muốn cập nhật thông tin cho khách hàng" + SelectedCustomer.MAKH);

            DataProvider.Ins.DB.SaveChanges();
            //LoadDataGrid();
        }
        private void DeleteCustomer()
        {
            MessageBox.Show("Bạn muốn xóa khách hàng " +SelectedCustomer.MAKH);
            DataProvider.Ins.DB.KHACHHANGs.Remove(SelectedCustomer);
            DataProvider.Ins.DB.SaveChanges();
            ListKhachhang.Remove(SelectedCustomer);
            //LoadDataGrid();
        }
    }
}
