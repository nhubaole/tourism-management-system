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
        private string _SelectedFilter;
        private string _SearchText;
        private ObservableCollection<string> _FilterCbItems = new ObservableCollection<string>() {  "Mã vé", "Mã hành khách", "Mã phiếu", "Ngày bán", "Trạng thái" };
        private ObservableCollection<VE> _ListVE = new ObservableCollection<VE>(DataProvider.Ins.DB.VEs);
        private ObservableCollection<string> _ListStatus = new ObservableCollection<string>() { "Chưa sử dụng", "Đã sử dụng", "Đã hủy" };

        public TicketVM()
        {
            SelectedFilter = FilterCbItems.First();
            ShowTicketDetailCommand = new RelayCommand<object>((p) => true,
            (p) => {
                VE selectedVe = p as VE;
                DetailTicketVM.SelectedTicket = selectedVe;
                DetailTicketWindow detailTicketWindow = new DetailTicketWindow();
                detailTicketWindow.Show();
            });

            UpdateStatusCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                VE selectedVe = p as VE;
                var itemToUpdate = DataProvider.Ins.DB.VEs.FirstOrDefault(item => item.MAVE == selectedVe.MAVE);
                if (itemToUpdate != null)
                {
                    itemToUpdate = selectedVe;
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật trạng thái vé thành công!");
                }
            });
        }


        public ICommand ShowTicketDetailCommand { get; set; }
        public ICommand UpdateStatusCommand { get; set; }

        public ObservableCollection<string> ListStatus { get => _ListStatus; set => _ListStatus = value; }
        public ObservableCollection<VE> ListVE { get => _ListVE; set { _ListVE = value; OnPropertyChanged(); } }
        public ObservableCollection<string> FilterCbItems { get => _FilterCbItems; set { _FilterCbItems = value; OnPropertyChanged(); } }
        public string SelectedFilter { get => _SelectedFilter; set { _SelectedFilter = value; OnPropertyChanged(); } }
        public string SearchText
        {
            get => _SearchText;
            set
            {
                _SearchText = value;
                if (SelectedFilter == FilterCbItems[0])
                {
                    ListVE = new ObservableCollection<VE>(DataProvider.Ins.DB.VEs.Where(t => t.MAVE.Contains(SearchText)));
                }
                else if (SelectedFilter == FilterCbItems[1])
                {
                    ListVE = new ObservableCollection<VE>(DataProvider.Ins.DB.VEs.Where(t => t.MAHK.Contains(SearchText)));
                }
                else if (SelectedFilter == FilterCbItems[2])
                {
                    ListVE = new ObservableCollection<VE>(DataProvider.Ins.DB.VEs.Where(t => t.MAPHIEU.Contains(SearchText)));
                }
                else if (SelectedFilter == FilterCbItems[3])
                {
                    ListVE = new ObservableCollection<VE>(DataProvider.Ins.DB.VEs.Where(t => t.NGAYBAN.ToString().Contains(SearchText)));
                }
                else if (SelectedFilter == FilterCbItems[4])
                {
                    ListVE = new ObservableCollection<VE>(DataProvider.Ins.DB.VEs.Where(t => t.TRANGTHAI.Contains(SearchText)));
                }
                OnPropertyChanged();
            }
        }
    }
}