using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    public class SalesStatisticsVM : BaseViewModel
    {
        private ObservableCollection<string> _FilterItems1;
        public ObservableCollection<string> FilterItems1
        {
            get { return _FilterItems1; }
            set { _FilterItems1 = value; }
        }

        private ObservableCollection<int> _FilterMonth;

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
        //chọn tháng hay năm
        private string _Filter1;
        public string Filter1
        {
            get { return _Filter1; }
            set
            {
                _Filter1 = value;
                seriesCollection.Clear();
                TimeOfChart = null;
                Year = 0; Month = 0;
                OnPropertyChanged(nameof(seriesCollection));
                OnPropertyChanged(nameof(TimeOfChart));
                OnPropertyChanged(nameof(Year));
                OnPropertyChanged(nameof(Month));
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


        private int _Month;
        public int Month
        {
            get { return _Month; }
            set
            {
                _Month = value;
                dayCollection =  GetDaysInMonth(Year, Month);
                OnPropertyChanged(nameof(dayCollection));

                if (_Month != 0)
                {
                    TimeOfChart = " tháng " + _Month.ToString() + " năm " + Year.ToString();
                }
                OnPropertyChanged();
                DrawChart();

            }
        }

        private int _Year;
        public int Year
        {
            get { return _Year; }
            set
            {
                _Year = value;
                if (Year != 0)
                {
                    if (Filter1 == "Tháng")
                    {
                        CanChoseMonth = true;
                        FilterMonth = new ObservableCollection<int>(GetMonthsForYear(Year));
                        OnPropertyChanged(nameof(FilterMonth));
                        ;
                    }
                    else
                    {
                        TimeOfChart = " năm " + Year.ToString();
                        DrawChart();
                    }
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

       
        private SeriesCollection _seriesCollection;
        public SeriesCollection seriesCollection
        {
            get { return _seriesCollection; }
            set
            {
                _seriesCollection = value;
                OnPropertyChanged();
            }
        }

        private List<int> _dayCollection;
        public List<int> dayCollection
        {
            get { return _dayCollection; }
            set
            {
                _dayCollection = value;
                OnPropertyChanged("ThangCollection");
            }
        }

        public SalesStatisticsVM()
        {
            FilterItems1 = new ObservableCollection<String>(new List<string> { "Tháng", "Năm" });
            seriesCollection = new SeriesCollection();
            dayCollection = new List<int>();

            Filter1 = "Năm";
        }
        private void UpdateFilterYear()
        {
            FilterYear = new ObservableCollection<int>(GetYears());

            if (Filter1 == "Tháng")
            {
                CanChoseYear = true;
            }
            else if (Filter1 == "Năm")
            {

                CanChoseYear = true;
                CanChoseMonth = false;

            }
            else
            {
                CanChoseYear = false;
                CanChoseMonth = false;
            }
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

        public List<int> GetMonthsForYear(int year)
        {
            List<int> months = new List<int>();

            // Lấy tháng hiện tại
            months = DataProvider.Ins.DB.CHUYENs
               .Where(t => t.TGBATDAU.HasValue && t.TGBATDAU.Value.Year == year)
               .Select(t => t.TGBATDAU.Value.Month)
               .Distinct().ToList();
            return months;
        }
        public static List<int> GetDaysInMonth(int year, int month)
        {
            List<int> daysInMonth = new List<int>();
            if (year != 0 && month != 0)
            {
                

                // Xác định ngày đầu tiên của tháng
                DateTime firstDayOfMonth = new DateTime(year, month, 1);

                // Xác định số ngày trong tháng
                int numberOfDaysInMonth = DateTime.DaysInMonth(year, month);

                // Thêm các ngày vào danh sách
                for (int day = 1; day <= numberOfDaysInMonth; day++)
                {
                    daysInMonth.Add(day);
                }

                
            }
            return daysInMonth;

        }
        public List<int> GetMonthsInYear()
        {
            List<int> months = Enumerable.Range(1, 12).ToList();
            return months;
        }
        private void DrawChart()
        {
            seriesCollection.Clear();

            
            // Kiểm tra điều kiện để vẽ biểu đồ
            if (Filter1 == "Tháng" && Year != 0 && Month != 0)
            {
                List<int> daysInMonth = GetDaysInMonth(Year, Month);

                foreach (int day in daysInMonth)
                {
                    int revenueValue = GetRevenueForDay(day, Month, Year); // Hàm để lấy doanh thu cho ngày cụ thể

                    ColumnSeries dayRevenueSeries = new ColumnSeries
                    {
                        Title = $"{day}",
                        Values = new ChartValues<int> { revenueValue },
                        DataLabels = true
                    };

                    seriesCollection.Add(dayRevenueSeries);
                }
                
            }
            else if (Filter1 == "Năm" && Year != 0)
            {
                List<int> monthsInYear = GetMonthsInYear();
               
                // Lấy doanh thu cho từng ngày trong tháng
                foreach (int month in monthsInYear)
                {
                    int revenueValue = GetRevenueForMonth(month, Year); // Hàm để lấy doanh thu cho thngs cụ thể

                    ColumnSeries MonthRevenueSeries = new ColumnSeries
                    {
                        Title = $"{month}",
                        Values = new ChartValues<int> { revenueValue },
                        DataLabels = true
                    };

                    seriesCollection.Add(MonthRevenueSeries);
                }
            }
        }

        private int GetRevenueForDay(int day, int month, int year)
        {
            int Revenue = 0;
            foreach (var item in DataProvider.Ins.DB.THONGTINTHANHTOANs.Where(item => item.THOIGIAN.Value.Day == day 
                                                                            && item.THOIGIAN.Value.Month == month
                                                                            && item.THOIGIAN.Value.Year == Year))
            {
                Revenue += (int)item.SOTIEN;
            }

            return Revenue;
        }
        private int GetRevenueForMonth( int month, int year)
        {
            int Revenue = 0;
            foreach (var item in DataProvider.Ins.DB.THONGTINTHANHTOANs.Where(item =>item.THOIGIAN.Value.Month == month
                                                                            && item.THOIGIAN.Value.Year == Year))
            {
                Revenue += (int)item.SOTIEN;
            }

            return Revenue;
        }
    }
}
