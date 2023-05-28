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
        public ICommand SwitchWindowCommand { get; set; }

        public ICommand OnlyNumericCommand { get; private set; }

        public ICommand AddDataCommand { get; set; }
        public ICommand DeleteDataCommand { get; set; }
        public ICommand UpdateDataCommand { get; set; }
        private ObservableCollection<string> _status = new ObservableCollection<string>() { "Chưa sử dụng", "Đã sử dụng", "Đã hủy" };
        public ObservableCollection<string> status { get => _status; set { _status = value; OnPropertyChanged(nameof(status)); } }

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


        public TicketVM()
        {
            SwitchWindowCommand = new RelayCommand<object>((p) => {

                return true;

            }, (p) =>
            { SwitchWindow(p); });

            AddDataCommand = new RelayCommand<object>((p) => {
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
                        
                    };

                    DataProvider.Ins.DB.VEs.Add(temp);
                    DataProvider.Ins.DB.SaveChanges();
                    ListVE.Add(temp);
                    //LoadDataGrid();



                    MessageBox.Show("Đã tạo mới khách hàng thành công");


                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.InnerException.Message);
                }


            });



           // OnlyNumericCommand = new RelayCommand<object>((p) =>
           // {
           //     return true;
           // },
           //(p) => { OnlyNumericExecute(); });

        }
        private void SwitchWindow(object parameter)
        {
            DetailTicketWindow detailTicketWindow= new DetailTicketWindow();
            detailTicketWindow.Show();

        }
    }
}