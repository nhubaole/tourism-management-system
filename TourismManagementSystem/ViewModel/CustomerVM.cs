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
    
    public class CustomerVM : BaseViewModel, INotifyDataErrorInfo
    {

        private ObservableCollection<string> _filter = new ObservableCollection<string>() { "Mã khách hàng", "Tên khách hàng","Số điện thoại" };

        public ICommand SwitchWindowCommand { get; set; }
        public ICommand AddDataCommand { get; set; }
        public ICommand DeleteDataCommand { get; set; }
        public ICommand UpdateDataCommand { get; set; }
        public ICommand BookingButtonCommand { get; set; }


        private KHACHHANG _selectedCustomer;
        public KHACHHANG SelectedCustomer { get => _selectedCustomer; set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
                HOTEN = SelectedCustomer.HOTEN;
                CCCD = SelectedCustomer.CCCD;
                SDT = SelectedCustomer.SDT;
                DIACHI = SelectedCustomer.DIACHI;
                EMAIL = SelectedCustomer.EMAIL;
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



        //private ObservableCollection<KHACHHANG> _ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
        private ObservableCollection<KHACHHANG> _ListKhachhang;
        public ObservableCollection<KHACHHANG> ListKhachhang { get => _ListKhachhang; set { _ListKhachhang= value; OnPropertyChanged(nameof(ListKhachhang)); } }

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
        public CustomerVM()
        {
            ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
            selectedFilter = filter[0];
            SwitchWindowCommand = new RelayCommand<object>((p) => {
               
                  return true;
                
            }, (p) =>
            {SwitchWindow(p);});

            UpdateDataCommand = new RelayCommand<object>((p) =>
            {
                if (HasErrors || DIACHI == null || HOTEN == null || EMAIL == null || SDT == null || CCCD == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            },
            (p) => { UpdateCustomer(); });
            DeleteDataCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) => {
                if (SelectedCustomer != null)
                {
                    if (SelectedCustomer.PHIEUDATCHOes.Count > 0)
                    {
                        MessageBox.Show("Có tồn tại phiếu đặt chỗ của khách hàng này. Không thể xóa!");
                    }
                    else
                    {
                        if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng " + SelectedCustomer.HOTEN + " ?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            DataProvider.Ins.DB.KHACHHANGs.Remove(SelectedCustomer);
                            DataProvider.Ins.DB.SaveChanges();
                            ListKhachhang.Remove(SelectedCustomer);
                        }
                    }
                }
            });

            BookingButtonCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                BookingDetailVM.SelectedPhieu = p as PHIEUDATCHO;
                BookingDetailsWindow bookingDetailsWindow = new BookingDetailsWindow();
                bookingDetailsWindow.ShowDialog();
            });
        }
        
        //đẻ test
        public CustomerVM(ObservableCollection<KHACHHANG> ListKHTest)
        {
            ListKhachhang = ListKHTest;
            selectedFilter = filter[0];
            SwitchWindowCommand = new RelayCommand<object>((p) => {

                return true;

            }, (p) =>
            { SwitchWindow(p); });

            UpdateDataCommand = new RelayCommand<object>((p) =>
            {
                if (HasErrors || DIACHI == null || HOTEN == null || EMAIL == null || SDT == null || CCCD == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            },
            (p) => { UpdateCustomer(); });
            DeleteDataCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) => {
                if (SelectedCustomer != null)
                {
                    if (SelectedCustomer.PHIEUDATCHOes.Count > 0)
                    {
                        MessageBox.Show("Có tồn tại phiếu đặt chỗ của khách hàng này. Không thể xóa!");
                    }
                    else
                    {
                        if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng " + SelectedCustomer.HOTEN + " ?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            ListKHTest.Remove(SelectedCustomer);
                        }
                    }
                }
            });

            BookingButtonCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                BookingDetailVM.SelectedPhieu = p as PHIEUDATCHO;
                BookingDetailsWindow bookingDetailsWindow = new BookingDetailsWindow();
                bookingDetailsWindow.ShowDialog();
            });
        }
        private void SwitchWindow(object parameter)
        {
            try
            {
                AddCustomerWindow addCustomerWindow = new AddCustomerWindow();
                addCustomerWindow.ShowDialog();
                ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
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
                        SelectedCustomer.HOTEN = HOTEN;
                        SelectedCustomer.CCCD = CCCD;
                        SelectedCustomer.SDT = SDT;
                        SelectedCustomer.DIACHI = DIACHI;
                        SelectedCustomer.EMAIL = EMAIL;
                        itemToUpdate = SelectedCustomer;
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Cập nhật thông tin khách hàng thành công!");
                        ListKhachhang = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
                    }
                }
            }
        }

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


        public void ValidateProperty(string propertyName)
        {
            List<string> errors = new List<string>();

            if (propertyName == nameof(SDT))
            {
                if (string.IsNullOrEmpty(SDT))
                {
                    errors.Add("Vui lòng nhập số điện thoại hợp lệ");
                }
                else if (!IsNumber(SDT))
                {
                    errors.Add("Vui lòng nhập số điện thoại hợp lệ");

                }
                else if (SDT.Length != 10)
                {
                    errors.Add("Vui lòng nhập số điện thoại hợp lệ");

                }

            }

            if (propertyName == nameof(CCCD))
            {
                if (string.IsNullOrEmpty(CCCD))
                {
                    errors.Add("Vui lòng nhập số CCCD gồm 12 chữ số");
                }
                else if (!IsNumber(CCCD))
                {
                    errors.Add("Vui lòng nhập số CCCD gồm 12 chữ số");
                }
                else if (cccd.Length != 12)
                {
                    errors.Add("Vui lòng nhập số CCCD gồm 12 chữ số");
                }
                else
                {
                    var existingCustomer = DataProvider.Ins.DB.KHACHHANGs.FirstOrDefault(c => c.CCCD == CCCD);
                    if (existingCustomer != null && existingCustomer != SelectedCustomer)
                    {
                        errors.Add("Số CCCD đã tồn tại. Vui lòng kiểm tra lại");
                    }
                }

            }

            if (propertyName == nameof(HOTEN))
            {
                if (string.IsNullOrEmpty(HOTEN) || !IsNameValid(hoTen))
                {
                    errors.Add("Vui lòng nhập họ và tên hợp lệ");
                }


            }
            if (propertyName == nameof(EMAIL))
            {
                if (string.IsNullOrEmpty(EMAIL) || !IsEmailValid(email))
                {
                    errors.Add("Vui lòng nhập email hợp lệ");
                }


            }
            if (propertyName == nameof(DIACHI))
            {
                if (string.IsNullOrEmpty(DIACHI))
                {
                    errors.Add("Vui lòng nhập địa chỉ");
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

        public bool IsNumber(string value)
        {
            return double.TryParse(value, out _);
        }
        public bool IsNameValid(string name)
        {
            foreach (char c in name)
            {
                if (!char.IsLetter(c) && c != ' ')
                {
                    return false;
                }
            }

            return true;
        }
        public bool IsEmailValid(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            // Regular expression pattern for email validation
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            return Regex.IsMatch(email, pattern);
        }
    }
}
