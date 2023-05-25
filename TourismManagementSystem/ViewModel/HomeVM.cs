using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    internal class HomeVM : BaseViewModel
    {
        private int _TripNumber = 0;
        private int _TicketNumber = 0;
        private int _Revenue = 0;
        private string _SelectedRadioBtn;
        private ObservableCollection<CHUYEN> _UpComingTrip;

        public HomeVM()
        {
            init();
            RadioButtonCommand = new RelayCommand<string>((p) => true, (p) => { 
                if(p == "Ngày")
                {
                    DateTime now = DateTime.Now.Date;
                    TripNumber = DataProvider.Ins.DB.CHUYENs.Where(item => item.TGBATDAU == now).Count();
                    TicketNumber = DataProvider.Ins.DB.VEs.Where(item => item.NGAYBAN == now).Count();
                    Revenue = 0;
                    foreach (var item in DataProvider.Ins.DB.THONGTINTHANHTOANs.Where(item => item.THOIGIAN == now))
                    {
                        Revenue += (int)item.SOTIEN;
                    }
                }
                else if (p == "Tháng")
                {
                    DateTime now = DateTime.Now.Date;
                    TripNumber = DataProvider.Ins.DB.CHUYENs.Where(item => item.TGBATDAU.Value.Month == now.Month).Count();
                    TicketNumber = DataProvider.Ins.DB.VEs.Where(item => item.NGAYBAN.Value.Month == now.Month).Count();
                    Revenue = 0;
                    foreach (var item in DataProvider.Ins.DB.THONGTINTHANHTOANs.Where(item => item.THOIGIAN.Value.Month == now.Month))
                    {
                        Revenue += (int)item.SOTIEN;
                    }
                }
                else if (p == "Năm")
                {
                    DateTime now = DateTime.Now.Date;
                    TripNumber = DataProvider.Ins.DB.CHUYENs.Where(item => item.TGBATDAU.Value.Year == now.Year).Count();
                    TicketNumber = DataProvider.Ins.DB.VEs.Where(item => item.NGAYBAN.Value.Year == now.Year).Count();
                    Revenue = 0;
                    foreach (var item in DataProvider.Ins.DB.THONGTINTHANHTOANs.Where(item => item.THOIGIAN.Value.Year == now.Year))
                    {
                        Revenue += (int)item.SOTIEN;
                    }
                }
            });
        }

        public void init() {
            SelectedRadioBtn = "Ngày";
            DateTime dateNow = DateTime.Now.Date;
            TripNumber = DataProvider.Ins.DB.CHUYENs.Where(item => item.TGBATDAU == dateNow).Count();
            TicketNumber = DataProvider.Ins.DB.VEs.Where(item => item.NGAYBAN == dateNow).Count();
            Revenue = 0;
            foreach (var item in DataProvider.Ins.DB.THONGTINTHANHTOANs.Where(item => item.THOIGIAN == dateNow))
            {
                Revenue += (int)item.SOTIEN;
            }

            UpComingTrip = new ObservableCollection<CHUYEN>(DataProvider.Ins.DB.CHUYENs
                .Where(chuyen => DateTime.Compare((DateTime)chuyen.TGBATDAU, dateNow) >= 0)
                .OrderBy(chuyen => chuyen.TGBATDAU)
                .Take(3));
        }
        public ICommand RadioButtonCommand { get; set; }

        public int TripNumber { get => _TripNumber; set { _TripNumber = value; OnPropertyChanged(); } }
        public int TicketNumber { get => _TicketNumber; set { _TicketNumber = value; OnPropertyChanged(); } }
        public int Revenue { get => _Revenue; set { _Revenue = value; OnPropertyChanged(); } }

        public string SelectedRadioBtn { get => _SelectedRadioBtn; set { _SelectedRadioBtn = value; OnPropertyChanged(); } }

        public ObservableCollection<CHUYEN> UpComingTrip { get => _UpComingTrip; set => _UpComingTrip = value; }
    }
}
