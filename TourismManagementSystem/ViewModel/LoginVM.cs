using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
                        if (Password == "admin" && Username == "admin")
                        {
                            IsLogin = true;
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

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Password = p.Password;
            });
        }
        public bool IsLogin { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged(); } }
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
    }
}
