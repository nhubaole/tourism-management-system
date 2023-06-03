using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                if (_Month != 0)
                {
                    TimeOfChart = "tháng " + _Month.ToString() + " năm " + Year.ToString();
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

        
        public SalesStatisticsVM()
        {
            FilterItems1 = new ObservableCollection<String>(new List<string> { "Tháng", "Năm" });
            seriesCollection = new SeriesCollection();

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
        private void DrawChart()
        {
            // Kiểm tra điều kiện để vẽ biểu đồ
            if (Filter1 == "Năm" && Year != 0)
            {
                // Lấy số lượng chuyến thành công và chuyến bị hủy trong năm được chọn
                int successCount = DataProvider.Ins.DB.CHUYENs
                    .Count(t => t.TGBATDAU.HasValue && t.TGBATDAU.Value.Year == Year && t.THANHCONG == true);

                int cancelledCount = DataProvider.Ins.DB.CHUYENs
                    .Count(t => t.TGBATDAU.HasValue && t.TGBATDAU.Value.Year == Year && t.THANHCONG == false);

                PieSeries successSeries = new PieSeries
                {
                    Title = "Thành công",
                    Values = new ChartValues<double> { successCount },
                    DataLabels = true,
                    LabelPoint = chartPoint => chartPoint.Y.ToString()
                };
                PieSeries cancelledSeries = new PieSeries
                {
                    Title = "Hủy",
                    Values = new ChartValues<double> { cancelledCount },
                    DataLabels = true,
                    LabelPoint = chartPoint => chartPoint.Y.ToString()
                };

                // Xóa dữ liệu cũ trong biểu đồ
                seriesCollection.Clear();

                // Thêm chuỗi dữ liệu vào SeriesCollection
                seriesCollection.Add(successSeries);
                seriesCollection.Add(cancelledSeries);
            }
            else if (Filter1 == "Tháng" && Year != 0 && Month != 0)
            {
                // Lấy số lượng chuyến thành công và chuyến bị hủy trong tháng và năm được chọn
                int successCount = DataProvider.Ins.DB.CHUYENs
                    .Count(t => t.TGBATDAU.HasValue && t.TGBATDAU.Value.Year == Year && t.TGBATDAU.Value.Month == Month && t.THANHCONG == true);
                int cancelledCount = DataProvider.Ins.DB.CHUYENs
                    .Count(t => t.TGBATDAU.HasValue && t.TGBATDAU.Value.Year == Year && t.TGBATDAU.Value.Month == Month && t.THANHCONG == false);

                // Tạo chuỗi dữ liệu cho phần trăm chuyến đi thành công
                PieSeries successSeries = new PieSeries
                {
                    Title = "Thành công",
                    Values = new ChartValues<double> { successCount },
                    DataLabels = true,
                    LabelPoint = chartPoint => chartPoint.Y.ToString()
                };

                // Tạo chuỗi dữ liệu cho phần trăm chuyến đi bị hủy
                PieSeries cancelledSeries = new PieSeries
                {
                    Title = "Hủy",
                    Values = new ChartValues<double> { cancelledCount },
                    DataLabels = true,
                    LabelPoint = chartPoint => chartPoint.Y.ToString()
                };

                // Xóa dữ liệu cũ trong biểu đồ
                seriesCollection.Clear();

                // Thêm chuỗi dữ liệu vào SeriesCollection
                seriesCollection.Add(successSeries);
                seriesCollection.Add(cancelledSeries);
            }
        }
       
       
    }
}
