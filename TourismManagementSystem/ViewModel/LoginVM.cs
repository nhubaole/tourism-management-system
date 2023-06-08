using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    internal class LoginVM : BaseViewModel
    {
        private string _Username;
        private string _Password;
        public LoginVM ()
        {
            IsLogin = false;
            LoginCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                if (p == null) return;
                else
                {
                    if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username))
                    {
                        MessageBox.Show("Hãy điền đủ thông tin!");
                    }
                    else
                    {
                        string pass = MD5Hash(Base64Encode(Password));
                        var count = DataProvider.Ins.DB.TAIKHOANs.Where(a => a.TENTK == Username && a.MATKHAU == pass).Count();

                        if (count > 0)
                        {
                            IsLogin = true;
                            IsGuest = false;
                            p.Close();
                        }
                        else
                        {
                            IsLogin = false;
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu bị sai!");
                        }
                    }
                }
            });

            ContinueAsGuestCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                if (p == null) return;
                else
                {
                    IsGuest = true;
                    p.Close();
                }
            });

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Password = p.Password;
            });
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        public bool IsLogin { get; set; }
        public bool IsGuest { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand ContinueAsGuestCommand { get; set; }
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged(); } }
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
    }
}
