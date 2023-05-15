using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace TourismManagementSystem
{
    /// <summary>
    /// Interaction logic for RevenueStatisticsWindow.xaml
    /// </summary>
    public partial class RevenueStatisticsWindow : Window
    {
        public RevenueStatisticsWindow()
        {
            InitializeComponent();
            //SeriesCollection lưu trữ dl biểu đồ
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries{
                    Title ="A",
                    Values = new ChartValues<double> {20, 10, 16, 20, 100, 112, 10, 40, 60 , 50,  49, 65},
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#965620"))

        }
            };
            //SeriesCollection.Add(new ColumnSeries
            //{

            //    Title = "B",
            //    Values = new ChartValues<double> { 12, 10, 0, 100, 100, 112, 10, 40, 60, 50, 49 }
            //});

            //SeriesCollection[1].Values.Add(48d);
            //Nhãn của các cột
            Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            // chuyển từ double sang String
            Values = values => values.ToString("N");
            DataContext = this;

        }
        public SeriesCollection SeriesCollection { get; private set; }  
        public String[] Labels { get; private set; }
        //lấy giá trị trong Value để chuyển đổi thành dạng cột Y
        public Func<double, String> Values { get; private set; }
        
    }
}
