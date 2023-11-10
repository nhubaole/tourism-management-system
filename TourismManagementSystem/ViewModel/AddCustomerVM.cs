using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    public class AddCustomerVM : BaseViewModel, INotifyDataErrorInfo
    {
        private KHACHHANG NewCustomer;

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

        public AddCustomerVM()
        {
            NewCustomer = new KHACHHANG();
            string formattedID;
            ObservableCollection<KHACHHANG> ListKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
            if (ListKH.Count() == 0)
            {
                formattedID = string.Format("KH{0:D6}", 1);
            }
            else
            {
                string lastID = ListKH.Last().MAKH;
                int previousNumber = int.Parse(lastID.Substring(2));
                int nextNumber = previousNumber + 1;
                formattedID = string.Format("KH{0:D6}", nextNumber);
            }
            MAKH = formattedID;

            AddDataCommand = new RelayCommand<object>((p) => {
                if (HasErrors || DIACHI == null || HOTEN == null || EMAIL == null || SDT == null || CCCD == null)
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
                    if (w != null)
                    {
                        NewCustomer.MAKH = MAKH;
                        NewCustomer.HOTEN = HOTEN;
                        NewCustomer.EMAIL = EMAIL;
                        NewCustomer.SDT = SDT;
                        NewCustomer.DIACHI = DIACHI;
                        NewCustomer.CCCD = CCCD;
                        DataProvider.Ins.DB.KHACHHANGs.Add(NewCustomer);
                        DataProvider.Ins.DB.SaveChanges();

                        MessageBox.Show("Đã tạo mới khách hàng thành công");
                        w.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }


            });
        }

        public ICommand AddDataCommand { get; set; }


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
                    if (existingCustomer != null)
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
