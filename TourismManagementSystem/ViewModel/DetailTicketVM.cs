using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.Model;

namespace TourismManagementSystem.ViewModel
{
    internal class DetailTicketVM : BaseViewModel
    {
        public static VE SelectedTicket { get; set; }

        private ObservableCollection<VE> _Ticket;
        private ObservableCollection<HANHKHACH> _Passenger;
        private ObservableCollection<CHUYEN> _Trip;

        public DetailTicketVM()
        {
            Ticket = new ObservableCollection<VE>();
            Passenger = new ObservableCollection<HANHKHACH>();
            Trip = new ObservableCollection<CHUYEN>();
            Ticket.Add(SelectedTicket);
            if(_Ticket.First().HANHKHACH != null)
                Passenger.Add(_Ticket.First().HANHKHACH);
            if (_Ticket.First().PHIEUDATCHO != null)
                Trip.Add(_Ticket.First().PHIEUDATCHO.CHUYEN);
        }
        public ObservableCollection<VE> Ticket { 
            get => _Ticket; 
            set { 
                _Ticket = value;
                OnPropertyChanged(); 
            } 
        }
        public ObservableCollection<HANHKHACH> Passenger { get => _Passenger; set { _Passenger = value; OnPropertyChanged(); } }
        public ObservableCollection<CHUYEN> Trip { get => _Trip; set { _Trip = value; OnPropertyChanged(); } }
    }
}
