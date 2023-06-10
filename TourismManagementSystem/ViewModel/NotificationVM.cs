using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    internal class NotificationVM : BaseViewModel
    {
        private ObservableCollection<THONGBAO> _ListThongBao = new ObservableCollection<THONGBAO>(DataProvider.Ins.DB.THONGBAOs.OrderByDescending(tb => tb.MATB).Take(4));
        public NotificationVM()
        {

        }
        public ObservableCollection<THONGBAO> ListThongBao { get => _ListThongBao; set { _ListThongBao = value; OnPropertyChanged(); } }
    }
}
