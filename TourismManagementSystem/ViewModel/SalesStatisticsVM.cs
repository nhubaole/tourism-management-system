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
    public class SalesStatisticsVM : BaseViewModel
    {
        private ObservableCollection<string> _FilterItems1;
        public ObservableCollection<string> FilterItems1
        {
            get { return _FilterItems1; }
            set { _FilterItems1 = value; }
        }

        private ObservableCollection<string> _FilterItems2;
        public ObservableCollection<string> FilterItems2
        {
            get { return _FilterItems2; }
            set { _FilterItems2 = value; }
        }

        private string _Filter1;
        public string Filter1
        {
            get { return _Filter1; }
            set
            {
                _Filter1 = value;

                IsFilter2Enable = !string.IsNullOrEmpty(value);
                UpdateFilter2();
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
            get { return Year; }
            set
            {
                Year = value;
                OnPropertyChanged();

            }
        }

        private bool _IsFilter2Enable;
        public bool IsFilter2Enable
        {
            get { return _IsFilter2Enable; }
            set
            {
                _IsFilter2Enable = value;
                OnPropertyChanged();
            }
        }


        //dữ liệu vẽ biểu đồ cột
        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection
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

            SeriesCollection = new SeriesCollection();

            DrawChart();
        }
        private void UpdateFilter2()
        {
            if (Filter1 == "Tháng")
            {
                // Hiển thị danh sách các tháng
                FilterItems2 = new ObservableCollection<string>(Enumerable.Range(1, 12).Select(month => month.ToString()));

            }
            else if (Filter1 == "Năm")
            {
                // Hiển thị danh sách các năm từ 2020
                int currentYear = DateTime.Now.Year;
                FilterItems2 = new ObservableCollection<string>(Enumerable.Range(2020, currentYear - 2020 + 1).Select(year => year.ToString()));
            }
            else
            {
                // Nếu Filter1 không phải "Tháng" hoặc "Năm", không cần cập nhật danh sách FilterItems2
                FilterItems2 = new ObservableCollection<string>();
            }
            OnPropertyChanged(nameof(FilterItems2));


        }
        private void DrawChart()
        {
            // Xử lý logic để vẽ biểu đồ dựa trên giá trị của Filter2
            // Ví dụ: vẽ lại biểu đồ với dữ liệu tương ứng với Filter2 đã chọn

            // Xóa các chuỗi dữ liệu cũ trong SeriesCollection (nếu có)
            SeriesCollection.Clear();

            // Thêm các chuỗi dữ liệu mới vào SeriesCollection
            // Ví dụ: thêm chuỗi dữ liệu đường mới
            LineSeries lineSeries = new LineSeries
            {
                Title = "Doanh thu",
                Values = new ChartValues<double> { 20, 30, 40, 50, 60 } // Thay đổi giá trị tùy ý
            };
            SeriesCollection.Add(lineSeries);

            // Thêm các chuỗi dữ liệu khác (nếu có) vào SeriesCollection
            // Ví dụ: thêm chuỗi dữ liệu cột mới
            ColumnSeries columnSeries = new ColumnSeries
            {
                Title = "Sản phẩm",
                Values = new ChartValues<double> { 15, 25, 35, 45, -55 },// Thay đổi giá trị tùy ý
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#965620")),
            };
            SeriesCollection.Add(columnSeries);
        }
    }
}
