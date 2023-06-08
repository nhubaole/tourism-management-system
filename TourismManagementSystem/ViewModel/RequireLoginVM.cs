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
    internal class RequireLoginVM : BaseViewModel
    {
        public RequireLoginVM()
        {
            LoginCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {
                MainWindow mainWindow = Window.GetWindow(p) as MainWindow;
                if (mainWindow != null)
                {
                    MainWindow main = new MainWindow();
                    mainWindow.Close();
                    main.Show();
                }

            });
        }
        public ICommand LoginCommand { get; set; }
    }
}
