using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Data.SqlClient;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    internal class TripsStatisticsVM: BaseViewModel
    {

        private string connectionString; // Chuỗi kết nối đến cơ sở dữ liệu

        private ObservableCollection<string> _FilterItems1;
        public ObservableCollection<string> FilterItems1
        {
            get { return _FilterItems1; }
            set { _FilterItems1 = value; }
        }

        private ObservableCollection<int>  _FilterMonth;

        public ObservableCollection<int> FilterMonth
        {
            get { return _FilterMonth; }
            set { _FilterMonth = value; }
        }

        private ObservableCollection<int> _FilterYear;

        public ObservableCollection<int> FilterYear
        {
            get { return _FilterYear; }
            set { _FilterYear = value; }
        }

        private string _Filter1;
        public string Filter1
        {
            get { return _Filter1; }
            set
            {
                _Filter1 = value;
                OnPropertyChanged();
                UpdateFilterYear();
            }
        }

        private bool _CanChoseMonth;
        public bool CanChoseMonth
        {
            get { return _CanChoseMonth; }
            set
            {
                _CanChoseMonth = value;
                OnPropertyChanged();
            }
        }

        private bool _CanChoseYear;
        public bool CanChoseYear
        {
            get { return _CanChoseYear; }
            set
            {
                _CanChoseYear = value;
                OnPropertyChanged();
            }
        }

     
        private string _Month;
        public string Month
        {
            get { return _Month; }
            set
            {
                _Month = value;
                OnPropertyChanged();

            }
        }

        private string _Year;
        public string Year
        {
            get { return _Year; }
            set
            {
                _Year = value;
                if (Filter1 == "Tháng")
                {
                    CanChoseMonth = true;
                }
                OnPropertyChanged();
            }
        }

        private string _TimeOfChart;
        public string TimeOfChart
        {
            get { return _TimeOfChart; }
            set
            {
                _TimeOfChart = value;
                
                OnPropertyChanged();
            }
        }

        private SeriesCollection _seriesCollection1;
        public SeriesCollection seriesCollection1
        {
            get { return _seriesCollection2; }
            set
            {
                _seriesCollection2 = value;
                OnPropertyChanged();
            }
        }

        private SeriesCollection _seriesCollection2;
        public SeriesCollection seriesCollection2
        {
            get { return _seriesCollection2; }
            set
            {
                _seriesCollection2 = value;
                OnPropertyChanged();
            }
        }
        public TripsStatisticsVM() {

            FilterItems1 = new ObservableCollection<String>(new List<string> { "Tháng", "Năm" });


            seriesCollection2 = new SeriesCollection();

            Filter1 = "Năm";

            FilterMonth = new ObservableCollection<int>();
            FilterYear = new ObservableCollection<int>();


            //DrawChart();
        }
        private void UpdateFilterYear()
        {
            FilterYear = GetYears();

            if (Filter1 == "Tháng")
            {
                CanChoseYear = true;
            }
            else if (Filter1 == "Năm")
            {
                
                CanChoseYear = true;
                CanChoseMonth = false;
                // Load danh sách các năm

            }
            else
            {
                // Nếu Filter1 không phải "Tháng" hoặc "Năm", không cần cập nhật danh sách FilterItems2
                CanChoseYear = false;
                CanChoseMonth = false;
            }
        }
        private void DrawChart()
        {
            
        }
        public List<int> GetYears()
        {
            List<int> years = new List<int>();
            years = DataProvider.Ins.DB.CHUYENs
               .Where(t => t.TGBATDAU.HasValue)
               .Select(t => t.TGBATDAU.Value.Year)
               .Distinct().ToList();


            return years;
        }
    }
}
