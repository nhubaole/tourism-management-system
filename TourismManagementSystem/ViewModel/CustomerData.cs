using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TourismManagementSystem.ViewModel
{
    internal class CustomerData:BaseViewModel
    {
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }

        public ICommand AddCustomerCommand { get; set; }
        public CustomerData() {
            
        }
    }
}
