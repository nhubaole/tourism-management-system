﻿using iTextSharp.text;
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
        ////chọn tháng hay năm
        //private string _Filter1;
        //public string Filter1
        //{
        //    get { return _Filter1; }
        //    set
        //    {
        //        _Filter1 = value;
        //        seriesCollection.Clear();
        //        TimeOfChart = null;
        //        OnPropertyChanged(nameof(seriesCollection));
        //        OnPropertyChanged(nameof(TimeOfChart));

        //        UpdateFilterYear();
        //        Year = FilterYear[0];
        //        OnPropertyChanged(nameof(Year));

        //        OnPropertyChanged();
        //    }
        //}

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


        //private int _Month;
        //public int Month
        //{
        //    get { return _Month; }
        //    set
        //    {
        //        _Month = value;
        //        OnPropertyChanged(nameof(labelCollection));

        //        if (_Month != 0)
        //        {
        //            TimeOfChart = " tháng " + _Month.ToString() + " năm " + Year.ToString();
        //        }
        //        OnPropertyChanged(nameof(TimeOfChart));
        //        OnPropertyChanged();
        //        DrawChart();

        //    }
        //}

        //private int _Year;
        //public int Year
        //{
        //    get { return _Year; }
        //    set
        //    {
        //        _Year = value;
        //        if (Year != 0)
        //        {
        //            if (Filter1 == "Tháng")
        //            {
        //                CanChoseMonth = true;
        //                FilterMonth = new ObservableCollection<int>(GetMonthsForYear(Year));
        //                OnPropertyChanged(nameof(FilterMonth));
        //                Month = FilterMonth[0];
        //                OnPropertyChanged(nameof(Month));

        //                ;
        //            }
        //            else
        //            {
        //                TimeOfChart = " năm " + Year.ToString();
        //                OnPropertyChanged(nameof(TimeOfChart));
        //                DrawChart();
        //            }
        //        }
        //        OnPropertyChanged();
        //    }
        //}

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


        //private SeriesCollection _seriesCollection;
        //public SeriesCollection seriesCollection
        //{
        //    get { return _seriesCollection; }
        //    set
        //    {
        //        _seriesCollection = value;
        //        OnPropertyChanged();
        //    }
        //}

        //private List<string> _labelCollection;
        //public List<string> labelCollection
        //{
        //    get { return _labelCollection; }
        //    set
        //    {
        //        _labelCollection = value;
        //        OnPropertyChanged();
        //    }
        //}

        public SalesStatisticsVM()
        {
            //FilterItems1 = new ObservableCollection<String>(new List<string> { "Tháng", "Năm" });
            //seriesCollection = new SeriesCollection();
            //labelCollection = new List<string>();

            //Filter1 = "Năm";
        }
        //private void UpdateFilterYear()
        //{
        //    FilterYear = new ObservableCollection<int>(GetYears());

        //    if (Filter1 == "Tháng")
        //    {
        //        CanChoseYear = true;
        //    }
        //    else if (Filter1 == "Năm")
        //    {

        //        CanChoseYear = true;
        //        CanChoseMonth = false;

        //    }
        //    else
        //    {
        //        CanChoseYear = false;
        //        CanChoseMonth = false;
        //    }
        //    OnPropertyChanged(nameof(CanChoseYear));
        //    OnPropertyChanged(nameof(CanChoseMonth));

        //}
        //public List<int> GetYears()
        //{
        //    List<int> years = new List<int>();

        //    //years.Add(2023);

        //    years = DataProvider.Ins.DB.CHUYENs
        //       .Where(t => t.TGBATDAU.HasValue)
        //       .Select(t => t.TGBATDAU.Value.Year)
        //       .Distinct().ToList();
        //    return years;
        //}

        //public List<int> GetMonthsForYear(int year) // lấy tháng trong database từ thông tin chuyến
        //{
        //    List<int> months = new List<int>();

        //   // months.Add(12);

        //    // Lấy tháng hiện tại
        //    months = DataProvider.Ins.DB.CHUYENs
        //       .Where(t => t.TGBATDAU.HasValue && t.TGBATDAU.Value.Year == year)
        //       .Select(t => t.TGBATDAU.Value.Month)
        //       .Distinct().ToList();
        //    return months;
        //}
        //public static List<int> GetDaysInMonth(int year, int month)
        //{
        //    List<int> daysInMonth = new List<int>();
        //    if (year != 0 && month != 0)
        //    {
        //        // Xác định số ngày trong tháng
        //        int numberOfDaysInMonth = DateTime.DaysInMonth(year, month);

        //        for (int day = 1; day <= numberOfDaysInMonth; day++)
        //        {
        //            daysInMonth.Add(day);
        //        }

        //    }
        //    return daysInMonth;

        //}
        //public List<int> GetMonthsInYear()
        //{
        //    List<int> months = Enumerable.Range(1, 12).ToList();
        //    return months;
        //}
        //private void DrawChart()
        //{
        //    seriesCollection.Clear();


        //    // Kiểm tra điều kiện để vẽ biểu đồ
        //    if (Filter1 == "Tháng" && Year != 0 && Month != 0)
        //    {
        //        List<int> daysInMonth = GetDaysInMonth(Year, Month);
        //        ChartValues<int> statistic = new ChartValues<int>();
        //        foreach (int day in daysInMonth)
        //        {
        //            int revenueValue = GetRevenueForDay(day, Month, Year); // Hàm để lấy doanh thu cho ngày cụ thể
        //            statistic.Add(revenueValue);
        //            labelCollection.Add("Ngày " + day.ToString());
        //        }
        //        ColumnSeries dayRevenueSeries = new ColumnSeries
        //        {
        //            //Title = $"{day}",
        //            Title = "Doanh thu",
        //            Values = statistic,
        //            DataLabels = true
        //        };
        //        seriesCollection.Add(dayRevenueSeries);
        //    }
        //    else if (Filter1 == "Năm" && Year != 0)
        //    {
        //        List<int> monthsInYear = GetMonthsInYear();
        //        ChartValues<int> statistic = new ChartValues<int> { };
        //        foreach (int month in monthsInYear)
        //        {
        //            int revenueValue = GetRevenueForMonth(month, Year); // Hàm để lấy doanh thu cho thngs cụ thể
        //            statistic.Add(revenueValue);
        //            labelCollection.Add("Tháng " + month.ToString());

        //        }
        //        ColumnSeries MonthRevenueSeries = new ColumnSeries
        //        {
        //            Title = "Doanh thu",
        //            Values = statistic,
        //            DataLabels = true
        //        };

        //        seriesCollection.Add(MonthRevenueSeries);
        //    }
        //}

        //private int GetRevenueForDay(int day, int month, int year)
        //{
        //    int Revenue = 0;

        //    foreach (var item in DataProvider.Ins.DB.THONGTINTHANHTOANs.Where(item => item.THOIGIAN.Value.Day == day
        //                                                                    && item.THOIGIAN.Value.Month == month
        //                                                                    && item.THOIGIAN.Value.Year == Year))
        //    {
        //        Revenue += (int)item.SOTIEN;
        //    }

        //    return Revenue;
        //}
        //private int GetRevenueForMonth(int month, int year)
        //{
        //    int Revenue = 0;
        //    foreach (var item in DataProvider.Ins.DB.THONGTINTHANHTOANs.Where(item => item.THOIGIAN.Value.Month == month
        //                                                                    && item.THOIGIAN.Value.Year == Year))
        //    {
        //        Revenue += (int)item.SOTIEN;
        //    }

        //    return Revenue;
        //}

    }

}
