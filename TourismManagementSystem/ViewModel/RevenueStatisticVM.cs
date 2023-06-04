using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TourismManagementSystem.ViewModel
{
    internal class RevenueStatisticVM : BaseViewModel
    {
        

        private string _IsSaleChecked;
        public string IsSaleChecked
        {
            get { return _IsSaleChecked; }
            set
            {
                _IsSaleChecked = value;

                OnPropertyChanged();
                UpdateCurrentView();
            }
        }

      
        private string _IsTripCheck;
        public string IsTripCheck
        {
            get { return _IsTripCheck; }
            set
            {
                _IsTripCheck = value;
                OnPropertyChanged();
                UpdateCurrentView();

            }
        }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }
        public RevenueStatisticVM ()
        {
            
        }
        private void SalesStatistics(object obj) => CurrentView = new SalesStatisticsVM();
        private void TripsStatistics(object obj) => CurrentView = new TripsStatisticsVM();

        private void UpdateCurrentView()
        {
            if (IsSaleChecked == "True")
            {
                CurrentView = new SalesStatisticsVM();
            }
            else if (IsTripCheck == "True")
            {
                CurrentView = new TripsStatisticsVM();
            }
            else
            {
                // Không có lựa chọn nào được chọn, đặt CurrentView thành null hoặc giá trị mặc định
                CurrentView = null;
            }
        }
    }
}
