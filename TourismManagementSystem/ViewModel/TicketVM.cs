using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourismManagementSystem.ViewModel
{
    internal class TicketVM : BaseViewModel
    {
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
        

    }
}