using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.ViewModel;
using System.Collections.ObjectModel;
using TourismManagementSystem.Model;

namespace TestDoAn.TestVM
{
    [TestClass]
    public class DetailTicketVMTest
    {
        [TestMethod]
        public void Ticket_Property()
        {
            var detailTicketVm = new DetailTicketVM();
            ObservableCollection<VE> ticket = new ObservableCollection<VE>();

            detailTicketVm.Ticket = ticket;

            Assert.AreEqual(ticket, detailTicketVm.Ticket);
        }

        [TestMethod]
        public void Passenger_Property()
        {
            var detailTicketVm = new DetailTicketVM();
            ObservableCollection<HANHKHACH> passenger = new ObservableCollection<HANHKHACH>();

            detailTicketVm.Passenger = passenger ;

            Assert.AreEqual(passenger, detailTicketVm.Passenger);
        }

        [TestMethod]
        public void Trip_Property()
        {
            var detailTicketVm = new DetailTicketVM();
            ObservableCollection<CHUYEN> trip = new ObservableCollection<CHUYEN>();

            detailTicketVm.Trip = trip;

            Assert.AreEqual(trip, detailTicketVm.Trip);
        }
    }
}
