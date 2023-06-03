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

     
        private int _Month;
        public int Month
        {
            get { return _Month; }
            set
            {
                _Month = value;
                if (_Month != 0)
                {
                    TimeOfChart = "tháng" + _Month.ToString() + "năm" + Year.ToString();
                }    
                OnPropertyChanged();

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
                        FilterMonth = GetMonthsForYear(Year);
                    }
                    else
                    {
                        TimeOfChart = "năm" + Year.ToString();
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

        private ChartValues<double> _success;
        public ChartValues<double> success
        {
            get { return _success; }
            set
            {
                _success = value;
                OnPropertyChanged();
            }
        }

        private ChartValues<double> _cancelled;
        public ChartValues<double> cancelled
        {
            get { return _cancelled; }
            set
            {
                _cancelled = value;
                OnPropertyChanged();
            }
        }


        private SeriesCollection _seriesCollection1;
        public SeriesCollection seriesCollection1
        {
            get { return _seriesCollection1; }
            set
            {
                _seriesCollection1 = value;
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
            seriesCollection1= new SeriesCollection();
            seriesCollection2 = new SeriesCollection();
            Filter1 = "Năm";




            //DrawChart();
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
            // Kiểm tra điều kiện để vẽ biểu đồ
            if (Filter1 == "Năm" && Year != 0)
            {
                // Lấy số lượng chuyến thành công và chuyến bị hủy trong năm được chọn
                int successCount = DataProvider.Ins.DB.CHUYENs
                    .Count(t => t.TGBATDAU.HasValue && t.TGBATDAU.Value.Year == Year && t.THANHCONG == 1);

                int cancelledCount = DataProvider.Ins.DB.CHUYENs
                    .Count(t => t.TGBATDAU.HasValue && t.TGBATDAU.Value.Year == Year && t.THANHCONG == 0);

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
                seriesCollection1.Clear();

                // Thêm chuỗi dữ liệu vào SeriesCollection
                seriesCollection1.Add(successSeries);
                seriesCollection1.Add(cancelledSeries);
            }
            else if (Filter1 == "Tháng" && Year != 0 && Month != 0)
            {
                // Lấy số lượng chuyến thành công và chuyến bị hủy trong tháng và năm được chọn
                int successCount = DataProvider.Ins.DB.CHUYENs
                    .Count(t => t.TGBATDAU.HasValue && t.TGBATDAU.Value.Year == Year && t.TGBATDAU.Value.Month == Month && t.THANHCONG == "Thành công");

                int cancelledCount = DataProvider.Ins.DB.CHUYENs
                    .Count(t => t.TGBATDAU.HasValue && t.TGBATDAU.Value.Year == Year && t.TGBATDAU.Value.Month == Month && t.THANHCONG == "Hủy");

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
                seriesCollection1.Clear();

                // Thêm chuỗi dữ liệu vào SeriesCollection
                seriesCollection1.Add(successSeries);
                seriesCollection1.Add(cancelledSeries);
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

        public ObservableCollection<int> GetMonthsForYear(int year)
        {
            ObservableCollection<int> months = new ObservableCollection<int>();

            // Lấy tháng hiện tại
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            if (year == currentYear)
            {
                // Nếu là năm hiện tại, chỉ lấy đến tháng hiện tại
                for (int month = 1; month <= currentMonth; month++)
                {
                    months.Add(month);
                }
            }
            else
            {
                // Nếu không phải là năm hiện tại, lấy tất cả 12 tháng
                months = new ObservableCollection<int>(Enumerable.Range(1, 12));
            }

            return months;
        }
    }
}
