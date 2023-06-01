using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TourismManagementSystem.Model;
using TourismManagementSystem.View;

namespace TourismManagementSystem.ViewModel
{
    internal class TicketVM : BaseViewModel
    {
        public ICommand ShowTicketDetailCommand { get; set; }

        public ICommand UpdateDataCommand { get; set; }
      
        private ObservableCollection<string> _filter = new ObservableCollection<string>() { "Mã phiếu", "Mã vé", "Mã khách hàng" };
        public ObservableCollection<string> filter { get => _filter; set { _filter = value; OnPropertyChanged(nameof(filter)); } }
       

        private string _ticket;
        public string VE
        {
            get => _ticket;
            set
            {
                _ticket= value;
                OnPropertyChanged(nameof(VE));
            }
        }
        private string MaVE;
        public string MAVE
        {
            get =>  MaVE;
            set
            {
                MaVE = value;
                OnPropertyChanged(nameof(MAVE));
            }
        }
        private string MaPHIEU;
        public string MAPHIEU
        {
            get => MaPHIEU;
            set
            {
                MaPHIEU = value;
                OnPropertyChanged(nameof(MAPHIEU));
            }
        }
        private string TrangThai;
        public string TRANGTHAI
        {
            get => TrangThai;
            set
            {
                TrangThai = value;
                OnPropertyChanged(nameof(TRANGTHAI));
            }
        }
        private DateTime NgayBan;
        public DateTime NGAYBAN
        {
            get => NgayBan;
            set
            {
                NgayBan = value;
                OnPropertyChanged(nameof(NGAYBAN));
            }
        }
        private int GiaVE;
        public int GIAVE
        {
            get => GiaVE;
            set
            {
                GiaVE= value;
                OnPropertyChanged(nameof(GIAVE));
            }
        }



        private ObservableCollection<VE> _ListVE = new ObservableCollection<VE>(DataProvider.Ins.DB.VEs);
        public ObservableCollection<VE> ListVE { get => _ListVE; set { _ListVE = value; OnPropertyChanged(nameof(DataProvider.Ins.DB.VEs)); } }

        private VE _selectedTicket;
        public VE SelectedTicket
        {
            get => _selectedTicket; set
            {
                _selectedTicket = value;
                OnPropertyChanged(nameof(SelectedTicket));
            }
        }

        private ObservableCollection<string> _status = new ObservableCollection<string>() { "Mã phiếu", "Mã vé", "Mã khách hàng" };
        public ObservableCollection<string> status { get => _status; set { _status = value; OnPropertyChanged(nameof(status)); } }



        public TicketVM()
        {
            status= new ObservableCollection<string>();
            ShowTicketDetailCommand = new RelayCommand<object>((p) => {

                return true;

            }, (p) =>
            { SwitchWindow(p); });

            UpdateDataCommand = new RelayCommand<object>((p) => {
                if (MaVE == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) =>
            {
                try
                {
                    var temp = new VE()
                    {
                        TRANGTHAI = TRANGTHAI,
                        NGAYBAN = NGAYBAN
                    };

                    DataProvider.Ins.DB.SaveChanges();
                    LoadDataGrid();
                    MessageBox.Show("Đã thay đổi thông tin vé thành công");


                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }


            });



       

        }
        private void SwitchWindow(object parameter)
        {
            DetailTicketWindow detailTicketWindow= new DetailTicketWindow();
            detailTicketWindow.Show();

        }
        private void LoadDataGrid()
        {
            ListVE = new ObservableCollection<VE>(DataProvider.Ins.DB.VEs);
            OnPropertyChanged(nameof(ListVE));
        }
    }
}