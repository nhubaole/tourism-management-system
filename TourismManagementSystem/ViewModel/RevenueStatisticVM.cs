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
    public class RevenueStatisticVM : BaseViewModel
    {


        private bool _IsSaleChecked;
        public bool IsSaleChecked
        {
            get { return _IsSaleChecked; }
            set
            {
                _IsSaleChecked = value;

                OnPropertyChanged();
                //UpdateCurrentView();
            }
        }


        private bool _IsTripCheck;
        public bool IsTripCheck
        {
            get { return _IsTripCheck; }
            set
            {
                _IsTripCheck = value;
                OnPropertyChanged();
                //UpdateCurrentView();

            }
        }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }
        public RevenueStatisticVM()
        {
            IsSaleChecked = true;
        }
        public void SalesStatistics(object obj) => CurrentView = new SalesStatisticsVM();
        public void TripsStatistics(object obj) => CurrentView = new TripsStatisticsVM();

        //private void UpdateCurrentView()
        //{
        //    if (IsSaleChecked == true)
        //    {
        //        CurrentView = new SalesStatisticsVM();
        //    }
        //    else if (IsTripCheck == true)
        //    {
        //        CurrentView = new TripsStatisticsVM();
        //    }
        //    else
        //    {
        //        // Không có lựa chọn nào được chọn, đặt CurrentView thành null hoặc giá trị mặc định
        //        CurrentView = null;
        //    }
        //}
    }
}