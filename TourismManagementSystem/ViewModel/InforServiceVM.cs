using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.Model;
using TourismManagementSystem.UserControls;

namespace TourismManagementSystem.ViewModel
{
   
    public class InforServiceVM :BaseViewModel
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        public static bool IsNew; //xác định là tạo mới hay cập nhập

        private ObservableCollection<string> _FilterItems;
        public ObservableCollection<string> FilterItems
        {
            get { return _FilterItems; }
            set { _FilterItems = value; OnPropertyChanged(); }
        }
        private string _Filter;
        public string Filter
        {
            get { return _Filter; }
            set
            {
                _Filter = value;

                OnPropertyChanged();
            }
        }

        public InforServiceVM() {
            FilterItems = new ObservableCollection<String>(new List<string> { "Phương tiện", "Khách sạn", "Nhà hàng", "Dịch vụ khác" });

            Filter = FilterItems.FirstOrDefault();
            CurrentView = new UCInforTrafffic();

            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Filter))
                {
                    switch (Filter)
                    {
                        case "Phương tiện":
                            Traffic(null);
                            break;
                        case "Nhà hàng":
                            Restaurant(null);
                            break;
                        case "Khách sạn":
                            Hotel(null);
                            break;
                        case "Dịch vụ khác":
                            OTherService(null);
                            break;
                        default:
                            // Xử lý cho trường hợp khác
                            break;
                    }
                }
            };
        }
        private void Traffic(object obj) => CurrentView = new UCInforTrafffic();
        private void Restaurant(object obj) => CurrentView = new UCInforRestaurant();
        private void Hotel(object obj) => CurrentView = new UCInforHotel();
        private void OTherService(object obj) => CurrentView = new UCInforOtherService();
    }
}
