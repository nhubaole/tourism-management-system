using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismManagementSystem.ViewModel;
using System.Collections.ObjectModel;
using Moq;
using TourismManagementSystem.Model;
using TourismManagementSystem.View;

namespace TestDoAn.TestVM
{
    [TestClass]
    public class HomeVMTest
    {
        [TestMethod]
        public void TripNumber_Property()
        {
            var homeVm = new HomeVM(2,5,690000);
            var tripNumber = 10;

            homeVm.TripNumber = tripNumber;

            Assert.AreEqual(tripNumber,homeVm.TripNumber);
        }

        [TestMethod]
        public void TicketNumber_Property()
        {
            var homeVm = new HomeVM(2, 5, 690000);
            var ticketNumber = 10;

            homeVm.TicketNumber = ticketNumber;

            Assert.AreEqual(ticketNumber, homeVm.TicketNumber);
        }

        [TestMethod]
        public void Revenue_Property()
        {
            var homeVm = new HomeVM(2, 5, 690000);
            var revenue= 10;

            homeVm.Revenue = revenue;

            Assert.AreEqual(revenue, homeVm.Revenue);
        }

        [TestMethod]
        public void SelectedRadioBtn_Property()
        {
            var homeVm = new HomeVM(2, 5, 690000);
            var selectedRadioBtn = "SelectedRadioBtn";

            homeVm.SelectedRadioBtn = selectedRadioBtn;

            Assert.AreEqual(selectedRadioBtn, homeVm.SelectedRadioBtn);
        }

        [TestMethod]
        public void UpComingTrip_Property()
        {
            var homeVm = new HomeVM(2, 5, 690000);
            ObservableCollection<CHUYEN> upComingTrip = new ObservableCollection<CHUYEN>();

            homeVm.UpComingTrip = upComingTrip;

            Assert.AreEqual(upComingTrip, homeVm.UpComingTrip);
        }

        [TestMethod]
        public void IsAdmin_Property()
        {
            var homeVm = new HomeVM(2, 5, 690000);
            var isAdmin = true;

            homeVm.IsAdmin = isAdmin;

            Assert.AreEqual(isAdmin,homeVm.IsAdmin);
        }

    }
}
